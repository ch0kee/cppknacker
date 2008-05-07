namespace CppKnacker
{
    partial class ManualForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.sugodoboz = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.fordfutt = new System.Windows.Forms.LinkLabel();
            this.forras = new System.Windows.Forms.LinkLabel();
            this.proj = new System.Windows.Forms.LinkLabel();
            this.fordito = new System.Windows.Forms.LinkLabel();
            this.szovegszerk = new System.Windows.Forms.LinkLabel();
            this.koszonto = new System.Windows.Forms.LinkLabel();
            this.menu = new System.Windows.Forms.LinkLabel();
            this.tema = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // sugodoboz
            // 
            this.sugodoboz.BackColor = System.Drawing.Color.White;
            this.sugodoboz.Cursor = System.Windows.Forms.Cursors.No;
            this.sugodoboz.Location = new System.Drawing.Point(286, 58);
            this.sugodoboz.Name = "sugodoboz";
            this.sugodoboz.ReadOnly = true;
            this.sugodoboz.Size = new System.Drawing.Size(326, 374);
            this.sugodoboz.TabIndex = 0;
            this.sugodoboz.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(56, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Súgó témák";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.fordfutt);
            this.panel1.Controls.Add(this.forras);
            this.panel1.Controls.Add(this.proj);
            this.panel1.Controls.Add(this.fordito);
            this.panel1.Controls.Add(this.szovegszerk);
            this.panel1.Controls.Add(this.koszonto);
            this.panel1.Controls.Add(this.menu);
            this.panel1.Location = new System.Drawing.Point(12, 58);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(208, 374);
            this.panel1.TabIndex = 2;
            // 
            // fordfutt
            // 
            this.fordfutt.AutoSize = true;
            this.fordfutt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.fordfutt.Location = new System.Drawing.Point(3, 343);
            this.fordfutt.Name = "fordfutt";
            this.fordfutt.Size = new System.Drawing.Size(129, 17);
            this.fordfutt.TabIndex = 10;
            this.fordfutt.TabStop = true;
            this.fordfutt.Text = "Fordítás és futtatás";
            this.fordfutt.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.fordfutt_LinkClicked);
            // 
            // forras
            // 
            this.forras.AutoSize = true;
            this.forras.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.forras.Location = new System.Drawing.Point(3, 233);
            this.forras.Name = "forras";
            this.forras.Size = new System.Drawing.Size(142, 17);
            this.forras.TabIndex = 9;
            this.forras.TabStop = true;
            this.forras.Text = "Forrásfájlok kezelése";
            this.forras.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.forras_LinkClicked);
            // 
            // proj
            // 
            this.proj.AutoSize = true;
            this.proj.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.proj.Location = new System.Drawing.Point(3, 179);
            this.proj.Name = "proj";
            this.proj.Size = new System.Drawing.Size(127, 17);
            this.proj.TabIndex = 8;
            this.proj.TabStop = true;
            this.proj.Text = "Projektek kezelése";
            this.proj.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.proj_LinkClicked);
            // 
            // fordito
            // 
            this.fordito.AutoSize = true;
            this.fordito.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.fordito.Location = new System.Drawing.Point(3, 290);
            this.fordito.Name = "fordito";
            this.fordito.Size = new System.Drawing.Size(79, 17);
            this.fordito.TabIndex = 7;
            this.fordito.TabStop = true;
            this.fordito.Text = "G++ fordító";
            this.fordito.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.fordito_LinkClicked);
            // 
            // szovegszerk
            // 
            this.szovegszerk.AutoSize = true;
            this.szovegszerk.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.szovegszerk.Location = new System.Drawing.Point(3, 121);
            this.szovegszerk.Name = "szovegszerk";
            this.szovegszerk.Size = new System.Drawing.Size(183, 17);
            this.szovegszerk.TabIndex = 6;
            this.szovegszerk.TabStop = true;
            this.szovegszerk.Text = "Szövegszerkesztõ kezelése";
            this.szovegszerk.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.szovegszerk_LinkClicked);
            // 
            // koszonto
            // 
            this.koszonto.AutoSize = true;
            this.koszonto.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.koszonto.Location = new System.Drawing.Point(3, 10);
            this.koszonto.Name = "koszonto";
            this.koszonto.Size = new System.Drawing.Size(67, 17);
            this.koszonto.TabIndex = 5;
            this.koszonto.TabStop = true;
            this.koszonto.Text = "Köszöntõ";
            this.koszonto.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.koszonto_LinkClicked);
            // 
            // menu
            // 
            this.menu.AutoSize = true;
            this.menu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.menu.Location = new System.Drawing.Point(3, 65);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(181, 17);
            this.menu.TabIndex = 4;
            this.menu.TabStop = true;
            this.menu.Text = "Menü felépítése/használata";
            this.menu.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.menu_LinkClicked);
            // 
            // tema
            // 
            this.tema.AutoSize = true;
            this.tema.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tema.Location = new System.Drawing.Point(420, 37);
            this.tema.Name = "tema";
            this.tema.Size = new System.Drawing.Size(73, 18);
            this.tema.TabIndex = 3;
            this.tema.Text = "Köszöntõ";
            // 
            // ManualForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 444);
            this.Controls.Add(this.tema);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sugodoboz);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ManualForm";
            this.Text = "Kézikönyv";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox sugodoboz;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel koszonto;
        private System.Windows.Forms.LinkLabel menu;
        private System.Windows.Forms.Label tema;
        private System.Windows.Forms.LinkLabel fordfutt;
        private System.Windows.Forms.LinkLabel forras;
        private System.Windows.Forms.LinkLabel proj;
        private System.Windows.Forms.LinkLabel fordito;
        private System.Windows.Forms.LinkLabel szovegszerk;
    }
}