//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Windows.Forms;

//namespace Test_Management_Software.Classes
//{
//    public class myForm : Form
//    {
//        ComponentFactory cf = new ComponentFactory();
//        List<Components> list = new List<Components>();
//        List<string> list2;
//        Label Name;
//        Label Desc;

//        public myForm(List<string> templist)
//        {
//            list2 = templist;
//            foreach(string a in list2){
//            switch (a)
//            {
//                case  "Textbox":
//                    //dothings
//                    list.Add(cf.createtxtBoxComponent(a));
//                    break;
//                case "TextArea":
//                    //dothings
//                    list.Add(cf.createtxtAreaComponent(a));
//                    break;
//                case "Yes/No" :
//                    //dothings
//                    list.Add(cf.createYNComponent(a));
//                    break;
//                case "Pass/Fail" :
//                    //dothings
//                    list.Add(cf.createPFComponent(a));
//                    break;
//                default:
//                    //dothings
//                    list.Add(cf.createlabelComponent(a));
//                    break;
//            }//end switch
                
//            }
            


//        }















//    }
//}
