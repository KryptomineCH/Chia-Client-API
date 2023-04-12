
using CHIA_RPC.FullNode_NS;
using CHIA_RPC.General_NS;
using System.Text.Json;

namespace Chia_Client_API.FullNodeAPI_NS
{
    public partial class FullNode_RPC_Client
    {
        /// <summary>
        /// Show the current network (eg mainnet) and network prefix (eg XCH)
        /// </summary>
        /// <returns></returns>
        public async Task<GetNetworkInfo_Response> GetNetworkInfo_Async()
        {
            string response = await SendCustomMessage_Async("get_network_info");
            GetNetworkInfo_Response json = JsonSerializer.Deserialize<GetNetworkInfo_Response>(response);
            return json;
        }
        /// <summary>
        /// Show the current network (eg mainnet) and network prefix (eg XCH)
        /// </summary>
        /// <returns></returns>
        public GetNetworkInfo_Response GetNetworkInfo_Sync()
        {
            Task<GetNetworkInfo_Response> data = Task.Run(() => GetNetworkInfo_Async());
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
        public async Task<GetCoinRecords_Response> GetCoinRecordsByNames_Async(GetCoinRecordsByNames_RPC name)
        {
            string response = await SendCustomMessage_Async("get_coin_records_by_names", name.ToString());
            GetCoinRecords_Response json = JsonSerializer.Deserialize<GetCoinRecords_Response>(response);
            return json;
        }
    }
}
