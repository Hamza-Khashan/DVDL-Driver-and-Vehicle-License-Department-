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
    public partial class Add_Edit : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;

        PersonServices _Service;
        int _PersonID;
        public Add_Edit(int personID)
        {
            InitializeComponent();
            _PersonID = personID;
            _Mode = _PersonID == -1 ? enMode.AddNew : enMode.Update;
        }
        private void _LoadData()
        {
            if (_Mode == enMode.AddNew)
            {
                _Service = new PersonServices();
                label1.Text = "Add New Person";
                ctrlAddPerson1.SetPersonService(_Service, isEditMode: false);
                return;
            }

            _Service = PersonServices.GetPersonByID(_PersonID);
            if (_Service == null)
            {
                MessageBox.Show("This person no longer exists.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            label1.Text = "Edit Person with ID: " + _PersonID.ToString();
            PersonID.Text = _PersonID.ToString();
            ctrlAddPerson1.SetPersonService(_Service, isEditMode: true);
        }
        private void Add_Edit_Load(object sender, EventArgs e)
        {
            _LoadData();
        }
    }
}
