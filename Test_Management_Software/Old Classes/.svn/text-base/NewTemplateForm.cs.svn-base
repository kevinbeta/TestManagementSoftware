using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Test_Management_Software
{
    public partial class NewTemplateForm : Form
    {
			public NewTemplateForm() {
				InitializeComponent();
			}

			private void closeButton_Click(object sender, EventArgs e) {
				this.Close();
			}

			private void nextButton_Click(object sender, EventArgs e) {
				//TODO: Open the next window
                string temp = textBox1.Text;
                string temp2 = templateDescriptionTextBox.Text;
                //Makes sure the fields are not blank
                if (temp != "" && temp2 != "")
                {
                    Console.WriteLine(temp);
                    Console.WriteLine(temp2);
                    //NewTemplateWizard wizardForm = new NewTemplateWizard(temp, temp2);
                    //wizardForm.Show();
                    this.Close();
                }
                else
                {
                    //Throw Error Message
                    MessageBox.Show("Both the Template Name and Description fields must not be blank");
                }
			}
		}
}
