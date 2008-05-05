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
        // segédfüggvények
        public static string GetFullPath(IntelNodeFile node) { return ProjectPath + @"\" + node.Text; }
        public static IntelNodeProject ProjectNode { get { return m_ProjectNode; } }
        //////////////////////////////////////////////////////////////////////////
        // statikus projektmanager beállítása **
        public static void Initialize(TreeView projecttree, TabControl filestab, PictureBox patternbox, CompilerOutBox output)
        {
            m_ProjectTree = projecttree;
            EditorField.SizePattern = patternbox.Size;
            m_FilesTabCtrl = filestab;
            m_OutPut = output;
        }
        //////////////////////////////////////////////////////////////////////////
        // Projectfa - kezelés
        //////////////////////////////////////////////////////////////////////////
        // projektfa felépítése és törlése **
        private static void CreateEmptyTree()
        {
            if (ProjectIsOpened)
                CloseProject();
            m_ProjectTree.BeginUpdate();
            m_ProjectTree.Nodes.Add(m_ProjectNode);
            m_ProjectTree.EndUpdate();
        }
        //////////////////////////////////////////////////////////////////////////
        // projektkönyvtár lekérdezése és beállítása **
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
        // projektfile lekérdezése **
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
        // projektfa teljes eltávolítása
        private static void CloseProjectTree()
        {
            m_ProjectTree.BeginUpdate();
            m_ProjectNode.Nodes.Clear();
            m_ProjectNode.Remove(); // Projekt eltávolítása
            m_ProjectTree.EndUpdate();
            m_FilesTabCtrl.Controls.Clear();
        }
        //////////////////////////////////////////////////////////////////////////
        // kiválaszott node
        public static IntelNode SelectedNode()
        {
            return m_ProjectTree.SelectedNode as IntelNode;
        }
        //////////////////////////////////////////////////////////////////////////
        // Project - kezelés
        //////////////////////////////////////////////////////////////////////////
        // projekt megnyitása **
        public static void OpenProject(string filename) 
        {            
            CreateEmptyTree();
            ProjectPath = Path.GetDirectoryName(filename);
            LoadProjectFile();
        }
        //////////////////////////////////////////////////////////////////////////
        // projekt bezárása
        public static void CloseProject()
        {
            CloseProjectTree();
        }
        //////////////////////////////////////////////////////////////////////////
        // új projekt létrehozása **
        public static void CreateNewProject(string projectpath) 
        {
            ProjectPath = projectpath;
            System.IO.Directory.CreateDirectory(ProjectPath);
            CreateEmptyTree();
            SaveProject(true);
        }
        //////////////////////////////////////////////////////////////////////////
        // projekt átnevezése, majd mentése az új néven
     /*   public static void SaveProjectAs(string FileName) 
        {
            //System.IO.Directory.
            m_ProjectNode.SetPath(FileName);
            SaveProject(true);
        }*/
        //////////////////////////////////////////////////////////////////////////
        // projekt elmentése
        public static void SaveProject(bool forcesaveall)
        {
            foreach (EditorPage page in m_FilesTabCtrl.TabPages)
                page.SaveContent(forcesaveall);     // elmentjük minden file tartalmát
            SaveProjectFile();
        }
        //////////////////////////////////////////////////////////////////////////
        // projekt file fába olvasása
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
        // projekt fa fileba írása
        private static void SaveProjectFile()
        {
            using (StreamWriter writer = new StreamWriter(ProjectFile, false /*Felülírás*/))
            {
                foreach (IntelNodeFile file in ProjectNodes)
                    writer.WriteLine(file.Text);
            }
        }
        //////////////////////////////////////////////////////////////////////////
        // tab aktiválása
        public static void ActivateTab(IntelNodeFile node)
        {
            // megnézzük, nincs-e már nyitva
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
        // tab hozzáadása
        private static void AddNewTab(IntelNodeFile node)
        {
            EditorPage NewTab = new EditorPage(node);
            m_FilesTabCtrl.TabPages.Add(NewTab);
            m_FilesTabCtrl.SelectedTab = NewTab;
            NewTab.LoadContent();
        }
        //////////////////////////////////////////////////////////////////////////
        // file hozzáadása a projekthez
        public static void AddFileToProject(string filename)
        {
            if (ProjectIsOpened)
                m_ProjectNode.AddFile(filename);
        }
        //////////////////////////////////////////////////////////////////////////
        // nyitva van-e projekt
        public static bool ProjectIsOpened
        {get{return m_ProjectNode.TreeView == m_ProjectTree;} }  //fel van csatolva a projektfára
        //////////////////////////////////////////////////////////////////////////
        // kimenetre írás
        public static CompilerOutBox Output
        { get{return m_OutPut;} }
        //////////////////////////////////////////////////////////////////////////
        private static TreeView m_ProjectTree;
        private static TabControl m_FilesTabCtrl;
        private static CompilerOutBox m_OutPut;
        private static IntelNodeProject m_ProjectNode = new IntelNodeProject();
        //////////////////////////////////////////////////////////////////////////
        // projekt másolása megadott helyre
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
                // sourceok számolása
                int sourcescount  = 0;
                foreach (IntelNodeFile file in ProjectNodes)
                {
                    if (file is IntelNodeSource)
                        ++sourcescount;
                }
                IntelNodeSource[] retval = new IntelNodeSource[sourcescount];
                // bemásolása
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
