﻿using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;

namespace Chia_Client_API.FarmerAPI_NS
{
    public partial class Farmer_RPC_Client
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Farmer_RPC_Client"/> class.
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
        /// <param name="targetApiAddress">The address of the target API. Defaults to "localhost".</param>
        /// <param name="targetApiPort">The port number for the API connection. Defaults to 8559.</param>
        /// <param name="targetCertificateBaseFolder">The file path to the folder containing the SSL/TLS certificate. 
        /// This is optional and if not provided, defaults to a standard certificate location in the user's profile.</param>

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Farmer_RPC_Client(bool reportResponseErrors, string targetApiAddress = "localhost", int targetApiPort = 8559, string? targetCertificateBaseFolder = null, TimeSpan? timeout = null)
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
            ".chia","mainnet","config","ssl");
            }
            SetNewCerticifactes();
            _Client.Timeout = timeout ?? TimeSpan.FromMinutes(5);
            ReportResponseErrors = reportResponseErrors;
        }
        /// <summary>
        /// Gets or sets the HTTP client used for communication with the API.
        /// </summary>
        private HttpClient _Client { get; set; }
        /// <summary>
        /// specifies if RPC response errors should be reported for api improvements
        /// </summary>
        public bool ReportResponseErrors { get; set; }
        /// <summary>
        /// the address under which the node can be reached. Defaults to localhost (127.0.0.1)
        /// </summary>
        public string TargetApiAddress { get; set; }
        /// <summary>
        /// the port which should be used. defaults to 8555
        /// </summary>
        public int TargetApiPort { get; set; }
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
            var handler = new SocketsHttpHandler();
            handler.SslOptions.ClientCertificates = CertificateLoader.GetCertificate(Endpoint.farmer, _API_CertificateFolder);
            handler.SslOptions.RemoteCertificateValidationCallback += (sender, cert, chain, errors) =>
            {
                Console.WriteLine($"SSL Policy Errors: {errors}");
                return true; // For testing purposes
            };
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
