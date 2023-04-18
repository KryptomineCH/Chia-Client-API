using CHIA_RPC.Wallet_NS.WalletNode_NS;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;

namespace Chia_Client_API.WalletAPI_NS
{
    public partial class Wallet_RPC_Client
    {
        public Wallet_RPC_Client(string targetApiAddress = "localhost", int targetApiPort = 9256, string? targetCertificateBaseFolder = null)
        {
            TargetApiAddress = targetApiAddress;
            TargetApiPort = targetApiPort;
            // this also sets the client
            if (targetCertificateBaseFolder != null)
            {
                API_CertificateFolder = targetCertificateBaseFolder;
            }
            else
            {
                API_CertificateFolder = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                    @".chia\mainnet\config\ssl\");
            }
        }
        private HttpClient _Client { get; set; }
        /// <summary>
        /// the address under which the node can be reached. Defaults to localhost (127.0.0.1)
        /// </summary>
        public string TargetApiAddress { get; set; }
        /// <summary>
        /// the port which should be used. defaults to 9256
        /// </summary>
        public int TargetApiPort { get; set;}
        /// <summary>
        /// the base folder is the folder where all certificates are contained within subfolders according to chias default structure
        /// </summary>
        public string API_CertificateFolder 
        {
            get { return _API_CertificateFolder; } 
            set { _API_CertificateFolder = value; SetNewCerticifactes(); } 
        }
        private string _API_CertificateFolder;
        /// <summary>
        /// this function creates a new http cliet with the set certificates
        /// </summary>
        private void SetNewCerticifactes()
        {
            if (_Client != null) _Client.Dispose();
            // initialize http client with proper certificate
            var handler = new HttpClientHandler();
            X509Certificate2 privateCertificate = CertificateLoader.GetCertificate(Endpoint.wallet, _API_CertificateFolder);
            handler.ServerCertificateCustomValidationCallback = (requestMessage, certificate, chain, policyErrors) => true;
            handler.ClientCertificates.Add(privateCertificate);
            _Client = new HttpClient(handler);
        }
        /// <summary>
        /// with this function you can execute any RPC against the wallet api. it is internally used by the library
        /// </summary>
        /// <param name="function"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public async Task<string> SendCustomMessage_Async(string function, string json = " { } ")
        {
            using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://" + TargetApiAddress + ":"+TargetApiPort.ToString()+"/" + function))
            {
                request.Content = new StringContent(json);
                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                var response = await _Client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync(); ;
            }
        }
        /// <summary>
        /// with this function you can execute any RPC against the wallet api. it is internally used by the library
        /// </summary>
        /// <param name="function"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public string SendCustomMessage_Sync(string function, string json = " { } ")
        {
            Task<string> data = Task.Run(() => SendCustomMessage_Async(function, json));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// waits until the wallet is fully synced with the blockchain
        /// </summary>
        /// <param name="cancellation"></param>
        /// <param name="timeoutSeconds"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> AwaitWalletSync_Async(CancellationToken cancellation,
            double timeoutSeconds = 60.0)
        {
            TimeSpan timeOut = TimeSpan.FromSeconds(timeoutSeconds);
            DateTime timeOutTracker = DateTime.Now;
            bool success = false;
            while (!cancellation.IsCancellationRequested)
            {
                GetWalletSyncStatus_Response syncStatus = await GetSyncStatus_Async();
                if (!syncStatus.success)
                {
                    if (syncStatus.error == "wallet state manager not assigned")
                    {
                        throw new Exception("no wallet is logged in!");
                    }
                    throw new Exception("Error while obtaining sync status!");
                }
                else if (!syncStatus.syncing && !syncStatus.synced)
                {
                    // not synced and not syncing, check timeout
                    if (timeOutTracker + timeOut < DateTime.Now)
                    {
                        return false;
                    }
                    // timeout is not met, wait for the wallet to start syncing
                }
                else if (syncStatus.success && (syncStatus.syncing || syncStatus.synced))
                {
                    // currently syncing, so we want to extend the timeout
                    timeOutTracker = DateTime.Now;
                }
                if (syncStatus.synced) return true;
                await Task.Delay(1000); // wait 1 second
            }
            return success;
        }
        /// <summary>
        /// waits until the wallet is fully synced with the blockchain
        /// </summary>
        /// <param name="cancellation"></param>
        /// <param name="timeoutSeconds"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool AwaitWalletSync_Sync(double timeoutSeconds = 60.0)
        {
            Task<bool> data = Task.Run(() => AwaitWalletSync_Async(CancellationToken.None, timeoutSeconds));
            data.Wait();
            return data.Result;
        }
    }
}
