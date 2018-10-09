using System;
using System.Security.Cryptography;

namespace ConsensusLibrary.CryptoContext
{
    public class CryptoServiceWithSalt : ICryptoService
    {
        public string CalculateHash(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            var hash = pbkdf2.GetBytes(20);
            var allHash = new byte[36];
            Array.Copy(salt, 0, allHash, 0, 16);
            Array.Copy(hash, 0, allHash, 16, 20);
            var passwordToSave = Convert.ToBase64String(allHash);
            return passwordToSave;
        }

        public bool CompareHashWithString(string hash, string password)
        {
            var allHash = Convert.FromBase64String(hash);
            var salt = new byte[16];
            Array.Copy(allHash, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            var newHash = pbkdf2.GetBytes(20);
            for (var i = 0; i < 20; i++)
                if (allHash[i + 16] != newHash[i])
                    return false;
            return true;
        }
    }
}