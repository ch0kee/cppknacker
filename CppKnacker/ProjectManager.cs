using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Drawing;

namespace CppKnacker
{
    static class ProjectManager
    {
        //////////////////////////////////////////////////////////////////////////
        // seg�df�ggv�nyek
        public static string GetFullPath(IntelNodeFile node) { return ProjectPath + @"\" + node.Text; }
        public static IntelNodeProject ProjectNode { get { return m_ProjectNode; } }
        //////////////////////////////////////////////////////////////////////////
        // statikus projektmanager be�ll�t�sa **
        public static void Initialize(TreeView projecttree, TabControl filestab, PictureBox patternbox, CompilerOutBox output)
        {
            m_ProjectTree = projecttree;
            EditorField.SizePattern = patternbox.Size;
            m_FilesTabCtrl = filestab;
            m_OutPut = output;
        }
        //////////////////////////////////////////////////////////////////////////
        // Projectfa - kezel�s
        //////////////////////////////////////////////////////////////////////////
        // projektfa fel�p�t�se �s t�rl�se **
        private static void CreateEmptyTree()
        {
            if (ProjectIsOpened)
                CloseProject();
            m_ProjectTree.BeginUpdate();
            m_ProjectTree.Nodes.Add(m_ProjectNode);
            m_ProjectTree.EndUpdate();
        }
        //////////////////////////////////////////////////////////////////////////
        // projektk�nyvt�r lek�rdez�se �s be�ll�t�sa **
        public static string ProjectPath
        {
            set
            {
                m_ProjectNode.SetPath(value);
            }
            get
            {
                return m_ProjectNode.Tag as string;
            }
        }
        //////////////////////////////////////////////////////////////////////////
        // projektfile lek�rdez�se **
        public static string ProjectFile
        {
            get
            {
                return (m_ProjectNode.Tag as string) + @"\" +(m_ProjectNode.Text) + ".ckp";
            }
        }
        public static TreeNodeCollection ProjectNodes
        {
            get
            {
                return m_ProjectNode.Nodes;
            }
        }
        //////////////////////////////////////////////////////////////////////////
        // projektfa teljes elt�vol�t�sa
        private static void CloseProjectTree()
        {
            m_ProjectTree.BeginUpdate();
            m_ProjectNode.Nodes.Clear();
            m_ProjectNode.Remove(); // Projekt elt�vol�t�sa
            m_ProjectTree.EndUpdate();
            m_FilesTabCtrl.Controls.Clear();
        }
        //////////////////////////////////////////////////////////////////////////
        // kiv�laszott node
        public static IntelNode SelectedNode()
        {
            return m_ProjectTree.SelectedNode as IntelNode;
        }
        //////////////////////////////////////////////////////////////////////////
        // Project - kezel�s
        //////////////////////////////////////////////////////////////////////////
        // projekt megnyit�sa **
        public static void OpenProject(string filename) 
        {            
            CreateEmptyTree();
            ProjectPath = Path.GetDirectoryName(filename);
            LoadProjectFile();
        }
        //////////////////////////////////////////////////////////////////////////
        // projekt bez�r�sa
        public static void CloseProject()
        {
            CloseProjectTree();
        }
        //////////////////////////////////////////////////////////////////////////
        // �j projekt l�trehoz�sa **
        public static void CreateNewProject(string projectpath) 
        {
            ProjectPath = projectpath;
            System.IO.Directory.CreateDirectory(ProjectPath);
            CreateEmptyTree();
            SaveProject(true);
        }
        //////////////////////////////////////////////////////////////////////////
        // projekt �tnevez�se, majd ment�se az �j n�ven
     /*   public static void SaveProjectAs(string FileName) 
        {
            //System.IO.Directory.
            m_ProjectNode.SetPath(FileName);
            SaveProject(true);
        }*/
        //////////////////////////////////////////////////////////////////////////
        // projekt elment�se
        public static void SaveProject(bool forcesaveall)
        {
            foreach (EditorPage page in m_FilesTabCtrl.TabPages)
                page.SaveContent(forcesaveall);     // elmentj�k minden file tartalm�t
            SaveProjectFile();
        }
        //////////////////////////////////////////////////////////////////////////
        // projekt file f�ba olvas�sa
        private static void LoadProjectFile()
        {
            using (StreamReader read = new StreamReader(ProjectFile))
            {
                string line;
                while ((line = read.ReadLine()) != null)
                    m_ProjectNode.AddFile(line);
            }
        }
        //////////////////////////////////////////////////////////////////////////
        // projekt fa fileba �r�sa
        private static void SaveProjectFile()
        {
            using (StreamWriter writer = new StreamWriter(ProjectFile, false /*Fel�l�r�s*/))
            {
                foreach (IntelNodeFile file in ProjectNodes)
                    writer.WriteLine(file.Text);
            }
        }
        //////////////////////////////////////////////////////////////////////////
        // tab aktiv�l�sa
        public static void ActivateTab(IntelNodeFile node)
        {
            // megn�zz�k, nincs-e m�r nyitva
            foreach (EditorPage page in m_FilesTabCtrl.TabPages)
            {
                if (page.IsMyNode(node)) 
                {
                    m_FilesTabCtrl.SelectedTab = page;
                    return;
                }
            }
            AddNewTab(node);
        }
        //////////////////////////////////////////////////////////////////////////
        // tab hozz�ad�sa
        private static void AddNewTab(IntelNodeFile node)
        {
            EditorPage NewTab = new EditorPage(node);
            m_FilesTabCtrl.TabPages.Add(NewTab);
            m_FilesTabCtrl.SelectedTab = NewTab;
            NewTab.LoadContent();
        }
        //////////////////////////////////////////////////////////////////////////
        // file hozz�ad�sa a projekthez
        public static void AddFileToProject(string filename)
        {
            if (ProjectIsOpened)
                m_ProjectNode.AddFile(filename);
        }
        //////////////////////////////////////////////////////////////////////////
        // nyitva van-e projekt
        public static bool ProjectIsOpened
        {get{return m_ProjectNode.TreeView == m_ProjectTree;} }  //fel van csatolva a projektf�ra
        //////////////////////////////////////////////////////////////////////////
        // kimenetre �r�s
        public static CompilerOutBox Output
        { get{return m_OutPut;} }
        //////////////////////////////////////////////////////////////////////////
        private static TreeView m_ProjectTree;
        private static TabControl m_FilesTabCtrl;
        private static CompilerOutBox m_OutPut;
        private static IntelNodeProject m_ProjectNode = new IntelNodeProject();
        //////////////////////////////////////////////////////////////////////////
        // projekt m�sol�sa megadott helyre
        public static void CopyProject(string newprojectfiledest)
        {
            System.IO.File.Copy(ProjectManager.ProjectFile, newprojectfiledest);
            foreach( IntelNodeFile file in ProjectNodes )
            {
                System.IO.File.Copy(GetFullPath(file), Path.GetDirectoryName(newprojectfiledest) + @"\" + file.Text);
            }
        }
        static public IntelNodeSource[] SourceNodes
        {
            get 
            {
                // sourceok sz�mol�sa
                int sourcescount  = 0;
                foreach (IntelNodeFile file in ProjectNodes)
                {
                    if (file is IntelNodeSource)
                        ++sourcescount;
                }
                IntelNodeSource[] retval = new IntelNodeSource[sourcescount];
                // bem�sol�sa
                int i = 0;
                foreach (IntelNodeFile file in ProjectNodes)
                {
                    if (file is IntelNodeSource)
                        retval[i++] = file as IntelNodeSource;
                }
                return retval;
            }
        }
    }
}
