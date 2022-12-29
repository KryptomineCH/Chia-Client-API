using CHIA_RPC.Wallet_RPC_NS.WalletNode_NS;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;

namespace Chia_Client_API.Wallet_NS.WalletAPI_NS
{
    public static partial class WalletApi
    {
        static WalletApi()
        {
            //initialize http client with proper certificate
            var handler = new HttpClientHandler();
            X509Certificate2 privateCertificate = CertificateLoader.GetCertificate(Endpoint.wallet);
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
        public static async Task<string> SendCustomMessage(string function, string json = " { } ")
        {
            using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://"+GlobalVar.API_TargetIP+":9256/" + function))
            {
                request.Content = new StringContent(json);
                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                var response = await _Client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync(); ;
            }
        }
        public static async Task<bool> AwaitWalletSync_Async(CancellationToken cancellation, 
            double timeoutSeconds = 60.0)
        {
            TimeSpan timeOut = TimeSpan.FromSeconds(timeoutSeconds);
            DateTime timeOutTracker = DateTime.Now;
            bool success = false;
            while(!cancellation.IsCancellationRequested)
            {
                GetSyncStatus_Response syncStatus = await WalletApi.GetSyncStatus();
                if (!syncStatus.success)
                {
                    throw new Exception("Error while obtaining sync status!");
                }
                else if(!syncStatus.syncing && !syncStatus.synced)
                {
                    // not synced and not syncing, check timeout
                    if (timeOutTracker + timeOut < DateTime.Now)
                    {
                        return false;
                    }
                    // timeout is not met, wait for the wallet to start syncing
                }
                else if(syncStatus.success && (syncStatus.syncing || syncStatus.synced))
                {
                    // currently syncing, so we want to extend the timeout
                    timeOutTracker = DateTime.Now;
                }
                if (syncStatus.synced) return true;
                await Task.Delay(1000); // wait 1 second
            }
            return success;
        }
    }
}
