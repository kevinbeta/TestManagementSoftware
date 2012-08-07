using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlServerCe;

namespace Test_Management_Software.Classes
{
		class Project: System.Object {
            List<Version> versions;

            private String projectName;

			public Project(String name) 
            {
				this.name = name;
                projectName = name;
                versions = new List<Version>();
                versions.Add(new Version(1));

                if (!makeProject())
                {
                    MessageBox.Show("Project creation failed");
                }

			}
			private String name = null;

			private bool makeProject() 
            {
                try
                {
                    string nextProjectSqlText = "INSERT INTO Project (projectName, dateCreated) VALUES ('" + projectName + "', '" + DateTime.Now.Date + "');";

                    DBCommand nextProjectSqlCmd = DBConnection.makeCommand(nextProjectSqlText);

                    nextProjectSqlCmd.RunNoReturnQuery();

                    return true;
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                    return false;
                }
			}

            public void addVersion(int vnum)
            {
                versions.Add(new Version(vnum));
            }
		}
}
