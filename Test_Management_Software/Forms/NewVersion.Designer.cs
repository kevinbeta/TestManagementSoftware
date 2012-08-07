namespace Test_Management_Software.Forms
{
    partial class NewVersion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewVersion));
            this.closeButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.versionNumberTextBox = new System.Windows.Forms.TextBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.projectLabel = new System.Windows.Forms.Label();
            this.projectsComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(293, 88);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(93, 23);
            this.closeButton.TabIndex = 9;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(194, 88);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(93, 23);
            this.addButton.TabIndex = 8;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // versionNumberTextBox
            // 
            this.versionNumberTextBox.Location = new System.Drawing.Point(162, 20);
            this.versionNumberTextBox.Name = "versionNumberTextBox";
            this.versionNumberTextBox.Size = new System.Drawing.Size(224, 20);
            this.versionNumberTextBox.TabIndex = 7;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLabel.Location = new System.Drawing.Point(18, 20);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(127, 20);
            this.nameLabel.TabIndex = 6;
            this.nameLabel.Text = "Version Number:";
            // 
            // projectLabel
            // 
            this.projectLabel.AutoSize = true;
            this.projectLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.projectLabel.Location = new System.Drawing.Point(18, 57);
            this.projectLabel.Name = "projectLabel";
            this.projectLabel.Size = new System.Drawing.Size(108, 20);
            this.projectLabel.TabIndex = 10;
            this.projectLabel.Text = "Project Name:";
            // 
            // projectsComboBox
            // 
            this.projectsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.projectsComboBox.FormattingEnabled = true;
            this.projectsComboBox.Location = new System.Drawing.Point(162, 56);
            this.projectsComboBox.Name = "projectsComboBox";
            this.projectsComboBox.Size = new System.Drawing.Size(224, 21);
            this.projectsComboBox.TabIndex = 11;
            // 
            // NewVersion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 131);
            this.Controls.Add(this.projectsComboBox);
            this.Controls.Add(this.projectLabel);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.versionNumberTextBox);
            this.Controls.Add(this.nameLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewVersion";
            this.Text = "New Version";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.TextBox versionNumberTextBox;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label projectLabel;
        private System.Windows.Forms.ComboBox projectsComboBox;
    }
}