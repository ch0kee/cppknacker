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

        //módosított keresõ, amely a rákövetkezõ és megelõzõ _ jelre nem ad találatot, pl.: _int
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
        // biztonságos keresõ, ami érvénytelen indexhatárokra sem rontja el a keresést
        int SafeFind(string text, int start, int end, RichTextBoxFinds options)
        {
            if (start < 0) return -1;
            if (end > TextLength - 1) return -1;
            return (end <= start) ? -1 : this.Find(text, start, end, options);
        }
        // megkeresi az összes elõfordulást
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
        // teljes szöveg, vagy egy sor színezése
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
            // akkor vagyunk sztringben, ha a megelõzõ nemkommentben lévõ idézõjelek száma páratlan
            int firstchar = GetFirstCharIndexFromLine(GetLineFromCharIndex(charindex));
            int dquotesctr = 0;
            for(int i = firstchar; i < charindex; ++i)
                if (Text[i] == '\"' && !IsInComment(i))
                    ++dquotesctr;
            return dquotesctr % 2 != 0;
        }
        // egy sor színezése, visszatérési érték a következõ parsolandó sor
        int ColorizeLine(int line)
        {
            // sor elsõ és utolsó karakterének megkeresése
            int start_char = this.GetFirstCharIndexFromLine(line);
            int end_char = start_char;
            while (end_char < TextLength && Text[end_char] != '\n') ++end_char;
            if (end_char == TextLength) end_char = TextLength - 1;
            int linestart = start_char;
            int lineend = end_char;
            // kiszûrjük a kommentblokkokat, azokat nem színezzük
            // külsõ kommentblokk szerinti szûkítés
            for (; start_char <= end_char && IsInComment(start_char); ++start_char) ;
            for (; end_char >= start_char && IsInComment(end_char); --end_char) ;
            // belsõ kommentblokkokat a sztringeknek megfelelõen színezzük
            ColorizeInterval(start_char, end_char);
            // ha van nyitó vagy csukó kommentjel a sorban, újraparse innentõl
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
            // sor feketére színezése
            this.Select(startchar, endchar - startchar + 1);
            this.SelectionColor = m_DefaultColor;            //
            // komment keresése
            endchar = ColorizeFrom("//", m_CommentColor, startchar, endchar);
            // sztring és c stílusú blokkok színezése
            List<block> StringDelimitedBlocks = CollectIntervals('\"'.ToString(), startchar, endchar);
            ColorizeBetweenBlocks(StringDelimitedBlocks, m_StringColor);  // beszínezzük közöttük
            //  List<block> I2 = CollectIntervals("/*", "*/", startchar, endchar);
            foreach (block b in StringDelimitedBlocks)
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
                Select(b[i - 1].end, b[i].start - b[i - 1].end + 1);
                SelectionColor = color;
            }
        }
        // felvágja intervallumokra a szakaszt, a kihagyott részt színezi
        private List<block> CollectIntervals(string mark, int startchar, int endchar)
        {
            List<block> intervals = new List<block>();
            int[] finds = SafeFindAll(mark, startchar, endchar, RichTextBoxFinds.NoHighlight,false).ToArray();
            // párosítás
            if (finds.Length > 0)
            {
                intervals.Add(new block(startchar, finds[0]));
                for (int j = 2; j < finds.Length; j+=2)
                    intervals.Add(new block(finds[j-1], finds[j]));

                if (finds.Length % 2 == 0)// páratlan (van nyitott)
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
        List<block> m_CommentBlocks = new List<block>();
        bool IsInComment(int charindex)
        {
            // kommentblokkok
            bool incomment = false;
            for (int i = 0; !incomment && i < m_CommentBlocks.Count; ++i)
                incomment = m_CommentBlocks[i].start <= charindex && charindex <= m_CommentBlocks[i].end + 1;

            return incomment;
        }
        void SetCommentBlocks() // beállítja a blokkokat
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
                        // keressük meg a párját
                        cb.end = SafeFind("*/", find + 2, TextLength - 1, RichTextBoxFinds.NoHighlight);
                        if (cb.end == -1 )
                            cb.end = TextLength - 1;
                        m_CommentBlocks.Add(cb);
                        find = cb.end + 2;
                    }
                        else find += 2; // ha sztringben van nézzük tovább

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
        // színezés adott karaktersortól, azzal együtt
        // visszatérési érték az utolsó nem színes karakter
        int ColorizeFrom(string marker, Color clr, int from, int to)
        {
            int last_non_colored_char = to;
            int startindex = SafeFind(marker, from, to, RichTextBoxFinds.NoHighlight | RichTextBoxFinds.MatchCase);
            if (startindex != -1 && !IsInString(startindex)) // talált 
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
        //////////////////////////////////////////////////////////////////////////
        // scrollbar visszaállítására
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
