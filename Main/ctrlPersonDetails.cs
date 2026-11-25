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
using Models;

namespace Main
{
    public partial class ctrlPersonDetails : UserControl
    {
        public ctrlPersonDetails()
        {
            InitializeComponent();
        }

        public void LoadPersonDetails(int personID)
        {
            PersonServices service = PersonServices.GetPersonByID(personID);
            if (service == null)
                return;

            stPerson p = service.Person;
            lblPersonID.Text = p.PersonID.ToString();
            lblName.Text = string.Join(" ", new[] { p.FirstName, p.SecondName, p.ThirdName, p.LastName }
                .Where(n => !string.IsNullOrWhiteSpace(n)));
            lblNationalNO.Text = p.NationalNO;
            lblGendor.Text = p.Gender.ToString();
            lblEmail.Text = p.Email;
            lblAddress.Text = p.Address;
            lblDOB.Text = p.DateOfBirth.ToString("dd/MM/yyyy");
            lblPhone.Text = p.Phone;
            lblCountry.Text = CountryServices.GetCountryName(p.CountryID);
        }
    }
}
