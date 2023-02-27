using CHIA_RPC.FullNode_RPC_NS;
using CHIA_RPC.Wallet_RPC_NS.Wallet_NS;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace Chia_Client_API.Clients_NS
{
    public static class FullNodeApi
    {
        static FullNodeApi()
        {
            //initialize http client with proper certificate
            var handler = new HttpClientHandler();
            X509Certificate2 privateCertificate = CertificateLoader.GetCertificate(Endpoint.full_node);
            handler.ServerCertificateCustomValidationCallback = (requestMessage, certificate, chain, policyErrors) => true;
            handler.ClientCertificates.Add(privateCertificate);
            _Client = new HttpClient(handler);
        }
        private static HttpClient _Client;
        /// <summary>
        /// with this function you can execute any RPC against the wallet api. it is internally used by the library
        /// </summary>
        /// <param name="function"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public static async Task<string> SendCustomMessage_Async(string function, string json = " { } ")
        {
            using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://" + GlobalVar.API_TargetIP + ":8555/" + function))
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
        public static string SendCustomMessage_Sync(string function, string json = " { } ")
        {
            Task<string> data = Task.Run(() => SendCustomMessage_Async(function, json));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Show the current network (eg mainnet) and network prefix (eg XCH)
        /// </summary>
        /// <returns></returns>
        public async static Task GetAllMempoolItems_Async()
        {
            string response = await SendCustomMessage_Async("get_all_mempool_items");
            GetAllMempoolItems_Response json = JsonSerializer.Deserialize<GetAllMempoolItems_Response>(response);
            return;
        }
        /// <summary>
        /// finds a coin record in the blockchain. 
        /// Warning: This is barely useful, since it does not include spent coins!
        /// </summary>
        public async static Task<GetCoinRecordByName_Response> GetCoinRecordByName_Async(GetCoinRecordByName_RPC name)
        {
            string response = await SendCustomMessage_Async("get_coin_record_by_name", name.ToString());
            GetCoinRecordByName_Response json = JsonSerializer.Deserialize<GetCoinRecordByName_Response>(response);
            return json;
        }
        public async static Task<GetCoinRecordsByNames_Response> GetCoinRecordsByNames_Async(GetCoinRecordsByNames_RPC name)
        {
            string response = await SendCustomMessage_Async("get_coin_records_by_names", name.ToString());
            GetCoinRecordsByNames_Response json = JsonSerializer.Deserialize<GetCoinRecordsByNames_Response>(response);
            return json;
        }
    }
}
