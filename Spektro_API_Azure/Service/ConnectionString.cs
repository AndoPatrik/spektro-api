namespace Spektro_API_Azure.Service
{
    public class ConnectionString
    {
        private static string conString = "Server=tcp:3rdsemprogramming.database.windows.net,1433;Initial Catalog=trialdb;Persist Security Info=False;User ID=patr589d;Password=CookieMonsta88;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        private static string secretKey = "spektro_team_ando_eckmann_kolesar_witt_next_level_developers";

        public static string GetConnectionString() 
        {
            return conString;
        }

        public static string GetSecretKey()
        {
            return secretKey;
        }
    }
}
