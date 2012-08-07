using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Test_Management_Software.Classes
{
    [Serializable]
    public class DocumentStorage
    {
        private string documentType;
        private string[] documentData;
        private string documentName;
        private int versionID;

        public DocumentStorage()
        {
        }

        public DocumentStorage(string documentName, string documentType, string[] documentData)
        {
            this.documentData = documentData;
            this.documentType = documentType;
            this.documentName = documentName;
        }

        public string[] DocumentData
        {
            get { return documentData; }
            set { documentData = value; }
        }

        public string DocumentType
        {
            get { return documentType; }
            set { documentType = value; }
        }

        public string DocumentName
        {
            get { return documentName; }
            set { documentName = value; }
        }
    }
}
