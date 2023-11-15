using Renci.SshNet;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Chia_Client_API.Helpers_NS
{
    public class ReportError
    {
        // Dictionary to store filenames and their last used timestamp
        private static readonly Dictionary<string, DateTime> FilenameCache = new Dictionary<string, DateTime>();

        /// <summary>
        /// sends the following information with asymetric rsa 4096 and AES encryption to kryptomine.ch for improving the API: <br/>
        /// ChiaVersion<br/>
        /// ApiVersion<br/>
        /// RpcVersion<br/>
        /// ErrorTime<br/>
        /// ErrorText<br/>
        /// RawServerResponse<br/>
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public static async Task UploadFileAsync(Error error)
        {
            // Hardcoded values
            string targetUri = "sftp.kryptomine.ch";
            string ftpUser = "errorlog";
            string baseDir = "/errorlog/chia-client-library/";

            // Read Private Key from embedded resources
            Stream keyStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Chia_Client_API.errorlog");
            var keyFile = new PrivateKeyFile(keyStream);
            var keyFiles = new[] { keyFile };
            var methods = new List<AuthenticationMethod>();
            methods.Add(new PrivateKeyAuthenticationMethod(ftpUser, keyFiles));

            var connInfo = new ConnectionInfo(targetUri, ftpUser, methods.ToArray());

            // Check if filename exists in cache and was used in the last 24 hours
            if (FilenameCache.TryGetValue(error.Endpoint+error.Function, out DateTime lastUsed) && (DateTime.Now - lastUsed).TotalDays < 1)
            {
                return; // Skip upload for this filename
            }

            // Update cache
            FilenameCache[error.Endpoint + error.Function] = DateTime.Now;

            // Prepare content and file name
            string content = EncryptionHelper.HybridEncrypt(JsonSerializer.Serialize(error));
            byte[] fileContents = Encoding.UTF8.GetBytes(content);
            string currentUnixTimestampMs = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
            string prefixedFilename = $"{currentUnixTimestampMs}-{error.Endpoint}";
            string fullPath = baseDir + prefixedFilename;

            try
            {
                using (var sftp = new SftpClient(connInfo))
                {
                    sftp.Connect();
                    //if (!sftp.Exists(baseDir))
                    //{
                    //    sftp.CreateDirectory(baseDir);
                    //}
                    using (var stream = new MemoryStream(fileContents))
                    {
                        await Task.Run(() => sftp.UploadFile(stream, fullPath)).ConfigureAwait(false);
                    }
                    sftp.Disconnect();
                }
            }
            catch (Exception ex)
            {
                // Silently omit failures
                { }
            }
        }
    }
}
