using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlServerCe;

namespace Test_Management_Software
{
    public partial class IndexForm : Form
    {
        /// <summary>
        /// Form contains terminology and other need-to-know
        /// information for software testers and developers.
        /// These terms were derived from:
        /// http://www.aptest.com/glossary.html
        /// Coded by Matthew Mills.
        /// </summary>
        private String[] terms;

        public IndexForm()
        {
            InitializeComponent();
            searchToolStripTextBox.MaxLength = 50;
            populateIndex(); //Initializes Variables
        }

        #region Private Methods
        private void populateIndex()
        {
            ///
            /// Populates the left panel to display each term correctly.
            ///
            string nextSqlText = "SELECT definitionName FROM definitions";
            string countSql = "SELECT COUNT(definitionName) FROM definitions";

            DBCommand nextSqlCmd = DBConnection.makeCommand(nextSqlText);
            DBCommand countSqlCmd = DBConnection.makeCommand(countSql);

            SqlCeDataReader countSqlReader = countSqlCmd.Start();
            countSqlReader.Read();
            terms = new String[countSqlReader.GetInt32(0)]; //Initializes array
            countSqlCmd.Stop(); //Stops command.

            SqlCeDataReader nextSqlReader = nextSqlCmd.Start();

            int i = 0;
            while (nextSqlReader.Read())
            {
                terms[i] = nextSqlReader.GetString(0);
                i++;
            }
            nextSqlCmd.Stop(); //Stops command.
            termsListBox.Items.Clear();
            termsListBox.Items.AddRange(terms); //Populates Listbox.
        }

        private void populateIndex(String[] filteredTerms)
        {
            termsListBox.Items.Clear();
            termsListBox.Items.AddRange(filteredTerms); //Populates Listbox.
        }
        private void displayDefinition(String key)
        {
            ///
            /// Populates outputTextArea according to the selected term.
            ///
            string returnSqlText = "SELECT definitionName, definitionDescription FROM definitions WHERE definitionName = '" + key + "'";
            DBCommand returnSqlCmd = DBConnection.makeCommand(returnSqlText);
            SqlCeDataReader returnSqlReader = returnSqlCmd.Start();
            returnSqlReader.Read();
            outputTextBox.Text = returnSqlReader.GetString(1);
            returnSqlCmd.Stop(); //Stops command.
        }

        /// <summary>
        /// Simple search functionality. Should suffice for now.
        /// </summary>
        /// <param name="key"></param>
        private void searchIndex(String key)
        {
           
            if (key.Equals(""))
            {
                populateIndex();
            }
            else
            {
                string nextSqlText = "SELECT definitionName, definitionDescription FROM definitions";
                DBCommand nextSqlCmd = DBConnection.makeCommand(nextSqlText);
                SqlCeDataReader nextSqlReader = nextSqlCmd.Start();
                String[] filteredTerms;
                ArrayList tempArray = new ArrayList();
                while (nextSqlReader.Read())
                {
                    //This new search method is NOT case sensitive.
                    if (ContainsCaseInsensitive(nextSqlReader.GetString(0), key) || ContainsCaseInsensitive(nextSqlReader.GetString(1), key))
                    {
                        tempArray.Add(nextSqlReader.GetString(0));
                    }

                }
                nextSqlCmd.Stop(); //Stops command.
                filteredTerms = (String[])tempArray.ToArray(typeof(String));
                if (filteredTerms.Length != 0)
                {
                    populateIndex(filteredTerms);
                }
                else
                {
                    MessageBox.Show("No items match your search.",
                         "Search Results",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Exclamation,
                          MessageBoxDefaultButton.Button1);
                }
            }
        }

        private bool ContainsCaseInsensitive(string source, string find)
        {
            return source.IndexOf(find, StringComparison.CurrentCultureIgnoreCase) != -1;
        }

        #endregion

        #region Component Methods
        private void searchToolStripButton_Click(object sender, EventArgs e)
        {
            String temp = searchToolStripTextBox.Text.Trim();
            searchIndex(temp);
        }

        private void IndexForm_Resize(object sender, EventArgs e)
        {
            searchToolStripTextBox.Width = this.Width - 100;
        }

        private void termsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            displayDefinition(termsListBox.SelectedItem.ToString());
        }
        
        private void searchToolStripTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                searchToolStripButton.PerformClick();
            }
        }
        #endregion
    }
}
