using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CppKnacker
{
    // Tablap
    class EditorPage : TabPage
    {
        IntelNodeFile m_Node;    // ez a hozz�rendelt csom�pont a projekt f�ban
        public bool IsSourceFile { get { return m_Node is IntelNodeSource; } }
        public bool IsHeaderFile { get { return m_Node is IntelNodeHeader; } }

        EditorField m_Editor;   // ez a szerkeszt�mez�

        // az �n csom�pontom ?
        public bool IsMyNode(IntelNodeFile Node) { return Node.Equals(m_Node); }

        public EditorPage(IntelNodeFile node)
        {
            m_Node = node;
            Text = m_Node.Text;
            Controls.Add(m_Editor = new EditorField(this));
        }
        // tartalom ment�se
        public void SaveContent(bool forcesave)
        {
            m_Editor.SaveFile(ProjectManager.GetFullPath(m_Node), forcesave);
        }
        // tartalom ment�se m�sk�nt, �s a csom�pont friss�t�se
      /*  public void SaveContentAs(string Filename)
        {
            m_Node.Tag = Filename; m_Node.Text = ProjectManager.GetFile(m_Node);
            m_Editor.ForceSaveAndRefreshText(m_Node);    // att�l f�ggetlen�l, hogy m�dosult-e, ments�k
            SaveContent();
        }*/
        // tartalom bet�lt�se a csom�pont Tag v�ltoz�j�ban lev� fileb�l
        public void LoadContent()
        {
            m_Editor.LoadFile(ProjectManager.GetFullPath(m_Node));
        }

        public EditorField Editor { get { return m_Editor; } }
    }
}
