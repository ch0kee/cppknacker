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

        //módosított keresõ, amely a rákövetkezõ és megelõzõ _ jelre nem ad találatot, pl.: _int
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
        // biztonságos keresõ, ami érvénytelen indexhatárokra sem rontja el a keresést
        int SafeFind(string text, int start, int end, RichTextBoxFinds options)
        {
            if (start < 0 || end > TextLength - 1) return -1;
            return (end <= start) ? -1 : this.Find(text, start, end, options);
        }
        // teljes szöveg, vagy egy sor színezése
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
        // egy sor színezése
        void ColorizeLine(int line)
        {
            // sor elsõ és utolsó karakterének megkeresése
            int start_char = this.GetFirstCharIndexFromLine(line);
            int end_char = start_char;
            while (end_char < TextLength && Text[end_char] != '\n') ++end_char;
            // kiszûrjük a kommentblokkokat, azokat nem színezzük
            // külsõ kommentblokk szerinti szûkítés
            for (; start_char <= end_char && IsInCommentBlock(start_char); ++start_char) ;
            for (; end_char >= start_char && IsInCommentBlock(end_char); --end_char) ;
            // belsõ kommentblokkokat a sztringeknek megfelelõen színezzük
            ColorizeInterval(start_char, end_char);
        }
        void ColorizeInterval(int startchar, int endchar)
        {
            // sor feketére színezése
            this.Select(startchar, endchar - startchar + 1);
            this.SelectionColor = m_DefaultColor;            //
            // komment keresése
            endchar = ColorizeFrom("//", m_CommentColor, startchar, endchar);
            // sztring és c stílusú blokkok színezése
            List<block> I1 = CollectIntervals('\"'.ToString(), '\"'.ToString(), startchar, endchar);
            ColorizeBetweenBlocks(I1, m_StringColor );  // beszínezzük közöttük
          //  List<block> I2 = CollectIntervals("/*", "*/", startchar, endchar);
            foreach (block b in I1)
            {
                // kulcsszavak színezése
                foreach (string keyword in m_KeywordStrings)
                    ColorizeWord(keyword, m_KeywordColor, b.start, b.end);
                // direktívák színezése
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
        // felvágja intervallumokra a szakaszt, a kihagyott részt színezé
        private List<block> CollectIntervals(string sm, string em, int startchar, int endchar)
        {
            List<block> intervals = new List<block>();
            int blockstart = startchar;
            int blockend = blockstart;
            while (blockstart < endchar)
            {
                // blokkvége beállítása
                blockend = SafeFind(sm, blockend, endchar, RichTextBoxFinds.NoHighlight);
                if (blockend == -1)
                    blockend = endchar;
                // blokk felvétele
                intervals.Add(new block(blockstart, blockend));
                // blokkezdés beállítása
                blockstart = SafeFind(em, blockend + sm.Length, endchar, RichTextBoxFinds.NoHighlight);
                if (blockstart == -1)
                    blockstart = endchar;
                blockend = blockstart + sm.Length;              
            }
            // lezáró blokk
            intervals.Add(new block(endchar - 1, endchar));
            return intervals;
        }
        //////////////////////////////////////////////////////////////////////////
        // adott szó összes elõfordulásának szinezése
        void ColorizeWord(string word, Color clr, int from, int to)
        {
            while (from < to)
            {
                int startindex = FindWord(word, from, to, RichTextBoxFinds.NoHighlight | RichTextBoxFinds.MatchCase | RichTextBoxFinds.WholeWord);
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
                int startindex = FindWord(word, from, to, RichTextBoxFinds.NoHighlight | RichTextBoxFinds.MatchCase | RichTextBoxFinds.WholeWord);
                if (startindex == -1) // nincs több elõfordulás
                    return;
                // ha az elsõ szó a sorban akkor színezzük
                bool colorizeit = true;
                {
                    // sor elejétõl megnézzük van-e whitespace-en kívül más
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
        void SetCommentBlocks() // beállítja a blokkokat
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
                    // keressük meg a párját
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
        // színezés adott karaktersortól, azzal együtt
        // visszatérési érték az utolsó nem színes karakter
        int ColorizeFrom(string marker, Color clr, int from, int to)
        {
            int last_non_colored_char = to;
            int startindex = SafeFind(marker, from, to, RichTextBoxFinds.NoHighlight | RichTextBoxFinds.MatchCase);
            if (startindex != -1) // talált 
            {
                last_non_colored_char = startindex-1;
                this.Select(startindex, to-startindex+1);
                this.SelectionColor = clr;
            }
            return last_non_colored_char;
        }
        //////////////////////////////////////////////////////////////////////////
        // adott blokkon belül minden színezése
        void ColorizeBlock(string startmarker, string endmarker, Color clr, int from, int to)
        {
            while (from < to)
            {
                // keressük a blokkjel elejét
                int startindex = SafeFind(startmarker, from, to, RichTextBoxFinds.NoHighlight | RichTextBoxFinds.MatchCase);
                if (startindex == -1) // nincs több elõfordulás
                    return;

                int endindex = SafeFind(endmarker, startindex + startmarker.Length, to + 1, RichTextBoxFinds.NoHighlight | RichTextBoxFinds.MatchCase);
                if (endindex == -1) // legvégig színezünk
                    endindex = TextLength;
                else
                    endindex += endmarker.Length;
                // színezés
                this.Select(startindex, endindex - startindex+1);
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
        bool m_NeedReparseAll = false;
        protected override void OnTextChanged(EventArgs e)
        {
            Parse(m_NeedReparseAll);
            base.OnTextChanged(e);
            m_NeedReparseAll = false;
        }
        //////////////////////////////////////////////////////////////////////////
        // villogás megszüntetése
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
