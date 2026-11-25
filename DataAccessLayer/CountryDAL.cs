using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class CountryDAL
    {
        public static DataTable GetCountries()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Countries", conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                        dt.Load(reader);
                }
            }
            catch (Exception ex) { clsDataAccessSettings.LogError("GetCountries", ex); }
            return dt;
        }

        public static string GetCountryName(int countryID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand("SELECT CountryName FROM Countries WHERE CountryID = @CountryID", conn))
                {
                    cmd.Parameters.AddWithValue("@CountryID", countryID);
                    conn.Open();
                    return cmd.ExecuteScalar() as string ?? string.Empty;
                }
            }
            catch (Exception ex) { clsDataAccessSettings.LogError("GetCountryName", ex); }
            return string.Empty;
        }
    }
}
