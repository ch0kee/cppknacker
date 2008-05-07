using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace CppKnacker
{
    public partial class CompilerSettings : Form
    {
        public CompilerSettings()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Fordító (g++) tallózása...";
            dialog.Filter = "g++ alkalmazás (g++.exe)|g++.exe";
            if (dialog.ShowDialog() == DialogResult.OK && dialog.FileName.Length > 0)
                txtPath.Text = dialog.FileName;
           
        }

        private void CompilerSettings_Load(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(MainForm.ProgramDirectory + "options.xml");
            XmlNode CppKnackerNode = doc.ChildNodes[0];
            XmlNode CompilerNode = CppKnackerNode.ChildNodes[0];
            txtPath.Text = CompilerNode.Attributes["Path"].Value;
            txtParameters.Text = CompilerNode.Attributes["Parameters"].Value;
        }

        private void btnSaveAndExit_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(MainForm.ProgramDirectory + "options.xml");
            XmlNode CppKnackerNode = doc.ChildNodes[0];
            XmlNode CompilerNode = CppKnackerNode.ChildNodes[0];
            CompilerNode.Attributes["Path"].Value = txtPath.Text;
            CompilerNode.Attributes["Parameters"].Value = txtParameters.Text;
            doc.Save(MainForm.ProgramDirectory + "options.xml");
            Close();
        }
    }
}