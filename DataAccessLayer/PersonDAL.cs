using System;
using System.Data;
using System.Data.SqlClient;
using Models;

namespace DataAccessLayer
{
    public class PersonDAL
    {
        public static DataTable GetPeople()
        {
            DataTable dt = new DataTable();
            const string query = @"SELECT p.PersonID, p.NationalNO, p.FirstName, p.SecondName, p.ThirdName, p.LastName,
                                          p.DateOfBirth, CASE WHEN p.Gendor = 0 THEN 'Male' ELSE 'Female' END AS Gendor,
                                          p.Address, p.Phone, p.Email, c.CountryName AS Nationality, p.ImagePath
                                   FROM People p
                                   LEFT JOIN Countries c ON p.NationalityCountryID = c.CountryID";
            try
            {
                using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                        dt.Load(reader);
                }
            }
            catch (Exception ex) { clsDataAccessSettings.LogError("GetPeople", ex); }
            return dt;
        }

        public static bool IsNationalNoExist(string nationalNo, int personIDToExclude)
        {
            const string query = "SELECT COUNT(*) FROM People WHERE NationalNO = @NationalNO AND PersonID <> @PersonID";
            try
            {
                using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NationalNO", nationalNo);
                    cmd.Parameters.AddWithValue("@PersonID", personIDToExclude);
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
            catch (Exception ex) { clsDataAccessSettings.LogError("IsNationalNoExist", ex); }
            return false;
        }

        public static bool GetPersonByID(int personID, ref stPerson person)
        {
            const string query = "SELECT * FROM People WHERE PersonID = @PersonID";
            try
            {
                using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PersonID", personID);
                    conn.Open();
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        if (!r.Read())
                            return false;

                        person.PersonID = (int)r["PersonID"];
                        person.NationalNO = (string)r["NationalNO"];
                        person.FirstName = (string)r["FirstName"];
                        person.SecondName = (string)r["SecondName"];
                        person.ThirdName = _StringOrEmpty(r["ThirdName"]);
                        person.LastName = (string)r["LastName"];
                        person.DateOfBirth = (DateTime)r["DateOfBirth"];
                        person.Gender = (stPerson.enGender)Convert.ToInt16(r["Gendor"]);
                        person.Address = (string)r["Address"];
                        person.Phone = (string)r["Phone"];
                        person.Email = _StringOrEmpty(r["Email"]);
                        person.CountryID = (int)r["NationalityCountryID"];
                        person.ImagePath = _StringOrEmpty(r["ImagePath"]);
                        return true;
                    }
                }
            }
            catch (Exception ex) { clsDataAccessSettings.LogError("GetPersonByID", ex); }
            return false;
        }

        public static int AddNewPerson(ref stPerson person)
        {
            const string query = @"INSERT INTO People (NationalNO, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath)
                                   VALUES (@NationalNO, @FirstName, @SecondName, @ThirdName, @LastName, @DateOfBirth, @Gendor, @Address, @Phone, @Email, @CountryID, @ImagePath);
                                   SELECT SCOPE_IDENTITY();";
            try
            {
                using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    _AddPersonParameters(cmd, person);
                    conn.Open();
                    person.PersonID = Convert.ToInt32(cmd.ExecuteScalar());
                    return person.PersonID;
                }
            }
            catch (Exception ex) { clsDataAccessSettings.LogError("AddNewPerson", ex); }
            return -1;
        }

        public static bool UpdatePerson(ref stPerson person)
        {
            const string query = @"UPDATE People SET NationalNO = @NationalNO, FirstName = @FirstName, SecondName = @SecondName, ThirdName = @ThirdName,
                                          LastName = @LastName, DateOfBirth = @DateOfBirth, Gendor = @Gendor, Address = @Address, Phone = @Phone,
                                          Email = @Email, NationalityCountryID = @CountryID, ImagePath = @ImagePath
                                   WHERE PersonID = @PersonID";
            try
            {
                using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    _AddPersonParameters(cmd, person);
                    cmd.Parameters.AddWithValue("@PersonID", person.PersonID);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex) { clsDataAccessSettings.LogError("UpdatePerson", ex); }
            return false;
        }

        public static bool DeletePerson(int personID)
        {
            const string query = "DELETE FROM People WHERE PersonID = @PersonID";
            try
            {
                using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PersonID", personID);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex) { clsDataAccessSettings.LogError("DeletePerson", ex); }
            return false;
        }

        // Parameters shared by INSERT and UPDATE.
        private static void _AddPersonParameters(SqlCommand cmd, stPerson p)
        {
            cmd.Parameters.AddWithValue("@NationalNO", p.NationalNO);
            cmd.Parameters.AddWithValue("@FirstName", p.FirstName);
            cmd.Parameters.AddWithValue("@SecondName", p.SecondName);
            cmd.Parameters.AddWithValue("@ThirdName", _DbValueOrNull(p.ThirdName));
            cmd.Parameters.AddWithValue("@LastName", p.LastName);
            cmd.Parameters.AddWithValue("@DateOfBirth", p.DateOfBirth);
            cmd.Parameters.AddWithValue("@Gendor", (short)p.Gender);
            cmd.Parameters.AddWithValue("@Address", p.Address);
            cmd.Parameters.AddWithValue("@Phone", p.Phone);
            cmd.Parameters.AddWithValue("@Email", _DbValueOrNull(p.Email));
            cmd.Parameters.AddWithValue("@CountryID", p.CountryID);
            cmd.Parameters.AddWithValue("@ImagePath", _DbValueOrNull(p.ImagePath));
        }

        private static object _DbValueOrNull(string value)
        {
            return string.IsNullOrEmpty(value) ? (object)DBNull.Value : value;
        }

        private static string _StringOrEmpty(object value)
        {
            return value == DBNull.Value ? string.Empty : (string)value;
        }
    }
}
