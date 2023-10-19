using CHIA_RPC.Wallet_NS.WalletNode_NS;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;

namespace Chia_Client_API.WalletAPI_NS
{
    public partial class Wallet_RPC_Client
    {
        /// <summary>
        /// Initializes a new instance of the Wallet_RPC_Client class that can be used to interact with a Chia wallet.
        /// </summary>
        /// <param name="targetApiAddress">The IP address of the Chia wallet's RPC server. Defaults to 'localhost'.</param>
        /// <param name="targetApiPort">The port number of the Chia wallet's RPC server. Defaults to 9256.</param>
        /// <param name="targetCertificateBaseFolder">The base directory for the SSL/TLS certificate. This certificate is used to establish a secure connection with the Chia wallet's RPC server. If null, the default certificate location will be used.</param>
        /// <remarks>
        /// <param name="reportResponseErrors">sends the following information with asymetric rsa 4096 encryption to kryptomine.ch for improving the API: <br/>
        /// ChiaVersion<br/>
        /// ApiVersion<br/>
        /// RpcVersion<br/>
        /// ErrorTime<br/>
        /// ErrorText<br/>
        /// RawServerResponse<br/>
        /// Preferrably use on testnet or with a testwallet<br/>
        /// Any responses with potential key / authentication information are omitted</param>
        /// This constructor configures the connection details for the Chia wallet's RPC server. It is important to ensure the correctness of these details for successful communication with the wallet. 
        /// The 'localhost' default for the targetApiAddress parameter is suitable for scenarios where the wallet and the application are running on the same machine. 
        /// For remote wallets, provide the appropriate IP address.
        /// The default targetApiPort is the default port number where the Chia Wallet RPC server is configured to listen for incoming requests.
        /// The targetCertificateBaseFolder parameter needs to point to a directory containing a valid SSL/TLS certificate. This is required to establish a secure (HTTPS) connection to the wallet's RPC server. If left null, it assumes the certificate is in the default location.
        /// </remarks>
        public Wallet_RPC_Client(bool reportResponseErrors, string targetApiAddress = "localhost", int targetApiPort = 9256, string? targetCertificateBaseFolder = null, TimeSpan? timeout = null)
        {
            TargetApiAddress = targetApiAddress;
            TargetApiPort = targetApiPort;
            // this also sets the client
            if (targetCertificateBaseFolder != null)
            {
                _API_CertificateFolder = targetCertificateBaseFolder;
            }
            else
            {
                _API_CertificateFolder = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                    @".chia\mainnet\config\ssl\");
            }
            SetNewCerticifactes();
            _Client.Timeout = timeout ?? TimeSpan.FromMinutes(5);
            ReportResponseErrors = reportResponseErrors;
        }
        private HttpClient? _Client { get; set; }
        /// <summary>
        /// the address under which the node can be reached. Defaults to localhost (127.0.0.1)
        /// </summary>
        public string TargetApiAddress { get; set; }
        /// <summary>
        /// the port which should be used. defaults to 9256
        /// </summary>
        public int TargetApiPort { get; set;}
        /// <summary>
        /// specifies if RPC response errors should be reported for api improvements
        /// </summary>
        public bool ReportResponseErrors { get; set; }
        /// <summary>
        /// the base folder is the folder where all certificates are contained within subfolders according to chias default structure
        /// </summary>
        public string? API_CertificateFolder 
        {
            get { return _API_CertificateFolder; } 
            set { _API_CertificateFolder = value; SetNewCerticifactes(); } 
        }
        private string? _API_CertificateFolder;
        /// <summary>
        /// this function creates a new http cliet with the set certificates
        /// </summary>
        private void SetNewCerticifactes()
        {
            if (_Client != null) _Client.Dispose();
            // initialize http client with proper certificate
            var handler = new HttpClientHandler();
            if (_API_CertificateFolder == null)
            {
                throw new ArgumentNullException(nameof(_API_CertificateFolder));
            }
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
        public async Task<string> SendCustomMessage_Async(string function, string? json = " { } ")
        {
            if (_Client == null)
            {
                throw new NullReferenceException(nameof(_Client));
            }
            using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://" + TargetApiAddress + ":"+TargetApiPort.ToString()+"/" + function))
            {
                request.Content = new StringContent(json ?? "");
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
                GetWalletSyncStatus_Response? syncStatus = await GetSyncStatus_Async();
                if (syncStatus == null || !(syncStatus.success ?? false))
                {
                    if (syncStatus != null && syncStatus.error == "wallet state manager not assigned")
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
                else if ((syncStatus.success ?? false) && (syncStatus.syncing || syncStatus.synced))
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
