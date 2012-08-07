using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test_Management_Software.Classes
{
    class Version
    {
        int versionID;
        List<Document> documents = new List<Document>();
        public Version(int versionNumber)
        {
            versionID = versionNumber;

        }
        public void addDucument()
        {
            //add new documents to the list
        }
    }
}
