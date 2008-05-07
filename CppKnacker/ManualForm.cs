using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CppKnacker
{
    public partial class ManualForm : Form
    {
        public ManualForm()
        {
            InitializeComponent();
        }

        private void koszonto_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tema.Text = "Köszöntõ";
            tema.Location = new Point(382, 37);
            sugodoboz.Clear();
            sugodoboz.LoadFile(MainForm.ProgramDirectory + "koszonto.txt", RichTextBoxStreamType.PlainText);
        }

        private void menu_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tema.Text = "Menü felépítése és használata";
            tema.Location = new Point(321, 37);
            sugodoboz.Clear();
            sugodoboz.LoadFile(MainForm.ProgramDirectory + "menu.txt", RichTextBoxStreamType.PlainText);
        }

        private void szovegszerk_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tema.Text = "Szövegszerkesztõ kezelése";
            tema.Location = new Point(319, 37);
            sugodoboz.Clear();
            sugodoboz.LoadFile(MainForm.ProgramDirectory + "szovegszerk.txt", RichTextBoxStreamType.PlainText);
        }

        private void proj_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tema.Text = "Projektek kezelése";
            tema.Location = new Point(353, 37);
            sugodoboz.Clear();
            sugodoboz.LoadFile(MainForm.ProgramDirectory + "proj.txt", RichTextBoxStreamType.PlainText);
        }

        private void forras_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tema.Text = "Forrásfájlok kezelése";
            tema.Location = new Point(339, 37);
            sugodoboz.Clear();
            sugodoboz.LoadFile(MainForm.ProgramDirectory + "forras.txt", RichTextBoxStreamType.PlainText);
        }

        private void fordito_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tema.Text = "G++ fordító";
            tema.Location = new Point(376, 37);
            sugodoboz.Clear();
            sugodoboz.LoadFile(MainForm.ProgramDirectory + "fordito.txt", RichTextBoxStreamType.PlainText);
        }

        private void fordfutt_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tema.Text = "Fordítás és Futtatás";
            tema.Location = new Point(346, 37);
            sugodoboz.Clear();
            sugodoboz.LoadFile(MainForm.ProgramDirectory + "fordfutt.txt", RichTextBoxStreamType.PlainText);
        }

        private void ManualForm_Load(object sender, EventArgs e)
        {
            sugodoboz.Clear();
            sugodoboz.LoadFile(MainForm.ProgramDirectory + "koszonto.txt", RichTextBoxStreamType.PlainText);
        }
    }
}