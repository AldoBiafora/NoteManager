using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utils
{
    public abstract class CipherUtils
    {
        private static string PRIVATE_KEY = "OVpndXo0TjNiM1dvcVB2djVEdGhvQT09LFhwRGJLZklHU1Y0STRHQnlBektpS0FUUXpuRUs5ekNSL2FvNGF0RVBlUEE9";
        private static int KEY_SIZE = 256;

        public static string EncryptPublic(string plainStr)
        {
            return Scrambler.Encrypt(plainStr);
        }

        public static string DecryptPublic(string encryptedText)
        {
            return Scrambler.Decrypt(encryptedText);
        }

        public static string EncryptPrivate(string plainStr)
        {
            return Encrypt(plainStr, PRIVATE_KEY);
        }

        public static string DecryptPrivate(string encryptedText)
        {
            return Decrypt(encryptedText, PRIVATE_KEY);
        }

        private static string Encrypt(string plainStr, string completeEncodedKey)
        {
            AesManaged aesEncryption = new AesManaged();
            aesEncryption.KeySize = KEY_SIZE;
            aesEncryption.BlockSize = 128;
            aesEncryption.Mode = CipherMode.CBC;
            aesEncryption.Padding = PaddingMode.PKCS7;
            aesEncryption.IV = Convert.FromBase64String(ASCIIEncoding.UTF8.GetString(Convert.FromBase64String(completeEncodedKey)).Split(',')[0]);
            aesEncryption.Key = Convert.FromBase64String(ASCIIEncoding.UTF8.GetString(Convert.FromBase64String(completeEncodedKey)).Split(',')[1]);
            byte[] plainText = ASCIIEncoding.UTF8.GetBytes(plainStr);
            ICryptoTransform crypto = aesEncryption.CreateEncryptor();
            // The result of the encryption and decryption           
            byte[] cipherText = crypto.TransformFinalBlock(plainText, 0, plainText.Length);
            return Convert.ToBase64String(cipherText);
        }

        private static string Decrypt(string encryptedText, string completeEncodedKey)
        {
            AesManaged aesEncryption = new AesManaged();
            aesEncryption.KeySize = KEY_SIZE;
            aesEncryption.BlockSize = 128;
            aesEncryption.Mode = CipherMode.CBC;
            aesEncryption.Padding = PaddingMode.PKCS7;
            aesEncryption.IV = Convert.FromBase64String(ASCIIEncoding.UTF8.GetString(Convert.FromBase64String(completeEncodedKey)).Split(',')[0]);
            aesEncryption.Key = Convert.FromBase64String(ASCIIEncoding.UTF8.GetString(Convert.FromBase64String(completeEncodedKey)).Split(',')[1]);
            ICryptoTransform decrypto = aesEncryption.CreateDecryptor();
            byte[] encryptedBytes = Convert.FromBase64CharArray(encryptedText.ToCharArray(), 0, encryptedText.Length);
            return ASCIIEncoding.UTF8.GetString(decrypto.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length));
        }

        public static string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        internal class Scrambler
        {
            private const string PASS = "6D114A94-375E-4FFD-9F48-CF2C7A12620F";
            private const string SALT = "E5A8DB1B-5EAB-4C62-B322-CE24CE274303";

            internal static string Encrypt(string input)
            {
                // Test data
                byte[] utfdata = UTF8Encoding.UTF8.GetBytes(input);
                byte[] saltBytes = UTF8Encoding.UTF8.GetBytes(Scrambler.SALT);

                // We're using the PBKDF2 standard for password-based key generation

                Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(
                    Scrambler.PASS, saltBytes, 1000);

                // Our symmetric encryption algorithm

                AesManaged aes = new AesManaged();
                aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
                aes.KeySize = aes.LegalKeySizes[0].MaxSize;
                aes.Key = rfc.GetBytes(aes.KeySize / 8);
                aes.IV = rfc.GetBytes(aes.BlockSize / 8);

                // Encryption

                ICryptoTransform encryptTransf = aes.CreateEncryptor();

                // Output stream, can be also a FileStream

                MemoryStream encryptStream = new MemoryStream();

                CryptoStream encryptor = new CryptoStream(
                    encryptStream, encryptTransf, CryptoStreamMode.Write);
                encryptor.Write(utfdata, 0, utfdata.Length);
                encryptor.Flush();
                encryptor.Close();

                // Showing our encrypted content

                byte[] encryptBytes = encryptStream.ToArray();
                string encryptedString = Convert.ToBase64String(encryptBytes);

                // Close stream
                encryptStream.Close();

                // Return encrypted text
                return encryptedString;

            }

            internal static string Decrypt(string base64Input)
            {
                // Get inputs as bytes
                byte[] encryptBytes = Convert.FromBase64String(base64Input);
                byte[] saltBytes = Encoding.UTF8.GetBytes(Scrambler.SALT);

                // We're using the PBKDF2 standard for password-based key generation
                Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(
                    Scrambler.PASS, saltBytes);

                // Our symmetric encryption algorithm
                AesManaged aes = new AesManaged();
                aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
                aes.KeySize = aes.LegalKeySizes[0].MaxSize;
                aes.Key = rfc.GetBytes(aes.KeySize / 8);
                aes.IV = rfc.GetBytes(aes.BlockSize / 8);

                // Now, decryption
                ICryptoTransform decryptTrans = aes.CreateDecryptor();

                // Output stream, can be also a FileStream
                MemoryStream decryptStream = new MemoryStream();
                CryptoStream decryptor = new CryptoStream(
                    decryptStream, decryptTrans, CryptoStreamMode.Write);

                decryptor.Write(encryptBytes, 0, encryptBytes.Length);
                decryptor.Flush();
                decryptor.Close();

                // Showing our decrypted content
                byte[] decryptBytes = decryptStream.ToArray();
                string decryptedString = UTF8Encoding.UTF8.GetString(
                    decryptBytes, 0, decryptBytes.Length);

                // Close Stream
                decryptStream.Close();

                // Return decrypted text
                return decryptedString;

            }

        }

    }
}
