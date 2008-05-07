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
            tema.Location = new Point(420, 37);
            sugodoboz.Clear();
            sugodoboz.LoadFile("koszonto.txt", RichTextBoxStreamType.PlainText);
        }

        private void menu_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tema.Text = "Menü felépítése és használata";
            tema.Location = new Point(357, 37);
            sugodoboz.Clear();
            sugodoboz.LoadFile("menu.txt", RichTextBoxStreamType.PlainText);
        }

        private void szovegszerk_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tema.Text = "Szövegszerkesztõ kezelése";
            tema.Location = new Point(352, 37);
            sugodoboz.Clear();
            sugodoboz.LoadFile("szovegszerk.txt", RichTextBoxStreamType.PlainText);
        }

        private void proj_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tema.Text = "Projektek kezelése";
            tema.Location = new Point(386, 37);
            sugodoboz.Clear();
            sugodoboz.LoadFile("proj.txt", RichTextBoxStreamType.PlainText);
        }

        private void forras_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tema.Text = "Forrásfájlok kezelése";
            tema.Location = new Point(372, 37);
            sugodoboz.Clear();
            sugodoboz.LoadFile("forras.txt", RichTextBoxStreamType.PlainText);
        }

        private void fordito_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tema.Text = "G++ fordító";
            tema.Location = new Point(409, 37);
            sugodoboz.Clear();
            sugodoboz.LoadFile("fordito.txt", RichTextBoxStreamType.PlainText);
        }

        private void fordfutt_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tema.Text = "Fordítás és Futtatás";
            tema.Location = new Point(379, 37);
            sugodoboz.Clear();
            sugodoboz.LoadFile("fordfutt.txt", RichTextBoxStreamType.PlainText);
        }
    }
}