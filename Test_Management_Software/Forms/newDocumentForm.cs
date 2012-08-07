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

namespace Test_Management_Software
{
    public partial class newDocumentForm : Form
    {
        private String[] componentItems = { "Test Case Specification", "Test Design Specification", "Test Incident Report", "Test Item Transmittal Report","Test Log","Test Plan","Test Procedure","Test Summary Report", "Custom"};
        private List<string> componentsList = new List<string>();
        private MainForm parentForm;
        public newDocumentForm(MainForm parentForm)
        {
            InitializeComponent();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.parentForm = parentForm;
            nameTextBox.MaxLength = 50;
            descriptionTextBox.MaxLength = 255;

            //Populates combobox
            for (int i = 0; i < componentItems.Length; i++)
            {
                comboBox1.Items.Add(componentItems[i]);
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            String temp1 = nameTextBox.Text.Trim();
            String temp2 = descriptionTextBox.Text.Trim();
            if (!checkDB(temp1))
            {
                if (temp1 != "")
                {
                    switch (comboBox1.Text)
                    {
                        case "Test Case Specification":

                            TestCaseSpecificationWizard tcsw = new TestCaseSpecificationWizard(temp1, temp2, parentForm);
                            tcsw.Show();
                            this.Close();
                            break;
                        case "Test Design Specification":

                            TestDesignWizard tdw = new TestDesignWizard(temp1, temp2, parentForm);
                            tdw.Show();
                            this.Close();
                            break;
                        case "Test Incident Report":
                            TestIncidentReportWizard tir = new TestIncidentReportWizard(temp1, temp2, parentForm);
                            tir.Show();
                            this.Close();
                            break;
                        case "Test Item Transmittal Report":
                            TestItemTransmittalReportWizard titrw = new TestItemTransmittalReportWizard(temp1, temp2, parentForm);
                            titrw.Show();
                            this.Close();
                            break;
                        case "Test Log":
                            TestLogWizard tlw = new TestLogWizard(temp1, temp2, parentForm);
                            tlw.Show();
                            this.Close();
                            break;
                        case "Test Plan":

                            TestPlanWizard tpw = new TestPlanWizard(temp1, temp2, parentForm);
                            tpw.Show();
                            this.Close();

                            break;
                        case "Test Procedure":
                            TestProcedureWizard tprow = new TestProcedureWizard(temp1, temp2, parentForm);
                            tprow.Show();
                            this.Close();
                            break;
                        case "Test Summary Report":
                            TestSummaryReportWizard tsrw = new TestSummaryReportWizard(temp1, temp2, parentForm);
                            tsrw.Show();
                            this.Close();
                            break;
                        case "Custom":
                            CustomWizard cw = new CustomWizard(temp1, temp2, parentForm);
                            cw.Show();
                            this.Close();
                            break;

                    }
                }
                else
                {
                    MessageBox.Show("You must enter a name.", "Test Management Tool", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else { MessageBox.Show("Document name already exists.", "Test Management Tool", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
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
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
