using Chia_Client_API.Helpers_NS;
using CHIA_RPC.General_NS;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace CHIA_API_Tests.Helpers_NS
{
    public class EncryptionHelper_Tests
    {
        [Fact]
        public void TestEncryptionAndDecryption()
        {
            // Arrange
            string plaintext = "Hello, world!";
            string privateKeyPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) , ".ssh","decrypt.xml");


            // Act
            string encryptedText = EncryptionHelper.HybridEncrypt(plaintext);
            string decryptedText = EncryptionHelper.HybridDecrypt(encryptedText, privateKeyPath);

            // Assert
            Assert.Equal(plaintext, decryptedText);
        }
    }
}
