using System;
using System.Collections.Generic;
using System.Collections;
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
    public partial class CustomWizard : Form
    {
        private DocumentFactory df = new DocumentFactory();
        private List<string> componentsList = new List<string>();
        private String[] componentItems = { "Textbox", "TextArea"};
        int count = 1;
        private string name;
        private string description;
        private string version;
        private MainForm parentForm;

        public CustomWizard(string name, string desc, MainForm parentForm)
        {

            this.parentForm = parentForm;
            this.name = name;
            this.description = desc;
            InitializeComponent();
            preveiwPanel.AutoScroll = true;
            componentsComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            textBox1.MaxLength = 255;
            //Populates combobox
            for (int i = 0; i < componentItems.Length; i++)
            {
                componentsComboBox.Items.Add(componentItems[i]);
            }
        }



        private void createDocumentReview()
        {

            //populate so user can input necessary data
            DocumentDisplayForm ddf = new DocumentDisplayForm(df.createDocument(name, description, componentsList));
            if (ddf.ShowDialog() == DialogResult.OK)
            {
                //Serializes the newly created document. This should be added to each Wizard.
                TemplateStorage document = new TemplateStorage(name, version, description, componentsList);
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
                this.componentsList.Clear();
            }
        }

        private void finishButton_Click(object sender, EventArgs e)
        {
            createDocumentReview();
            this.Close();
        }


        private void nextButton_Click(object sender, EventArgs e)
        {
            if (count < 50)
            {

                //TODO: Add component to componentsList and clear fields for another component
                string temp = textBox1.Text.Trim();
                string temp2 = componentsComboBox.Text.Trim();
                //checks the contents of the boxes for valid entries
                if (temp.Length > 0 && temp2.Length > 0)
                {
                    componentsList.Add(count + ". " + temp);
                    componentsList.Add(temp2);
                    Console.WriteLine(temp);
                    Console.WriteLine(temp2);
                    count++;
                }
                else
                {
                    //Throw Error Message
                    MessageBox.Show("Both the Conponent Name and Component Type fields must not be blank");
                }
                textBox1.Clear();
                preveiwPanel.Controls.Clear();
                preveiwPanel.Controls.Add(df.createDocument(name, description, componentsList));
            }
            else
            {
                MessageBox.Show("Max Components have been added");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (count > 1)
            {
                componentsList.RemoveAt(componentsList.Count - 1);
                componentsList.RemoveAt(componentsList.Count - 1);
                count--;
            }
            preveiwPanel.Controls.Clear();
            preveiwPanel.Controls.Add(df.createDocument(name, version, componentsList));
        }


    }
}
