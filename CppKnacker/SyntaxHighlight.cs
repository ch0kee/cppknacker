using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Windows;
using System.Collections;

namespace CppKnacker
{
    class SyntaxHighlight : RichTextBox
    {
        //
        static Color m_DefaultColor = Color.Black;
        // kulcsszavak
        static Color m_KeywordColor = Color.Blue;
        static string[] m_KeywordStrings = {  "auto", "bool", "break", "case", "catch", "char", "class", "const", "continue", "default",
                                            "delete", "do", "double", "else", "enum", "extern", "float", "for", "friend", "goto",
                                            "if", "inline", "int", "long", "mutable", "namespace", "new", "operator", "private",
                                            "protected", "public", "register", "return", "short", "signed", "sizeof", "static",
                                            "struct", "switch", "template", "this", "throw", "try", "typedef", "typeid", "typename",
                                            "union", "unsigned", "using", "virtual", "void", "volatile", "while" };
        // preprocessor
        static Color m_DirectiveColor = Color.Purple;
            // nem végleges a lista
        static string[] m_DirectiveStrings = { "#include", "#define", "#pragma", "#if", "#ifdef", "#ifndef", "#endif", "#else", "#elif" };
        //kommentek
        static Color m_CommentColor = Color.Green;
        //sztringek
        static Color m_StringColor = Color.Red;
        ////////////////////////////////////////////////////////////////////////////
        // A kulcsszavak keresése úgy fog mûködni, hogy ha paste vagy file nyitás //
        // volt, akkor minden sort végigparzol, egyébként csak az aktuális sorban //
        // színez, mert ez a leglassabb pontja a színezésnek                      //
        ////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        // syntax highlight
        protected void ParseText()
        {
            _Paint = false;
            int[] savselection = { this.SelectionStart, this.SelectionLength };
            Color savcolor = this.SelectionColor;
            //////////////////////////////////////////////////////////////////////////
            Select(0, Text.Length);
            SelectionColor = m_DefaultColor;
            // kulcsszavak
            foreach (string keyword in m_KeywordStrings)
                ColorizeWord(keyword, m_KeywordColor, 0, Text.Length);
            // preprocesszor
            foreach (string directive in m_DirectiveStrings)
                ColorizeStartWord(directive, m_DirectiveColor, 0, Text.Length);            
            // sztring színezés
            ColorizeBlock("\"", "\"", m_StringColor, 0, Text.Length);
            //komment szinezes
            ColorizeLine("//", m_CommentColor, 0, Text.Length);
            ColorizeBlock("/*", "*/", m_CommentColor, 0, Text.Length);
            //////////////////////////////////////////////////////////////////////////
            SelectionStart = savselection[0]; SelectionLength = savselection[1];
            SelectionColor = savcolor;
            _Paint = true;
        }
        //////////////////////////////////////////////////////////////////////////
        // adott szó összes elõfordulásának ilyen színüre szinezése
        void ColorizeWord(string word, Color clr, int from, int to)
        {
            while (from < to)
            {
                int startindex = Find(word, from, to, RichTextBoxFinds.NoHighlight | RichTextBoxFinds.MatchCase | RichTextBoxFinds.WholeWord);
                if (startindex == -1) // nincs több elõfordulás
                    return;
                // beszinezzük a szót
                this.Select(startindex, word.Length);
                this.SelectionColor = clr;
                from = startindex + word.Length;
            }
        }
        //////////////////////////////////////////////////////////////////////////
        // adott szó sor eleji elõfordulásának színezése
        void ColorizeStartWord(string word, Color clr, int from, int to)
        {
            while (from < to)
            {
                int startindex = Find(word, from, to, RichTextBoxFinds.NoHighlight | RichTextBoxFinds.MatchCase | RichTextBoxFinds.WholeWord);
                if (startindex == -1) // nincs több elõfordulás
                    return;
                // ha az elsõ szó a sorban akkor színezzük
                bool colorizeit = true;
                {
                    // sor elejétõl megnézzük van-e whitespace-en kívül más
                    for (int i = GetFirstCharIndexFromLine(GetLineFromCharIndex(startindex)); i < startindex; ++i)
                        if (Text[i] != ' ' && Text[i] != '\t')
                        {
                            colorizeit = false;
                            break;
                        }
                }
                if (colorizeit)
                {
                    this.Select(startindex, word.Length);
                    this.SelectionColor = clr;
                }

                from = startindex + word.Length;
            }
        }
        //////////////////////////////////////////////////////////////////////////
        // sor színezése adott karaktersortól, azzal együtt
        void ColorizeLine(string marker, Color clr, int from, int to)
        {
            while(from < to)
            {
                int startindex = Find(marker, from, to, RichTextBoxFinds.NoHighlight | RichTextBoxFinds.MatchCase);
                if (startindex == -1) // nincs több elõfordulás
                    return;
                // legközelebbi sorvége jel megkeresése
                int eol = startindex+marker.Length;
                while (eol < Text.Length && eol < to && Text[eol] != '\n') ++eol;
                // beszinezünk mindent eolig
                this.Select(startindex, eol - startindex);
                this.SelectionColor = clr;
                from = eol;
            }
        }
        //////////////////////////////////////////////////////////////////////////
        // adott blokkon belül minden színezése
        void ColorizeBlock(string startmarker, string endmarker, Color clr, int from, int to)
        {
            while (from < to)
            {
                // keressük a blokkjel elejét
                int startindex = Find(startmarker, from, to, RichTextBoxFinds.NoHighlight | RichTextBoxFinds.MatchCase);
                if (startindex == -1) // nincs több elõfordulás
                    return;

                int endindex = Find(endmarker, startindex + startmarker.Length, to+1, RichTextBoxFinds.NoHighlight | RichTextBoxFinds.MatchCase);
                if (endindex == -1) // legvégig színezünk
                    endindex = Text.Length;
                else
                    endindex += endmarker.Length;
                // színezés
                this.Select(startindex, endindex - startindex);
                this.SelectionColor = clr;
                from = endindex;
            }
        }
        // kiegészítés karakterekre
        void ColorizeBlock(char startmarker, char endmarker, Color clr, int from, int to)
        {
            ColorizeBlock(startmarker.ToString(), endmarker.ToString(), clr, from, to);
        }
        // parse
        protected override void OnTextChanged(EventArgs e)
        {
            ParseText();
            base.OnTextChanged(e);
        }
        //////////////////////////////////////////////////////////////////////////
        // villogás megszüntetése
        const short WM_PAINT = 0x00f;
        public static bool _Paint = true;
        override protected void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == WM_PAINT)
            {
                if (_Paint)
                    base.WndProc(ref m);
                else
                    m.Result = IntPtr.Zero;
            }
            else
                base.WndProc(ref m);
        }
    }

    
}
