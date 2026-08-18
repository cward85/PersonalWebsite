using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace PersonalWebsite.Helpers
{
    public class HelperFunctions
    {
        /// <summary>
        /// Generates a cryptographically secure random passcode.
        /// </summary>
        /// <param name="length">Length of the passcode (must be >= 4).</param>
        /// <param name="allowedChars">Characters allowed in the passcode.</param>
        /// <returns>Randomly generated passcode string.</returns>
        public static string GeneratePasscode(int length, string allowedChars = null)
        {
            if (length < 4)
                throw new ArgumentException("Passcode length must be at least 4 characters.");

            // Default allowed characters: uppercase, lowercase, digits, and symbols
            if (string.IsNullOrEmpty(allowedChars))
            {
                allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                               "abcdefghijklmnopqrstuvwxyz" +
                               "0123456789";
                               
            }

            char[] chars = allowedChars.ToCharArray();
            byte[] randomBytes = new byte[length];

            // Use cryptographically secure RNG
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            StringBuilder result = new StringBuilder(length);
            foreach (byte b in randomBytes)
            {
                result.Append(chars[b % chars.Length]);
            }

            return result.ToString();
        }
    }
}