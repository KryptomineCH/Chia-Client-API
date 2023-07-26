using CHIA_RPC.FullNode_NS;
using CHIA_RPC.General_NS;
using CHIA_RPC.Wallet_NS.Wallet_NS;

namespace Chia_Client_API.FullNodeAPI_NS
{
    /// <summary>
    /// Provides the client which is used to make requests against a full node
    /// </summary>
    public partial class FullNode_RPC_Client
    {
        // custom code
        /// <summary>
        /// waits for a transaction to get completed
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="cancel"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public async Task<GetCoinRecords_Response?> AwaitTransactionComplete_Async(GetTransaction_Response transaction, CancellationToken cancel, TimeSpan? timeout = null)
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
                GetCoinRecords_Response? response = await GetCoinRecordsByPuzzleHash_Async(rpc);
                if (response == null) return null;
                if (response.error == null && response.coin_records.Length > 0)
                {
                    ulong? test = response.coin_records[0].confirmed_block_index;
                    return response;
                }
            }
            return null;
        }
        /// <summary>
        /// waits for a transaction to get completed
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="cancel"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public GetCoinRecords_Response? AwaitTransactionComplete_Sync(GetTransaction_Response transaction, CancellationToken cancel, TimeSpan? timeout = null)
        {
            Task<GetCoinRecords_Response?> data = Task.Run(() => AwaitTransactionComplete_Async(transaction, cancel, timeout));
            data.Wait();
            return data.Result;
        }

        // code as per documentation
        /// <summary>
        /// Retrieves the additions and removals (state transitions) for a certain block. Returns coin records for each addition and removal
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_additions_and_removals"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetAdditionsAndRemovals_Response?> GetAdditionsAndRemovals_Async(HeaderHash_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_additions_and_removals", rpc.ToString());
            GetAdditionsAndRemovals_Response? deserializedObject = GetAdditionsAndRemovals_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Retrieves the additions and removals (state transitions) for a certain block. Returns coin records for each addition and removal
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_additions_and_removals"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetAdditionsAndRemovals_Response? GetAdditionsAndRemovals_Sync(HeaderHash_RPC rpc)
        {
            Task<GetAdditionsAndRemovals_Response?> data = Task.Run(() => GetAdditionsAndRemovals_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Returns all items in the mempool
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_all_mempool_items"/></remarks>
        /// <returns></returns>
        public async Task<GetAllMempoolItems_Response?> GetAllMempoolItems_Async()
        {
            string responseJson = await SendCustomMessage_Async("get_all_mempool_items");
            GetAllMempoolItems_Response? deserializedObject = GetAllMempoolItems_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Returns all items in the mempool
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_all_mempool_items"/></remarks>
        /// <returns></returns>
        public GetAllMempoolItems_Response? GetAllMempoolItems_Sync()
        {
            Task<GetAllMempoolItems_Response?> data = Task.Run(() => GetAllMempoolItems_Async());
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Returns a list of all transaction IDs (spend bundle hashes) in the mempool
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_all_mempool_tx_ids"/></remarks>
        /// <returns></returns>
        public async Task<GetAllMempoolTxIDs_Response?> GetAllMempoolTxIDs_Async()
        {
            string responseJson = await SendCustomMessage_Async("get_all_mempool_tx_ids");
            GetAllMempoolTxIDs_Response? deserializedObject = GetAllMempoolTxIDs_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Returns a list of all transaction IDs (spend bundle hashes) in the mempool
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_all_mempool_tx_ids"/></remarks>
        /// <returns></returns>
        public GetAllMempoolTxIDs_Response? GetAllMempoolTxIDs_Sync()
        {
            Task<GetAllMempoolTxIDs_Response?> data = Task.Run(() => GetAllMempoolTxIDs_Async());
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Retrieves an entire block as a Full Block by header hash
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_block"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetBlock_Response?> GetBlock_Async(HeaderHash_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_block", rpc.ToString());
            GetBlock_Response? deserializedObject = GetBlock_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Retrieves an entire block as a Full Block by header hash
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_block"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetBlock_Response? GetBlock_Sync(HeaderHash_RPC rpc)
        {
            Task<GetBlock_Response?> data = Task.Run(() => GetBlock_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Retrieves a summary of the current state of the blockchain and full node
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_blockchain_state"/></remarks>
        /// <returns></returns>
        public async Task<GetBlockchainState_Response?> GetBlockchainState_Async()
        {
            string responseJson = await SendCustomMessage_Async("get_blockchain_state");
            GetBlockchainState_Response? deserializedObject = GetBlockchainState_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Retrieves a summary of the current state of the blockchain and full node
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_blockchain_state"/></remarks>
        /// <returns></returns>
        public GetBlockchainState_Response? GetBlockchainState_Sync()
        {
            Task<GetBlockchainState_Response?> data = Task.Run(() => GetBlockchainState_Async());
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Gets a list of full blocks by height<br/><br/>
        /// IMPORTANT: There might be multiple blocks at each height. To find out which one is in the blockchain, use <see cref="GetBlockRecordByHeight_Async(Height_RPC)"/>
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_blocks"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetBlocks_Response?> GetBlocks_Async(GetBlocks_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_blocks", rpc.ToString());
            GetBlocks_Response? deserializedObject = GetBlocks_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Gets a list of full blocks by height<br/><br/>
        /// IMPORTANT: There might be multiple blocks at each height. To find out which one is in the blockchain, use <see cref="GetBlockRecordByHeight_Sync(Height_RPC)"/>
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_blocks"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetBlocks_Response? GetBlocks_Sync(GetBlocks_RPC rpc)
        {
            Task<GetBlocks_Response?> data = Task.Run(() => GetBlocks_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Gets various metrics for the blockchain's blocks<br/><br/>
        /// Currently shows:<br/>
        /// - compact blocks<br/>
        /// - hint count<br/>
        /// - uncompact blocks
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_block_count_metrics"/></remarks>
        /// <returns></returns>
        public async Task<GetBlockCountMetrics_Response?> GetBlockCountMetrics_Async()
        {
            string responseJson = await SendCustomMessage_Async("get_block_count_metrics");
            GetBlockCountMetrics_Response? deserializedObject = GetBlockCountMetrics_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Gets various metrics for the blockchain's blocks<br/><br/>
        /// Currently shows:<br/>
        /// - compact blocks<br/>
        /// - hint count<br/>
        /// - uncompact blocks
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_block_count_metrics"/></remarks>
        /// <returns></returns>
        public GetBlockCountMetrics_Response? GetBlockCountMetrics_Sync()
        {
            Task<GetBlockCountMetrics_Response?> data = Task.Run(() => GetBlockCountMetrics_Async());
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Retrieves the coins for given parent coin IDs; by default only returns unspent coins
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_coin_records_by_parent_ids"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetCoinRecords_Response?> GetCoinRecordsByParentIDs_Async(GetCoinRecordsByParentIDs_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_coin_records_by_parent_ids", rpc.ToString());
            GetCoinRecords_Response? deserializedObject = GetCoinRecords_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Retrieves the coins for given parent coin IDs; by default only returns unspent coins
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_coin_records_by_parent_ids"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetCoinRecords_Response? GetCoinRecordsByParentIDs_Sync(GetCoinRecordsByParentIDs_RPC rpc)
        {
            Task<GetCoinRecords_Response?> data = Task.Run(() => GetCoinRecordsByParentIDs_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Retrieves a block record by header hash
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_block_record"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetBlockRecord_Response?> GetBlockRecord_Async(HeaderHash_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_block_record", rpc.ToString());
            GetBlockRecord_Response? deserializedObject = GetBlockRecord_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Retrieves a block record by header hash
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_block_record"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetBlockRecord_Response? GetBlockRecord_Sync(HeaderHash_RPC rpc)
        {
            Task<GetBlockRecord_Response?> data = Task.Run(() => GetBlockRecord_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Retrieves block records in a range
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_block_records"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetBlockRecords_Response?> GetBlockRecords_Async(StartEnd_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_block_records", rpc.ToString());
            GetBlockRecords_Response? deserializedObject = GetBlockRecords_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Retrieves block records in a range
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_block_records"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetBlockRecords_Response? GetBlockRecords_Sync(StartEnd_RPC rpc)
        {
            Task<GetBlockRecords_Response?> data = Task.Run(() => GetBlockRecords_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Retrieves a block record by height
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_block_record_by_height"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetBlockRecord_Response?> GetBlockRecordByHeight_Async(Height_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_block_record_by_height", rpc.ToString());
            GetBlockRecord_Response? deserializedObject = GetBlockRecord_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Retrieves a block record by height
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_block_record_by_height"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetBlockRecord_Response? GetBlockRecordByHeight_Sync(Height_RPC rpc)
        {
            Task<GetBlockRecord_Response?> data = Task.Run(() => GetBlockRecordByHeight_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Retrieves every coin that was spent in a block. Requires the header hash of the block to retrieve
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_block_spends"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetBlockSpends_Response?> GetBlockSpends_Async(HeaderHash_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_block_spends", rpc.ToString());
            GetBlockSpends_Response? deserializedObject = GetBlockSpends_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Retrieves every coin that was spent in a block. Requires the header hash of the block to retrieve
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_block_spends"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetBlockSpends_Response? GetBlockSpends_Sync(HeaderHash_RPC rpc)
        {
            Task<GetBlockSpends_Response?> data = Task.Run(() => GetBlockSpends_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Retrieves coins by hint; by default only returns unspent coins
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_coin_records_by_hint"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetCoinRecords_Response?> GetCoinRecordsByHint_Async(GetCoinRecordsByHint_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_coin_records_by_hint", rpc.ToString());
            GetCoinRecords_Response? deserializedObject = GetCoinRecords_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Retrieves coins by hint; by default only returns unspent coins
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_coin_records_by_hint"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetCoinRecords_Response? GetCoinRecordsByHint_Sync(GetCoinRecordsByHint_RPC rpc)
        {
            Task<GetCoinRecords_Response?> data = Task.Run(() => GetCoinRecordsByHint_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Retrieves a list of coin records with a certain puzzle hash
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_coin_records_by_puzzle_hash"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetCoinRecords_Response?> GetCoinRecordsByPuzzleHash_Async(GetCoinRecordsByPuzzleHash_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_coin_records_by_puzzle_hash", rpc.ToString());
            GetCoinRecords_Response? deserializedObject = GetCoinRecords_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Retrieves a list of coin records with a certain puzzle hash
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_coin_records_by_puzzle_hash"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetCoinRecords_Response? GetCoinRecordsByPuzzleHash_Sync(GetCoinRecordsByPuzzleHash_RPC rpc)
        {
            Task<GetCoinRecords_Response?> data = Task.Run(() => GetCoinRecordsByPuzzleHash_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Retrieves the coins for a given puzzlehashes; by default only returns unspent coins
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_coin_records_by_puzzle_hashes"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetCoinRecords_Response?> GetCoinRecordsByPuzzleHashes_Async(GetCoinRecordsByPuzzleHashes_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_coin_records_by_puzzle_hashes", rpc.ToString());
            GetCoinRecords_Response? deserializedObject = GetCoinRecords_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Retrieves the coins for a given puzzlehashes; by default only returns unspent coins
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_coin_records_by_puzzle_hashes"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetCoinRecords_Response? GetCoinRecordsByPuzzleHashes_Sync(GetCoinRecordsByPuzzleHashes_RPC rpc)
        {
            Task<GetCoinRecords_Response?> data = Task.Run(() => GetCoinRecordsByPuzzleHashes_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Retrieves a coin record by its name or coin ID
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_coin_record_by_name"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetCoinRecordByName_Response?> GetCoinRecordByName_Async(GetCoinRecordByName_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_coin_record_by_name", rpc.ToString());
            GetCoinRecordByName_Response? deserializedObject = GetCoinRecordByName_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Retrieves a coin record by its name or coin ID
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_coin_record_by_name"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetCoinRecordByName_Response? GetCoinRecordByName_Sync(GetCoinRecordByName_RPC rpc)
        {
            Task<GetCoinRecordByName_Response?> data = Task.Run(() => GetCoinRecordByName_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Retrieves the coins for given coin IDs; by default only returns unspent coins
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_coin_records_by_names"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetCoinRecords_Response?> GetCoinRecordsByNames_Async(GetCoinRecordsByNames_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_coin_records_by_names", rpc.ToString());
            GetCoinRecords_Response? deserializedObject = GetCoinRecords_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Retrieves the coins for given coin IDs; by default only returns unspent coins
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_coin_records_by_names"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetCoinRecords_Response? GetCoinRecordsByNames_Sync(GetCoinRecordsByNames_RPC rpc)
        {
            Task<GetCoinRecords_Response?> data = Task.Run(() => GetCoinRecordsByNames_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Obtain an estimated fee for one or more targeted times for a transaction to be included in the blockchain.<br/><br/>
        /// Explicit conversions exist so you can call:<br/><br/>
        /// <code>
        /// GetFeeEstimate_RPC rpc = TimeSpan.FromMinutes(5);
        /// GetFeeEstimate_RPC rpc = new TimeSpan[] {TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(15)};
        /// GetFeeEstimate_Response? fees = Client.GetFeeEstimate_Sync(TimeSpan.FromMinutes(5));
        /// </code>
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_fee_estimate"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetFeeEstimate_Response?> GetFeeEstimate_Async(GetFeeEstimate_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_fee_estimate", rpc.ToString());
            GetFeeEstimate_Response? deserializedObject = GetFeeEstimate_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Obtain an estimated fee for one or more targeted times for a transaction to be included in the blockchain.<br/><br/>
        /// Explicit conversions exist so you can call:<br/><br/>
        /// <code>
        /// GetFeeEstimate_RPC rpc = TimeSpan.FromMinutes(5);
        /// GetFeeEstimate_RPC rpc = new TimeSpan[] {TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(15)};
        /// GetFeeEstimate_Response? fees = Client.GetFeeEstimate_Sync(TimeSpan.FromMinutes(5));
        /// </code>
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_fee_estimate"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetFeeEstimate_Response? GetFeeEstimate_Sync(GetFeeEstimate_RPC rpc)
        {
            Task<GetFeeEstimate_Response?> data = Task.Run(() => GetFeeEstimate_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Gets a mempool item by tx id
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_mempool_item_by_tx_id"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetMempoolItemByTxID_Response?> GetMempoolItemByTxID_Async(GetMempoolItemByTxID_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_mempool_item_by_tx_id", rpc.ToString());
            GetMempoolItemByTxID_Response? deserializedObject = GetMempoolItemByTxID_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Gets a mempool item by tx id
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_mempool_item_by_tx_id"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetMempoolItemByTxID_Response? GetMempoolItemByTxID_Sync(GetMempoolItemByTxID_RPC rpc)
        {
            Task<GetMempoolItemByTxID_Response?> data = Task.Run(() => GetMempoolItemByTxID_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Show the current network (eg mainnet) and network prefix (eg XCH)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_network_info"/></remarks>
        /// <returns></returns>
        public async Task<GetNetworkInfo_Response?> GetNetworkInfo_Async()
        {
            string responseJson = await SendCustomMessage_Async("get_network_info");
            GetNetworkInfo_Response? deserializedObject = GetNetworkInfo_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Show the current network (eg mainnet) and network prefix (eg XCH)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_network_info"/></remarks>
        /// <returns></returns>
        public GetNetworkInfo_Response? GetNetworkInfo_Sync()
        {
            Task<GetNetworkInfo_Response?> data = Task.Run(() => GetNetworkInfo_Async());
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Retrieves an estimate of the netspace, which is the total plotted space of all farmers, in bytes
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_network_space"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetNetworkSpace_Response?> GetNetworkSpace_Async(GetNetworkSpace_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_network_space", rpc.ToString());
            GetNetworkSpace_Response? deserializedObject = GetNetworkSpace_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Retrieves an estimate of the netspace, which is the total plotted space of all farmers, in bytes
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_network_space"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetNetworkSpace_Response? GetNetworkSpace_Sync(GetNetworkSpace_RPC rpc)
        {
            Task<GetNetworkSpace_Response?> data = Task.Run(() => GetNetworkSpace_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Retrieves a coin's spend record by its coin ID, sometimes referred to as "coin name". 
        /// Coin IDs can be calculated by hashing the coin: sha256(parent_coin + puzzle_hash + amount)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_puzzle_and_solution"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetPuzzleAndSolution_Response?> GetPuzzleAndSolution_Async(GetPuzzleAndSolution_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_puzzle_and_solution", rpc.ToString());
            GetPuzzleAndSolution_Response? deserializedObject = GetPuzzleAndSolution_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Retrieves a coin's spend record by its coin ID, sometimes referred to as "coin name". 
        /// Coin IDs can be calculated by hashing the coin: sha256(parent_coin + puzzle_hash + amount)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_puzzle_and_solution"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetPuzzleAndSolution_Response? GetPuzzleAndSolution_Sync(GetPuzzleAndSolution_RPC rpc)
        {
            Task<GetPuzzleAndSolution_Response?> data = Task.Run(() => GetPuzzleAndSolution_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Retrieves a recent signage point or end of slot
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_recent_signage_point_or_eos"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetRecentSignagePointOrEos_Response?> GetRecentSignagePointOrEos_Async(GetRecentSignagePointOrEos_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_recent_signage_point_or_eos", rpc.ToString());
            GetRecentSignagePointOrEos_Response? deserializedObject = GetRecentSignagePointOrEos_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Retrieves a recent signage point or end of slot
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_recent_signage_point_or_eos"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetRecentSignagePointOrEos_Response? GetRecentSignagePointOrEos_Sync(GetRecentSignagePointOrEos_RPC rpc)
        {
            Task<GetRecentSignagePointOrEos_Response?> data = Task.Run(() => GetRecentSignagePointOrEos_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Show all RPC endpoints. This endpoint is lightweight and can be used as a health check. However, a better option may be <see cref="HealthZ_Async"/>
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_routes"/></remarks>
        /// <returns></returns>
        public async Task<GetRoutes_Response?> GetRoutes_Async()
        {
            string responseJson = await SendCustomMessage_Async("get_routes");
            GetRoutes_Response? deserializedObject = GetRoutes_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Show all RPC endpoints. This endpoint is lightweight and can be used as a health check. However, a better option may be <see cref="HealthZ_Sync"/>
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_routes"/></remarks>
        /// <returns></returns>
        public GetRoutes_Response? GetRoutes_Sync()
        {
            Task<GetRoutes_Response?> data = Task.Run(() => GetRoutes_Async());
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Retrieves recent unfinished header blocks
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_unfinished_block_headers"/></remarks>
        /// <returns></returns>
        public async Task<GetUnfinishedBlockHeaders_Response?> GetUnfinishedBlockHeaders_Async()
        {
            string responseJson = await SendCustomMessage_Async("get_unfinished_block_headers");
            GetUnfinishedBlockHeaders_Response? deserializedObject = GetUnfinishedBlockHeaders_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Retrieves recent unfinished header blocks
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#get_unfinished_block_headers"/></remarks>
        /// <returns></returns>
        public GetUnfinishedBlockHeaders_Response? GetUnfinishedBlockHeaders_Sync()
        {
            Task<GetUnfinishedBlockHeaders_Response?> data = Task.Run(() => GetUnfinishedBlockHeaders_Async());
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Verifies that the RPC service is running
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#healthz"/></remarks>
        /// <returns></returns>
        public async Task<Success_Response?> HealthZ_Async()
        {
            string responseJson = await SendCustomMessage_Async("healthz");
            Success_Response? deserializedObject = Success_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Verifies that the RPC service is running
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#healthz"/></remarks>
        /// <returns></returns>
        public Success_Response? HealthZ_Sync()
        {
            Task<Success_Response?> data = Task.Run(() => HealthZ_Async());
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Pushes a transaction / spend bundle to the mempool and blockchain. Returns whether the spend bundle was successfully included into the mempool
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#push_tx"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<PushTx_Response?> PushTx_Async(PushTx_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("push_tx", rpc.ToString());
            PushTx_Response? deserializedObject = PushTx_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Pushes a transaction / spend bundle to the mempool and blockchain. Returns whether the spend bundle was successfully included into the mempool
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/full-node-rpc#push_tx"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public PushTx_Response? PushTx_Sync(PushTx_RPC rpc)
        {
            Task<PushTx_Response?> data = Task.Run(() => PushTx_Async(rpc));
            data.Wait();
            return data.Result;
        }
        
        /* Custom Functions */

    }
}
