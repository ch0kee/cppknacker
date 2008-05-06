using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Windows;
using System.Collections;
using System.Collections.Specialized;

namespace CppKnacker
{
    class SyntaxHighlight : RichTextBox
    {
        static readonly Color m_DefaultColor = Color.Black;
        // kulcsszavak
        static readonly Color m_KeywordColor = Color.Blue;
        static readonly string[] m_KeywordStrings = {  "auto", "bool", "break", "case", "catch", "char", "class", "const", "continue", "default",
                                            "delete", "do", "double", "else", "enum", "extern", "float", "for", "friend", "goto",
                                            "if", "inline", "int", "long", "mutable", "namespace", "new", "operator", "private",
                                            "protected", "public", "register", "return", "short", "signed", "sizeof", "static",
                                            "struct", "switch", "template", "this", "throw", "try", "typedef", "typeid", "typename",
                                            "union", "unsigned", "using", "virtual", "void", "volatile", "while" };
        // preprocessor
        static readonly Color m_DirectiveColor = Color.Purple;
        static readonly string[] m_DirectiveStrings = { "#include", "#define", "#pragma", "#if", "#ifdef", "#ifndef", "#endif", "#else", "#elif" };
        //kommentek
        static readonly Color m_CommentColor = Color.Green;
        //sztringek
        static readonly Color m_StringColor = Color.Red;

        //m�dos�tott keres�, amely a r�k�vetkez� �s megel�z� _ jelre nem ad tal�latot, pl.: _int
        int FindWord(string text, int start, int end, RichTextBoxFinds options)
        {
            int ret = SafeFind(text, start, end, options);
            if (ret != -1)
            {
                if (ret + text.Length < TextLength  && Text[ret + text.Length] == '_'
                    || ret - 1 > 0                  && Text[ret - 1] == '_')
                    ret = -1;
            }
            return ret;   
        }
        // biztons�gos keres�, ami �rv�nytelen indexhat�rokra sem rontja el a keres�st
        int SafeFind(string text, int start, int end, RichTextBoxFinds options)
        {
            if (start < 0 || end > TextLength - 1) return -1;
            return (end <= start) ? -1 : this.Find(text, start, end, options);
        }
        // teljes sz�veg, vagy egy sor sz�nez�se
        public void Parse(bool wholetext)
        {
            int[] savselection = { this.SelectionStart, this.SelectionLength };
            Color savcolor = this.SelectionColor;
            //////////////////////////////////////////////////////////////////////////
            m_EnablePaint = false;
            SetCommentBlocks();
            ColorizeCommentBlocks();

            if (wholetext)
                for (int i = 0; i < Lines.Length; ++i)
                    ColorizeLine(i);
            else
                ColorizeLine(GetLineFromCharIndex(SelectionStart));
            SelectionStart = savselection[0]; SelectionLength = savselection[1];
            SelectionColor = savcolor;
            m_EnablePaint = true;
            //////////////////////////////////////////////////////////////////////////
        }
        // egy sor sz�nez�se
        void ColorizeLine(int line)
        {
            // sor els� �s utols� karakter�nek megkeres�se
            int start_char = this.GetFirstCharIndexFromLine(line);
            int end_char = start_char;
            while (end_char < TextLength && Text[end_char] != '\n') ++end_char;
            // kisz�rj�k a kommentblokkokat, azokat nem sz�nezz�k
            // k�ls� kommentblokk szerinti sz�k�t�s
            for (; start_char <= end_char && IsInCommentBlock(start_char); ++start_char) ;
            for (; end_char >= start_char && IsInCommentBlock(end_char); --end_char) ;
            // bels� kommentblokkokat a sztringeknek megfelel�en sz�nezz�k
            ColorizeInterval(start_char, end_char);
        }
        void ColorizeInterval(int startchar, int endchar)
        {
            // sor feket�re sz�nez�se
            this.Select(startchar, endchar - startchar + 1);
            this.SelectionColor = m_DefaultColor;            //
            // komment keres�se
            endchar = ColorizeFrom("//", m_CommentColor, startchar, endchar);
            // sztring �s c st�lus� blokkok sz�nez�se
            List<block> I1 = CollectIntervals('\"'.ToString(), '\"'.ToString(), startchar, endchar);
            ColorizeBetweenBlocks(I1, m_StringColor );  // besz�nezz�k k�z�tt�k
          //  List<block> I2 = CollectIntervals("/*", "*/", startchar, endchar);
            foreach (block b in I1)
            {
                // kulcsszavak sz�nez�se
                foreach (string keyword in m_KeywordStrings)
                    ColorizeWord(keyword, m_KeywordColor, b.start, b.end);
                // direkt�v�k sz�nez�se
                foreach (string directive in m_DirectiveStrings)
                    ColorizeStartWord(directive, m_DirectiveColor, b.start, b.end);
            }
        }

        private void ColorizeBetweenBlocks(List<block> b, Color color)
        {
            for (int i = 1; i < b.Count; ++i)
            {
                Select(b[i - 1].end, b[i].start - b[i - 1].end+1);
                SelectionColor = color;
            }
        }
        // felv�gja intervallumokra a szakaszt, a kihagyott r�szt sz�nez�
        private List<block> CollectIntervals(string sm, string em, int startchar, int endchar)
        {
            List<block> intervals = new List<block>();
            int blockstart = startchar;
            int blockend = blockstart;
            while (blockstart < endchar)
            {
                // blokkv�ge be�ll�t�sa
                blockend = SafeFind(sm, blockend, endchar, RichTextBoxFinds.NoHighlight);
                if (blockend == -1)
                    blockend = endchar;
                // blokk felv�tele
                intervals.Add(new block(blockstart, blockend));
                // blokkezd�s be�ll�t�sa
                blockstart = SafeFind(em, blockend + sm.Length, endchar, RichTextBoxFinds.NoHighlight);
                if (blockstart == -1)
                    blockstart = endchar;
                blockend = blockstart + sm.Length;              
            }
            // lez�r� blokk
            intervals.Add(new block(endchar - 1, endchar));
            return intervals;
        }
        //////////////////////////////////////////////////////////////////////////
        // adott sz� �sszes el�fordul�s�nak szinez�se
        void ColorizeWord(string word, Color clr, int from, int to)
        {
            while (from < to)
            {
                int startindex = FindWord(word, from, to, RichTextBoxFinds.NoHighlight | RichTextBoxFinds.MatchCase | RichTextBoxFinds.WholeWord);
                if (startindex == -1) // nincs t�bb el�fordul�s
                    return;
                // beszinezz�k a sz�t
                this.Select(startindex, word.Length);
                this.SelectionColor = clr;
                from = startindex + word.Length;
            }
        }
        //////////////////////////////////////////////////////////////////////////
        // adott sz� sor eleji el�fordul�s�nak sz�nez�se
        void ColorizeStartWord(string word, Color clr, int from, int to)
        {
            
            while (from < to)
            {
                int startindex = FindWord(word, from, to, RichTextBoxFinds.NoHighlight | RichTextBoxFinds.MatchCase | RichTextBoxFinds.WholeWord);
                if (startindex == -1) // nincs t�bb el�fordul�s
                    return;
                // ha az els� sz� a sorban akkor sz�nezz�k
                bool colorizeit = true;
                {
                    // sor elej�t�l megn�zz�k van-e whitespace-en k�v�l m�s
                    for (int i = GetFirstCharIndexFromLine(GetLineFromCharIndex(startindex)); i < startindex; ++i)
                        if (!char.IsWhiteSpace(Text[i]))
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
        // kommentblokkok
        struct block
        {
            public block(int s, int e) { start = s; end = e; }
            public int start;
            public int end;
        }
        List<block> comments = new List<block>();
        bool IsInCommentBlock(int charindex)
        {
            bool incomment = false;
            for (int i = 0; !incomment && i < comments.Count; ++i)
                incomment = comments[i].start + 1 < charindex && charindex < comments[i].end - 1;
            return incomment;
        }
        void SetCommentBlocks() // be�ll�tja a blokkokat
        {
            comments.Clear();/**/
            int find = 0;
            while (find!=-1)
            {
                find = Find("/*", find, RichTextBoxFinds.NoHighlight);
                if (find != -1)
                {
                    block cb = new block();
                    cb.start = find;
                    // keress�k meg a p�rj�t
                    cb.end = SafeFind("*/", find+2, TextLength-1, RichTextBoxFinds.NoHighlight);
                    if (cb.end == -1)
                        cb.end = TextLength - 1;
                    comments.Add(cb);
                    find = cb.end;
                }
            }
        }
        void ColorizeCommentBlocks()
        {
            for (int i = 0; i < comments.Count; ++i)
            {
                Select(comments[i].start, comments[i].end - comments[i].start + 2);
                SelectionColor = m_CommentColor;
            }
        }
        //////////////////////////////////////////////////////////////////////////
        // sz�nez�s adott karaktersort�l, azzal egy�tt
        // visszat�r�si �rt�k az utols� nem sz�nes karakter
        int ColorizeFrom(string marker, Color clr, int from, int to)
        {
            int last_non_colored_char = to;
            int startindex = SafeFind(marker, from, to, RichTextBoxFinds.NoHighlight | RichTextBoxFinds.MatchCase);
            if (startindex != -1) // tal�lt 
            {
                last_non_colored_char = startindex-1;
                this.Select(startindex, to-startindex+1);
                this.SelectionColor = clr;
            }
            return last_non_colored_char;
        }
        //////////////////////////////////////////////////////////////////////////
        // adott blokkon bel�l minden sz�nez�se
        void ColorizeBlock(string startmarker, string endmarker, Color clr, int from, int to)
        {
            while (from < to)
            {
                // keress�k a blokkjel elej�t
                int startindex = SafeFind(startmarker, from, to, RichTextBoxFinds.NoHighlight | RichTextBoxFinds.MatchCase);
                if (startindex == -1) // nincs t�bb el�fordul�s
                    return;

                int endindex = SafeFind(endmarker, startindex + startmarker.Length, to + 1, RichTextBoxFinds.NoHighlight | RichTextBoxFinds.MatchCase);
                if (endindex == -1) // legv�gig sz�nez�nk
                    endindex = TextLength;
                else
                    endindex += endmarker.Length;
                // sz�nez�s
                this.Select(startindex, endindex - startindex+1);
                this.SelectionColor = clr;
                from = endindex;                
            }
        }
        // kieg�sz�t�s karakterekre
        void ColorizeBlock(char startmarker, char endmarker, Color clr, int from, int to)
        {
            ColorizeBlock(startmarker.ToString(), endmarker.ToString(), clr, from, to);
        }
        // parse
        bool m_NeedReparseAll = false;
        protected override void OnTextChanged(EventArgs e)
        {
            Parse(m_NeedReparseAll);
            base.OnTextChanged(e);
            m_NeedReparseAll = false;
        }
        //////////////////////////////////////////////////////////////////////////
        // villog�s megsz�ntet�se
        const short WM_PAINT = 0x00f;
        public static bool m_EnablePaint = true;
        override protected void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == WM_PAINT)
            {
                if (m_EnablePaint)
                    base.WndProc(ref m);
                else
                    m.Result = IntPtr.Zero;
            }
            else
                base.WndProc(ref m);
        }
    }

    
}