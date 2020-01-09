namespace Spektro_API_Azure.Service
{
    public class SecretStrings
    {
        public static string GetConnectionString() 
        {
            return "Server=tcp:3rdsemprogramming.database.windows.net,1433;Initial Catalog=trialdb;Persist Security Info=False;User ID=patr589d;Password=CookieMonsta88;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }

        public static string GetSecretKey()
        {
            return "spektro_team_ando_eckmann_kolesar_witt_next_level_developers";
        }

        public static string GetTwillioSID() 
        {
            return "AC20cf01f0c9fe1b333fba34ad8eb5f9f7";
        }

        public static string GetTwillioAuthToken() 
        {
            return "1937e2bf0870eafac8a7ff80c31cf941";
        }
    }
}
