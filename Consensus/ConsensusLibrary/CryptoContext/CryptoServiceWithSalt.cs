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
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] allHash = new byte[36];
            Array.Copy(salt, 0, allHash, 0, 16);
            Array.Copy(hash, 0, allHash, 16, 20);
            var passwordToSave = Convert.ToBase64String(allHash);
            return passwordToSave;
        }

        public bool CompareHashWithString(string hash, string password)
        {
            byte[] allHash = Convert.FromBase64String(hash);
            byte[] salt = new byte[16];
            Array.Copy(allHash, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] newHash = pbkdf2.GetBytes(20);
            for (int i = 0; i < 20; i++)
                if (allHash[i + 16] != newHash[i])
                    return false;
            return true;
        }
    }
}
