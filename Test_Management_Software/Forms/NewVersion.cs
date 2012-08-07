using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlServerCe;

namespace Test_Management_Software.Forms
{
    public partial class NewVersion : Form
    {
        public NewVersion(Object[] projects)
        {
            InitializeComponent();
            versionNumberTextBox.MaxLength = 50;
            populateComboBox(projects);
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private bool checkDB(Double version,String name)
        {
            string findProjectSqlText = "SELECT projectID FROM Project WHERE projectName='" + projectsComboBox.Text + "';";
            DBCommand findProjectSqlCmd = DBConnection.makeCommand(findProjectSqlText);
            SqlCeDataReader findProjectSqlReader = findProjectSqlCmd.Start();
            findProjectSqlReader.Read();
            int projectID = findProjectSqlReader.GetInt32(0);
            findProjectSqlReader.Close();
            findProjectSqlCmd.Stop();

            string getSqlText = "SELECT versionNumber FROM Version WHERE project='"+projectID+"';";
            DBCommand getSqlCmd = DBConnection.makeCommand(getSqlText);
            SqlCeDataReader getSqlReader = getSqlCmd.Start();
            List<Double> data = new List<Double>();
            while (getSqlReader.Read())
            {
                data.Add(getSqlReader.GetDouble(0));
            }

            if (data.Contains(version))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void addButton_Click(object sender, EventArgs e)
        {
            
                if (projectsComboBox.SelectedIndex >= 0)
                {
                    try
                    {
                        Double temp = Double.Parse(versionNumberTextBox.Text.Trim());
                        String temp2 = projectsComboBox.Text;
                        if (!checkDB(temp,temp2))
                        {
                            addVersionToDB(temp);
                        this.Close();
                        }
                        else { MessageBox.Show("Version already exists for this Project.", "Test Management Tool", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("You must enter a numeric value.", "Test Management Tool", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        versionNumberTextBox.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("You must select a project", "Test Management Tool", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            
        }

        private void populateComboBox(Object[] projects)
        {
            projectsComboBox.Items.Clear();
            projectsComboBox.Items.AddRange(projects);
        }
        private void addVersionToDB(double versionNumber)
        {
            try
            {
                string findProjectSqlText = "SELECT projectID FROM Project WHERE projectName='" + projectsComboBox.Text + "';";
                DBCommand findProjectSqlCmd = DBConnection.makeCommand(findProjectSqlText);
                SqlCeDataReader findProjectSqlReader = findProjectSqlCmd.Start();
                findProjectSqlReader.Read();
                int projectID = findProjectSqlReader.GetInt32(0);
                findProjectSqlReader.Close();
                findProjectSqlCmd.Stop();
                string nextVersionSqlText = "INSERT INTO Version (versionNumber, dateCreated, project) VALUES (" + versionNumber + ", '" + DateTime.Now.Date + "'," + projectID + ");";

                DBCommand nextVersionSqlCmd = DBConnection.makeCommand(nextVersionSqlText);

                nextVersionSqlCmd.RunNoReturnQuery();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);

            }
        }

    }
}

