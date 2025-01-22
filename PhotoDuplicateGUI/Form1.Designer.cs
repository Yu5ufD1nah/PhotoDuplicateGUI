namespace PhotoDuplicateGUI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnSelectFolder = new Button();
            txtFolderPath = new Label();
            listBoxLog = new ListBox();
            btnOrganize = new Button();
            progressBar = new ProgressBar();
            SuspendLayout();
            // 
            // btnSelectFolder
            // 
            btnSelectFolder.Location = new Point(576, 33);
            btnSelectFolder.Name = "btnSelectFolder";
            btnSelectFolder.Size = new Size(195, 50);
            btnSelectFolder.TabIndex = 0;
            btnSelectFolder.Text = "Select Folder";
            btnSelectFolder.UseVisualStyleBackColor = true;
            btnSelectFolder.Click += btnSelectFolder_Click;
            // 
            // txtFolderPath
            // 
            txtFolderPath.AutoSize = true;
            txtFolderPath.Location = new Point(55, 33);
            txtFolderPath.Name = "txtFolderPath";
            txtFolderPath.Size = new Size(255, 20);
            txtFolderPath.TabIndex = 1;
            txtFolderPath.Text = "Selected folder path will appear here";
            // 
            // listBoxLog
            // 
            listBoxLog.FormattingEnabled = true;
            listBoxLog.Location = new Point(55, 155);
            listBoxLog.Name = "listBoxLog";
            listBoxLog.ScrollAlwaysVisible = true;
            listBoxLog.Size = new Size(485, 84);
            listBoxLog.TabIndex = 2;
            // 
            // btnOrganize
            // 
            btnOrganize.Location = new Point(576, 182);
            btnOrganize.Name = "btnOrganize";
            btnOrganize.Size = new Size(195, 57);
            btnOrganize.TabIndex = 3;
            btnOrganize.Text = "Organise";
            btnOrganize.UseVisualStyleBackColor = true;
            btnOrganize.Click += btnOrganize_Click;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(55, 92);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(485, 29);
            progressBar.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 260);
            Controls.Add(progressBar);
            Controls.Add(btnOrganize);
            Controls.Add(listBoxLog);
            Controls.Add(txtFolderPath);
            Controls.Add(btnSelectFolder);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSelectFolder;
        private Label txtFolderPath;
        private ListBox listBoxLog;
        private Button btnOrganize;
        private ProgressBar progressBar;
    }
}
