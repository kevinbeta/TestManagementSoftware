using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Test_Management_Software.Classes.Document_Factory_Utilities;

namespace Test_Management_Software.Classes
{
    abstract class AbstractDocumentFactory
    {
       public abstract Documents createDocument(string info, string version, List<String> otherInfo);
    }
}
