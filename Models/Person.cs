using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("BusinessLayer")]
[assembly: InternalsVisibleTo("DataAccessLayer")]

namespace Models
{
    public struct stPerson
    {
        public enum enMode { AddNew, Update }
        public enum enGender : short { Male = 0, Female = 1 }

        public int PersonID;
        public string NationalNO;
        public string FirstName;
        public string SecondName;
        public string ThirdName;
        public string LastName;
        public DateTime DateOfBirth;
        public enGender Gender;
        public string Address;
        public string Phone;
        public string Email;
        public int CountryID;
        public string ImagePath;

        internal enMode Mode;
    }
}
