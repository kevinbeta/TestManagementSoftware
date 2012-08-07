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
    public partial class TestDesignWizard : Form
    {

        private DocumentFactory df = new DocumentFactory();
        private List<string> componentlist = new List<string>();
        private string name;
        private string version;
        private string description;
        private bool selectall = false;
        private MainForm parentForm;

        public TestDesignWizard(string name, string description, MainForm parentForm)
        {
            InitializeComponent();
            this.checkedListBox1.CheckOnClick = true;
            this.name = name;
            //this.version = version;
            this.description = description;
            this.parentForm = parentForm;
        }
        public TestDesignWizard()
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


        private void setcomponentlist()
        {
            String[] doneWithSubset = {
            "Introduction",
            "Test Plan",
            "Verification Testing",
            "Validation Testing"
           };
            String[] introSubset = {
            "*Purpose",
            "*Scope",
            "*Glossary",
            "*References",
            "*Overview of Document"
           };
            String[] testplanSubset = {
            "*Schedules and Resources",
            "*Test Recording",
            "*Test Reporting",
            "*Static Testing"
           };
            String[] verifSubset = {
            "*Unit Testing",
            "*Integrative Testing"
           };
            String[] validSubset = {
            "*System Testing",
            "*Acceptance and Beta Testing"
           };


            // i is the item counter
            // e is a subset counter
            int i = 1;
            int e = 0;
            //If the begining of subsets are not check this returns true.
            //This is used so they do not add subsets if the begining is not selected
            bool app = !checkedListBox1.CheckedItems.Contains("Introduction");
            bool app1 = !checkedListBox1.CheckedItems.Contains("Test Plan");
            bool app2 = !checkedListBox1.CheckedItems.Contains("Verification Testing");
            bool app3 = !checkedListBox1.CheckedItems.Contains("Validation Testing");
            if (checkBox2.Checked)
            {
                foreach (string a in checkedListBox1.Items)
                {
                    if (app)
                    {
                        if (introSubset.Contains(a))
                        { continue; }
                    }
                    if (app1)
                    {
                        if (testplanSubset.Contains(a))
                        { continue; }
                    }
                    if (app2)
                    {
                        if (verifSubset.Contains(a))
                        { continue; }
                    }
                    if (app3)
                    {
                        if (validSubset.Contains(a))
                        { continue; }
                    }
                    if (doneWithSubset.Contains(a))
                    {
                        e = 0;
                    }
                    if (e > 0)
                    {
                        componentlist.Add(i - 1 + "." + e + "  " + a);
                        if (checkedListBox1.CheckedItems.Contains(a))
                        {
                            componentlist.Add("TextArea");
                        }
                        else
                        {
                            componentlist.Add("     ");
                        }
                        e++;
                    }
                    else
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
                    if (doneWithSubset.Contains(a))
                    {
                        e = 1;
                    }
                }
            }
            else 
            {
                foreach (string a in checkedListBox1.CheckedItems)
                {
                    if (app)
                    {
                        if (introSubset.Contains(a))
                        { continue; }
                    }
                    if (app1)
                    {
                        if (testplanSubset.Contains(a))
                        { continue; }
                    }
                    if (app2)
                    {
                        if (verifSubset.Contains(a))
                        { continue; }
                    }
                    if (app3)
                    {
                        if (validSubset.Contains(a))
                        { continue; }
                    }
                    if (doneWithSubset.Contains(a))
                    {
                        e = 0;
                    }
                    if (e > 0)
                    {
                        componentlist.Add(i - 1 + "." + e + "  " + a);
                        componentlist.Add("TextArea");
                        e++;
                    }
                    else
                    {
                        componentlist.Add(i + ". " + a);
                        componentlist.Add("TextArea");
                        i++;
                    }
                    if (doneWithSubset.Contains(a))
                    {
                        e = 1;
                    }

                    }
                }

            }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            selectall = !selectall;
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, selectall);
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


        }
}


