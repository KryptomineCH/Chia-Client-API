using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Chia_Client_API.Helpers_NS
{
    public class EncryptionHelper
    {
        public static string Encrypt(string plaintext)
        {
            string publicKey;
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Chia_Client_API.encrypt.xml"))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    publicKey = reader.ReadToEnd();
                }
            }
            using (var rsa = new RSACryptoServiceProvider(4096))
            {
                rsa.FromXmlString(publicKey);
                var encryptedData = rsa.Encrypt(Encoding.UTF8.GetBytes(plaintext), true);
                return Convert.ToBase64String(encryptedData);
            }
        }
        public static string Decrypt(string encryptedText, string privateKeyPath)
        {
            string privateKey = System.IO.File.ReadAllText(privateKeyPath);
            using (var rsa = new RSACryptoServiceProvider(4096))
            {
                rsa.FromXmlString(privateKey);
                var decryptedData = rsa.Decrypt(Convert.FromBase64String(encryptedText), true);
                return Encoding.UTF8.GetString(decryptedData);
            }
        }

    }
}
