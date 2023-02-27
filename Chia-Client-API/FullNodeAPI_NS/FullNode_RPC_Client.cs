﻿using CHIA_RPC.FullNode_RPC_NS;
using CHIA_RPC.Wallet_RPC_NS.Wallet_NS;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace Chia_Client_API.FullNodeAPI_NS
{
    public partial class FullNode_RPC_Client
    {
        public FullNode_RPC_Client(string targetApiAddress = "localhost", int targetApiPort = 8555, string? targetCertificateBaseFolder = null)
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
            var handler = new HttpClientHandler();
            X509Certificate2 privateCertificate = CertificateLoader.GetCertificate(Endpoint.full_node, _API_CertificateFolder);
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
        /// <summary>
        /// Show the current network (eg mainnet) and network prefix (eg XCH)
        /// </summary>
        /// <returns></returns>
        public async Task GetAllMempoolItems_Async()
        {
            string response = await SendCustomMessage_Async("get_all_mempool_items");
            GetAllMempoolItems_Response json = JsonSerializer.Deserialize<GetAllMempoolItems_Response>(response);
            return;
        }
        /// <summary>
        /// finds a coin record in the blockchain. 
        /// Warning: This is barely useful, since it does not include spent coins!
        /// </summary>
        public async Task<GetCoinRecordByName_Response> GetCoinRecordByName_Async(GetCoinRecordByName_RPC name)
        {
            string response = await SendCustomMessage_Async("get_coin_record_by_name", name.ToString());
            GetCoinRecordByName_Response json = JsonSerializer.Deserialize<GetCoinRecordByName_Response>(response);
            return json;
        }
        public async Task<GetCoinRecordsByNames_Response> GetCoinRecordsByNames_Async(GetCoinRecordsByNames_RPC name)
        {
            string response = await SendCustomMessage_Async("get_coin_records_by_names", name.ToString());
            GetCoinRecordsByNames_Response json = JsonSerializer.Deserialize<GetCoinRecordsByNames_Response>(response);
            return json;
        }
    }
}
