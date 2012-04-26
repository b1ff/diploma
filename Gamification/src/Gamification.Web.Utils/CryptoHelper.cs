using System;
using System.Security.Cryptography;
using System.Text;

namespace Gamification.Web.Utils
{
    public static class CryptoHelper
    {
        private const string ValidationKey = "6BAF110F1BA6D715EE18770C23B08C36460862EB81BBFA201846977313982472E1C56E88A6BD2D3A692A484FE17A2F3AD8EB40A36C2473BAD1BCDA078AFD721E";

        public static string GetHash(this string password)
        {
            var hash = new HMACSHA1
            {
                Key = HexToByte(ValidationKey)
            };

            var hashedPass = Convert.ToBase64String(hash.ComputeHash(Encoding.Unicode.GetBytes(password)));

            return hashedPass;
        }

        private static byte[] HexToByte(string hexString)
        {
            var returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
            {
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }

            return returnBytes;
        }
    }
}
