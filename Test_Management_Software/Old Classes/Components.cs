using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Test_Management_Software.Classes
{
    //Much cleaned up components for use in dynamically creating templates--Jason
    //abstract class used for the ComponentFactory
    interface Components
    {

    }


    public class txtBoxComponent : TextBox, Components
    {

    }
    public class txtAreaComponent : TextBox, Components
    {

    }
    public class PassFailComponent : CheckBox, Components
    {

    }
    public class YNComponent : CheckBox, Components
    {

           
    }
    public class labelComponent : Label, Components
    {

    }
}
/*
Idea for making a component factory. Using the users input to create a list of compenents. Giving each component a label and a type.
Storing the location of the components should not be needed we can just add the components to a gridlayout(2,listsize). Adding the label
to column one and the type to colmun two.

By Jason and Anthony

Example

label1  type1
label2  type2

*/