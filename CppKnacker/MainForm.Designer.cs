namespace CppKnacker
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabEditorTabs = new System.Windows.Forms.TabControl();
            this.patternBox = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.treeProjectTree = new System.Windows.Forms.TreeView();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNewFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddFileToProject = new System.Windows.Forms.ToolStripMenuItem();
            this.fileRemoveFileFromProject = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSaveFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.menuOpenProject = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNewProject = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSaveProject = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSaveProjectAs = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCloseProject = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditing = new System.Windows.Forms.ToolStripMenuItem();
            this.kiv�gToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m�solToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.besz�rToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.t�r�lToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.mindenKiv�lasztToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCompiling = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCompileAndRun = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuCompile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRun = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuCompilerSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuManual = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNevjegy = new System.Windows.Forms.ToolStripMenuItem();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.txtOutput = new CppKnacker.CompilerOutBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.patternBox)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.menu.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabEditorTabs
            // 
            this.tabEditorTabs.Location = new System.Drawing.Point(222, 49);
            this.tabEditorTabs.Name = "tabEditorTabs";
            this.tabEditorTabs.SelectedIndex = 0;
            this.tabEditorTabs.Size = new System.Drawing.Size(758, 487);
            this.tabEditorTabs.TabIndex = 5;
            // 
            // patternBox
            // 
            this.patternBox.Location = new System.Drawing.Point(224, 70);
            this.patternBox.Name = "patternBox";
            this.patternBox.Size = new System.Drawing.Size(751, 459);
            this.patternBox.TabIndex = 6;
            this.patternBox.TabStop = false;
            this.patternBox.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.treeProjectTree);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(7, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.groupBox1.Size = new System.Drawing.Size(213, 494);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Projekt menedzser";
            // 
            // treeProjectTree
            // 
            this.treeProjectTree.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeProjectTree.Location = new System.Drawing.Point(5, 19);
            this.treeProjectTree.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.treeProjectTree.Name = "treeProjectTree";
            this.treeProjectTree.Size = new System.Drawing.Size(204, 484);
            this.treeProjectTree.TabIndex = 1;
            this.treeProjectTree.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeProjectTree_NodeMouseDoubleClick);
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuNewFile,
            this.menuAddFileToProject,
            this.fileRemoveFileFromProject,
            this.menuSaveFile,
            this.toolStripSeparator4,
            this.menuOpenProject,
            this.menuNewProject,
            this.menuSaveProject,
            this.menuSaveProjectAs,
            this.menuCloseProject,
            this.toolStripSeparator3,
            this.menuExit});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(36, 20);
            this.menuFile.Text = "F�jl";
            // 
            // menuNewFile
            // 
            this.menuNewFile.Name = "menuNewFile";
            this.menuNewFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.menuNewFile.Size = new System.Drawing.Size(269, 22);
            this.menuNewFile.Text = "�j file...";
            this.menuNewFile.Click += new System.EventHandler(this.menuNewFile_Click);
            // 
            // menuAddFileToProject
            // 
            this.menuAddFileToProject.Name = "menuAddFileToProject";
            this.menuAddFileToProject.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.menuAddFileToProject.Size = new System.Drawing.Size(269, 22);
            this.menuAddFileToProject.Text = "File hozz�ad�s a projekthez...";
            this.menuAddFileToProject.Click += new System.EventHandler(this.menuAddFileToProject_Click);
            // 
            // fileRemoveFileFromProject
            // 
            this.fileRemoveFileFromProject.Name = "fileRemoveFileFromProject";
            this.fileRemoveFileFromProject.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.fileRemoveFileFromProject.Size = new System.Drawing.Size(269, 22);
            this.fileRemoveFileFromProject.Text = "File elv�tele a projektb�l...";
            this.fileRemoveFileFromProject.Click += new System.EventHandler(this.fileRemoveFileFromProject_Click);
            // 
            // menuSaveFile
            // 
            this.menuSaveFile.Name = "menuSaveFile";
            this.menuSaveFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menuSaveFile.Size = new System.Drawing.Size(269, 22);
            this.menuSaveFile.Text = "File ment�s";
            this.menuSaveFile.Click += new System.EventHandler(this.menuSaveFile_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(266, 6);
            // 
            // menuOpenProject
            // 
            this.menuOpenProject.Name = "menuOpenProject";
            this.menuOpenProject.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.O)));
            this.menuOpenProject.Size = new System.Drawing.Size(269, 22);
            this.menuOpenProject.Text = "Projekt megnyit�s...";
            this.menuOpenProject.Click += new System.EventHandler(this.menuOpenProject_Click);
            // 
            // menuNewProject
            // 
            this.menuNewProject.Name = "menuNewProject";
            this.menuNewProject.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.N)));
            this.menuNewProject.Size = new System.Drawing.Size(269, 22);
            this.menuNewProject.Text = "Projekt l�trehoz�s...";
            this.menuNewProject.Click += new System.EventHandler(this.menuNewProject_Click);
            // 
            // menuSaveProject
            // 
            this.menuSaveProject.Name = "menuSaveProject";
            this.menuSaveProject.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.S)));
            this.menuSaveProject.Size = new System.Drawing.Size(269, 22);
            this.menuSaveProject.Text = "Projekt ment�s";
            this.menuSaveProject.Click += new System.EventHandler(this.menuSaveProject_Click);
            // 
            // menuSaveProjectAs
            // 
            this.menuSaveProjectAs.Name = "menuSaveProjectAs";
            this.menuSaveProjectAs.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D)));
            this.menuSaveProjectAs.Size = new System.Drawing.Size(269, 22);
            this.menuSaveProjectAs.Text = "Projekt ment�s m�sk�nt...";
            this.menuSaveProjectAs.Click += new System.EventHandler(this.menuSaveProjectAs_Click);
            // 
            // menuCloseProject
            // 
            this.menuCloseProject.Name = "menuCloseProject";
            this.menuCloseProject.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Q)));
            this.menuCloseProject.Size = new System.Drawing.Size(269, 22);
            this.menuCloseProject.Text = "Projekt bez�r�s";
            this.menuCloseProject.Click += new System.EventHandler(this.menuCloseProject_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(266, 6);
            // 
            // menuExit
            // 
            this.menuExit.Name = "menuExit";
            this.menuExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.menuExit.Size = new System.Drawing.Size(269, 22);
            this.menuExit.Text = "Kil�p�s";
            this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
            // 
            // menuEditing
            // 
            this.menuEditing.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kiv�gToolStripMenuItem,
            this.m�solToolStripMenuItem,
            this.besz�rToolStripMenuItem,
            this.t�r�lToolStripMenuItem,
            this.toolStripSeparator6,
            this.mindenKiv�lasztToolStripMenuItem});
            this.menuEditing.Name = "menuEditing";
            this.menuEditing.Size = new System.Drawing.Size(76, 20);
            this.menuEditing.Text = "Szerkeszt�s";
            // 
            // kiv�gToolStripMenuItem
            // 
            this.kiv�gToolStripMenuItem.Name = "kiv�gToolStripMenuItem";
            this.kiv�gToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.kiv�gToolStripMenuItem.Text = "Kiv�g";
            this.kiv�gToolStripMenuItem.Click += new System.EventHandler(this.kiv�gToolStripMenuItem_Click);
            // 
            // m�solToolStripMenuItem
            // 
            this.m�solToolStripMenuItem.Name = "m�solToolStripMenuItem";
            this.m�solToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.m�solToolStripMenuItem.Text = "M�sol";
            this.m�solToolStripMenuItem.Click += new System.EventHandler(this.m�solToolStripMenuItem_Click);
            // 
            // besz�rToolStripMenuItem
            // 
            this.besz�rToolStripMenuItem.Name = "besz�rToolStripMenuItem";
            this.besz�rToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.besz�rToolStripMenuItem.Text = "Besz�r";
            this.besz�rToolStripMenuItem.Click += new System.EventHandler(this.besz�rToolStripMenuItem_Click);
            // 
            // t�r�lToolStripMenuItem
            // 
            this.t�r�lToolStripMenuItem.Name = "t�r�lToolStripMenuItem";
            this.t�r�lToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.t�r�lToolStripMenuItem.Text = "T�r�l";
            this.t�r�lToolStripMenuItem.Click += new System.EventHandler(this.t�r�lToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(164, 6);
            // 
            // mindenKiv�lasztToolStripMenuItem
            // 
            this.mindenKiv�lasztToolStripMenuItem.Name = "mindenKiv�lasztToolStripMenuItem";
            this.mindenKiv�lasztToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.mindenKiv�lasztToolStripMenuItem.Text = "Mindent kiv�laszt";
            this.mindenKiv�lasztToolStripMenuItem.Click += new System.EventHandler(this.mindenKiv�lasztToolStripMenuItem_Click);
            // 
            // menuCompiling
            // 
            this.menuCompiling.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuCompileAndRun,
            this.toolStripSeparator1,
            this.menuCompile,
            this.menuRun,
            this.toolStripSeparator2,
            this.menuCompilerSettings});
            this.menuCompiling.Name = "menuCompiling";
            this.menuCompiling.Size = new System.Drawing.Size(58, 20);
            this.menuCompiling.Text = "Ford�t�s";
            // 
            // menuCompileAndRun
            // 
            this.menuCompileAndRun.Name = "menuCompileAndRun";
            this.menuCompileAndRun.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.menuCompileAndRun.Size = new System.Drawing.Size(197, 22);
            this.menuCompileAndRun.Text = "Ford�t�s && Futtat�s";
            this.menuCompileAndRun.Click += new System.EventHandler(this.menuCompileAndRun_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(194, 6);
            // 
            // menuCompile
            // 
            this.menuCompile.Name = "menuCompile";
            this.menuCompile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F7)));
            this.menuCompile.Size = new System.Drawing.Size(197, 22);
            this.menuCompile.Text = "Ford�t�s";
            this.menuCompile.Click += new System.EventHandler(this.menuCompile_Click);
            // 
            // menuRun
            // 
            this.menuRun.Name = "menuRun";
            this.menuRun.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F5)));
            this.menuRun.Size = new System.Drawing.Size(197, 22);
            this.menuRun.Text = "Futtat�s";
            this.menuRun.Click += new System.EventHandler(this.menuRun_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(194, 6);
            // 
            // menuCompilerSettings
            // 
            this.menuCompilerSettings.Name = "menuCompilerSettings";
            this.menuCompilerSettings.Size = new System.Drawing.Size(197, 22);
            this.menuCompilerSettings.Text = "Ford�t� be�ll�t�sa...";
            this.menuCompilerSettings.Click += new System.EventHandler(this.menuCompilerSettings_Click);
            // 
            // menuHelp
            // 
            this.menuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuManual,
            this.menuNevjegy});
            this.menuHelp.Name = "menuHelp";
            this.menuHelp.Size = new System.Drawing.Size(60, 20);
            this.menuHelp.Text = "Seg�ts�g";
            // 
            // menuManual
            // 
            this.menuManual.Name = "menuManual";
            this.menuManual.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.menuManual.Size = new System.Drawing.Size(152, 22);
            this.menuManual.Text = "K�zik�nyv";
            this.menuManual.Click += new System.EventHandler(this.menuManual_Click);
            // 
            // menuNevjegy
            // 
            this.menuNevjegy.Name = "menuNevjegy";
            this.menuNevjegy.Size = new System.Drawing.Size(152, 22);
            this.menuNevjegy.Text = "N�vjegy";
            this.menuNevjegy.Click += new System.EventHandler(this.menuNevjegy_Click);
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuEditing,
            this.menuCompiling,
            this.menuHelp});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(1000, 24);
            this.menu.TabIndex = 0;
            this.menu.Text = "menu";
            // 
            // txtOutput
            // 
            this.txtOutput.Cursor = System.Windows.Forms.Cursors.No;
            this.txtOutput.Location = new System.Drawing.Point(7, 549);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOutput.Size = new System.Drawing.Size(973, 96);
            this.txtOutput.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton7,
            this.toolStripButton6,
            this.toolStripButton5,
            this.toolStripButton4,
            this.toolStripButton3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1000, 25);
            this.toolStrip1.TabIndex = 8;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "toolStripButton1";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton4.Text = "toolStripButton1";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton5.Text = "toolStripButton1";
            this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton6.Text = "toolStripButton1";
            this.toolStripButton6.Click += new System.EventHandler(this.toolStripButton6_Click);
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton7.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton7.Image")));
            this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton7.Text = "toolStripButton1";
            this.toolStripButton7.Click += new System.EventHandler(this.toolStripButton7_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1000, 668);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.patternBox);
            this.Controls.Add(this.tabEditorTabs);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.groupBox1);
            this.MainMenuStrip = this.menu;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1008, 702);
            this.MinimumSize = new System.Drawing.Size(1008, 702);
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "C++ Knacker";
            ((System.ComponentModel.ISupportInitialize)(this.patternBox)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabEditorTabs;
        private System.Windows.Forms.PictureBox patternBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView treeProjectTree;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuNewFile;
        private System.Windows.Forms.ToolStripMenuItem menuAddFileToProject;
        private System.Windows.Forms.ToolStripMenuItem fileRemoveFileFromProject;
        private System.Windows.Forms.ToolStripMenuItem menuSaveFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem menuOpenProject;
        private System.Windows.Forms.ToolStripMenuItem menuNewProject;
        private System.Windows.Forms.ToolStripMenuItem menuSaveProject;
        private System.Windows.Forms.ToolStripMenuItem menuSaveProjectAs;
        private System.Windows.Forms.ToolStripMenuItem menuCloseProject;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
        private System.Windows.Forms.ToolStripMenuItem menuEditing;
        private System.Windows.Forms.ToolStripMenuItem menuCompiling;
        private System.Windows.Forms.ToolStripMenuItem menuCompileAndRun;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuCompile;
        private System.Windows.Forms.ToolStripMenuItem menuRun;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem menuCompilerSettings;
        private System.Windows.Forms.ToolStripMenuItem menuHelp;
        private System.Windows.Forms.ToolStripMenuItem menuManual;
        private System.Windows.Forms.MenuStrip menu;
        private CompilerOutBox txtOutput;
        private System.Windows.Forms.ToolStripMenuItem menuNevjegy;
        private System.Windows.Forms.ToolStripMenuItem kiv�gToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m�solToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem besz�rToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem t�r�lToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem mindenKiv�lasztToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton7;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
    }
}

