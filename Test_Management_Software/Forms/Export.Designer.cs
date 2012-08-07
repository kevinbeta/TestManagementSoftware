namespace Test_Management_Software.Forms
{
    partial class Export
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Export));
            this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.dataLabel = new System.Windows.Forms.Label();
            this.projectComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.versionComboBox = new System.Windows.Forms.ComboBox();
            this.exportButton = new System.Windows.Forms.Button();
            this.exportBW = new System.ComponentModel.BackgroundWorker();
            this.button1 = new System.Windows.Forms.Button();
            this.percentLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.BW_Progress_Bar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.label3 = new System.Windows.Forms.Label();
            this.fileComboBox = new System.Windows.Forms.ComboBox();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // folderBrowser
            // 
            this.folderBrowser.SelectedPath = "C:\\Users\\cit_student3\\Desktop";
            // 
            // dataLabel
            // 
            this.dataLabel.AutoSize = true;
            this.dataLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataLabel.Location = new System.Drawing.Point(12, 86);
            this.dataLabel.Name = "dataLabel";
            this.dataLabel.Size = new System.Drawing.Size(137, 24);
            this.dataLabel.TabIndex = 0;
            this.dataLabel.Text = "Select Version:";
            // 
            // projectComboBox
            // 
            this.projectComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.projectComboBox.FormattingEnabled = true;
            this.projectComboBox.Location = new System.Drawing.Point(153, 37);
            this.projectComboBox.Name = "projectComboBox";
            this.projectComboBox.Size = new System.Drawing.Size(236, 21);
            this.projectComboBox.TabIndex = 1;
            this.projectComboBox.SelectedIndexChanged += new System.EventHandler(this.projectComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 24);
            this.label2.TabIndex = 2;
            this.label2.Text = "Select Project: ";
            // 
            // versionComboBox
            // 
            this.versionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.versionComboBox.Enabled = false;
            this.versionComboBox.FormattingEnabled = true;
            this.versionComboBox.Location = new System.Drawing.Point(155, 86);
            this.versionComboBox.Name = "versionComboBox";
            this.versionComboBox.Size = new System.Drawing.Size(234, 21);
            this.versionComboBox.TabIndex = 3;
            this.versionComboBox.SelectedIndexChanged += new System.EventHandler(this.versionComboBox_SelectedIndexChanged);
            // 
            // exportButton
            // 
            this.exportButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.exportButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exportButton.Location = new System.Drawing.Point(100, 187);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(101, 33);
            this.exportButton.TabIndex = 6;
            this.exportButton.Text = "Export";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // exportBW
            // 
            this.exportBW.WorkerReportsProgress = true;
            this.exportBW.WorkerSupportsCancellation = true;
            this.exportBW.DoWork += new System.ComponentModel.DoWorkEventHandler(this.exportBW_DoWork);
            this.exportBW.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.exportBW_ProgressChanged);
            this.exportBW.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.exportBW_RunWorkerCompleted);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(207, 187);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 33);
            this.button1.TabIndex = 10;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // percentLabel
            // 
            this.percentLabel.Name = "percentLabel";
            this.percentLabel.Size = new System.Drawing.Size(23, 15);
            this.percentLabel.Text = "0%";
            // 
            // BW_Progress_Bar
            // 
            this.BW_Progress_Bar.Name = "BW_Progress_Bar";
            this.BW_Progress_Bar.Size = new System.Drawing.Size(300, 16);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(55, 15);
            this.toolStripStatusLabel1.Text = ":Progress";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.percentLabel,
            this.BW_Progress_Bar,
            this.toolStripStatusLabel1});
            this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.statusStrip1.Location = new System.Drawing.Point(0, 223);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusStrip1.Size = new System.Drawing.Size(409, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 24);
            this.label3.TabIndex = 11;
            this.label3.Text = "Select File:";
            // 
            // fileComboBox
            // 
            this.fileComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fileComboBox.Enabled = false;
            this.fileComboBox.FormattingEnabled = true;
            this.fileComboBox.Location = new System.Drawing.Point(153, 135);
            this.fileComboBox.Name = "fileComboBox";
            this.fileComboBox.Size = new System.Drawing.Size(234, 21);
            this.fileComboBox.TabIndex = 12;
            // 
            // Export
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 245);
            this.Controls.Add(this.fileComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.exportButton);
            this.Controls.Add(this.versionComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.projectComboBox);
            this.Controls.Add(this.dataLabel);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Export";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Export Wizard";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowser;
        private System.Windows.Forms.Label dataLabel;
        private System.Windows.Forms.ComboBox projectComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox versionComboBox;
        private System.Windows.Forms.Button exportButton;
        private System.ComponentModel.BackgroundWorker exportBW;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripStatusLabel percentLabel;
        private System.Windows.Forms.ToolStripProgressBar BW_Progress_Bar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox fileComboBox;
    }
}