using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace GymBookingSystem.Cryptography
{
    public class Hasher : IHasher
    {
        /*
         In cryptography, a salt is random data that is used as an additional input to a one-way function that hashes data, a password or passphrase.
         Salts defend against attacks that use precomputed tables (e.g. rainbow tables)
         example:
                    salt                password + salt                                     hash
         user1	E1F53135E559C253	password123E1F53135E559C253	72AE25495A7981C40622D49F9A52E4F1565C90F048F59027BD9C8C8900D5C3D8
         */
        public const int SALT_BYTE_SIZE = 192 / 8;
        public const int HASH_BYTE_SIZE = 256 / 8;

        /*
        PBKDF2 applies a pseudorandom function, such as hash-based message authentication code (HMAC),
        to the input password or passphrase along with a salt value and repeats the process many times
        to produce a derived key, which can then be used as a cryptographic key in subsequent operations.
        The added computational work makes password cracking much more difficult, and is known as key stretching.
        */
        public const int PBKDF2_ITERATIONS = 10000;
        public const int ITERATION_INDEX = 0;
        public const int SALT_INDEX = 1;
        public const int PBKDF2_INDEX = 2;
        public string CreateHash(string password)
        {
            // Generate a random salt
            RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[SALT_BYTE_SIZE];
            csprng.GetBytes(salt);

            // Hash the password and encode the parameters
            byte[] hash = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA256, PBKDF2_ITERATIONS, HASH_BYTE_SIZE);
            return PBKDF2_ITERATIONS + ":" +
                Convert.ToBase64String(salt) + ":" +
                Convert.ToBase64String(hash);
        }

        public bool ValidatePassword(string password, string correctHash)
        {
            // Extract the parameters from the hash
            char[] delimiter = { ':' };
            string[] split = correctHash.Split(delimiter);
            int iterations = Int32.Parse(split[ITERATION_INDEX]);
            byte[] salt = Convert.FromBase64String(split[SALT_INDEX]);
            byte[] hash = Convert.FromBase64String(split[PBKDF2_INDEX]);

            byte[] testHash = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA256, iterations, HASH_BYTE_SIZE);
            return SlowEquals(hash, testHash);
        }

        private static bool SlowEquals(byte[] a, byte[] b)
        {
            uint diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
                diff |= (uint)(a[i] ^ b[i]);
            return diff == 0;
        }
    }
}
