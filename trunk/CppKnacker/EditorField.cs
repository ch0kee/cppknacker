using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace CppKnacker
{
    // Szerkeszt�
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
            ContextMenu.MenuItems.Add(new MenuItem("Kiv�g", OnContextMenuCut));
            ContextMenu.MenuItems.Add(new MenuItem("M�sol", OnContextMenuCopy));
            ContextMenu.MenuItems.Add(new MenuItem("T�r�l", OnContextMenuCut));
            ContextMenu.MenuItems.Add(new MenuItem("Besz�r", OnContextMenuPaste));
            ContextMenu.MenuItems.Add(new MenuItem("Mindent kiv�laszt", OnContextMenuSelectAll));
            ContextMenu.MenuItems.Add(new MenuItem("Ment�s", OnContextMenuSave));
            ContextMenu.MenuItems.Add(new MenuItem("Bez�r�s",OnContextMenuClose));
        }
        //////////////////////////////////////////////////////////////////////////
        // file bet�lt�se
        public new void LoadFile(string filename)
        {
            base.LoadFile(filename, RichTextBoxStreamType.PlainText);
            base.Parse(true);
            MarkAsSaved();
        }
        //////////////////////////////////////////////////////////////////////////
        // file ment�se
        public void SaveFile(string filename, bool forcesave)
        {
            if (!forcesave && !m_IsModified)
                return;
            base.SaveFile(filename, RichTextBoxStreamType.PlainText);
            MarkAsSaved();
        }
        //////////////////////////////////////////////////////////////////////////
        // ha megv�ltozik a sz�veg
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
        // megjel�l�s, mint v�ltozott
        private void MarkAsModified()
        {
            m_IsModified = true;
            m_Page.Text = Name + "*";
        }
        //////////////////////////////////////////////////////////////////////////
        // megjel�l�s lev�tele, mentett
        private void MarkAsSaved()
        {
            m_IsModified = false;
            m_Page.Text = Name;
        }
        //////////////////////////////////////////////////////////////////////////
        // CONTEXT-MENU esem�nykezel�k
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
                DialogResult res = MessageBox.Show("A f�jl tartalma megv�ltozott a legut�bbi ment�s �ta.\nElmenti?", "File ment�se", MessageBoxButtons.YesNoCancel);
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
        // seg�df�ggv�nyek, propertyk

    }
}
