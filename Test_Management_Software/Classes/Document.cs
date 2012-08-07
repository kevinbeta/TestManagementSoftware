using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace Test_Management_Software.Classes
{
    class Document : Panel
    {
        DocumentFactory df = new DocumentFactory();
        public Document(String name, List<String> list2) 
        {

            this.VerticalScroll.Visible = true;
            this.AutoScroll = true;
            this.AutoSize = true;
            documentWizard(list2);
           //Will call the Document Factory
           //This will have to be called from one of the new forms that will take input from the user
           //and pass this info to the appropiate factory component based on its type. Kind of like we
           //did with the template class but for template type. this will be pass to the template class.
           //and return the panel and add it to one here. Each type of document with a differnt format.
 
        }
        private void documentWizard(List<String> list2)
        {
        }
        public Panel getDocument()
        {
            return this;
        }
    }
}
