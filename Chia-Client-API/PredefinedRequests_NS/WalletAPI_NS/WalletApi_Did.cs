using CHIA_RPC.General;
using CHIA_RPC.Wallet_RPC_NS.DID_NS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Chia_Client_API.PredefinedRequests_NS.WalletAPI_NS
{
    public static partial class WalletApi
    {
        /// <summary>
        /// Functionality: Fetch the did-ID and coin_id (if applicable) settings for a given did-wallet
        /// Usage: chia rpc wallet[OPTIONS] did_get_did[REQUEST]
        /// </summary>
        /// <param name="rpc">the wallet id of the did wallet</param>
        /// <returns></returns>
        public async static Task<DidGetDid_Response> DidGetDid_Async(WalletID_RPC rpc)
        {
            string response = await SendCustomMessage_Async("did_get_did", rpc.ToString());
            DidGetDid_Response json = JsonSerializer.Deserialize<DidGetDid_Response>(response);
            return json;
        }
        /// <summary>
        /// Functionality: Fetch the did-ID and coin_id (if applicable) settings for a given did-wallet
        /// Usage: chia rpc wallet[OPTIONS] did_get_did[REQUEST]
        /// </summary>
        /// <param name="rpc">the wallet id of the did wallet</param>
        /// <returns></returns>
        public static DidGetDid_Response DidGetDid_Sync(WalletID_RPC rpc)
        {
            Task<DidGetDid_Response> data = Task.Run(() => DidGetDid_Async(rpc));
            data.Wait();
            return data.Result;
        }
    }
}
