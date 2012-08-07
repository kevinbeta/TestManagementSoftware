using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace Test_Management_Software.Classes
{
    class Template : TableLayoutPanel

        //need to be reconfigured to accept info from the new forms. Based on type of document arrange the panel with the approiate number of compnents.
        //This info is on web site. http://www.coleyconsulting.co.uk/IEEE829.htm . 

    {
        public Template(String name, List<String> list2)
        {

            this.name = name;
            this.ColumnCount = 2;
            this.GrowStyle = TableLayoutPanelGrowStyle.AddRows;
            this.CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset;
            //this.VerticalScroll.Visible = true;
            this.AutoScroll = true;
            this.AutoSize = true;
            //this.Dock = DockStyle.Fill;
            templateWizard(list2);
        }
        private String name = null;
        

        //TODO: Needs to store components for template.
        //TODO: Needs to store description of template.
        //TODO: Needs to be added to the TemplateStorage class.
        //Takes the string list and parses it for specific stuff

        ComponentFactory cf = new ComponentFactory();

        private void templateWizard(List<String> list2)
        {
            //TODO: Create wizard (step by step process) to allow the user to create a new template
            foreach (string a in list2)
            {
                Console.WriteLine(a);
                switch (a)
                {
                    case "Textbox":
                        //dothings
                        Console.WriteLine("got here - Textbox");
                        this.Controls.Add(cf.createtextBoxComponent());
                        break;
                    case "TextArea":
                        //dothings
                        Console.WriteLine("got here - TextArea");
                        this.Controls.Add(cf.createtextAreaComponent(300, 150));
                        break;
                    default:
                        //dothings
                        Console.WriteLine("got here - Default");
                        this.Controls.Add(cf.createlabelComponent(a));
                        break;
                }//end switch

            }

        }
        public Panel getTemplate()
        {
            return this;
        }
    }
}
