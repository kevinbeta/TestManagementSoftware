using System;
using System.Linq;
using System.Text;
using System.Reflection;        

namespace Test_Management_Software.Classes
{
    //Singlton component factory to dynamiclly create controls from users input
    // reference used http://www.codeproject.com/KB/architecture/CSharpClassFactory.aspx
    //The component factory is finished 
    //This Factory can be used to create the windows controls from the string inputs from NewTemplateForm.cs --Jason Phelps
   public class ComponentFactoryJ
    {       
        // Cached reference to the containing assembly
        private Assembly m_asm;

       
        #region Constructor
        public ComponentFactoryJ()
        {
            // Get the assembly that contains this code
            Assembly asm = Assembly.GetCallingAssembly();
            m_asm = asm;

        }
        #endregion


        #region Methods
        //these methods can beused to create our gui components for user created templates
        
      public txtAreaComponent createtxtAreaComponent(string comptype)
        {
            if (comptype == "TextArea")
            {
                object inst = m_asm.CreateInstance(typeof(txtAreaComponent).FullName, true,
                BindingFlags.CreateInstance,
                null, null, null, null);
                
                txtAreaComponent comp = (txtAreaComponent)inst;
                comp.Multiline = true;
                comp.Size = new System.Drawing.Size(307, 100);
                return comp;

            }
            else
            {
                return null;
            }
        }
       public txtBoxComponent createtxtBoxComponent(string comptype)
        {
            if (comptype == "TextBox")
            {
                object inst = m_asm.CreateInstance(typeof(txtBoxComponent).FullName, true,
                BindingFlags.CreateInstance,
                null, null, null, null);

                txtBoxComponent comp = (txtBoxComponent)inst;
                return comp;

            }
            else
            {
                return null;
            }
        }
       public PassFailComponent createPFComponent(string comptype)
        {
            if (comptype == "Pass/Fail")
            {
                object inst = m_asm.CreateInstance(typeof(PassFailComponent).FullName, true,
                BindingFlags.CreateInstance,
                null, null, null, null);

                PassFailComponent comp = (PassFailComponent)inst;
                comp.Text = "Pass/Fail";
                return comp;

            }
            else
            {
                return null;
            }
        }

       public YNComponent createYNComponent(string comptype)
        {
            if (comptype == "Yes/No")
            {
                object inst = m_asm.CreateInstance(typeof(YNComponent).FullName, true,
                BindingFlags.CreateInstance,
                null, null, null, null);

                YNComponent comp = (YNComponent)inst;
                comp.Text = "Yes/No";
                return comp;

            }
            else
            {
                return null;
            }
        }
        public labelComponent createlabelComponent(string text)
        {

            object inst = m_asm.CreateInstance(typeof(labelComponent).FullName, true,
                BindingFlags.CreateInstance,
                null, null, null, null);

            labelComponent comp = (labelComponent)inst; 
            comp.Text = text;
            return comp;
        }



        #endregion





    }
}
