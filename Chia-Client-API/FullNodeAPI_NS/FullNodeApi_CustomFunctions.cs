using CHIA_RPC.FullNode_NS;
using CHIA_RPC.General_NS;
using CHIA_RPC.HelperFunctions_NS;
using CHIA_RPC.Wallet_NS.Wallet_NS;

namespace Chia_Client_API.FullNodeAPI_NS
{
    public abstract partial class FullNodeRpcBase
    {
        /// <summary>
        /// waits for a transaction to get completed
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="cancel"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public async Task<GetCoinRecords_Response> AwaitTransactionComplete_Async(GetTransaction_Response transaction, CancellationToken cancel, TimeSpan? timeout = null)
        {
            if (transaction.transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction.transaction));
            }
            if (transaction.transaction.additions == null || transaction.transaction.additions.Length == 0)
            {
                throw new ArgumentNullException(nameof(transaction.transaction.additions));
            }

            GetCoinRecordsByPuzzleHash_RPC rpc = new GetCoinRecordsByPuzzleHash_RPC(transaction.transaction.additions[0].puzzle_hash);
            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.MaxValue;
            if (timeout != null)
            {
                endTime = startTime.Add(timeout.Value);
            }

            while (!cancel.IsCancellationRequested && DateTime.Now < endTime)
            {
                string responseJson = await SendCustomMessageAsync("get_coin_records_by_puzzle_hash", rpc.ToString());
                ActionResult<GetCoinRecords_Response> deserializationResult = GetCoinRecords_Response.LoadResponseFromString(responseJson);
                GetCoinRecords_Response response = new GetCoinRecords_Response();

                if (deserializationResult.Data != null)
                {
                    response = deserializationResult.Data;
                    ulong? test = response.coin_records[0].confirmed_block_index;
                    return response;
                }
                else
                {
                    response.success = deserializationResult.Success;
                    response.error = deserializationResult.Error;
                    response.RawContent = deserializationResult.RawJson;
                    if (response.error == null && response.coin_records.Length > 0)
                    {
                        return response;
                    }
                }
            }
            return new GetCoinRecords_Response(); // Returns an empty response or consider throwing an exception if needed
        }

        /// <summary>
        /// waits for a transaction to get completed
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="cancel"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public GetCoinRecords_Response AwaitTransactionComplete_Sync(GetTransaction_Response transaction, CancellationToken cancel, TimeSpan? timeout = null)
        {
            Task<GetCoinRecords_Response> data = Task.Run(() => AwaitTransactionComplete_Async(transaction, cancel, timeout));
            data.Wait();
            return data.Result;
        }
    }
}
