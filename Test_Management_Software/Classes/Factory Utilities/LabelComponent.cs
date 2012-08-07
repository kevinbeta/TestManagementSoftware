using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Test_Management_Software.Classes
{
    /// <summary>
    /// Custom Label Component that allows us to dynamically create
    /// custom controls with specific attributes as needed.
    /// Coded & Designed by: Matthew Mills, Jason Phelps, Anthony Ferrari
    /// </summary>
    class LabelComponent : Label
    {
        public LabelComponent(String text)
        {
            this.setText(text);
            this.AutoSize = true;
        }

        public string getText()
        {
            return this.Text;
        }

        public void setText(String text)
        {
            this.Text = text;
        }
    }
}
