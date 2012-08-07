namespace Test_Management_Software
{
    partial class NewTemplateReviewForm
    {
			private System.Windows.Forms.Button applyButton;
			private System.Windows.Forms.Button cancelButton;

			/// <summary>
			/// Required designer variable.
			/// </summary>
            private System.ComponentModel.IContainer components = null;

			/// <summary>
			/// Clean up any resources being used.
			/// </summary>
			/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
			protected override void Dispose(bool disposing) {
				if (disposing && (components != null))
				{
						components.Dispose();
				}
				base.Dispose(disposing);
			}

			/// <summary>
			/// Required method for Designer support - do not modify
			/// the contents of this method with the code editor.
			/// </summary>
			private void InitializeComponent() {
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewTemplateReviewForm));
                this.applyButton = new System.Windows.Forms.Button();
                this.cancelButton = new System.Windows.Forms.Button();
                this.panel1 = new System.Windows.Forms.Panel();
                this.SuspendLayout();
                // 
                // applyButton
                // 
                this.applyButton.Location = new System.Drawing.Point(340, 423);
                this.applyButton.Name = "applyButton";
                this.applyButton.Size = new System.Drawing.Size(75, 23);
                this.applyButton.TabIndex = 1;
                this.applyButton.Text = "Apply";
                this.applyButton.UseVisualStyleBackColor = true;
                this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
                // 
                // cancelButton
                // 
                this.cancelButton.Location = new System.Drawing.Point(430, 423);
                this.cancelButton.Name = "cancelButton";
                this.cancelButton.Size = new System.Drawing.Size(75, 23);
                this.cancelButton.TabIndex = 2;
                this.cancelButton.Text = "Cancel";
                this.cancelButton.UseVisualStyleBackColor = true;
                this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
                // 
                // panel1
                // 
                this.panel1.Location = new System.Drawing.Point(55, 12);
                this.panel1.Name = "panel1";
                this.panel1.Size = new System.Drawing.Size(416, 405);
                this.panel1.TabIndex = 3;
                // 
                // NewTemplateReviewForm
                // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.ClientSize = new System.Drawing.Size(517, 458);
                this.Controls.Add(this.panel1);
                this.Controls.Add(this.cancelButton);
                this.Controls.Add(this.applyButton);
                this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
                this.Name = "NewTemplateReviewForm";
                this.Text = "NewTemplateReviewForm";
                this.ResumeLayout(false);

			}

            private System.Windows.Forms.Panel panel1;
		}
}
