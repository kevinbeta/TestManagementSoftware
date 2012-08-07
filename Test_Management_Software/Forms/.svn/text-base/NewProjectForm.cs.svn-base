using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlServerCe;
using Test_Management_Software.Classes.Database_Utilities;

namespace Test_Management_Software.Classes
{
		public partial class NewProjectForm: Form {

			public NewProjectForm() {
				InitializeComponent();
                nameTextBox.MaxLength = 50;
			}

			private void addButton_Click(object sender, EventArgs e) {
				//TODO: Call add function\
                String temp = nameTextBox.Text.Trim();
                if (!checkDB(temp))
                {

                    if (temp != "")
                    {

                        addProject(temp);
                    }
                }
                else { MessageBox.Show("Project name already exists.", "Test Management Tool", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
			}

			private void addProject(String name) {

				Project project = new Project(name);
                this.Close();
				//TODO:Populate MainForm Tree list
			}
            private bool checkDB(String name)
            {
                string getSqlText = "SELECT projectName FROM Project;";
                DBCommand getSqlCmd = DBConnection.makeCommand(getSqlText);
                SqlCeDataReader getSqlReader = getSqlCmd.Start();
                List<String> data = new List<string>();
                while (getSqlReader.Read())
                {
                    data.Add(getSqlReader.GetString(0));
                }

                if (data.Contains(name))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
			private void closeButton_Click(object sender, EventArgs e) {
				this.Close();
			}
		}
}
