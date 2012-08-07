using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Test_Management_Software.Classes
{
    /// <summary>
    /// Custom TextBox Component that allows us to dynamically create
    /// custom controls with specific attributes as needed.
    /// Coded & Designed by: Matthew Mills, Jason Phelps, Anthony Ferrari
    /// </summary>
    class TextBoxComponent : TextBox
    {
        public TextBoxComponent()
        {
            this.MaxLength = 50;
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
