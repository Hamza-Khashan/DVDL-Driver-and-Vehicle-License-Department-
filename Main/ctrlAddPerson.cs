using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using BusinessLayer;
using Models;
using System.Text.RegularExpressions;

namespace Main
{
    public partial class ctrlAddPerson : UserControl
    {
        PersonServices _Service;
        public ctrlAddPerson()
        {
            InitializeComponent();
        }

        private void _FillCountriesInComboBox()
        {
            DataTable dtCountries = CountryServices.GetAllCountries();
            comboBox1.DataSource = dtCountries;
            comboBox1.DisplayMember = "CountryName";
            comboBox1.ValueMember = "CountryID";
            comboBox1.SelectedIndex = comboBox1.FindStringExact("Jordan");
        }

        private void _SetMaxDateOfBirth()
        {
            dateTimePicker1.MaxDate = DateTime.Today.AddYears(-18);
            dateTimePicker1.Value = dateTimePicker1.MaxDate;
        }
        void UpdatePictureBox()
        {
            if (rdbMale.Checked)
                pictureBox10.Image = Properties.Resources.Male_512;
            else if (rdbFemale.Checked)
                pictureBox10.Image = Properties.Resources.Female_512;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePictureBox();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePictureBox();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.ParentForm?.Close();
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
                return;
            
            _Service.Person.NationalNO = txtNationalNO.Text;
            _Service.Person.FirstName = txtFirstName.Text;
            _Service.Person.SecondName = txtSecondName.Text;
            _Service.Person.ThirdName = txtThirdName.Text;
            _Service.Person.LastName = txtLastName.Text;
            _Service.Person.DateOfBirth = dateTimePicker1.Value;
            _Service.Person.Gender = rdbMale.Checked ? stPerson.enGender.Male : stPerson.enGender.Female;
            _Service.Person.Address = txtAddress.Text;
            _Service.Person.Phone = txtPhone.Text;
            _Service.Person.Email = txtEmail.Text;
            _Service.Person.CountryID = (int)comboBox1.SelectedValue;

            if (_Service.Save())
            {
                MessageBox.Show("Person saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ParentForm?.Close();
            }
            else
            {
                MessageBox.Show("An error occurred while saving the person.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Called by the host form (Add_Edit) right after it decides whether
        // this is an AddNew or Update, and builds the matching PersonServices object.
        public void SetPersonService(PersonServices service, bool isEditMode)
        {
            _Service = service;
            if (isEditMode)
                _FillFieldsFromPerson();
        }

        private void _FillFieldsFromPerson()
        {
            txtFirstName.Text = _Service.Person.FirstName;
            txtSecondName.Text = _Service.Person.SecondName;
            txtThirdName.Text = _Service.Person.ThirdName;
            txtLastName.Text = _Service.Person.LastName;
            txtNationalNO.Text = _Service.Person.NationalNO;
            txtPhone.Text = _Service.Person.Phone;
            txtEmail.Text = _Service.Person.Email;
            txtAddress.Text = _Service.Person.Address;
            DateTime dob = _Service.Person.DateOfBirth;
            dateTimePicker1.Value = dob > dateTimePicker1.MaxDate ? dateTimePicker1.MaxDate : dob;
            rdbMale.Checked = _Service.Person.Gender == stPerson.enGender.Male;
            rdbFemale.Checked = _Service.Person.Gender == stPerson.enGender.Female;
            comboBox1.SelectedValue = _Service.Person.CountryID;
            UpdatePictureBox();
        }

        private void ctrlAddPerson_Load(object sender, EventArgs e)
        {
            _SetMaxDateOfBirth();
            _FillCountriesInComboBox();
        }

        private void SetErrorBlankField(TextBox txt, CancelEventArgs e, string message)
        {
            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                e.Cancel = true;
                txt.Focus();
                errorProvider1.SetError(txt, message);
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txt, string.Empty);
            }
        }
        
        private void txtFirstName_Validating(object sender, CancelEventArgs e)
        {
            SetErrorBlankField(txtFirstName, e, "First name is required.");
        }
        private void txtSecondName_Validating(object sender, CancelEventArgs e)
        {
            SetErrorBlankField(txtSecondName, e, "Second name is required.");
        }
        private void txtLastName_Validating(object sender, CancelEventArgs e)
        {
            SetErrorBlankField(txtLastName, e, "Last name is required.");
        }
        private void txtNationalNO_Validating(object sender, CancelEventArgs e)
        {
            SetErrorBlankField(txtNationalNO, e, "National ID number is required.");
            if (e.Cancel)
                return; // already flagged as blank, no need to hit the database too

            if (PersonServices.IsNationalNoExist(txtNationalNO.Text.Trim(), _Service.Person.PersonID))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNO, "This National ID number is already used by another person.");
            }
            else
            {
                errorProvider1.SetError(txtNationalNO, string.Empty);
            }
        }
        private void txtPhone_Validating(object sender, CancelEventArgs e)
        {
            SetErrorBlankField(txtPhone, e, "Phone number is required.");
        }
        private void txtAddress_Validating(object sender, CancelEventArgs e)
        {
            SetErrorBlankField(txtAddress, e, "Address is required.");
        }
        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                e.Cancel = false; // Allow empty email
                return;
            }
            else if (!Regex.IsMatch(txtEmail.Text, emailPattern))
            {
                errorProvider1.SetError(txtEmail, "Please enter a valid email address (e.g., name@example.com).");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtEmail, "");
            }
        }

    }
}
