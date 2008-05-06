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
        int m_RegionStart;
        int m_RegionLength;
        public EditorField(EditorPage page)
        {
            Size = m_SizePattern;
            Name = page.Text;
            AcceptsTab = true;
            m_Page = page;
            m_RegionStart = 0;
            m_RegionLength = 0;
            Font = new Font("Courier New", 10);
            ContextMenu = new ContextMenu();
            ContextMenu.MenuItems.Add(new MenuItem("Ment�s",OnContextMenuSave));
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
            HandleIntendation();
            if (!m_IsModified)
                MarkAsModified();
            base.OnTextChanged(e);
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
            SelectRegion();
            if (m_RegionLength > 0)
            {
                int f = Find("\n", m_RegionStart, m_RegionStart + m_RegionLength - 1, RichTextBoxFinds.WholeWord);
                if (f == -1)
                    Text.Insert(m_RegionStart, "\n");
                SelectionStart = f == -1 ? m_RegionStart + 1 : f + 1;
                SelectionLength = m_RegionLength;
                Select();
                SelectionIndent += 9;
                DeselectAll();
            }
        }
        //////////////////////////////////////////////////////////////////////////
        // seg�df�ggv�nyek, propertyk
        protected void SelectRegion()
        {

            if (Text.Length == 0) return;   //ures a szerkeszto
            else
            {
                int f = m_RegionStart;
                f = Find("{", f, RichTextBoxFinds.WholeWord);   //az elso nyitozarojel
                if (f == -1)
                {
                    m_RegionLength = 0;
                    return;
                }
                
                if (f + 1 < Text.Length)
                {
                    int ItsPair = f + 1;
                    int OpenBrackets = 0;
                    //amig nem ertunk az editor vegere es nem talaltuk meg a parjat
                    while (ItsPair < Text.Length && (Text[ItsPair] != '}' || OpenBrackets > 0))
                    {
                        if (Text[ItsPair] == '{')
                            ++OpenBrackets;
                        else if (Text[ItsPair] == '}')
                            --OpenBrackets;
                        ++ItsPair;
                    }
                    if (ItsPair < Text.Length)  //ha megvan a parja
                    {
                        MessageBox.Show("start: " + m_RegionStart + ", ItsPair: " + ItsPair + ", Text.Length: " + Text.Length);
                        m_RegionStart = f + 1;
                        m_RegionLength = ItsPair - f - 1;
                        MessageBox.Show("start: " + m_RegionStart + ", length: " + m_RegionLength);
                    }
                    else
                    {
                        m_RegionStart = f + 1;
                        m_RegionLength = Text.Length - f - 1;
                    }
                }
                else
                {
                    m_RegionLength = 0;
                }
            }
        }

    }
}
