using System.Text;

namespace HanuEdmsApi.Helpers
{
    public static class PasswordHasher
    {
        public static string SHA256Hashing(string original)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(original));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }

        public static string DefaultPassword()
        {
            return SHA256Hashing("1");
        }
    }
}
