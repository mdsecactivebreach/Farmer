using System;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Farmer
{
    class Crypto
    {
        public static string Encrypt(string content, string key)
        {
            byte[] IV = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            int BlockSize = 128;
            byte[] bytes = Encoding.Unicode.GetBytes(content);
            string result;
            SymmetricAlgorithm crypt = Aes.Create();
            HashAlgorithm hash = MD5.Create();
            crypt.BlockSize = BlockSize;
            crypt.Key = hash.ComputeHash(Encoding.Unicode.GetBytes(key));
            crypt.IV = IV;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream =
                   new CryptoStream(memoryStream, crypt.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(bytes, 0, bytes.Length);
                }

                result = Convert.ToBase64String(memoryStream.ToArray());
            }
            return result;
        }
    }
}
