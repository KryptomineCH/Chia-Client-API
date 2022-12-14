using CHIA_RPC.Objects;
using CHIA_RPC.Wallet_RPC_NS.WalletNode_NS;
using System.Text.Json;

namespace Chia_Client_API.Wallet_NS.WalletAPI_NS
{
    public static partial class WalletApi
    {
        /// <summary>
        /// Show whether the current wallet is syncing or synced
        /// </summary>
        /// <returns></returns>
        public async static Task<GetSyncStatus_Response> GetSyncStatus()
        {
            string response = await SendCustomMessage("get_sync_status");
            GetSyncStatus_Response json = JsonSerializer.Deserialize<GetSyncStatus_Response>(response);
            return json;
        }
        /// <summary>
        /// Show the block height to which the current wallet is synced
        /// </summary>
        /// <returns></returns>
        public async static Task<GetHeightInfo_Response> GetHeightInfo()
        {
            string response = await SendCustomMessage("get_height_info");
            GetHeightInfo_Response json = JsonSerializer.Deserialize<GetHeightInfo_Response>(response);
            return json;
        }
        /// <summary>
        /// Pushes a transaction / spend bundle to the mempool and blockchain. Returns whether the spend bundle was successfully included into the mempool.
        /// </summary>
        /// <remarks>
        /// due to insufficient documentation, this request point may not be implemented correctly
        /// </remarks>
        /// <param name="bundle"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async static Task<PushTx_Response> PushTx(SpendBundle spendBundle)
        {
            PushTx_RPC rpc = new PushTx_RPC { spend_bundle = spendBundle };
            string response = await SendCustomMessage("push_tx", rpc.ToString());
            PushTx_Response json = JsonSerializer.Deserialize<PushTx_Response>(response);
            return json;
        }
        /// <summary>
        /// due to insufficient documentation, this request point is not implemented yet
        /// </summary>
        /// <param name="bundles"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async static Task<PushTx_Response> PushTransactions(SpendBundle[] bundles)
        {
            PushTransactions_RPC rpc = new PushTransactions_RPC { transactions = bundles };
            string response = await SendCustomMessage("push_tx", rpc.ToString());
            PushTx_Response json = JsonSerializer.Deserialize<PushTx_Response>(response);
            return json;
        }
        /// <summary>
        /// Show the current network (eg mainnet) and network prefix (eg XCH)
        /// </summary>
        /// <returns></returns>
        public async static Task<GetNetworkInfo_Response> GetNetworkInfo()
        {
            string response = await SendCustomMessage("get_network_info");
            GetNetworkInfo_Response json = JsonSerializer.Deserialize<GetNetworkInfo_Response>(response);
            return json;
        }
    }
}
