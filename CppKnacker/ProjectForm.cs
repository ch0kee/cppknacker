using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CppKnacker
{
    public partial class ProjectForm : Form
    {
        public ProjectForm()
        {
            InitializeComponent();
        }

        private void btnBrowseParentPath_Click(object sender, EventArgs e)
        {
            FolderBrowser FBrowser = new FolderBrowser();
            if (FBrowser.ShowDialog() == DialogResult.OK && FBrowser.DirectoryPath.Length > 0)
            {
                ActualizeProjectPath(FBrowser.DirectoryPath);
                txtProjectName.Enabled = true;
            }
        }
        // megvan a szülõ könyvtár
        private void ActualizeProjectPath(string ProjectPath)
        {
            txtProjectFilePath.Text = (m_ProjectPath = ProjectPath) + (m_ProjectPath.EndsWith(@"\") ? "" : @"\");            
        }
        // elkezdte bepötyögni a projekt nevét a csávinger
        private void txtProjectName_TextChanged(object sender, EventArgs e)
        {
            ActualizeProjectPath(m_ProjectPath);
            if (btnCreateProject.Enabled = txtProjectName.Text.Length > 0)
            {
                txtProjectFilePath.Text += txtProjectName.Text + @"\" + txtProjectName.Text + ".ckp";
            }
        }
        string m_ProjectPath;
        public string ProjectPath
        { get { return m_ProjectPath; } }
        public string ProjectFile
        { get { return m_ProjectPath + @"\" + System.IO.Path.GetFileName(m_ProjectPath) + ".ckp"; } }

        private void btnCreateProject_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            // adatok mentése
            m_ProjectPath = txtProjectFilePath.Text.Substring(0, txtProjectFilePath.Text.LastIndexOf(@"\"));
            //
            Close();
        }
        public enum ProjectFormWorkMode { SaveProjectAs, NewProject}
        public ProjectFormWorkMode WorkMode
        { 
            set 
            {
                if (value == ProjectFormWorkMode.SaveProjectAs)
                {
                    Text = "Projekt mentése másként";
                    btnCreateProject.Text = "Projekt mentése";
                }

        }

        }

    }
}