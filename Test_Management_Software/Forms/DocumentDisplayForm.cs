using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Test_Management_Software.Classes.Database_Utilities;
using System.Runtime.Serialization;


namespace Test_Management_Software.Forms
{
    public partial class DocumentDisplayForm : Form

    {

        public DocumentDisplayForm(TableLayoutPanel panel)
        {

            this.AutoScroll = true;
            this.Height = 700;
            this.Width = 1000;
            InitializeComponent();
            panel1.Controls.Add(panel);
            panel1.Focus();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}
