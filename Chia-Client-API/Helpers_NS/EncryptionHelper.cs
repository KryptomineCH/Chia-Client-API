using System.Reflection;
using System.Security.Cryptography;

namespace Chia_Client_API.Helpers_NS
{
    public class EncryptionHelper
    {
        /// <summary>
        /// Encrypts Data with AES, then encrypts the AES Key asymmetrically and appends it to the Data
        /// </summary>
        /// <param name="plaintext"></param>
        /// <returns></returns>
        public static string HybridEncrypt(string plaintext)
        {
            string publicKey;
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Chia_Client_API.encrypt.xml"))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    publicKey = reader.ReadToEnd();
                }
            }

            // Generate AES key and IV
            using (Aes aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.GenerateKey();
                aes.GenerateIV();

                byte[] encryptedAESKey, encryptedAESIv, encryptedData;

                // Encrypt data using AES
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(plaintext);
                            }
                        }
                    }
                    encryptedData = msEncrypt.ToArray();
                }

                // Encrypt AES key and IV using RSA
                using (var rsa = new RSACryptoServiceProvider(4096))
                {
                    rsa.FromXmlString(publicKey);
                    encryptedAESKey = rsa.Encrypt(aes.Key, true);
                    encryptedAESIv = rsa.Encrypt(aes.IV, true);
                }

                // Combine and convert to Base64
                byte[] combinedData = new byte[encryptedAESKey.Length + encryptedAESIv.Length + encryptedData.Length];
                Buffer.BlockCopy(encryptedAESKey, 0, combinedData, 0, encryptedAESKey.Length);
                Buffer.BlockCopy(encryptedAESIv, 0, combinedData, encryptedAESKey.Length, encryptedAESIv.Length);
                Buffer.BlockCopy(encryptedData, 0, combinedData, encryptedAESKey.Length + encryptedAESIv.Length, encryptedData.Length);

                return Convert.ToBase64String(combinedData);
            }
        }
        /// <summary>
        /// decrypts the AES Key of the encrypted Data and then decrypts the data given the correct private key
        /// </summary>
        /// <param name="encryptedText"></param>
        /// <param name="privateKeyPath"></param>
        /// <returns></returns>
        public static string HybridDecrypt(string encryptedText, string privateKeyPath)
        {
            string privateKey = System.IO.File.ReadAllText(privateKeyPath);
            byte[] combinedData = Convert.FromBase64String(encryptedText);

            // Extract lengths of encrypted AES Key and IV
            int keyLength = 512; // Encrypted AES key length with RSA 4096 = 512 bytes
            int ivLength = 512;  // Encrypted AES IV length with RSA 4096 = 512 bytes

            // Extract encrypted AES key, IV, and data
            byte[] encryptedAESKey = new byte[keyLength];
            byte[] encryptedAESIv = new byte[ivLength];
            byte[] encryptedData = new byte[combinedData.Length - keyLength - ivLength];
            Buffer.BlockCopy(combinedData, 0, encryptedAESKey, 0, keyLength);
            Buffer.BlockCopy(combinedData, keyLength, encryptedAESIv, 0, ivLength);
            Buffer.BlockCopy(combinedData, keyLength + ivLength, encryptedData, 0, encryptedData.Length);

            byte[] decryptedAESKey, decryptedAESIv;

            // Decrypt AES key and IV using RSA
            using (var rsa = new RSACryptoServiceProvider(4096))
            {
                rsa.FromXmlString(privateKey);
                decryptedAESKey = rsa.Decrypt(encryptedAESKey, true);
                decryptedAESIv = rsa.Decrypt(encryptedAESIv, true);
            }

            // Decrypt data using AES
            using (Aes aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.Key = decryptedAESKey;
                aes.IV = decryptedAESIv;

                using (MemoryStream msDecrypt = new MemoryStream(encryptedData))
                {
                    using (ICryptoTransform decryptor = aes.CreateDecryptor())
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                return srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
            }
        }
    }
}
