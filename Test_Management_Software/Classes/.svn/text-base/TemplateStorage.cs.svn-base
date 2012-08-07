using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Test_Management_Software.Classes;
using System.Data.OleDb;
using System.Data.SqlServerCe;
using System.Windows.Forms;

namespace Test_Management_Software
{
    /// <summary>
    /// This class is used to serialize the template into a XML string. This string
    /// will then be stored into the database with its according template. Later it will
    /// be recalled to regenerate the template.
    /// </summary>
    [Serializable]
		public class TemplateStorage {

            private string name;   
            private string version;
            private string description;
            private String[] componentlist;

            public TemplateStorage()
            {
            }
			public TemplateStorage(string name, string version, string description, List<string> list) {
                this.name = name;
                this.version = version;
                this.componentlist = list.ToArray();
                this.description = description;
                
			}

            public string Name
            {
                get { return name; }
                set { name = value; }
            }

            public string Version
            {
                get { return version; }
                set { version = value; }
            }

            public String[] Componentlist
            {
                get { return componentlist; }
                set { componentlist = value; }
            }

            public string Description
            {
                get { return description; }
                set { description = value; }
            }
		}
}
