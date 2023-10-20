using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;

namespace Chia_Client_API.DatalayerAPI_NS
{
    /// <summary>
    /// prioovides the http client for the datalayer node
    /// </summary>
    public partial class Datalayer_RPC_Client
    {
        /// <summary>
        /// Constructor for the Datalayer_RPC_Client class. This method is automatically called 
        /// whenever an instance of the class is created. It sets up the Datalayer_RPC_Client with
        /// </summary>
        /// <param name="reportResponseErrors">sends the following information with asymetric rsa 4096 and AES encryption to kryptomine.ch for improving the API: <br/>
        /// ChiaVersion<br/>
        /// ApiVersion<br/>
        /// RpcVersion<br/>
        /// ErrorTime<br/>
        /// ErrorText<br/>
        /// RawServerResponse<br/>
        /// Preferrably use on testnet or with a testwallet<br/>
        /// Any responses with potential key / authentication information are omitted</param>
        /// <param name="targetApiAddress">This parameter sets the address of the target API. 
        /// This could be an IP address or a domain name. The default value is "localhost", which points to the machine where the code is currently running.</param>
        /// <param name="targetApiPort">This parameter sets the port number that the client should use to connect to the API. 
        /// The default value is 8562. The valid range for port numbers is between 1 and 65535, 
        /// with the range 49152–65535 being the dynamic or private ports typically used for dynamic ports.</param>
        /// <param name="targetCertificateBaseFolder">This optional parameter sets the file path to the folder that contains the SSL/TLS certificate
        /// that the client will use to secure its connection to the API. 
        /// If this parameter is null (which is its default value), it indicates that the client should not use a certificate (the connection won't be secured).</param>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Datalayer_RPC_Client(bool reportResponseErrors, string targetApiAddress = "localhost", int targetApiPort = 8562, string? targetCertificateBaseFolder = null, TimeSpan? timeout = null)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
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
        /// <summary>
        /// this is the client through which requests are made
        /// </summary>
        private HttpClient _Client { get; set; }
        /// <summary>
        /// the address under which the node can be reached. Defaults to localhost (127.0.0.1)
        /// </summary>
        public string TargetApiAddress { get; set; }
        /// <summary>
        /// the port which should be used. defaults to 8555
        /// </summary>
        public int TargetApiPort { get; set; }
        /// <summary>
        /// specifies if RPC response errors should be reported for api improvements
        /// </summary>
        public bool ReportResponseErrors { get; set; }
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
            X509Certificate2 privateCertificate = CertificateLoader.GetCertificate(Endpoint.data_layer, _API_CertificateFolder);
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
            using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://" + TargetApiAddress + ":" + TargetApiPort.ToString() + "/" + function))
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
    }
}
