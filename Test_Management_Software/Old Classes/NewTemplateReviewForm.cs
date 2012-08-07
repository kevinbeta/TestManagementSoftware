using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Test_Management_Software.Classes;
using Test_Management_Software.Classes.Database_Utilities;

namespace Test_Management_Software
{
        
		public partial class NewTemplateReviewForm: Form {
            
            public NewTemplateReviewForm(List<string> componentList)
            {
				InitializeComponent();
                
                foreach(string a in componentList){
                    Console.WriteLine(a);
                }
                Template t = new Template(componentList.ElementAt(0), componentList);

                this.panel1.Controls.Add(t.getTemplate());



                //Test Case : Failed
                //ComponentFactoryJ component = new ComponentFactoryJ();
                //Components comp = component.createlabelComponent("Test");

                //Test Case : Worked
                //ComponentFactoryJ component = new ComponentFactoryJ();
                //Components comp = component.createtxtAreaComponent("TextArea");
                
                //this.panel1.Controls.Add((System.Windows.Forms.Control)comp);


                /* Test Cases for new Factory */
                AbstractComponentFactory txtBoxTest = new ComponentFactory();
                TextBoxComponent txtBox = txtBoxTest.createtextBoxComponent();
                //this.panel1.Controls.Add(txtBox);

                AbstractComponentFactory txtAreaTest = new ComponentFactory();
                TextAreaComponent txtArea = txtAreaTest.createtextAreaComponent(100, 50);
                //this.panel1.Controls.Add(txtArea);

                AbstractComponentFactory checkBoxTest = new ComponentFactory();
                //CheckBoxComponent checkBox = checkBoxTest.createCheckbox("Test checkbox");
                //this.panel1.Controls.Add(checkBox);

                AbstractComponentFactory labelText = new ComponentFactory();
                LabelComponent label = labelText.createlabelComponent("Test label");
                //this.panel1.Controls.Add(label);

                //Serialization testSerialization = new Serialization(TemplateStorage.GetInstance());
                //testSerialization.serialize(@"C:\\Users\\cit_student3\\Desktop\\test.xml");
                
			}

            private void createTemplate()
            {

                
				//TODO: Will save and populate template options on main form.
			}

			private void applyButton_Click(object sender, EventArgs e) {
				createTemplate();
				this.Close();
			}

			private void cancelButton_Click(object sender, EventArgs e) {
				this.Close();
			}
		}
}
