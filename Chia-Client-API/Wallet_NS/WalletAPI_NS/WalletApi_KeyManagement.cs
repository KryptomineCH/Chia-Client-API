using Chia_Client_API.Wallet_NS.WalletApiResponses_NS;
using CHIA_RPC.General;
using CHIA_RPC.Wallet_RPC_NS.KeyManagement;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace Chia_Client_API.Wallet_NS.WalletAPI_NS
{
    public static partial class WalletApi
    {
        /// <summary>
        /// opens a given wallet
        /// </summary>
        /// <param name="fingerprint"></param>
        /// <returns></returns>
        public async static Task<LogIn_Response> LogIn(FingerPrint_RPC fingerprint)
        {
            string response = await SendCustomMessage("log_in", fingerprint.ToString());
            LogIn_Response json = JsonSerializer.Deserialize<LogIn_Response>(response);
            return json;
        }
        /// <summary>
        /// returns the currently active wallet
        /// </summary>
        /// <returns></returns>
        public async static Task<LogIn_Response> GetLoggedInFingerprint()
        {
            string response = await SendCustomMessage("get_logged_in_fingerprint");
            LogIn_Response json = JsonSerializer.Deserialize<LogIn_Response>(response);
            return json;
        }
        /// <summary>
        /// shows the subwallets of the currently active wallet
        /// </summary>
        /// <returns></returns>
        public async static Task<GetPublicKeys_Response> GetPublicKeys()
        {
            string response = await SendCustomMessage("get_public_keys");
            GetPublicKeys_Response json = JsonSerializer.Deserialize<GetPublicKeys_Response>(response);
            return json;
        }
        public async static Task<GetPrivateKey_Response> GetPrivateKey(FingerPrint_RPC fingerprint)
        {
            string response = await SendCustomMessage("get_private_key", fingerprint.ToString());
            GetPrivateKey_Response json = JsonSerializer.Deserialize<GetPrivateKey_Response>(response);
            return json;
        }
        public async static Task<GenerateMnemonic_Response> GenerateMnemonic()
        {
            string response = await SendCustomMessage("generate_mnemonic");
            GenerateMnemonic_Response json = JsonSerializer.Deserialize<GenerateMnemonic_Response>(response);
            return json;
        }
    }
}
