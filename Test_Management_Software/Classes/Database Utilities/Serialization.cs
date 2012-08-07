using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.OleDb;
using System.Data.SqlServerCe;
using System.Xml.Serialization;
using System.Xml;

namespace Test_Management_Software.Classes.Database_Utilities
{
    /// <summary>
    /// Serializes an object that is passed into it and returns the serialization as a string.
    /// This will allow the application to De-serialize the string to reload 
    /// the templates at each start-up.
    /// </summary>
    class Serialization
    {
        public Serialization()
        {

        }

        #region Public Methods

        public string serialize(Object m_object)
        {
            //MemoryStream memStream = new MemoryStream(); //Removed due to SQL error it invoked.
            StringWriter stringWriter = new StringWriter();
            try
            {
                Type objectType = m_object.GetType();
                string name = objectType.Name;
                XmlSerializer serializer = null;
                if (name == "DocumentStorage")
                {
                   serializer = new XmlSerializer(typeof(DocumentStorage));
                }

                if (name == "TemplateStorage")
                {
                    serializer = new XmlSerializer(typeof(TemplateStorage));
                }
                if (name.Contains("String"))
                {
                    serializer = new XmlSerializer(typeof(String[]));
                }
                
                serializer.Serialize(stringWriter, m_object);

                String output = stringWriter.ToString();

                return output;

                
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        /// <summary>
        /// This method will deserialize a string
        /// </summary>
        /// <param name="filePath"></param>
        public Object deSerialize(Object m_object, String data)
        {
            try
            {
                XmlSerializer serializer;
                StringReader stringReader = new StringReader(data);
                XmlTextReader textReader = new XmlTextReader(stringReader);

                Type objectType = m_object.GetType();
                string name = objectType.Name;

                if (name == "DocumentStorage")
                {
                    serializer = new XmlSerializer(typeof(DocumentStorage));
                    m_object = (DocumentStorage)serializer.Deserialize(textReader);
                }

                if (name == "TemplateStorage")
                {
                    serializer = new XmlSerializer(typeof(TemplateStorage));
                    m_object = (TemplateStorage)serializer.Deserialize(textReader);
                }

                textReader.Close();
                stringReader.Close();
                System.Console.WriteLine(Environment.NewLine + "Object 'readStore' deserialized!");

                return m_object;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

    }
}
