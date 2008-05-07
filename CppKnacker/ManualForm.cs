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
            tema.Text = "K�sz�nt�";
            tema.Location = new Point(420, 37);
            sugodoboz.Clear();
            sugodoboz.LoadFile("koszonto.txt", RichTextBoxStreamType.PlainText);
        }

        private void menu_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tema.Text = "Men� fel�p�t�se �s haszn�lata";
            tema.Location = new Point(357, 37);
            sugodoboz.Clear();
            sugodoboz.LoadFile("menu.txt", RichTextBoxStreamType.PlainText);
        }

        private void szovegszerk_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tema.Text = "Sz�vegszerkeszt� kezel�se";
            tema.Location = new Point(352, 37);
            sugodoboz.Clear();
            sugodoboz.LoadFile("szovegszerk.txt", RichTextBoxStreamType.PlainText);
        }

        private void proj_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tema.Text = "Projektek kezel�se";
            tema.Location = new Point(386, 37);
            sugodoboz.Clear();
            sugodoboz.LoadFile("proj.txt", RichTextBoxStreamType.PlainText);
        }

        private void forras_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tema.Text = "Forr�sf�jlok kezel�se";
            tema.Location = new Point(372, 37);
            sugodoboz.Clear();
            sugodoboz.LoadFile("forras.txt", RichTextBoxStreamType.PlainText);
        }

        private void fordito_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tema.Text = "G++ ford�t�";
            tema.Location = new Point(409, 37);
            sugodoboz.Clear();
            sugodoboz.LoadFile("fordito.txt", RichTextBoxStreamType.PlainText);
        }

        private void fordfutt_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tema.Text = "Ford�t�s �s Futtat�s";
            tema.Location = new Point(379, 37);
            sugodoboz.Clear();
            sugodoboz.LoadFile("fordfutt.txt", RichTextBoxStreamType.PlainText);
        }
    }
}