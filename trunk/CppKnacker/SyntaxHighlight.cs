using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Windows;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.InteropServices;

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
                if (ret + text.Length < TextLength && Text[ret + text.Length] == '_'
                    || ret - 1 > 0 && Text[ret - 1] == '_')
                    ret = -1;
            }
            return ret;
        }
        // biztons�gos keres�, ami �rv�nytelen indexhat�rokra sem rontja el a keres�st
        int SafeFind(string text, int start, int end, RichTextBoxFinds options)
        {
            if (start < 0) return -1;
            if (end > TextLength - 1) return -1;
            return (end <= start) ? -1 : this.Find(text, start, end, options);
        }
        // megkeresi az �sszes el�fordul�st
        List<int> SafeFindAll(string text, int start, int end, RichTextBoxFinds options, bool searchInComments)
        {
            List<int> results = new List<int>();
            bool searchagain = false;
            do 
            {
                int found = SafeFind(text,start,end,options);
                if (searchagain = found != -1)
                    if (!searchInComments && !IsInComment(found) || searchInComments)
                        results.Add(found);
                start = found+1;
            } while (searchagain);
            return results;
        }
        // teljes sz�veg, vagy egy sor sz�nez�se
        public void Parse(bool wholetext)
        {
            int[] savselection = { this.SelectionStart, this.SelectionLength };
            Color savcolor = this.SelectionColor;
            //////////////////////////////////////////////////////////////////////////
            m_EnablePaint = false;
            Point savedscrollpos = ScrollPos;
            if (wholetext)
            {
                for (int i = 0; i < Lines.Length; ++i)
                    ColorizeLine(i);
            }
            else
            {
                int current_line = GetLineFromCharIndex(savselection[0]);
                int parsefromline = ColorizeLine(current_line);
                if (Lines.Length > 0 && 0 <= parsefromline && parsefromline < Lines.Length)
                {
                    for (int i = parsefromline; i < Lines.Length; ++i)
                        ColorizeLine(i);
                }
            }
            ColorizeCommentBlocks();
            SelectionStart = savselection[0]; SelectionLength = savselection[1];
            SelectionColor = savcolor;
            ScrollPos = savedscrollpos;
            m_EnablePaint = true;
            //////////////////////////////////////////////////////////////////////////
        }
        bool IsInString(int charindex)
        {
            // akkor vagyunk sztringben, ha a megel�z� nemkommentben l�v� id�z�jelek sz�ma p�ratlan
            int firstchar = GetFirstCharIndexFromLine(GetLineFromCharIndex(charindex));
            int dquotesctr = 0;
            for(int i = firstchar; i < charindex; ++i)
                if (Text[i] == '\"' && !IsInComment(i))
                    ++dquotesctr;
            return dquotesctr % 2 != 0;
        }
        // egy sor sz�nez�se, visszat�r�si �rt�k a k�vetkez� parsoland� sor
        int ColorizeLine(int line)
        {
            // sor els� �s utols� karakter�nek megkeres�se
            int start_char = this.GetFirstCharIndexFromLine(line);
            int end_char = start_char;
            while (end_char < TextLength && Text[end_char] != '\n') ++end_char;
            if (end_char == TextLength) end_char = TextLength - 1;
            int linestart = start_char;
            int lineend = end_char;
            // kisz�rj�k a kommentblokkokat, azokat nem sz�nezz�k
            // k�ls� kommentblokk szerinti sz�k�t�s
            for (; start_char <= end_char && IsInComment(start_char); ++start_char) ;
            for (; end_char >= start_char && IsInComment(end_char); --end_char) ;
            // bels� kommentblokkokat a sztringeknek megfelel�en sz�nezz�k
            ColorizeInterval(start_char, end_char);
            // ha van nyit� vagy csuk� kommentjel a sorban, �jraparse innent�l
            if (lineend > linestart && linestart >= 0 && lineend < TextLength)
            {
                string linestr = Text.Substring(linestart, lineend - linestart + 1);
                if (linestr.Contains("/*") || linestr.Contains("*/"))
                    return line + 1;
            }
            return -1;
        }
        void ColorizeInterval(int startchar, int endchar)
        {
            // sor feket�re sz�nez�se
            this.Select(startchar, endchar - startchar + 1);
            this.SelectionColor = m_DefaultColor;            //
            // komment keres�se
            endchar = ColorizeFrom("//", m_CommentColor, startchar, endchar);
            // sztring �s c st�lus� blokkok sz�nez�se
            List<block> StringDelimitedBlocks = CollectIntervals('\"'.ToString(), startchar, endchar);
            ColorizeBetweenBlocks(StringDelimitedBlocks, m_StringColor);  // besz�nezz�k k�z�tt�k
            //  List<block> I2 = CollectIntervals("/*", "*/", startchar, endchar);
            foreach (block b in StringDelimitedBlocks)
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
                Select(b[i - 1].end, b[i].start - b[i - 1].end + 1);
                SelectionColor = color;
            }
        }
        // felv�gja intervallumokra a szakaszt, a kihagyott r�szt sz�nezi
        private List<block> CollectIntervals(string mark, int startchar, int endchar)
        {
            List<block> intervals = new List<block>();
            int[] finds = SafeFindAll(mark, startchar, endchar, RichTextBoxFinds.NoHighlight,false).ToArray();
            // p�ros�t�s
            if (finds.Length > 0)
            {
                intervals.Add(new block(startchar, finds[0]));
                for (int j = 2; j < finds.Length; j+=2)
                    intervals.Add(new block(finds[j-1], finds[j]));

                if (finds.Length % 2 == 0)// p�ratlan (van nyitott)
                    intervals.Add(new block(finds[finds.Length-1], endchar));
                else
                    intervals.Add(new block(endchar, endchar));

            } else
            {
                intervals.Add(new block(startchar, endchar));
            }
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
        List<block> m_CommentBlocks = new List<block>();
        bool IsInComment(int charindex)
        {
            // kommentblokkok
            bool incomment = false;
            for (int i = 0; !incomment && i < m_CommentBlocks.Count; ++i)
                incomment = m_CommentBlocks[i].start <= charindex && charindex <= m_CommentBlocks[i].end + 1;

            return incomment;
        }
        void SetCommentBlocks() // be�ll�tja a blokkokat
        {
            m_CommentBlocks.Clear();/**/
            int find = 0;
            while (find != -1)
            {
                find = SafeFind("/*", find, TextLength - 1, RichTextBoxFinds.NoHighlight);
                if (find != -1)
                {
                    if (!IsInString(find))
                    {
                        block cb = new block();
                        cb.start = find;
                        // keress�k meg a p�rj�t
                        cb.end = SafeFind("*/", find + 2, TextLength - 1, RichTextBoxFinds.NoHighlight);
                        if (cb.end == -1 )
                            cb.end = TextLength - 1;
                        m_CommentBlocks.Add(cb);
                        find = cb.end + 2;
                    }
                        else find += 2; // ha sztringben van n�zz�k tov�bb

                }
            }
        }

        void ColorizeCommentBlocks()
        {
            for (int i = 0; i < m_CommentBlocks.Count; ++i)
            {
                Select(m_CommentBlocks[i].start, m_CommentBlocks[i].end - m_CommentBlocks[i].start + 2);
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
            if (startindex != -1 && !IsInString(startindex)) // tal�lt 
            {
                last_non_colored_char = startindex - 1;
                this.Select(startindex, to - startindex + 1);
                this.SelectionColor = clr;
            }
            return last_non_colored_char;
        }
        // parse
        int m_PreviousTextLength;
        bool m_NeedReparseAll = false;
        protected override void OnTextChanged(EventArgs e)
        {
            SetCommentBlocks();
            Parse(m_NeedReparseAll || System.Math.Abs(m_PreviousTextLength - TextLength) > 1);
            base.OnTextChanged(e);
            m_PreviousTextLength = TextLength;
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
        //////////////////////////////////////////////////////////////////////////
        // scrollbar vissza�ll�t�s�ra
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
       // private static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, ref Point lParam);
        //  lngResult = SendMessage(btn1.Handle, WM_LBUTTONUP, IntPtr.Zero, IntPtr.Zero);
        const int WM_USER = 1024;
        const int EM_GETSCROLLPOS = WM_USER + 221;
        const int EM_SETSCROLLPOS = WM_USER + 222;
        public Point ScrollPos
        {
            get
            {
                Point scrollPoint = new Point();
                SendMessage(this.Handle, EM_GETSCROLLPOS, 0, ref scrollPoint);
                return scrollPoint;
            }
            set
            {
                double _Yfactor = 1;
                Point original = value;
                if (original.Y < 0)
                    original.Y = 0;
                if (original.X < 0)
                    original.X = 0;

                Point factored = value;
                factored.Y = (int)((double)original.Y * _Yfactor);

                Point result = value;

                SendMessage(this.Handle, EM_SETSCROLLPOS, 0, ref
                factored);
                SendMessage(this.Handle, EM_GETSCROLLPOS, 0, ref
                result);

                int loopcount = 0;
                int maxloop = 100;
                while (result.Y != original.Y)
                {
                    // Adjust the input.
                    if (result.Y > original.Y)
                        factored.Y -= (result.Y - original.Y) / 2 - 1;
                    else if (result.Y < original.Y)
                        factored.Y += (original.Y - result.Y) / 2 + 1;

                    // test the new input.
                    SendMessage(this.Handle, EM_SETSCROLLPOS, 0, ref
                    factored);
                    SendMessage(this.Handle, EM_GETSCROLLPOS, 0, ref
                    result);

                    // save new factor, test for exit.
                    loopcount++;
                    if (loopcount >= maxloop || result.Y == original.Y)
                    {
                        _Yfactor = (double)factored.Y / (double)original.Y;
                        break;
                    }
                }
            }
        }
    }

    
}
