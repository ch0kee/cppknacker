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

        public static string ProgramDirectory
        {
            get
            {
                string dir = AppDomain.CurrentDomain.BaseDirectory;
                if (!dir.EndsWith(@"\"))
                    dir += @"\";
                return dir;
            }
        }

        public MainForm()
        {
            InitializeComponent();
            ProjectManager.Initialize(treeProjectTree, tabEditorTabs, patternBox, txtOutput);

            string guessedpath = @"C:\MinGW\bin\g++.exe";
            if (System.IO.File.Exists(guessedpath))
            {
                this.Text = m_MAINFORM_CAPTION;
            }
        }
        //////////////////////////////////////////////////////////////////////////
        // új projekt létrehozása **
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
        // új file létrehozása **
        private void menuNewFile_Click(object sender, EventArgs e)
        {
            ShowFileDialog(new SaveFileDialog(), "Új file létrehozása", m_SOURCE_FILE_FILTER + "|" + m_HEADER_FILE_FILTER, ProjectManager.AddFileToProject);
        }
        //////////////////////////////////////////////////////////////////////////
        // projekt megnyitása **
        private void menuOpenProject_Click(object sender, EventArgs e)
        {
            if (ShowFileDialog(new OpenFileDialog(), "Projekt megnyitása", m_PROJECT_FILE_FILTER, ProjectManager.OpenProject))
                this.Text = m_MAINFORM_CAPTION + " - " + System.IO.Path.GetFileNameWithoutExtension(ProjectManager.ProjectFile);
        }
        //////////////////////////////////////////////////////////////////////////
        // projektfán file kiválasztása **
        private void treeProjectTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // megnyitás, de csak ha Source vagy Header
            if (e.Node is IntelNodeSource || e.Node is IntelNodeHeader)
                ProjectManager.ActivateTab(e.Node as IntelNodeFile);
        }
        //////////////////////////////////////////////////////////////////////////
        // projekt mentése **
        private void menuSaveProject_Click(object sender, EventArgs e)
        {
            ProjectManager.SaveProject(false);
        }
        //////////////////////////////////////////////////////////////////////////
        // projekt mentése másként **
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
        // projekt bezárása **
        private void menuCloseProject_Click(object sender, EventArgs e)
        {
            if (!ProjectManager.ProjectIsOpened)
                return;
            if (DialogResult.Yes == MessageBox.Show("Elmenti a projektet ?", "Projekt mentése", MessageBoxButtons.YesNo))
                menuSaveProject_Click(sender, e);
            ProjectManager.CloseProject();
            this.Text = m_MAINFORM_CAPTION;
        }
        //////////////////////////////////////////////////////////////////////////
        // kilépés **
        private void menuExit_Click(object sender, EventArgs e)
        {
            menuCloseProject_Click(sender, e);
            Close();
        }
        //////////////////////////////////////////////////////////////////////////
        // file projekthez adása **
        private void menuAddFileToProject_Click(object sender, EventArgs e)
        {
            ShowFileDialog(new OpenFileDialog(), "File projekthez adása", m_SOURCE_FILE_FILTER + "|" + m_HEADER_FILE_FILTER, ProjectManager.AddFileToProject);
        }
        //////////////////////////////////////////////////////////////////////////
        // file elvétele a projektbõl
        private void fileRemoveFileFromProject_Click(object sender, EventArgs e)
        {
            // ezek a hívások egyszerûsödni fognak, ha lesz saját EditorNode osztályunk TreeNode helyett
            if (ProjectManager.SelectedNode() is IntelNodeSource || ProjectManager.SelectedNode() is IntelNodeHeader)
                treeProjectTree.SelectedNode.Remove();  // majd kiveszi a tabot is a saját EditorNode osztály
        }
        //////////////////////////////////////////////////////////////////////////
        // file mentése **
        private void menuSaveFile_Click(object sender, EventArgs e)
        {
            if (tabEditorTabs.SelectedTab != null)
            {
                EditorPage  selectedpage = tabEditorTabs.SelectedTab as EditorPage;
                selectedpage.SaveContent(false);
            }
        }
        //////////////////////////////////////////////////////////////////////////
        // fordít és futtat
        private void menuCompileAndRun_Click(object sender, EventArgs e)
        {
            menuCompile_Click(sender, e);   // azért ez így nem maradhat a végén, csak akkor Runolunk, ha sikeres volt a fordítás
            menuRun_Click(sender, e);
        }
        //////////////////////////////////////////////////////////////////////////
        // exe fordítása
        private void menuCompile_Click(object sender, EventArgs e)
        {
            if (!ProjectManager.ProjectIsOpened)
                ProjectManager.Output.Write("Nincs nyitott projekt!");
            else
            {
                ProjectManager.SaveProject(false);
                if (!CompilerManager.Compile())
                    ProjectManager.Output.Write("Ellenõrizze a fordító beállításait!");
            }

        }
        //////////////////////////////////////////////////////////////////////////
        // lefordított exe futtatása
        private void menuRun_Click(object sender, EventArgs e)
        {
            CompilerManager.Run();
        }
        // kikapcsoljuk a nodeok szerkeszthetõségét a szerkesztés után
        //////////////////////////////////////////////////////////////////////////
        // egy általános dialogkezelõ függvény
        private delegate void FileDialogFunction(string FileName);
        private bool ShowFileDialog(FileDialog dialog, string title, string filterstring, FileDialogFunction func)
        {
            dialog.Title = title;
            dialog.Filter = filterstring;
            dialog.InitialDirectory = ProjectManager.ProjectPath;
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == DialogResult.OK && dialog.FileName.Length > 0)
                func(dialog.FileName);
            else // ha nem választottunk normális filet, hamis
                return false;
            return true;
        }
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        // fordító beállítása
        private void menuCompilerSettings_Click(object sender, EventArgs e)
        {
            CompilerSettings CompilerForm = new CompilerSettings();
            CompilerForm.ShowDialog();
        }

        private void menuNevjegy_Click(object sender, EventArgs e)
        {
            HelpForm newhelpform = new HelpForm();
            newhelpform.ShowDialog();
        }


        private void kivágToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ProjectManager.ActiveTab != null)
                ProjectManager.ActiveTab.Editor.Cut();
        }

        private void másolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ProjectManager.ActiveTab != null)
                ProjectManager.ActiveTab.Editor.Copy();
        }

        private void beszúrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ProjectManager.ActiveTab != null)
                ProjectManager.ActiveTab.Editor.Paste();
        }

        private void törölToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ProjectManager.ActiveTab != null)
                ProjectManager.ActiveTab.Editor.Cut();
        }

        private void mindenKiválasztToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ProjectManager.ActiveTab != null)
                ProjectManager.ActiveTab.Editor.SelectAll();
        }

        private void menuManual_Click(object sender, EventArgs e)
        {
            ManualForm newmanualform = new ManualForm();
            newmanualform.ShowDialog();
        }

     
    }
}