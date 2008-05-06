using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace CppKnacker
{
    public partial class MainForm : Form
    {
        const string m_VERSION = "0.34";

        const string m_MAINFORM_CAPTION = "C++ Knacker" + " " + m_VERSION;

        const string m_PROJECT_FILE_FILTER = "C++ Knacker Project (*.ckp)|*.ckp";
        const string m_SOURCE_FILE_FILTER = "C++ Source File (*.cpp)|*.cpp";
        const string m_HEADER_FILE_FILTER = "C++ Header File (*.h)|*.h";

        public MainForm()
        {
            InitializeComponent();
            ProjectManager.Initialize(treeProjectTree, tabEditorTabs, patternBox, txtOutput);

            string guessedpath = @"C:\MinGW\bin\g++.exe";
            if (System.IO.File.Exists(guessedpath))
            {
                CompilerManager.SetupCompilerPath(guessedpath);
                menuCompile.Enabled = true;

                this.Text = m_MAINFORM_CAPTION;
            }
        }
        //////////////////////////////////////////////////////////////////////////
        // �j projekt l�trehoz�sa **
        private void menuNewProject_Click(object sender, EventArgs e)
        {
            ProjectForm newprojectform = new ProjectForm();
            if (newprojectform.ShowDialog() == DialogResult.OK)
            {
                ProjectManager.CreateNewProject(newprojectform.ProjectPath);
                this.Text = m_MAINFORM_CAPTION + " - " + System.IO.Path.GetFileNameWithoutExtension(ProjectManager.ProjectFile);
            }
        }
        //////////////////////////////////////////////////////////////////////////
        // �j file l�trehoz�sa **
        private void menuNewFile_Click(object sender, EventArgs e)
        {
            ShowFileDialog(new SaveFileDialog(), "�j file l�trehoz�sa", m_SOURCE_FILE_FILTER + "|" + m_HEADER_FILE_FILTER, ProjectManager.AddFileToProject);
        }
        //////////////////////////////////////////////////////////////////////////
        // projekt megnyit�sa **
        private void menuOpenProject_Click(object sender, EventArgs e)
        {
            if (ShowFileDialog(new OpenFileDialog(), "Projekt megnyit�sa", m_PROJECT_FILE_FILTER, ProjectManager.OpenProject))
                this.Text = m_MAINFORM_CAPTION + " - " + System.IO.Path.GetFileNameWithoutExtension(ProjectManager.ProjectFile);
        }
        //////////////////////////////////////////////////////////////////////////
        // projektf�n file kiv�laszt�sa **
        private void treeProjectTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // megnyit�s, de csak ha Source vagy Header
            if (e.Node is IntelNodeSource || e.Node is IntelNodeHeader)
                ProjectManager.ActivateTab(e.Node as IntelNodeFile);
        }
        //////////////////////////////////////////////////////////////////////////
        // projekt ment�se **
        private void menuSaveProject_Click(object sender, EventArgs e)
        {
            ProjectManager.SaveProject(false);
        }
        //////////////////////////////////////////////////////////////////////////
        // projekt ment�se m�sk�nt **
        private void menuSaveProjectAs_Click(object sender, EventArgs e)
        {
            ProjectForm saveasprojectform = new ProjectForm();
            saveasprojectform.WorkMode = ProjectForm.ProjectFormWorkMode.SaveProjectAs;
            if (saveasprojectform.ShowDialog() == DialogResult.OK)
            {
                ProjectManager.SaveProject(false);
                System.IO.Directory.CreateDirectory(saveasprojectform.ProjectPath);
                ProjectManager.CopyProject(saveasprojectform.ProjectFile);
                ProjectManager.ProjectPath = saveasprojectform.ProjectPath;

                this.Text = m_MAINFORM_CAPTION + " - " + System.IO.Path.GetFileNameWithoutExtension(ProjectManager.ProjectFile);
            }
        }
        //////////////////////////////////////////////////////////////////////////
        // projekt bez�r�sa **
        private void menuCloseProject_Click(object sender, EventArgs e)
        {
            if (!ProjectManager.ProjectIsOpened)
                return;
            if (DialogResult.Yes == MessageBox.Show("Elmenti a projektet ?", "Projekt ment�se", MessageBoxButtons.YesNo))
                menuSaveProject_Click(sender, e);
            ProjectManager.CloseProject();
            this.Text = m_MAINFORM_CAPTION;
        }
        //////////////////////////////////////////////////////////////////////////
        // kil�p�s **
        private void menuExit_Click(object sender, EventArgs e)
        {
            menuCloseProject_Click(sender, e);
            Close();
        }
        //////////////////////////////////////////////////////////////////////////
        // file projekthez ad�sa **
        private void menuAddFileToProject_Click(object sender, EventArgs e)
        {
            ShowFileDialog(new OpenFileDialog(), "File projekthez ad�sa", m_SOURCE_FILE_FILTER + "|" + m_HEADER_FILE_FILTER, ProjectManager.AddFileToProject);
        }
        //////////////////////////////////////////////////////////////////////////
        // file elv�tele a projektb�l
        private void fileRemoveFileFromProject_Click(object sender, EventArgs e)
        {
            // ezek a h�v�sok egyszer�s�dni fognak, ha lesz saj�t EditorNode oszt�lyunk TreeNode helyett
            if (ProjectManager.SelectedNode() is IntelNodeSource || ProjectManager.SelectedNode() is IntelNodeHeader)
                treeProjectTree.SelectedNode.Remove();  // majd kiveszi a tabot is a saj�t EditorNode oszt�ly
        }
        //////////////////////////////////////////////////////////////////////////
        // file ment�se **
        private void menuSaveFile_Click(object sender, EventArgs e)
        {
            if (tabEditorTabs.SelectedTab != null)
            {
                EditorPage  selectedpage = tabEditorTabs.SelectedTab as EditorPage;
                selectedpage.SaveContent(false);
            }
        }
        // file ment�se m�sk�nt
        private void menuSaveFileAs_Click(object sender, EventArgs e)
        {
      /* 
            if (tabEditorTabs.SelectedTab != null)
            {
                EditorPage SelectedPage = tabEditorTabs.SelectedTab as EditorPage;
                string FilterString = "";
                if (SelectedPage.IsSourceFile)
                    FilterString = SourceFileFilter;
                else if (SelectedPage.IsHeaderFile)
                    FilterString = HeaderFileFilter;
                ShowFileDialog(new SaveFileDialog(), "File ment�se m�sk�nt", FilterString, SelectedPage.SaveContentAs);
            }*/
        }
        //////////////////////////////////////////////////////////////////////////
        // ford�t �s futtat
        private void menuCompileAndRun_Click(object sender, EventArgs e)
        {
            menuCompile_Click(sender, e);   // az�rt ez �gy nem maradhat a v�g�n, csak akkor Runolunk, ha sikeres volt a ford�t�s
            menuRun_Click(sender, e);
        }
        //////////////////////////////////////////////////////////////////////////
        // exe ford�t�sa
        private void menuCompile_Click(object sender, EventArgs e)
        {
            ProjectManager.SaveProject(false);
            menuRun.Enabled = CompilerManager.Compile();
        }
        //////////////////////////////////////////////////////////////////////////
        // leford�tott exe futtat�sa
        private void menuRun_Click(object sender, EventArgs e)
        {
            CompilerManager.Run();
        }
        // kikapcsoljuk a nodeok szerkeszthet�s�g�t a szerkeszt�s ut�n
        //////////////////////////////////////////////////////////////////////////
        // egy �ltal�nos dialogkezel� f�ggv�ny
        private delegate void FileDialogFunction(string FileName);
        private bool ShowFileDialog(FileDialog dialog, string title, string filterstring, FileDialogFunction func)
        {
            dialog.Title = title;
            dialog.Filter = filterstring;
            dialog.InitialDirectory = ProjectManager.ProjectPath;
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == DialogResult.OK && dialog.FileName.Length > 0)
                func(dialog.FileName);
            else // ha nem v�lasztottunk norm�lis filet, hamis
                return false;
            return true;
        }
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        // a szoftver bemutat�s�hoz �tmeneti kieg�sz�t�sek : by ch0kee
        private void menuCompilerSettings_Click(object sender, EventArgs e)
        {
            string guessedpath = @"C:\MinGW\bin\g++.exe";
            if (System.IO.File.Exists(guessedpath))
            {
                CompilerManager.SetupCompilerPath(guessedpath);
                menuCompile.Enabled = true;
            }
            else if (ShowFileDialog(new OpenFileDialog(), "g++ tall�z�sa", "g++ alkalmaz�s (g++.exe)|g++.exe", CompilerManager.SetupCompilerPath))
                menuCompile.Enabled = true;
        }

        private void menuNevjegy_Click(object sender, EventArgs e)
        {
            HelpForm newhelpform = new HelpForm();
            newhelpform.ShowDialog();
        }

     
    }
}