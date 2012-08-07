using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Test_Management_Software.Classes;
using Test_Management_Software.Classes.Database_Utilities;
using System.Data.SqlServerCe;
using System.Text.RegularExpressions;

namespace Test_Management_Software.Forms
{
    public partial class SubmitForm : Form
    {
        private String[] projects;
        private String[] data;
        private String type;
        private int[] projectID;

        public SubmitForm(List<String> projects, String[] data, String type, int[] projectID)
        {
            InitializeComponent();
            docNameTextBox.MaxLength = 50;
            this.projects = projects.ToArray();
            this.data = data;
            this.type = type;
            this.projectID = projectID;
            populateProjects();
        }

        private void populateProjects()
        {
            projectComboBox.Items.AddRange(this.projects);
        }
        private bool checkDB(String name)
        {
            int versionIDs;
            int projectID;
            String temp = docNameTextBox.Text.Trim();
            List<String> temp2 = new List<String>();

            String getProjectIDSqlText = "SELECT projectID FROM Project WHERE projectName='" + projectComboBox.Text + "';";
            DBCommand getProjectIDSqlCmd = DBConnection.makeCommand(getProjectIDSqlText);
            SqlCeDataReader getProjectIdReader = getProjectIDSqlCmd.Start();
            getProjectIdReader.Read();
            projectID = getProjectIdReader.GetInt32(0);

            String getVersionIDsSqlText = "SELECT versionID FROM Version, Project WHERE Version.versionNumber=" + versionComboBox.Text.Substring(7) + " AND Version.project=" + projectID + ";";
            DBCommand getVersionIDsSqlCmd = DBConnection.makeCommand(getVersionIDsSqlText);
            SqlCeDataReader getVersionIDsReader = getVersionIDsSqlCmd.Start();
            getVersionIDsReader.Read();
            versionIDs = getVersionIDsReader.GetInt32(0);

            String getSqlText = "SELECT documentName FROM Document WHERE versionID =" + versionIDs + ";";
            DBCommand getSqlCmd = DBConnection.makeCommand(getSqlText);
            SqlCeDataReader getReader = getSqlCmd.Start();


            while (getReader.Read())
            {
                temp2.Add(getReader.GetString(0));
            }
            if(temp2.Contains(name)){
            return true;}

            return false;
        }
        private bool submitDataToDatabase()
        {
            String temp = docNameTextBox.Text.Trim();
            if (!checkDB(temp))
            {
                if (temp != "" && projectComboBox.Text != "" && versionComboBox.Text != "")
                {
                    DocumentStorage documentStorage = new DocumentStorage(temp, type, data);
                    Serialization serialize = new Serialization();
                    string output = serialize.serialize(documentStorage);

                    int versionIDs;
                    int projectID;

                    String getProjectIDSqlText = "SELECT projectID FROM Project WHERE projectName='" + projectComboBox.Text + "';";
                    DBCommand getProjectIDSqlCmd = DBConnection.makeCommand(getProjectIDSqlText);
                    SqlCeDataReader getProjectIdReader = getProjectIDSqlCmd.Start();
                    getProjectIdReader.Read();
                    projectID = getProjectIdReader.GetInt32(0);

                    String getVersionIDsSqlText = "SELECT versionID FROM Version, Project WHERE Version.versionNumber=" + versionComboBox.Text.Substring(7) + " AND Version.project=" + projectID + ";";
                    DBCommand getVersionIDsSqlCmd = DBConnection.makeCommand(getVersionIDsSqlText);
                    SqlCeDataReader getVersionIDsReader = getVersionIDsSqlCmd.Start();
                    getVersionIDsReader.Read();
                    versionIDs = getVersionIDsReader.GetInt32(0);

                    String nextSqlText = "INSERT INTO Document (documentType, dateCreated, data, documentName, versionID) VALUES ('" + documentStorage.DocumentType + "', '" + DateTime.Now.Date + "', '" + output + "', '" + documentStorage.DocumentName + "', '" + versionIDs + "');";
                    DBCommand insertSqlCmd = DBConnection.makeCommand(nextSqlText);
                    insertSqlCmd.RunNoReturnQuery();

                    return true;
                }
            } 
            MessageBox.Show("Version already exists for this Project.", "Test Management Tool", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        

        private void projectComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            versionComboBox.Items.Clear();
            
            string getVersionSqlText = "SELECT versionNumber FROM Version WHERE project='" + projectID[projectComboBox.SelectedIndex] + "';";
            DBCommand getVersionSqlCmd = DBConnection.makeCommand(getVersionSqlText);
            SqlCeDataReader getVersionSqlReader = getVersionSqlCmd.Start();

            while(getVersionSqlReader.Read())
            {
                versionComboBox.Items.Add("Version " + getVersionSqlReader.GetSqlDouble(0));
                versionComboBox.Enabled = true;
            }
            
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (submitDataToDatabase())
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("You must complete all fields to commit data.", "Test Management Tool", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
