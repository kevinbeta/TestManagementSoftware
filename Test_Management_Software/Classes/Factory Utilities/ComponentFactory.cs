using System;
using System.Linq;
using System.Text;
using System.Reflection;
using Test_Management_Software.Classes;
using System.Windows.Forms;

namespace Test_Management_Software.Classes
{
    /// <summary>
    /// Factory that allows the user to create form controls
    /// for templates.
    /// Coded & Designed by: Matthew Mills, Jason Phelps, Anthony Ferrari
    /// </summary>
    class ComponentFactory : AbstractComponentFactory
    {


        public override TextAreaComponent createtextAreaComponent(int width, int height)
        {
            return new TextAreaComponent(width, height);
        }

        public override TextBoxComponent createtextBoxComponent()
        {
            return new TextBoxComponent();
        }

        public override LabelComponent createlabelComponent(string text)
        {
            return new LabelComponent(text);
        }



    }

}

