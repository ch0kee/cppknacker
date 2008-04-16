using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms.Design;
using System.Windows.Forms;


namespace CppKnacker
{
    class FolderBrowser : FolderNameEditor
    {
        private FolderNameEditor.FolderBrowser m_Browser = null;
        private string m_Description;
        public FolderBrowser()
        {
            m_Description = "V�lassz mapp�t";
            m_Browser = new FolderNameEditor.FolderBrowser();
        }

        public string DirectoryPath
        {
            get { return this.m_Browser.DirectoryPath; }
        }

        public DialogResult ShowDialog()
        {
            m_Browser.Description = m_Description;
            return m_Browser.ShowDialog();
        }

    }
}
