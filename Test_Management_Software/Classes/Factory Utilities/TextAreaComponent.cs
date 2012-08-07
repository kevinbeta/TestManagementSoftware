using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Test_Management_Software.Classes
{
    /// <summary>
    /// Custom TextArea Component that allows us to dynamically create
    /// custom controls with specific attributes as needed.
    /// Coded & Designed by: Matthew Mills, Jason Phelps, Anthony Ferrari
    /// </summary>
    class TextAreaComponent : TextBox
    {

        public TextAreaComponent(int width, int height)
        {
            this.Multiline = true;
            this.ScrollToCaret();
            this.ScrollBars = ScrollBars.Vertical;
            this.WordWrap = true;
            this.AcceptsReturn = true;
            this.AcceptsTab = true;
            this.MaxLength = 1000;


            this.setSize(width, height);
        }

        public string getText()
        {
            return this.Text;
        }

        public void setText(String text)
        {
            this.Text = text;
        }

        public void setSize(int x, int y)
        {
            this.Width = x;
            this.Height = y;
        }

        public bool isEmpty()
        {
            if (this.TextLength == 0)
            {
                return false;
            }
            else
                return true;
        }
    }
}
