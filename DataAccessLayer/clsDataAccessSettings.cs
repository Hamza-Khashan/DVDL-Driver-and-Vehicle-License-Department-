using System;
using System.Configuration;
using System.Diagnostics;

namespace DataAccessLayer
{
    static class clsDataAccessSettings
    {
        // Read from the executable's App.config (<connectionStrings name="DVLD">).
        public static readonly string ConnectionString =
            ConfigurationManager.ConnectionStrings["DVLD"].ConnectionString;

        public static void LogError(string source, Exception ex)
        {
            Debug.WriteLine(source + " failed: " + ex.Message);
        }
    }
}
