using System.Data;
using DataAccessLayer;

namespace BusinessLayer
{
    public class CountryServices
    {
        public static DataTable GetAllCountries()
        {
            return CountryDAL.GetCountries();
        }

        public static string GetCountryName(int countryID)
        {
            return CountryDAL.GetCountryName(countryID);
        }
    }
}
