using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Test_Management_Software.Classes;
using Test_Management_Software.Forms;
using Test_Management_Software.Classes.Database_Utilities;

namespace Test_Management_Software
{
    public partial class TestIncidentReportWizard : Form
    {
        private DocumentFactory df = new DocumentFactory();
        private List<string> componentlist = new List<string>();
        private string name;
        private string version;
        private string description;
        private bool selectall = false;
        private MainForm parentForm;

        public TestIncidentReportWizard(string name, string description, MainForm parentForm)
        {
            //put in the designer
            //this.checkedListBox1.CheckOnClick = true;
            InitializeComponent();
            this.name = name;
            this.description = description;
            this.parentForm = parentForm;
            //this.version = version;
        }
        public TestIncidentReportWizard()
        {
            this.name = "Template";
            this.version = "Template";
            InitializeComponent();
        }
        private void makePanel()
        {
            setcomponentlist();
            //populate so user can input necessary data
            DocumentDisplayForm ddf = new DocumentDisplayForm(df.createDocument(name, version, componentlist));
            if (ddf.ShowDialog() == DialogResult.OK)
            {
                //Serializes the newly created document. This should be added to each Wizard.
                TemplateStorage document = new TemplateStorage(name, version, description, componentlist);
                Serialization saveDocument = new Serialization();
                string output = saveDocument.serialize(document);

                String nextSqlText = "INSERT INTO Template (templateName, templateDescription, dateCreated, templateData, enabled) VALUES ('" + document.Name + "', '" + document.Description + "', '" + DateTime.Now.Date + "', '" + output + "', " + 1 + ");";
                DBCommand insertSqlCmd = DBConnection.makeCommand(nextSqlText);
                insertSqlCmd.RunNoReturnQuery();
                parentForm.refreshAll();
                this.Close();
            }
            else
            {
                this.componentlist.Clear();
            }
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            //next button actions
            if (checkedListBox1.CheckedItems.Count == 0)
            {
                MessageBox.Show("You must check at least one item.", "Test Management Tool", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                makePanel();
            }

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            //cancel button actions
            this.Close();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            selectall = !selectall;
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, selectall);
            }
        }

        private void setcomponentlist()
        {
            {
                int i = 1;
                if (checkBox2.Checked)
                {
                    foreach (string a in checkedListBox1.Items)
                    {
                        componentlist.Add(i + ". " + a);
                        if (checkedListBox1.CheckedItems.Contains(a))
                        {
                            componentlist.Add("TextArea");
                        }
                        else
                        {
                            componentlist.Add("     ");
                        }
                        i++;
                    }
                }
                else
                {
                    foreach (string a in checkedListBox1.CheckedItems)
                    {


                        componentlist.Add(i + ". " + a);
                        componentlist.Add("TextArea");
                        i++;
                    }

                }
            }

        }


    }
}
