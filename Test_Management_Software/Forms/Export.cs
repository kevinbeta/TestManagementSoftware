using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;
using System.IO;
using System.Threading;
using System.Data.SqlServerCe;
using Test_Management_Software.Classes.Database_Utilities;
using Test_Management_Software.Classes;

namespace Test_Management_Software.Forms
{
    public partial class Export : Form
    {
        private String path;

        public Export()
        {
            InitializeComponent();
            populateProjects();

        }


        #region Private Methods

        private bool exportProject(String name)
        {
            try
            {
                if (Directory.Exists(path + "\\" + name))
                {
                    MessageBox.Show("Directory already exist");
                    return false; ;
                }

                DirectoryInfo di = Directory.CreateDirectory(path + "\\" + name);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool exportVersion(String project, String name)
        {
            try
            {
                if (Directory.Exists(path + "\\" + project + "\\" + name))
                {
                    MessageBox.Show("Directory already exist");
                    return false;
                }

                DirectoryInfo di = Directory.CreateDirectory(path + "\\" + project + "\\" + name);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// http://support.microsoft.com/kb/316384
        /// </summary>
        private void exportFileWord(String name, String[] headers, String[] data, string path)
        {
            object oMissing = System.Reflection.Missing.Value;
            object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */
            object isReadOnly = false;

            //Start Word and create a new document.
            Word._Application oWord;
            Word._Document oDoc;
            oWord = new Word.Application();
            oWord.Visible = false;
            oDoc = oWord.Documents.Add(ref oMissing, ref oMissing,
                ref oMissing, ref oMissing);

            String[] newHeaders = new String[data.Length];

            int count = 0;
            for (int i = 0; i < headers.Length; i++)
            {
                if (i % 2 == 0)
                {
                    newHeaders[count] = headers[i];
                    count++;
                }
            }

            //Insert a paragraph at the end of the document.
            Word.Paragraph oPara1;
            object oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oPara1 = oDoc.Content.Paragraphs.Add(ref oRng);
            oPara1.Range.Text = name;
            oPara1.Range.Font.Size = 20;
            oPara1.Range.Font.Bold = 1;
            oPara1.Format.SpaceAfter = 10;
            oPara1.Range.InsertParagraphAfter();

            for (int i = 0; i < newHeaders.Length; i++)
            {


                //Insert a paragraph at the end of the document.
                Word.Paragraph oPara2;
                oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                oPara2 = oDoc.Content.Paragraphs.Add(ref oRng);
                oPara2.Range.Text = newHeaders[i] + ":";
                oPara2.Range.Font.Size = 14;
                oPara2.Range.Font.Bold = 0;
                oPara2.Format.SpaceAfter = 10;
                oPara2.Range.InsertParagraphAfter();

                //Insert another paragraph.
                Word.Paragraph oPara3;
                oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;

                oPara3 = oDoc.Content.Paragraphs.Add(ref oRng);
                oPara3.Range.Font.Size = 12;
                oPara3.Range.Text = data[i];

                oPara3.Range.Font.Bold = 0;
                oPara3.Format.SpaceAfter = 24;
                oPara3.Range.InsertParagraphAfter();

            }

            //Close this form.
            object outputFileName = path + ".doc";
            if (File.Exists(outputFileName.ToString()))
            {
                if (MessageBox.Show(outputFileName.ToString() + "\nFile already exists.\nOverwrite?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    oDoc.SaveAs(ref outputFileName, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref isReadOnly, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);
            }
            else
            {
                oDoc.SaveAs(ref outputFileName, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref isReadOnly, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);
                //oDoc.SaveAs2(ref outputFileName, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref isReadOnly, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);
                //oDoc.Reload();
            }

            oDoc.Close(ref oMissing, ref oMissing, ref oMissing);
            oWord.Quit(ref oMissing, ref oMissing, ref oMissing);
        }

        private void populateProjects()
        {
            String[] projects;

            string nextProjectSqlText = "SELECT projectName, projectID FROM Project;";
            string countProjectSql = "SELECT COUNT(projectID) FROM Project";

            DBCommand nextProjectSqlCmd = DBConnection.makeCommand(nextProjectSqlText);
            DBCommand countProjectSqlCmd = DBConnection.makeCommand(countProjectSql);

            SqlCeDataReader countProjectSqlReader = countProjectSqlCmd.Start();
            countProjectSqlReader.Read();
            projects = new String[countProjectSqlReader.GetInt32(0)]; //Initializes array
            countProjectSqlCmd.Stop(); //Stops command.

            SqlCeDataReader nextProjectSqlReader = nextProjectSqlCmd.Start();

            int i = 0;
            while (nextProjectSqlReader.Read())
            {
                projects[i] = nextProjectSqlReader.GetString(0);
                i++;
            }
            nextProjectSqlCmd.Stop(); //Stops command.
            projectComboBox.Items.Add("All");
            projectComboBox.Items.AddRange(projects);
        }

        #endregion

        #region Component Methods
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            if (projectComboBox.Text != "" && versionComboBox.Text != "" && fileComboBox.Text != "")
            {
                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    path = folderBrowser.SelectedPath;

                    String[] selections = new String[4];

                    selections[0] = projectComboBox.Text;
                    selections[1] = versionComboBox.Text;
                    selections[2] = fileComboBox.Text;
                    BW_Progress_Bar.Value = 0;
                    exportBW.RunWorkerAsync(selections);

                }
            }
            else
            {
                MessageBox.Show("You must select an item from each drop down box.", "Test Management Tool", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        /// <summary>
        /// This is the method that is executed when the BackgroundWorker is activated.
        /// BackgroundWorkers make multi-threading simple and allow the user to do other
        /// things while exporting is occurring. It also provides an intuitive way to display
        /// the progress of the export to the user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exportBW_DoWork(object sender, DoWorkEventArgs e)
        {
            String[] selection = (String[])e.Argument;

            //Export everything to Word
            if (selection[0].Equals("All"))
            {
                String[] projects;

                string nextProjectSqlText = "SELECT projectName, projectID FROM Project;";
                string countProjectSql = "SELECT COUNT(projectID) FROM Project";

                DBCommand nextProjectSqlCmd = DBConnection.makeCommand(nextProjectSqlText);
                DBCommand countProjectSqlCmd = DBConnection.makeCommand(countProjectSql);

                SqlCeDataReader countProjectSqlReader = countProjectSqlCmd.Start();
                countProjectSqlReader.Read();
                projects = new String[countProjectSqlReader.GetInt32(0)]; //Initializes array
                countProjectSqlCmd.Stop(); //Stops command.

                SqlCeDataReader nextProjectSqlReader = nextProjectSqlCmd.Start();

                int i = 0;
                while (nextProjectSqlReader.Read())
                {
                    projects[i] = nextProjectSqlReader.GetString(0);
                    i++;
                }
                nextProjectSqlCmd.Stop(); //Stops command.

                for (int j = 0; j < projects.Length; j++)
                {
                    if (!exportProject("TMS_Export\\" + projects[j]))
                    {
                        e.Cancel = true;
                        break;
                    }

                    exportBW.ReportProgress((100 * (j + 1)) / projects.Length);

                    string getProjectIDText = "SELECT projectID FROM Project WHERE projectName='" + projects[j] + "';";
                    DBCommand getProjectIDCmd = DBConnection.makeCommand(getProjectIDText);
                    SqlCeDataReader getProjectIDReader = getProjectIDCmd.Start();
                    getProjectIDReader.Read();
                    int projectID = getProjectIDReader.GetInt32(0);

                    string getVersionSqlText = "SELECT versionNumber FROM Version WHERE project=" + projectID + ";";
                    DBCommand getVersionSqlCmd = DBConnection.makeCommand(getVersionSqlText);
                    SqlCeDataReader getVersionSqlReader = getVersionSqlCmd.Start();

                    while (getVersionSqlReader.Read())
                    {
                        double versionNum = (Double)getVersionSqlReader.GetSqlDouble(0);
                        exportVersion("TMS_Export\\" + projects[j], "Version " + versionNum);

                        string getVersionText = "SELECT versionID FROM Version WHERE versionNumber=" + versionNum + " AND project=" + projectID + ";";
                        DBCommand getVersionCmd = DBConnection.makeCommand(getVersionText);
                        SqlCeDataReader getVersionReader = getVersionCmd.Start();
                        getVersionReader.Read();
                        int versionID = getVersionReader.GetInt32(0);

                        string getFilesSqlText = "SELECT documentName FROM Document WHERE versionID=" + versionID + ";";
                        DBCommand getFilesSqlCmd = DBConnection.makeCommand(getFilesSqlText);
                        SqlCeDataReader getFilesSqlReader = getFilesSqlCmd.Start();
                        while (getFilesSqlReader.Read())
                        {
                            string documentName = getFilesSqlReader.GetString(0);

                            Serialization saveDocument = new Serialization();

                            string getTemplateTypeSqlText = "SELECT documentType FROM Document WHERE documentName='" + documentName + "' AND versionID=" + versionID + ";";
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
                            List<string> genericList = new List<string>(templateStorage.Componentlist);
                            string getDocSqlText = "SELECT data FROM Document, Version WHERE documentName='" + documentName + "' AND Document.versionID=" + versionID + ";";
                            DBCommand getDocSqlCmd = DBConnection.makeCommand(getDocSqlText);
                            SqlCeDataReader getDocSqlReader = getDocSqlCmd.Start();
                            String documentData = "";

                            getDocSqlReader.Read();
                            documentData += getDocSqlReader.GetString(0);

                            while (getDocSqlReader.Read())
                            {

                            }
                            DocumentStorage documentStorage = new DocumentStorage();
                            documentStorage = (DocumentStorage)saveDocument.deSerialize(documentStorage, documentData);

                            string[] controlData = documentStorage.DocumentData;

                            exportFileWord(documentName, templateStorage.Componentlist, documentStorage.DocumentData, this.path + "\\TMS_Export\\" + projects[j] + "\\Version " + versionNum + "\\" + documentName);

                        }
                    }
                }
            }
            else
            {
                //Export specific project
                string nextProjectSqlText = "SELECT projectName, projectID FROM Project WHERE projectName='" + selection[0] + "';";
                DBCommand nextProjectSqlCmd = DBConnection.makeCommand(nextProjectSqlText);
                SqlCeDataReader nextProjectSqlReader = nextProjectSqlCmd.Start();
                nextProjectSqlReader.Read();
                String project = nextProjectSqlReader.GetString(0);
                int projectID = nextProjectSqlReader.GetInt32(1);

                bool directoryExists;

                if (selection[1].Equals("All"))
                {

                    directoryExists = exportProject(project);

                    if (!directoryExists)
                    {
                        e.Cancel = true;
                    }

                    string getVersionSqlText = "SELECT versionNumber FROM Version WHERE project=" + projectID + ";";
                    DBCommand getVersionSqlCmd = DBConnection.makeCommand(getVersionSqlText);
                    SqlCeDataReader getVersionSqlReader = getVersionSqlCmd.Start();

                    string getVersionSqlCountText = "SELECT COUNT(versionID) FROM Version WHERE project=" + projectID + ";";
                    DBCommand getVersionSqlCountCmd = DBConnection.makeCommand(getVersionSqlCountText);
                    SqlCeDataReader getVersionSqlCountReader = getVersionSqlCountCmd.Start();
                    getVersionSqlCountReader.Read();
                    int numberOfVersions = getVersionSqlCountReader.GetInt32(0);
                    int count = 0;

                    while (getVersionSqlReader.Read())
                    {
                        double versionNum = (Double)getVersionSqlReader.GetSqlDouble(0);

                        if (!exportVersion(project, "Version " + versionNum))
                        {
                            e.Cancel = true;
                            break;
                        }

                        exportBW.ReportProgress((100 * (count + 1)) / numberOfVersions);
                        count++;

                        string getVersionText = "SELECT versionID FROM Version WHERE versionNumber=" + versionNum + " AND project=" + projectID + ";";
                        DBCommand getVersionCmd = DBConnection.makeCommand(getVersionText);
                        SqlCeDataReader getVersionReader = getVersionCmd.Start();
                        getVersionReader.Read();
                        int versionID = getVersionReader.GetInt32(0);

                        string getFilesSqlText = "SELECT documentName FROM Document WHERE versionID=" + versionID + ";";
                        DBCommand getFilesSqlCmd = DBConnection.makeCommand(getFilesSqlText);
                        SqlCeDataReader getFilesSqlReader = getFilesSqlCmd.Start();
                        while (getFilesSqlReader.Read())
                        {
                            string documentName = getFilesSqlReader.GetString(0);

                            Serialization saveDocument = new Serialization();

                            string getTemplateTypeSqlText = "SELECT documentType FROM Document WHERE documentName='" + documentName + "' AND versionID=" + versionID + ";";
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
                            List<string> genericList = new List<string>(templateStorage.Componentlist);
                            string getDocSqlText = "SELECT data FROM Document, Version WHERE documentName='" + documentName + "' AND Document.versionID=" + versionID + ";";
                            DBCommand getDocSqlCmd = DBConnection.makeCommand(getDocSqlText);
                            SqlCeDataReader getDocSqlReader = getDocSqlCmd.Start();
                            String documentData = "";

                            getDocSqlReader.Read();
                            documentData += getDocSqlReader.GetString(0);

                            while (getDocSqlReader.Read())
                            {

                            }
                            DocumentStorage documentStorage = new DocumentStorage();
                            documentStorage = (DocumentStorage)saveDocument.deSerialize(documentStorage, documentData);

                            string[] controlData = documentStorage.DocumentData;

                            exportFileWord(documentName, templateStorage.Componentlist, documentStorage.DocumentData, this.path + "\\" + project + "\\Version " + versionNum + "\\" + documentName);

                        }
                    }
                }
                else
                {
                    //Export specific version here
                    string getVersionSqlText = "SELECT versionNumber, versionID FROM Version WHERE project=" + projectID + " and versionNumber=" + selection[1].Substring(7) + ";";
                    DBCommand getVersionSqlCmd = DBConnection.makeCommand(getVersionSqlText);
                    SqlCeDataReader getVersionSqlReader = getVersionSqlCmd.Start();
                    getVersionSqlReader.Read();
                    Double versionNum = (Double)getVersionSqlReader.GetSqlDouble(0);
                    int versionID = getVersionSqlReader.GetInt32(1);




                    if (selection[2].Equals("All"))
                    {
                        directoryExists = exportVersion(project, "Version " + versionNum);

                        if (!directoryExists)
                        {
                            e.Cancel = true;
                        }

                        //Export all files for specific version here
                        string getFilesSqlText = "SELECT documentName FROM Document WHERE versionID=" + versionID + ";";
                        DBCommand getFilesSqlCmd = DBConnection.makeCommand(getFilesSqlText);
                        SqlCeDataReader getFilesSqlReader = getFilesSqlCmd.Start();

                        string getFileCountSqlText = "SELECT COUNT(documentID) FROM Document WHERE versionID=" + versionID + ";";
                        DBCommand getFileCountSqlCmd = DBConnection.makeCommand(getFileCountSqlText);
                        SqlCeDataReader getFileCountSqlReader = getFileCountSqlCmd.Start();
                        getFileCountSqlReader.Read();
                        int docCount = getFileCountSqlReader.GetInt32(0);
                        int count = 0;

                        while (getFilesSqlReader.Read())
                        {
                            exportBW.ReportProgress((100 * (count + 1)) / docCount);
                            count++;

                            string documentName = getFilesSqlReader.GetString(0);

                            Serialization saveDocument = new Serialization();

                            string getTemplateTypeSqlText = "SELECT documentType FROM Document WHERE documentName='" + documentName + "' AND versionID=" + versionID + ";";
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
                            List<string> genericList = new List<string>(templateStorage.Componentlist);
                            string getDocSqlText = "SELECT data FROM Document, Version WHERE documentName='" + documentName + "' AND Document.versionID=" + versionID + ";";
                            DBCommand getDocSqlCmd = DBConnection.makeCommand(getDocSqlText);
                            SqlCeDataReader getDocSqlReader = getDocSqlCmd.Start();
                            String documentData = "";

                            getDocSqlReader.Read();
                            documentData += getDocSqlReader.GetString(0);

                            while (getDocSqlReader.Read())
                            {

                            }
                            DocumentStorage documentStorage = new DocumentStorage();
                            documentStorage = (DocumentStorage)saveDocument.deSerialize(documentStorage, documentData);

                            string[] controlData = documentStorage.DocumentData;

                            exportFileWord(documentName, templateStorage.Componentlist, documentStorage.DocumentData, this.path + "\\" + project + "\\Version " + versionNum + "\\" + documentName);

                        }
                    }
                    else
                    {
                        //Export specific file here
                        //Export all files for specific version here
                        string getFileSqlText = "SELECT documentName FROM Document WHERE versionID=" + versionID + " AND documentName='" + selection[2] + "';";
                        DBCommand getFileSqlCmd = DBConnection.makeCommand(getFileSqlText);
                        SqlCeDataReader getFileSqlReader = getFileSqlCmd.Start();

                        while (getFileSqlReader.Read())
                        {

                            string documentName = getFileSqlReader.GetString(0);

                            Serialization saveDocument = new Serialization();

                            string getTemplateTypeSqlText = "SELECT documentType FROM Document WHERE documentName='" + documentName + "' AND versionID=" + versionID + ";";
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
                            List<string> genericList = new List<string>(templateStorage.Componentlist);
                            string getDocSqlText = "SELECT data FROM Document, Version WHERE documentName='" + documentName + "' AND Document.versionID=" + versionID + ";";
                            DBCommand getDocSqlCmd = DBConnection.makeCommand(getDocSqlText);
                            SqlCeDataReader getDocSqlReader = getDocSqlCmd.Start();
                            String documentData = "";

                            getDocSqlReader.Read();
                            documentData += getDocSqlReader.GetString(0);

                            while (getDocSqlReader.Read())
                            {

                            }
                            DocumentStorage documentStorage = new DocumentStorage();
                            documentStorage = (DocumentStorage)saveDocument.deSerialize(documentStorage, documentData);

                            string[] controlData = documentStorage.DocumentData;

                            exportFileWord(documentName, templateStorage.Componentlist, documentStorage.DocumentData, this.path + "\\" + documentName);

                            exportBW.ReportProgress(100);
                        }
                    }
                }
            }

        }

        private void exportBW_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            BW_Progress_Bar.Value = Math.Min(e.ProgressPercentage, 100);
            this.percentLabel.Text = e.ProgressPercentage.ToString() + "%";
        }

        private void projectComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (projectComboBox.Text.Equals("All"))
            {
                versionComboBox.Items.Add("All");
                versionComboBox.SelectedIndex = 0;
                versionComboBox.Enabled = false;
            }
            else
            {
                versionComboBox.Items.Clear();

                string getProjectIDText = "SELECT projectID FROM Project WHERE projectName='" + projectComboBox.Text + "';";
                DBCommand getProjectIDCmd = DBConnection.makeCommand(getProjectIDText);
                SqlCeDataReader getProjectIDReader = getProjectIDCmd.Start();
                getProjectIDReader.Read();
                int projectID = getProjectIDReader.GetInt32(0);

                string getVersionSqlText = "SELECT versionNumber FROM Version WHERE project=" + projectID + ";";
                DBCommand getVersionSqlCmd = DBConnection.makeCommand(getVersionSqlText);
                SqlCeDataReader getVersionSqlReader = getVersionSqlCmd.Start();

                versionComboBox.Items.Add("All");

                while (getVersionSqlReader.Read())
                {
                    versionComboBox.Items.Add("Version " + getVersionSqlReader.GetSqlDouble(0));
                }

                versionComboBox.Enabled = true;
            }

        }

        private void versionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (versionComboBox.Text.Equals("All"))
            {
                fileComboBox.Items.Add("All");
                fileComboBox.SelectedIndex = 0;
                fileComboBox.Enabled = false;
            }
            else
            {
                fileComboBox.Items.Clear();

                string getProjectIDText = "SELECT projectID FROM Project WHERE projectName='" + projectComboBox.Text + "';";
                DBCommand getProjectIDCmd = DBConnection.makeCommand(getProjectIDText);
                SqlCeDataReader getProjectIDReader = getProjectIDCmd.Start();
                getProjectIDReader.Read();
                int projectID = getProjectIDReader.GetInt32(0);

                string getVersionSqlText = "SELECT versionID FROM Version WHERE versionNumber=" + versionComboBox.Text.Substring(7) + " AND project=" + projectID + ";";
                DBCommand getVersionSqlCmd = DBConnection.makeCommand(getVersionSqlText);
                SqlCeDataReader getVersionSqlReader = getVersionSqlCmd.Start();
                getVersionSqlReader.Read();
                int versionID = getVersionSqlReader.GetInt32(0);

                string getFilesSqlText = "SELECT documentName FROM Document WHERE versionID=" + versionID + ";";
                DBCommand getFilesSqlCmd = DBConnection.makeCommand(getFilesSqlText);
                SqlCeDataReader getFilesSqlReader = getFilesSqlCmd.Start();

                fileComboBox.Items.Add("All");

                while (getFilesSqlReader.Read())
                {
                    fileComboBox.Items.Add(getFilesSqlReader.GetString(0));
                }

                fileComboBox.Enabled = true;
            }
        }

        private void exportBW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                MessageBox.Show("Export Successful", "Test Management Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion
    }
}
