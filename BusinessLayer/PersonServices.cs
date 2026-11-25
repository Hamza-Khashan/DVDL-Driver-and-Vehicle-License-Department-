using System.Data;
using DataAccessLayer;
using Models;

namespace BusinessLayer
{
    public class PersonServices
    {
        public stPerson Person;

        public PersonServices()
        {
            Person = new stPerson();
            Person.Mode = stPerson.enMode.AddNew;
        }

        private PersonServices(stPerson person)
        {
            Person = person;
            Person.Mode = stPerson.enMode.Update;
        }

        public static DataTable GetAllPeople()
        {
            return PersonDAL.GetPeople();
        }

        public bool Save()
        {
            switch (Person.Mode)
            {
                case stPerson.enMode.AddNew:
                    if (PersonDAL.AddNewPerson(ref Person) == -1)
                        return false;
                    Person.Mode = stPerson.enMode.Update;
                    return true;

                case stPerson.enMode.Update:
                    return PersonDAL.UpdatePerson(ref Person);
            }
            return false;
        }

        public static bool DeletePerson(int personID)
        {
            return PersonDAL.DeletePerson(personID);
        }

        public static PersonServices GetPersonByID(int personID)
        {
            stPerson person = new stPerson();
            return PersonDAL.GetPersonByID(personID, ref person) ? new PersonServices(person) : null;
        }

        public static bool IsNationalNoExist(string nationalNo, int personIDToExclude)
        {
            return PersonDAL.IsNationalNoExist(nationalNo, personIDToExclude);
        }
    }
}
