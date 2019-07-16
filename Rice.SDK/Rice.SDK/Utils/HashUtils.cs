using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Rice.SDK.Utils
{
    public static class HashUtils
    {
        /// <summary>
        /// Hashes the password using the provided salt
        /// </summary>
        /// <param name="value">text to be hashed (password)</param>
        /// <param name="salt">used salt</param>
        /// <returns></returns>
        public static byte[] Hash(string value, byte[] salt)
        {
            return Hash(Encoding.UTF8.GetBytes(value), salt);
        }

        /// <summary>
        /// Hashes the password using the provided salt
        /// </summary>
        /// <param name="value"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static byte[] Hash(byte[] value, byte[] salt)
        {
            byte[] saltedValue = value.Concat(salt).ToArray();
            return new SHA256Managed().ComputeHash(saltedValue);
        }

        /// <summary>
        /// Compares the password with the provided hash
        /// </summary>
        /// <param name="enteredPassword"></param>
        /// <param name="correctPassword"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static bool ConfirmPassword(string enteredPassword,
            byte[] correctPassword, byte[] salt)
        {
            byte[] passwordHash = Hash(enteredPassword, salt);
            return correctPassword.SequenceEqual(passwordHash);
        }

        /// <summary>
        /// Generates a cryptographic random number.
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static byte[] CreateSalt(int size)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[size];
            rng.GetBytes(salt);

            return salt;
        }
    }
}