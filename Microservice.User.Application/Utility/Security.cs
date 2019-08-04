using Microservice.User.ServiceModel.Users;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Microservice.User.Application.Utility
{
    public static class Security
    {
        public static Credential GenerateSecurePassword(string password)
        {
            string salt = GenerateSalt(10);
            string hashedPassword = GenerateSHA256Hash(password, salt);

            Credential userCredential = new Credential()
            {
                Salt = salt,
                HashedPassword = hashedPassword
            };

            return userCredential;
        }

        public static string ValidateHashedPassword(string password, string salt)
        {
            string hashedPassword = GenerateSHA256Hash(password, salt);
            return hashedPassword;
        }

        private static string GenerateSalt(int size)
        {
            var random = new RNGCryptoServiceProvider();
            var buffer = new byte[size];
            random.GetBytes(buffer);
            return Convert.ToBase64String(buffer);
        }

        private static string GenerateSHA256Hash(string password, string salt)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(password + salt);
            SHA256Managed sha256HashString = new SHA256Managed();
            byte[] hash = sha256HashString.ComputeHash(bytes);

            return ByteArrayToHexString(hash);
        }

        private static string ByteArrayToHexString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);

            foreach (byte b in ba)
            {
                hex.AppendFormat("{0:x2}", b);
            }

            return hex.ToString();
        }
    }
}
