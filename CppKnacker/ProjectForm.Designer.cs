namespace CppKnacker
{
    partial class ProjectForm
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
            System.Windows.Forms.Button btnCancel;
            this.btnCreateProject = new System.Windows.Forms.Button();
            this.txtProjectName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtProjectFilePath = new System.Windows.Forms.TextBox();
            this.btnBrowseParentPath = new System.Windows.Forms.Button();
            btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            btnCancel.Location = new System.Drawing.Point(290, 126);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(75, 23);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Mégsem";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnCreateProject
            // 
            this.btnCreateProject.Enabled = false;
            this.btnCreateProject.Location = new System.Drawing.Point(12, 126);
            this.btnCreateProject.Name = "btnCreateProject";
            this.btnCreateProject.Size = new System.Drawing.Size(125, 23);
            this.btnCreateProject.TabIndex = 0;
            this.btnCreateProject.Text = "Projekt létrehozása";
            this.btnCreateProject.UseVisualStyleBackColor = true;
            this.btnCreateProject.Click += new System.EventHandler(this.btnCreateProject_Click);
            // 
            // txtProjectName
            // 
            this.txtProjectName.Enabled = false;
            this.txtProjectName.Location = new System.Drawing.Point(12, 26);
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.Size = new System.Drawing.Size(204, 20);
            this.txtProjectName.TabIndex = 2;
            this.txtProjectName.TextChanged += new System.EventHandler(this.txtProjectName_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Projekt neve";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Projektfile elérési útja";
            // 
            // txtProjectFilePath
            // 
            this.txtProjectFilePath.Location = new System.Drawing.Point(12, 66);
            this.txtProjectFilePath.Name = "txtProjectFilePath";
            this.txtProjectFilePath.ReadOnly = true;
            this.txtProjectFilePath.Size = new System.Drawing.Size(204, 20);
            this.txtProjectFilePath.TabIndex = 5;
            // 
            // btnBrowseParentPath
            // 
            this.btnBrowseParentPath.Location = new System.Drawing.Point(222, 26);
            this.btnBrowseParentPath.Name = "btnBrowseParentPath";
            this.btnBrowseParentPath.Size = new System.Drawing.Size(143, 60);
            this.btnBrowseParentPath.TabIndex = 6;
            this.btnBrowseParentPath.Text = "Szülõ mappa tallózása...";
            this.btnBrowseParentPath.UseVisualStyleBackColor = true;
            this.btnBrowseParentPath.Click += new System.EventHandler(this.btnBrowseParentPath_Click);
            // 
            // ProjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 161);
            this.Controls.Add(this.btnBrowseParentPath);
            this.Controls.Add(this.txtProjectFilePath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtProjectName);
            this.Controls.Add(btnCancel);
            this.Controls.Add(this.btnCreateProject);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(400, 197);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 197);
            this.Name = "ProjectForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Projekt létrehozása";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCreateProject;
        private System.Windows.Forms.TextBox txtProjectName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtProjectFilePath;
        private System.Windows.Forms.Button btnBrowseParentPath;

    }
}