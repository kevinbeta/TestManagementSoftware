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
    public partial class TestPlanWizard : Form
    {
        private DocumentFactory df = new DocumentFactory();
        private List<string> componentlist = new List<string>();
        private string name;
        private string version;
        private string description;
        private MainForm parentForm;
        bool selectall = false;

        public TestPlanWizard(string name, string description, MainForm parentForm)
        {
            InitializeComponent();
            this.name = name;
            this.description = description;
            this.parentForm = parentForm;
        }
        private void makePanel()
        {
            setcomponentlist();

            //populate so user can input necessary data
            DocumentDisplayForm ddf = new DocumentDisplayForm(df.createDocument(name, description, componentlist));
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

        private void button1_Click(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            //cancel button actions
            this.Close();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            selectall = !selectall;
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, selectall);
            }
        }
        private void setcomponentlist()
        {
            String[] doneWithSubset = {
            "Item pass fail criteria",
            "Suspension criteria and resumption requirements",
            "Test deliverables",
            "Remiaing test tasks",
            "Environmental needs",
            "Staffing and training needs",
            "Responsibilities",
            "Schedule",
            "Planning risks and contingencies",
            "Approval"};
            string end = null;
            String[] appSubset = {
            "Testing Levels",
            "Configuration Management/Change Control",
            "Test Tools",
            "Meetings",
            "Measures and Metrics"};

            foreach (string a in doneWithSubset)
            {
                if (checkedListBox1.CheckedItems.Contains(a))
                {
                    end = a;
                    break;
                }
            }
            // i is the item counter
            // e is a subset counter
            int i = 1;
            int e = 0;
            bool app = !checkedListBox1.CheckedItems.Contains("Approach");
            if (checkBox1.Checked)
            {
                // Prints all
                foreach (string a in checkedListBox1.Items)
                {
                    if (app)
                    {
                        if(appSubset.Contains(a))
                        {continue;}
                    }
                    if (a.Equals("Item pass fail criteria"))
                    {
                        e = 0;
                    }
                    if (e >0)
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
                    if (a.Equals("Approach"))
                    {
                        e = 1;
                    }
                }
            }
            // Only those checked
            else
            {
                foreach (string a in checkedListBox1.CheckedItems)
                {
                    //skips sub sections of 8 if it is not selected
                    if (app)
                    {
                        if (appSubset.Contains(a))
                        { continue; }
                    }

                    if (a.Equals(end))
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


                    if (a.Equals("Approach"))
                    {
                        e = 1;
                    }
                }
            }



        }


    }
}
/*Could we clean this up and correct the capitalizations. Also, when the form is generated, it shouldnt
 * show the components not chosen and it should be resized accordingly. 
 * This would increase the appeal of the application. Good work guys!

 */

