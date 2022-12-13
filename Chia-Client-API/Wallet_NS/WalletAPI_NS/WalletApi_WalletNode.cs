using Chia_Client_API.Wallet_NS.WalletApiResponses_NS;
using CHIA_RPC.Objects;
using System.Text.Json;

namespace Chia_Client_API.Wallet_NS.WalletAPI_NS
{
    public static partial class WalletApi
    {
        public async static Task<get_sync_status> GetSyncStatus()
        {
            string response = await SendCustomMessage("get_sync_status");
            get_sync_status json = JsonSerializer.Deserialize<get_sync_status>(response);
            return json;
        }
        public async static Task<get_height_info> GetHeightInfo()
        {
            string response = await SendCustomMessage("get_height_info");
            get_height_info json = JsonSerializer.Deserialize<get_height_info>(response);
            return json;
        }
        /// <summary>
        /// due to insufficient documentation, this request point is not implemented yet
        /// </summary>
        /// <param name="bundle"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async static Task<bool> PushTx(SpendBundle bundle)
        {
            throw new NotImplementedException("due to insufficient documentation, this request point is not implemented yet");
        }
        /// <summary>
        /// due to insufficient documentation, this request point is not implemented yet
        /// </summary>
        /// <param name="bundles"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async static Task<bool> PushTransactions(SpendBundle[] bundles)
        {
            throw new NotImplementedException("due to insufficient documentation, this request point is not implemented yet");
        }
        public async static Task<get_network_info> GetNetworkInfo()
        {
            string response = await SendCustomMessage("get_network_info");
            get_network_info json = JsonSerializer.Deserialize<get_network_info>(response);
            return json;
        }
    }
}
