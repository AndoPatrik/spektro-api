using System.Security.Cryptography;
using System.Text;

namespace Spektro_API_Azure.Service
{
    public class Security
    {
        public static string StringHashToMD5(string input) 
        {
            StringBuilder sb = new StringBuilder();
            MD5 md5 = MD5.Create();
           
            byte[] inputBytes = Encoding.ASCII.GetBytes(input + "@Spektro2019@");
            byte[] hash = md5.ComputeHash(inputBytes);

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
