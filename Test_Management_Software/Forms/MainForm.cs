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
using Test_Management_Software.Classes;
using Test_Management_Software.Forms;
using Test_Management_Software.Classes.Database_Utilities;


namespace Test_Management_Software
{
    /// <summary>
    /// Stable Version due March 28th.
    /// Coded by Matthew Mills.
    /// </summary>
    public partial class MainForm : Form
    {
        List<String> projects = new List<String>();
        List<Document> documents = new List<Document>();
        TemplateStorage templateStorage;
        private int[] projectID;
        private bool newDocument = true;
        private int documentID;
        private ImageList imageList = new ImageList();


        public MainForm()
        {
            InitializeComponent();
            searchToolStripTextBox.MaxLength = 50;
            populateProjectTreeView();
            populateTemplateListView();
        }

        #region Component Methods
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBoxForm aboutForm = new AboutBoxForm();
            aboutForm.Show();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            useTemplateToProject();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            clearTemplateData();
        }

        private void exampleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainFormTab.SelectTab(1);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void indexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IndexForm indexForm = new IndexForm();
            indexForm.Show();
        }

        private void newProjectToolStripButton1_Click(object sender, EventArgs e)
        {
            NewProjectForm projectForm = new NewProjectForm();
            projectForm.ShowDialog();
            populateProjectTreeView();
        }

        private void newTemplateToolStripButton_Click(object sender, EventArgs e)
        {
            //NewTemplateForm templateForm = new NewTemplateForm();
            newDocumentForm documentForm = new newDocumentForm(this);
            //templateForm.Show();
            documentForm.Show();
        }

        private void deleteTemplateToolStripButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete the selected template?", "Test Management Tool", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                deleteDocumentTemplate();
            }

        }

        private void newTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //NewTemplateForm templateForm = new NewTemplateForm();
            newDocumentForm documentForm = new newDocumentForm(this);
            //templateForm.Show();
            documentForm.Show();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewProjectForm projectForm = new NewProjectForm();
            projectForm.ShowDialog();
            populateProjectTreeView();
        }

        private void searchToolStripButton_Click(object sender, EventArgs e)
        {
            String temp = searchToolStripTextBox.Text.Trim();
            searchTemplates(temp);
        }

        private void statToolStripMenuItem_Click(object sender, EventArgs e)
        {
            generateStatistics();
        }
        private void MainForm_Resize(object sender, EventArgs e)
        {
            searchToolStripTextBox.Width = templateListView.Width - 310;
            templateListView.Columns[1].Width = templateListView.Width;
        }

        private void newVersionToolStripButton_Click(object sender, EventArgs e)
        {
            addVersionToProject();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
           
        }

        private void removeTemplateButton_Click(object sender, EventArgs e)
        {
            exampleOutputPanel.Controls.Clear();
            outputPanel.Controls.Clear();
            templateControlToolStrip.Enabled = false;
        }

        private void useTemplateButton_Click(object sender, EventArgs e)
        {
            exampleOutputPanel.Controls.Clear();
            if (templateListView.SelectedItems.Count > 0)
            {
                newDocument = true;
                string nextSqlText = "SELECT templateData FROM Template WHERE templateName='" + templateListView.SelectedItems[0].Text + "';";
                DBCommand nextSqlCmd = DBConnection.makeCommand(nextSqlText);
                SqlCeDataReader nextSqlReader = nextSqlCmd.Start();
                String input = "";
                while (nextSqlReader.Read())
                {
                    input += nextSqlReader.GetString(0);
                }

                nextSqlCmd.Stop(); //Stops command.
                //Testing the deSerialize method.
                TemplateStorage templateStorage = new TemplateStorage();
                Serialization saveDocument = new Serialization();
                templateStorage = (TemplateStorage)saveDocument.deSerialize(templateStorage, input);
                this.templateStorage = templateStorage;
                List<string> genericList = new List<string>(templateStorage.Componentlist);

                DocumentFactory df = new DocumentFactory();
                outputPanel.Controls.Clear();
                outputPanel.Controls.Add(df.createDocument(templateStorage.Name, templateStorage.Description, genericList));
                templateControlToolStrip.Enabled = true;


                string next1SqlText = "SELECT documentID FROM Document WHERE documentType='" + templateListView.SelectedItems[0].Text + "';";
                DBCommand next1SqlCmd = DBConnection.makeCommand(next1SqlText);
                SqlCeDataReader next1SqlReader = next1SqlCmd.Start();
                int input1=0;
                if (next1SqlReader.Read())
                {
                    input1 = next1SqlReader.GetInt32(0);
                }
                                nextSqlCmd.Stop(); //Stops command.
                if (input1 == 0)
                {
                    Label noTemp = new Label();
                    noTemp.AutoSize = true;
                    noTemp.Text = "The Template has not been used.";
                    exampleOutputPanel.Controls.Add(noTemp);
                }
                else
                {
                    getExample(input1, templateListView.SelectedItems[0].Text);
                }
            }
        }

        private void clearTemplateButton_Click(object sender, EventArgs e)
        {
            //TODO: Reload current template to clear the form.
        }

        private void deleteToolStripButton_Click(object sender, EventArgs e)
        {
            deleteFromProjectTreeView();

        }

        private void clearTemplateButton_Click_1(object sender, EventArgs e)
        {
            List<string> genericList = new List<string>(templateStorage.Componentlist);
            DocumentFactory df = new DocumentFactory();
            outputPanel.Controls.Clear();
            outputPanel.Controls.Add(df.createDocument(templateStorage.Name, templateStorage.Version, genericList));
        }
        private void submitDocument_Click(object sender, EventArgs e)
        {
            if (newDocument)
            {
                submitDataToDB();
            }
            else
            {
                updateDataToDB();
            }
        }

        private void projectTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            getTemplate();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Test Management Tool", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }
        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Export exportForm = new Export();
            exportForm.Show();
        }

        #endregion

        #region Private Methods
        private void deleteDocumentTemplate()
        {
            try
            {
                //string deleteTemplateSqlText = "DELETE FROM Template WHERE templateName='" + templateListView.SelectedItems[0].Text + "';";
                string deleteTemplateSqlText = "UPDATE Template SET enabled=" + 0 + " WHERE templateName='" + templateListView.SelectedItems[0].Text + "';";
                DBCommand deleteTemplateSqlCmd = DBConnection.makeCommand(deleteTemplateSqlText);
                deleteTemplateSqlCmd.RunNoReturnQuery();
                populateTemplateListView();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void deleteFromProjectTreeView()
        {
            try
            {
                int selectedIndex = projectTreeView.SelectedNode.Level;

                //Project
                if (selectedIndex == 0)
                {
                    if (MessageBox.Show("Are you sure you want to delete Project " + projectTreeView.SelectedNode.Text + "?", "Test Management Tool", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        string deleteProjectSqlText = "DELETE FROM Project WHERE projectName='" + projectTreeView.SelectedNode.Text + "';";
                        DBCommand deleteProjectSqlCmd = DBConnection.makeCommand(deleteProjectSqlText);
                        deleteProjectSqlCmd.RunNoReturnQuery();

                        populateProjectTreeView();
                    }

                }
                //Version
                else if (selectedIndex == 1)
                {
                    if (MessageBox.Show("Are you sure you want to delete " + projectTreeView.SelectedNode.Text + "?", "Test Management Tool", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        string getProjectIDSql = "SELECT projectID FROM Project WHERE projectName='" + projectTreeView.SelectedNode.Parent.Text + "';";
                        DBCommand getProjectIDSqlCmd = DBConnection.makeCommand(getProjectIDSql);
                        SqlCeDataReader getProjectIDSqlReader = getProjectIDSqlCmd.Start();
                        getProjectIDSqlReader.Read();
                        int projectID = getProjectIDSqlReader.GetInt32(0);

                        string deleteVersionSqlText = "DELETE FROM Version WHERE versionNumber=" + projectTreeView.SelectedNode.Text.Substring(7) + " AND project = " + projectID + ";";
                        DBCommand deleteVersionSqlCmd = DBConnection.makeCommand(deleteVersionSqlText);
                        deleteVersionSqlCmd.RunNoReturnQuery();

                        populateProjectTreeView();
                    }
                }
                //Document folder
                else if (selectedIndex == 2)
                {
                    //TODO: Delete all files?
                }
                //File
                else if (selectedIndex == 3)
                {
                    if (MessageBox.Show("Are you sure you want to delete File " + projectTreeView.SelectedNode.Text + "?", "Test Management Tool", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        string getProjectIDSql = "SELECT projectID FROM Project WHERE projectName='" + projectTreeView.SelectedNode.Parent.Parent.Parent.Text + "';";
                        DBCommand getProjectIDSqlCmd = DBConnection.makeCommand(getProjectIDSql);
                        SqlCeDataReader getProjectIDSqlReader = getProjectIDSqlCmd.Start();
                        getProjectIDSqlReader.Read();
                        int projectID = getProjectIDSqlReader.GetInt32(0);

                        string getVersionIDSql = "SELECT versionID FROM Version WHERE versionNumber='" + projectTreeView.SelectedNode.Parent.Parent.Text.Substring(7) + "' AND project = " + projectID + ";";
                        DBCommand getVersionIDSqlCmd = DBConnection.makeCommand(getVersionIDSql);
                        SqlCeDataReader getVersionIDSqlReader = getVersionIDSqlCmd.Start();
                        getVersionIDSqlReader.Read();
                        int versionID = getVersionIDSqlReader.GetInt32(0);

                        string deleteDocumentSqlText = "DELETE FROM Document WHERE versionID=" + versionID + ";";
                        DBCommand deleteDocumentSqlCmd = DBConnection.makeCommand(deleteDocumentSqlText);
                        deleteDocumentSqlCmd.RunNoReturnQuery();

                        populateProjectTreeView();
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void populateProjectTreeView()
        {
            try
            {
                
                projectTreeView.Nodes.Clear();

                String[] projects;

                string nextProjectSqlText = "SELECT projectName, projectID FROM Project;";
                string countProjectSql = "SELECT COUNT(projectID) FROM Project";

                DBCommand nextProjectSqlCmd = DBConnection.makeCommand(nextProjectSqlText);
                DBCommand countProjectSqlCmd = DBConnection.makeCommand(countProjectSql);

                SqlCeDataReader countProjectSqlReader = countProjectSqlCmd.Start();
                countProjectSqlReader.Read();
                projects = new String[countProjectSqlReader.GetInt32(0)]; //Initializes array
                projectID = new int[countProjectSqlReader.GetInt32(0)];
                countProjectSqlCmd.Stop(); //Stops command.

                SqlCeDataReader nextProjectSqlReader = nextProjectSqlCmd.Start();

                int i = 0;
                while (nextProjectSqlReader.Read())
                {
                    projects[i] = nextProjectSqlReader.GetString(0);
                    projectID[i] = nextProjectSqlReader.GetInt32(1);
                    i++;

                }
                nextProjectSqlCmd.Stop(); //Stops command.

                TreeNode[] projectNodes = new TreeNode[projects.Length];

                /*Adds Versions to the Projects.
                 TODO: Add Test data to each version.  
                 */
                string nextVersionSqlText = "SELECT versionNumber FROM Version, Project WHERE Version.project = Project.projectID;";
                DBCommand nextVersionSqlCmd = DBConnection.makeCommand(nextVersionSqlText);
                SqlCeDataReader nextVersionSqlReader = nextVersionSqlCmd.Start();

                for (int j = 0; j < projects.Length; j++)
                {
                    projectNodes[j] = new TreeNode(projects[j]);

                    //Counts the number of versions to iterate the loop below
                    string countVersionSqlText = "SELECT COUNT(project) FROM Version WHERE project='" + projectID[j] + "';";
                    DBCommand countVersionSqlCmd = DBConnection.makeCommand(countVersionSqlText);
                    SqlCeDataReader countVersionSqlReader = countVersionSqlCmd.Start();
                    countVersionSqlReader.Read();
                    int numberOfVersions = countVersionSqlReader.GetInt32(0);

                    //Populates each project with version and each verison with data according to the DB
                    for (int l = numberOfVersions; l > 0; l--)
                    {
                        if (nextVersionSqlReader.Read())
                        {
                            double versionNumber = (double)nextVersionSqlReader.GetSqlDouble(0);
                            String tempVersionString = "Version " + versionNumber;
                            TreeNode versionNode = new TreeNode(tempVersionString);
                            versionNode.ImageIndex = 1;
                            versionNode.SelectedImageIndex = 1;
                            projectNodes[j].Nodes.Add(versionNode);

                            TreeNode documentsNode = new TreeNode("Documents");
                            documentsNode.ImageIndex = 2;
                            documentsNode.SelectedImageIndex = 2;
                            versionNode.Nodes.Add(documentsNode);

                            String getVersionIDsSqlText = "SELECT versionID FROM Version, Project WHERE Version.versionNumber=" + versionNumber + " AND Version.project=" + projectID[j] + ";";
                            DBCommand getVersionIDsSqlCmd = DBConnection.makeCommand(getVersionIDsSqlText);
                            SqlCeDataReader getVersionIDsReader = getVersionIDsSqlCmd.Start();
                            getVersionIDsReader.Read();
                            int versionIDs = getVersionIDsReader.GetInt32(0);

                            string getDocSqlText = "SELECT DISTINCT documentName FROM Document, Version WHERE Document.versionID=" + versionIDs + " AND Version.project='" + projectID[j] + "';";
                            DBCommand getDocSqlCmd = DBConnection.makeCommand(getDocSqlText);
                            SqlCeDataReader getDocSqlReader = getDocSqlCmd.Start();

                            while (getDocSqlReader.Read())
                            {
                                string documentName = getDocSqlReader.GetString(0);
                                documentsNode.Nodes.Add(documentName, documentName, 3, 3);
                            }

                            tempVersionString = "";
                        }
                    }
                }
                nextVersionSqlCmd.Stop();
                projectTreeView.Nodes.AddRange(projectNodes);
                this.projects.Clear();
                this.projects.AddRange(projects);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void refreshAll()
        {
            populateTemplateListView();
        }

        /// <summary>
        /// This method populates the template list box located on the
        /// MainForm. It will read the information inside the template table
        /// from the database and use this information to populate accordingly.
        /// </summary>
        private void populateTemplateListView()
        {
            try
            {
                templateListView.Items.Clear();

                String[] templates;
                String[] templateDescriptions;

                string nextSqlText = "SELECT templateName, templateDescription, dateCreated  FROM Template WHERE enabled=1";
                string countSql = "SELECT COUNT(templateID) FROM Template";

                DBCommand nextSqlCmd = DBConnection.makeCommand(nextSqlText);
                DBCommand countSqlCmd = DBConnection.makeCommand(countSql);

                SqlCeDataReader countSqlReader = countSqlCmd.Start();
                countSqlReader.Read();
                templates = new String[countSqlReader.GetInt32(0)]; //Initializes array
                templateDescriptions = new String[countSqlReader.GetInt32(0)];
                countSqlCmd.Stop(); //Stops command.

                SqlCeDataReader nextSqlReader = nextSqlCmd.Start();

                int i = 0;
                while (nextSqlReader.Read())
                {
                    templates[i] = nextSqlReader.GetString(0);
                    templateDescriptions[i] = nextSqlReader.GetString(1);
                    i++;
                }
                nextSqlCmd.Stop(); //Stops command.

                //Set up list view
                templateListView.View = View.Details;

                ListViewItem[] items = new ListViewItem[templates.Length];
                for (int j = 0; j < templates.Length; j++)
                {
                    if (templates[j] != null)
                    {
                        items[j] = new ListViewItem();
                        items[j].Text = templates[j];
                        items[j].SubItems.Add(templateDescriptions[j]);
                    }
                }

                for (int j = 0; j < items.Length; j++)
                {
                    if (items[j] != null)
                    {
                        templateListView.Items.Add(items[j]);
                    }
                }
                //templateListView.Items.AddRange(items);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }

        private void searchTemplates(String key)
        {
            if (key != "")
            {
                ListViewItem[] items = new ListViewItem[templateListView.Items.Count];

                for (int i = 0; i < items.Length; i++)
                {
                    if (ContainsCaseInsensitive(templateListView.Items[i].Text, key))
                    {
                        items[i] = templateListView.Items[i];
                    }
                    if (ContainsCaseInsensitive(templateListView.Items[i].SubItems[1].Text, key))
                    {
                        items[i] = templateListView.Items[i];
                    }
                    
                }
                templateListView.Items.Clear();
                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i] != null)
                    {
                        templateListView.Items.Add(items[i]);
                    }
                }
            }
            else
            {
                populateTemplateListView();
            }

        }
        private bool ContainsCaseInsensitive(string source, string find)
        {
            return source.IndexOf(find, StringComparison.CurrentCultureIgnoreCase) != -1;
        }

        private void clearTemplateData()
        {
            //TODO: Functionality
        }
        private void generateStatistics()
        {
            //TODO: Generate statistics based on test data. Possibly making a statistics class to manage the statistic.
           // StatisticsForm statForm = new StatisticsForm();
           // statForm.ShowDialog();

        }
        private void useTemplateToProject()
        {
            //TODO: Functionality
        }

        private void addVersionToProject()
        {
            String[] projects = this.projects.ToArray();
            NewVersion version = new NewVersion(projects);
            version.ShowDialog();
            populateProjectTreeView();
            version.Dispose();

        }

        private void submitDataToDB()
        {

            string[] controlData = new string[outputPanel.Controls[0].Controls[2].Controls.Count / 2];
            int counter = 0;
            for (int i = 0; i < outputPanel.Controls[0].Controls[2].Controls.Count; i++)
            {
                if (i % 2 != 0)
                {
                    controlData[counter] = outputPanel.Controls[0].Controls[2].Controls[i].Text;
                    counter++;
                }
            }

            SubmitForm submitForm = new SubmitForm(projects, controlData, templateStorage.Name, projectID);

            if (submitForm.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Data Synchronization Successful", "Test Management Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                populateProjectTreeView();
            }
        }

        private void updateDataToDB()
        {
            string[] controlData = new string[outputPanel.Controls[0].Controls[2].Controls.Count / 2];
            int counter = 0;
            for (int i = 0; i < outputPanel.Controls[0].Controls[2].Controls.Count; i++)
            {
                if (i % 2 != 0)
                {
                    controlData[counter] = outputPanel.Controls[0].Controls[2].Controls[i].Text;
                    counter++;
                }
            }

            Serialization saveString = new Serialization();
            DocumentStorage doc = new DocumentStorage("", "", controlData);
            string data = saveString.serialize(doc);
            string updateSqlText = "UPDATE Document SET data='" + data + "' WHERE documentID =" + documentID + ";";
            DBCommand updateSqlCmd = DBConnection.makeCommand(updateSqlText);
            updateSqlCmd.RunNoReturnQuery();
            MessageBox.Show("Update Data Synchronization Successful", "Test Management Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        /// <summary>
        /// Gets the document's data and the associated template and then generates the document for the user to view.
        /// </summary>
        private void getTemplate()
        {
            if (projectTreeView.SelectedNode.Nodes.Count == 0 && projectTreeView.SelectedNode.Text != "Documents" && projectTreeView.SelectedNode.Level != 0)
            {
                newDocument = false;
                Serialization saveDocument = new Serialization();

                string getVersionIDText = "SELECT Version.versionID, Project.projectID FROM Version, Project WHERE Version.versionNumber=" + projectTreeView.SelectedNode.Parent.Parent.Text.Substring(7) + " AND Project.projectName='" + projectTreeView.SelectedNode.Parent.Parent.Parent.Text + "' AND Version.project = Project.projectID;";
                DBCommand getVersionIDCmd = DBConnection.makeCommand(getVersionIDText);
                SqlCeDataReader getVersionIDReader = getVersionIDCmd.Start();
                int versionID;
                int projectID;
                getVersionIDReader.Read();
                versionID = getVersionIDReader.GetInt32(0);
                projectID = getVersionIDReader.GetInt32(1);

                string getTemplateTypeSqlText = "SELECT documentType FROM Document WHERE documentName='" + projectTreeView.SelectedNode.Text + "' AND versionID=" + versionID + ";";
                DBCommand getTemplateTypeSqlCmd = DBConnection.makeCommand(getTemplateTypeSqlText);
                SqlCeDataReader getTemplateTypeSqlReader = getTemplateTypeSqlCmd.Start();
                String templateType = "";
                while (getTemplateTypeSqlReader.Read())
                {
                    templateType += getTemplateTypeSqlReader.GetString(0);
                }
                


                string getTemplateSqlText = "SELECT templateData FROM Template WHERE templateName='" + templateType + "';";
                DBCommand getTemplateSqlCmd = DBConnection.makeCommand(getTemplateSqlText);
                SqlCeDataReader getTemplateSqlReader = getTemplateSqlCmd.Start();
                String templateInput = "";
                while (getTemplateSqlReader.Read())
                {
                    templateInput += getTemplateSqlReader.GetString(0);
                }
                //Testing the deSerialize method.
                TemplateStorage templateStorage = new TemplateStorage();
                templateStorage = (TemplateStorage)saveDocument.deSerialize(templateStorage, templateInput);
                this.templateStorage = templateStorage;
                List<string> genericList = new List<string>(templateStorage.Componentlist);

                DocumentFactory df = new DocumentFactory();
                outputPanel.Controls.Clear();
                outputPanel.Controls.Add(df.createDocument(templateStorage.Name, templateStorage.Version, genericList));
                templateControlToolStrip.Enabled = true;

                string getDocSqlText = "SELECT documentID, data FROM Document, Version WHERE documentName='" + projectTreeView.SelectedNode.Text + "' AND Document.versionID=" + versionID + ";";
                DBCommand getDocSqlCmd = DBConnection.makeCommand(getDocSqlText);
                SqlCeDataReader getDocSqlReader = getDocSqlCmd.Start();
                String documentData = "";

                getDocSqlReader.Read();
                this.documentID = (int)getDocSqlReader.GetSqlInt32(0);
                documentData += getDocSqlReader.GetString(1);

                while (getDocSqlReader.Read())
                {

                }
                DocumentStorage documentStorage = new DocumentStorage();
                documentStorage = (DocumentStorage)saveDocument.deSerialize(documentStorage, documentData);

                string[] controlData = documentStorage.DocumentData;
                if (controlData != null)
                {
                    int counter = 0;
                    for (int i = 0; i < outputPanel.Controls[0].Controls[2].Controls.Count; i++)
                    {
                        if (i % 2 != 0)
                        {
                            outputPanel.Controls[0].Controls[2].Controls[i].Text = controlData[counter];
                            counter++;
                        }
                    }
                }
            }
        }
        private void getExample(int documentID, String docuType)
        {

                Serialization saveDocument = new Serialization();


                string getTemplateSqlText = "SELECT templateData FROM Template WHERE templateName='" + docuType + "';";
                DBCommand getTemplateSqlCmd = DBConnection.makeCommand(getTemplateSqlText);
                SqlCeDataReader getTemplateSqlReader = getTemplateSqlCmd.Start();
                String templateInput = "";
                while (getTemplateSqlReader.Read())
                {
                    templateInput += getTemplateSqlReader.GetString(0);
                }
                //Testing the deSerialize method.
                TemplateStorage templateStorage = new TemplateStorage();
                templateStorage = (TemplateStorage)saveDocument.deSerialize(templateStorage, templateInput);
                this.templateStorage = templateStorage;
                List<string> genericList = new List<string>(templateStorage.Componentlist);

                DocumentFactory df = new DocumentFactory();
                exampleOutputPanel.Controls.Clear();
                exampleOutputPanel.Controls.Add(df.createDocument(templateStorage.Name, templateStorage.Version, genericList));
                
                string getDocSqlText = "SELECT data FROM Document WHERE documentID='" + documentID + "';";
                DBCommand getDocSqlCmd = DBConnection.makeCommand(getDocSqlText);
                SqlCeDataReader getDocSqlReader = getDocSqlCmd.Start();
                String documentData = "";

                getDocSqlReader.Read();
                documentData += getDocSqlReader.GetString(0);

                DocumentStorage documentStorage = new DocumentStorage();
                documentStorage = (DocumentStorage)saveDocument.deSerialize(documentStorage, documentData);

                string[] controlData = documentStorage.DocumentData;
                if (controlData != null)
                {
                    int counter = 0;
                    for (int i = 0; i < outputPanel.Controls[0].Controls[2].Controls.Count; i++)
                    {
                        if (i % 2 != 0)
                        {
                            exampleOutputPanel.Controls[0].Controls[2].Controls[i].Text = controlData[counter];
                            counter++;
                        }
                    }
                }

        }
        #endregion

    }
}
