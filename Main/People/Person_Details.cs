using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer;

namespace Main
{
    public partial class Person_Details : Form
    {
        int _PersonID;
        public Person_Details(int personID)
        {
            InitializeComponent();
            _PersonID = personID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Person_Details_Load(object sender, EventArgs e)
        {
            ctrlPersonDetails1.LoadPersonDetails(_PersonID);
        }
    }
}
