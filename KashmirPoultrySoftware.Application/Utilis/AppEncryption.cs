using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KashmirPoultrySoftware.Application.Utilis
{
    public static class AppEncryption
    {
        public static string GenerateSalt()
        {
            var randomNumbers = RandomNumberGenerator.GetBytes(32);
            var salt = Convert.ToBase64String(randomNumbers);
            return salt;
        }


        public static string GenerateHashedPassword(string salt, string password) 
        {
            var mixedPassword = string.Concat(salt, password);

            var bytes = Encoding.UTF8.GetBytes(mixedPassword);

            var sha = SHA256.Create();
            var computedPassword =  sha.ComputeHash(bytes);

            return Convert.ToBase64String(computedPassword);
        }


        public static bool ComparePassword(string dbPassword ,string salt, string userPassword)
        {
            return dbPassword == GenerateHashedPassword(salt, userPassword);
        }

        public static string GenerateNumericResetCode()
        {
            int codeLength = 6;

            Random random = new Random();
            const string digits = "0123456789";
            char[] codeArray = new char[codeLength];

            for (int i = 0; i < codeLength; i++)
            {
                codeArray[i] = digits[random.Next(digits.Length)];
            }

            string resetCode = new string(codeArray);

            return resetCode;
        }

    }
}
