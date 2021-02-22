using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace HarvestCrop
{
    class Crypto
    {
        public static string DecryptString(string encryptedcontent)
        {
            byte[] IV = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            int BlockSize = 128;
            byte[] bytes = Convert.FromBase64String(encryptedcontent);
            string decryptedresult;

            SymmetricAlgorithm crypt = Aes.Create();
            HashAlgorithm hash = MD5.Create();
            crypt.Key = hash.ComputeHash(Encoding.Unicode.GetBytes(Config.CryptoKey));
            crypt.IV = IV;

            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                using (CryptoStream cryptoStream =
                   new CryptoStream(memoryStream, crypt.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    byte[] decryptedBytes = new byte[bytes.Length];
                    cryptoStream.Read(decryptedBytes, 0, decryptedBytes.Length);
                    decryptedresult = Encoding.Unicode.GetString(decryptedBytes);
                }
            }
            return decryptedresult;
        }
    }
}
