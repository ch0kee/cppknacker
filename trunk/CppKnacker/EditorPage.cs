using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CppKnacker
{
    // Tablap
    class EditorPage : TabPage
    {
        IntelNodeFile m_Node;    // ez a hozzárendelt csomópont a projekt fában
        public bool IsSourceFile { get { return m_Node is IntelNodeSource; } }
        public bool IsHeaderFile { get { return m_Node is IntelNodeHeader; } }

        EditorField m_Editor;   // ez a szerkesztõmezõ

        // az én csomópontom ?
        public bool IsMyNode(IntelNodeFile Node) { return Node.Equals(m_Node); }

        public EditorPage(IntelNodeFile node)
        {
            m_Node = node;
            Text = m_Node.Text;
            Controls.Add(m_Editor = new EditorField(this));
        }
        // tartalom mentése
        public void SaveContent(bool forcesave)
        {
            m_Editor.SaveFile(ProjectManager.GetFullPath(m_Node), forcesave);
        }
        // tartalom mentése másként, és a csomópont frissítése
      /*  public void SaveContentAs(string Filename)
        {
            m_Node.Tag = Filename; m_Node.Text = ProjectManager.GetFile(m_Node);
            m_Editor.ForceSaveAndRefreshText(m_Node);    // attól függetlenül, hogy módosult-e, mentsük
            SaveContent();
        }*/
        // tartalom betöltése a csomópont Tag változójában levõ fileból
        public void LoadContent()
        {
            m_Editor.LoadFile(ProjectManager.GetFullPath(m_Node));
        }

        public EditorField Editor { get { return m_Editor; } }
    }
}
