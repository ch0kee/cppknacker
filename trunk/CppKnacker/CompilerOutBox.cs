using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CppKnacker
{
    public partial class CompilerOutBox : TextBox
    {
        public CompilerOutBox()
        {
            InitializeComponent();
        }
        // szöveg írása a kimenetre
        public void Write(string text)
        {
            AppendText(text + "\r\n");
            ScrollToCaret();
        }

        private void CompilerOutBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int charindex = this.GetCharIndexFromPosition(e.Location);
            int line = GetLineFromCharIndex(charindex);
            if (Lines.Length == 0) return;
            string linestring = Lines[line];
            char[] separator = { ':' };
            string[] data = linestring.Split(separator, 3);
            // data[0] = file, data[1] = sor, data[2] = oszlop
            // megpróbáljuk megnyitni
            foreach (IntelNodeFile file in ProjectManager.ProjectNodes)
            {
                if (file.Text.ToLower() == data[0].ToLower()) {
                    ProjectManager.ActivateTab(file);
                    break;
                }
            }
        }
    }
}
