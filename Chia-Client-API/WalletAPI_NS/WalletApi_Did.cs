using CHIA_RPC.General_NS;
using CHIA_RPC.Wallet_NS.DID_NS;
using System.Text.Json;

namespace Chia_Client_API.WalletAPI_NS
{
    public partial class Wallet_RPC_Client
    {
        /// <summary>
        /// Functionality: Fetch the did-ID and coin_id (if applicable) settings for a given did-wallet
        /// Usage: chia rpc wallet[OPTIONS] did_get_did[REQUEST]
        /// </summary>
        /// <param name="rpc">the wallet id of the did wallet</param>
        /// <returns></returns>
        public async Task<DidGetDid_Response> DidGetDid_Async(WalletID_RPC rpc)
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
        public DidGetDid_Response DidGetDid_Sync(WalletID_RPC rpc)
        {
            Task<DidGetDid_Response> data = Task.Run(() => DidGetDid_Async(rpc));
            data.Wait();
            return data.Result;
        }
    }
}
