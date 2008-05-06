using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace CppKnacker
{
    // Szerkesztõ
    class EditorField : SyntaxHighlight
    {
        static Size m_SizePattern;
        public static Size SizePattern { set { m_SizePattern = value; } }
        bool m_IsModified = false;
        EditorPage m_Page;
        public EditorField(EditorPage page)
        {
            Size = m_SizePattern;
            Name = page.Text;
            AcceptsTab = true;
            m_Page = page;
            Font = new Font("Courier New", 10);
            ContextMenu = new ContextMenu();
            ContextMenu.MenuItems.Add(new MenuItem("Mentés",OnContextMenuSave));
            ContextMenu.MenuItems.Add(new MenuItem("Bezárás",OnContextMenuClose));
        }
        //////////////////////////////////////////////////////////////////////////
        // file betöltése
        public new void LoadFile(string filename)
        {
            base.LoadFile(filename, RichTextBoxStreamType.PlainText);
            base.Parse(true);
            MarkAsSaved();
        }
        //////////////////////////////////////////////////////////////////////////
        // file mentése
        public void SaveFile(string filename, bool forcesave)
        {
            if (!forcesave && !m_IsModified)
                return;
            base.SaveFile(filename, RichTextBoxStreamType.PlainText);
            MarkAsSaved();
        }
        //////////////////////////////////////////////////////////////////////////
        // ha megváltozik a szöveg
        protected override void OnTextChanged(EventArgs e)
        {
         //   HandleIntendation();
            if (!m_IsModified)
                MarkAsModified();
            base.OnTextChanged(e);
        }
        //////////////////////////////////////////////////////////////////////////
        // megjelölés, mint változott
        private void MarkAsModified()
        {
            m_IsModified = true;
            m_Page.Text = Name + "*";
        }
        //////////////////////////////////////////////////////////////////////////
        // megjelölés levétele, mentett
        private void MarkAsSaved()
        {
            m_IsModified = false;
            m_Page.Text = Name;
        }
        //////////////////////////////////////////////////////////////////////////
        // CONTEXT-MENU eseménykezelõk
        protected void OnContextMenuSave(object sender, EventArgs e)
        {
            m_Page.SaveContent(true);
        }
        protected void OnContextMenuClose(object sender, EventArgs e)
        {
            if (m_IsModified)
            {
                DialogResult res = MessageBox.Show("A fájl tartalma megváltozott a legutóbbi mentés óta.\nElmenti?", "File mentése", MessageBoxButtons.YesNoCancel);
                switch (res)
                {
                    case DialogResult.Yes:
                        OnContextMenuSave(sender, e);
                        break;
                    case DialogResult.Cancel:
                        return;
                    default:
                        break;
                }
            }
            m_Page.Dispose();
        }
        //////////////////////////////////////////////////////////////////////////
        // Enterre bejjebb kell kezdeni
        void HandleIntendation()
        {
            int lastindex = LastCharacter;

            if (lastindex == -1)
                return;
            //
            if ( Text[lastindex] == '{')
            {
                Select(SelectionStart, 1);
                this.SelectedText += "}";
                DeselectAll();
            }
        }
        //////////////////////////////////////////////////////////////////////////
        // segédfüggvények, propertyk
        int LastCharacter // utolsó nem whitespace karakter a kurzortól vissza
        {
            get
            {
                if (Text.Length == 0) return -1;
                for (int i = SelectionStart-1; i >= 0; --i)
                {
                    if (Text[i] != '\t' && Text[i] != ' ')
                        return i;
                }
                return -1;
            }
        }
    }
}
