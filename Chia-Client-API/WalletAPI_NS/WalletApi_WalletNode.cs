using CHIA_RPC.General_NS;
using CHIA_RPC.Objects_NS;
using CHIA_RPC.Wallet_NS.WalletNode_NS;
using System.Text.Json;

namespace Chia_Client_API.WalletAPI_NS
{
    public partial class Wallet_RPC_Client
    {
        /// <summary>
        /// Resync the current logged in wallet. The transaction and offer records will be kept
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#set_wallet_resync_on_startup"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<Success_Response> SetWalletResyncOnStartup_Async(SetWalletResyncOnStartup_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("set_wallet_resync_on_startup", rpc.ToString());
            Success_Response deserialzedObject = JsonSerializer.Deserialize<Success_Response>(responseJson);
            return deserialzedObject;
        }
        /// <summary>
        /// Resync the current logged in wallet. The transaction and offer records will be kept
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#set_wallet_resync_on_startup"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public Success_Response SetWalletResyncOnStartup_Sync(SetWalletResyncOnStartup_RPC rpc)
        {
            Task<Success_Response> data = Task.Run(() => SendCustomMessage_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Show whether the current wallet is syncing or synced
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_sync_status"/></remarks>
        /// <returns></returns>
        public async Task<GetWalletSyncStatus_Response> GetSyncStatus_Async()
        {
            string responseJson = await SendCustomMessage_Async("get_sync_status");
            GetWalletSyncStatus_Response deserialzedObject = JsonSerializer.Deserialize<GetWalletSyncStatus_Response>(responseJson);
            return deserialzedObject;
        }
        /// <summary>
        /// Show whether the current wallet is syncing or synced
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_sync_status"/></remarks>
        /// <returns></returns>
        public GetWalletSyncStatus_Response GetSyncStatus_Sync()
        {
            Task<GetWalletSyncStatus_Response> data = Task.Run(() => GetSyncStatus_Async());
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Show the block height to which the current wallet is synced
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_height_info"/></remarks>
        /// <returns></returns>
        public async Task<GetHeightInfo_Response> GetHeightInfo_Async()
        {
            string responseJson = await SendCustomMessage_Async("get_height_info");
            GetHeightInfo_Response deserialzedObject = JsonSerializer.Deserialize<GetHeightInfo_Response>(responseJson);
            return deserialzedObject;
        }
        /// <summary>
        /// Show the block height to which the current wallet is synced
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_height_info"/></remarks>
        /// <returns></returns>
        public GetHeightInfo_Response GetHeightInfo_Sync()
        {
            Task<GetHeightInfo_Response> data = Task.Run(() => GetHeightInfo_Async());
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Pushes a transaction / spend bundle to the mempool and blockchain. Returns whether the spend bundle was successfully included into the mempool.<br/><br/>
        /// Note: due to insufficient documentation, this request point may not be implemented correctly
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#push_tx"/></remarks>
        /// <param name="bundle"></param>
        /// <returns></returns>
        public async Task<PushTx_Response> PushTx_Async(SpendBundle spendBundle)
        {
            PushTx_RPC rpc = new PushTx_RPC { spend_bundle = spendBundle };
            string responseJson = await SendCustomMessage_Async("push_tx", rpc.ToString());
            PushTx_Response deserialzedObject = JsonSerializer.Deserialize<PushTx_Response>(responseJson);
            return deserialzedObject;
        }
        /// <summary>
        /// Pushes a transaction / spend bundle to the mempool and blockchain. Returns whether the spend bundle was successfully included into the mempool.<br/><br/>
        /// Note: due to insufficient documentation, this request point may not be implemented correctly
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#push_tx"/></remarks>
        /// <param name="bundle"></param>
        /// <returns></returns>
        public PushTx_Response PushTx_Sync(SpendBundle spendBundle)
        {
            Task<PushTx_Response> data = Task.Run(() => PushTx_Async(spendBundle));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Push multiple transactions to the blockchain<br/><br/>
        /// Note: due to insufficient documentation, this request point may not be implemented correctly
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#push_transactions"/></remarks>
        /// <param name="bundles"></param>
        /// <returns></returns>
        public async Task<PushTx_Response> PushTransactions_Async(SpendBundle[] bundles)
        {
            PushTransactions_RPC rpc = new PushTransactions_RPC { transactions = bundles };
            string responseJson = await SendCustomMessage_Async("push_tx", rpc.ToString());
            PushTx_Response deserialzedObject = JsonSerializer.Deserialize<PushTx_Response>(responseJson);
            return deserialzedObject;
        }
        /// <summary>
        /// Push multiple transactions to the blockchain<br/><br/>
        /// Note: due to insufficient documentation, this request point may not be implemented correctly
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#push_transactions"/></remarks>
        /// <param name="bundles"></param>
        /// <returns></returns>
        public PushTx_Response PushTransactions_Sync(SpendBundle[] bundles)
        {
            Task<PushTx_Response> data = Task.Run(() => PushTransactions_Async(bundles));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Show the timestamp for a given block height
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_timestamp_for_height"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetTimestampForHeight_Response> GetTimestampForHeight_Async(Height_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_timestamp_for_height", rpc.ToString());
            GetTimestampForHeight_Response deserialzedObject = JsonSerializer.Deserialize<GetTimestampForHeight_Response>(responseJson);
            return deserialzedObject;
        }
        /// <summary>
        /// Show the timestamp for a given block height
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_timestamp_for_height"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetTimestampForHeight_Response GetTimestampForHeight_Sync(Height_RPC rpc)
        {
            Task<GetTimestampForHeight_Response> data = Task.Run(() => GetTimestampForHeight_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Show the current network (eg mainnet) and network prefix (eg XCH)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_network_info"/></remarks>
        /// <returns></returns>
        public async Task<GetNetworkInfo_Response> GetNetworkInfo_Async()
        {
            string responseJson = await SendCustomMessage_Async("get_network_info");
            GetNetworkInfo_Response deserialzedObject = JsonSerializer.Deserialize<GetNetworkInfo_Response>(responseJson);
            return deserialzedObject;
        }
        /// <summary>
        /// Show the current network (eg mainnet) and network prefix (eg XCH)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_network_info"/></remarks>
        /// <returns></returns>
        public GetNetworkInfo_Response GetNetworkInfo_Sync()
        {
            Task<GetNetworkInfo_Response> data = Task.Run(() => GetNetworkInfo_Async());
            data.Wait();
            return data.Result;
        }

    }
}
