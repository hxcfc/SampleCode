using System.Security.Cryptography;
using System.Text;

namespace Common.Shared
{
    public static class HashGenerator
    {
        private const int iterations = 350000;
        private const int keySize = 64;

        public static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static string HashPasword(string password, out byte[] salt)
        {
            HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

            salt = RandomNumberGenerator.GetBytes(keySize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);
            return Convert.ToHexString(hash);
        }

        public static bool VerifyPassword(string password, string hash, byte[] salt)
        {
            HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);
            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }
    }
}