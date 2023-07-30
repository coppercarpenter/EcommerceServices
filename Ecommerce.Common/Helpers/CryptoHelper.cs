using System.Security.Cryptography;
using System.Text;

namespace Ecommerce.Common.Helpers
{
    public static class CryptoHelper
    {
        #region Methods

        public static string SymmetricEncryptString(string key, string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using var memoryStream = new MemoryStream();
                using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
                using (var streamWriter = new StreamWriter(cryptoStream))
                {
                    streamWriter.Write(plainText);
                }
                array = memoryStream.ToArray();
            }
            return Convert.ToBase64String(array);
        }

        public static string SymmetricEncryptHexString(string key, string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using var memoryStream = new MemoryStream();
                using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
                using (var streamWriter = new StreamWriter(cryptoStream))
                {
                    streamWriter.Write(plainText);
                }
                array = memoryStream.ToArray();
            }
            return Convert.ToHexString(array);
        }

        public static string SymmetricDecryptString(string key, string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using Aes aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = iv;
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using var memoryStream = new MemoryStream(buffer);
            using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            using var streamReader = new StreamReader(cryptoStream);
            return streamReader.ReadToEnd();
        }

        public static string SymmetricDecryptHexString(string key, string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromHexString(cipherText);

            using Aes aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = iv;
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using var memoryStream = new MemoryStream(buffer);
            using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            using var streamReader = new StreamReader(cryptoStream);
            return streamReader.ReadToEnd();
        }

        public static string GenerateRandomNumberHexString(int? length)
        {
            using var rng = RandomNumberGenerator.Create();
            int blockSize = 32;

            if (length.HasValue)
            {
                blockSize = length.Value;
            }

            var randomNumber = new byte[blockSize];
            rng.GetBytes(randomNumber);

            return BitConverter.ToString(randomNumber).Replace("-", "").ToLower();
        }

        public static string GenerateHMACSHA256(string message, string secret)
        {
            secret ??= "";

            var encoding = new ASCIIEncoding();

            byte[] keyBytes = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(message);

            using var hmacsha256 = new HMACSHA256();
            if (!string.IsNullOrWhiteSpace(secret))
            {
                hmacsha256.Key = keyBytes;
            }

            byte[] hashMessage = hmacsha256.ComputeHash(messageBytes);

            string hashString = Convert.ToBase64String(hashMessage);

            return BitConverter.ToString(hashMessage).Replace("-", "").ToLower();
        }

        public static string StringToBase(string value)
        {
            byte[] byt = Encoding.UTF8.GetBytes(value);

            return Convert.ToBase64String(byt);
        }

        public static string BaseToString(string value)
        {
            byte[] byt = Convert.FromBase64String(value);

            return Encoding.UTF8.GetString(byt);
        }

        #endregion Methods
    }
}