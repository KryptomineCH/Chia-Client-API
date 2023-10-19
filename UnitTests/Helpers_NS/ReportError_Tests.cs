using Chia_Client_API.Helpers_NS;
using CHIA_RPC.General_NS;
using Renci.SshNet;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CHIA_API_Tests.Helpers_NS
{
    public class ReportError_Tests
    {
        [Fact]
        public async Task TestUploadFileAsync()
        {
            WalletID_Response response = new WalletID_Response();
            response.success = false;
            response.error = "this is a test error message";
            response.RawContent = "{success: false, error: this is a test error message}";
            // Arrange
            Error testError = new Error(response, "UnitTests", "TestUploadFileAsync"); // Replace this line as per your Error class definition
            string testFilename = "TestUploadFileAsync";

            // Act
            await ReportError.UploadFileAsync(testError);

            // Assert
            // Assuming you have some way to check that the file was actually uploaded
            // For example, you could directly check the SFTP directory,
            // or catch the success response from the SFTP server in your UploadFileAsync method and assert it here.
        }
        [Fact]
        public async Task DownloadDecryptSaveDeleteAsync()
        {
            // Hardcoded values
            string targetUri = "sftp.kryptomine.ch";
            string ftpUser = "errorlog";
            string baseDir = "/errorlog/chia-client-library/";
            string privateKeyPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\.ssh\\decrypt.xml";
            DirectoryInfo saveDir = new DirectoryInfo("ErrorLogs");
            if (!saveDir.Exists) saveDir.Create();
            // Read Private Key from embedded resources
            Stream keyStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Chia_Client_API.errorlog");
            var keyFile = new PrivateKeyFile(keyStream);
            var keyFiles = new[] { keyFile };
            var methods = new List<AuthenticationMethod>();
            methods.Add(new PrivateKeyAuthenticationMethod(ftpUser, keyFiles));

            var connInfo = new ConnectionInfo(targetUri, ftpUser, methods.ToArray());

            using (var sftp = new SftpClient(connInfo))
            {
                sftp.Connect();

                var files = sftp.ListDirectory(baseDir);
                foreach (var file in files)
                {
                    if (!file.IsDirectory)
                    {
                        var remoteFilePath = file.FullName;
                        using (var stream = new MemoryStream())
                        {
                            sftp.DownloadFile(remoteFilePath, stream);
                            stream.Position = 0;
                            string encryptedContent;
                            using (StreamReader reader = new StreamReader(stream))
                            {
                                encryptedContent = await reader.ReadToEndAsync();
                            }

                            // Decrypt content
                            string decryptedContent = EncryptionHelper.Decrypt(encryptedContent, privateKeyPath);

                            // Save to local disk
                            string localFilePath = Path.Combine(saveDir.FullName, Path.GetFileName(remoteFilePath));
                            await File.WriteAllTextAsync(localFilePath, decryptedContent);

                            // Delete the file from the server
                            sftp.DeleteFile(remoteFilePath);
                        }
                    }
                }
                sftp.Disconnect();
            }
        }
    }
}
