using System;
using System.Collections;
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
            ContextMenu.MenuItems.Add(new MenuItem("Kivág", OnContextMenuCut));
            ContextMenu.MenuItems.Add(new MenuItem("Másol", OnContextMenuCopy));
            ContextMenu.MenuItems.Add(new MenuItem("Töröl", OnContextMenuCut));
            ContextMenu.MenuItems.Add(new MenuItem("Beszúr", OnContextMenuPaste));
            ContextMenu.MenuItems.Add(new MenuItem("Mindent kiválaszt", OnContextMenuSelectAll));
            ContextMenu.MenuItems.Add(new MenuItem("Mentés", OnContextMenuSave));
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
            if (!m_IsModified)
            {
                MarkAsModified();
                //HandleIntendation();
            }
            MessageBox.Show("1: leutottel egy betut");
            base.OnTextChanged(e);
            MessageBox.Show("2: leutottel egy betut");
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
        protected void OnContextMenuCut(object sender, EventArgs e)
        {
            Cut();
        }
        protected void OnContextMenuCopy(object sender, EventArgs e)
        {
            Copy();
        }
        protected void OnContextMenuPaste(object sender, EventArgs e)
        {
            Paste();
        }
        protected void OnContextMenuSelectAll(object sender, EventArgs e)
        {
            SelectAll();
        }
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
        protected void HandleIntendation()
        {
            MessageBox.Show("leutottel egy betut");
            if (SelectionStart == 1)
                Text.Insert(SelectionStart, "*");
        }
        //////////////////////////////////////////////////////////////////////////
        // segédfüggvények, propertyk

    }
}
