﻿using System;
using System.Security.Cryptography;

namespace EncounterAPI
{
    public static class PasswordHasher
    {
        public static byte[] HashPassword(string password, byte[] salt = null)
        {
            if (salt == null)
            {
                new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            }

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 0, 20);

            return hashBytes;
        }

        public static byte[] GetSalt(byte[] hashBytes)
        {
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            return salt;
        }

        public static int ComparePasswords(byte[] hashBytes, string password)
        {
            var salt = GetSalt(hashBytes);
            var hash = HashPassword(password, salt);

            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i]) return -1;

            return 0;
        }
    }
}
