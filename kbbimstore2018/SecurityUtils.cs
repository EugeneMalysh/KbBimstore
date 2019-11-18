using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace KbBimstore
{
    public static class SecurityUtils
    {
        public static string GetPasswordHash(string password)
        {
            return CalculateMD5Hash(CreateRandomSalt(), password);
        }

        public static bool ValidatePassword(string password)
        {
            if (KbBimstoreApp.MainTab.hxp2.Length > 20)
            {
                string salt = string.Join("", KbBimstoreApp.MainTab.hxp2.Take(16));

                if (CalculateMD5Hash(salt, password) == KbBimstoreApp.MainTab.hxp2)
                {
                    return true;
                }
            }

            return false;
        }

        public static string CreateRandomSalt()
        {
            RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider();

            byte[] data = new byte[8];
            rand.GetNonZeroBytes(data);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("X2"));
            }

            return sb.ToString();
        }

        public static string CalculateMD5Hash(string salt, string pw)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(salt + pw);
            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return salt + sb.ToString();
        }
    }
}
