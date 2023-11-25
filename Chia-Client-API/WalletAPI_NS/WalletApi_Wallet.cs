using Chia_Client_API.Helpers_NS;
using CHIA_RPC.General_NS;
using CHIA_RPC.HelperFunctions_NS;
using CHIA_RPC.Objects_NS;
using CHIA_RPC.Wallet_NS.CATsAndTrading_NS;
using CHIA_RPC.Wallet_NS.NFT_NS;
using CHIA_RPC.Wallet_NS.Wallet_NS;
using CHIA_RPC.Wallet_NS.WalletManagement_NS;


namespace Chia_Client_API.WalletAPI_NS
{
    /// <summary>
    /// The Wallet_RPC_Client class provides the ability to interact with a Chia wallet. 
    /// It is part of the Chia RPC (Remote Procedure Call) API, which allows external 
    /// applications to interface with the Chia network.
    /// </summary>
    /// <remarks>
    /// This class is used to send commands and receive responses from the Chia wallet. 
    /// Commands include creating transactions, signing transactions, checking balance, 
    /// and other wallet-related operations. This class is also responsible for handling 
    /// any potential errors or exceptions that may occur during the communication process 
    /// with the wallet. 
    /// <br/>
    /// Note: The Wallet RPC Client can only interact with the wallet it is connected to. 
    /// Make sure the connection details provided are for the correct wallet you intend to 
    /// work with.
    /// </remarks>
    public partial class Wallet_RPC_Client
    {
        /// <summary>
        /// Create a signed transaction from the given wallet<br/><br/>
        /// WARNING: Due to lacking documentation may not be implemented correctly
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#create_signed_transaction"/></remarks>
        /// <returns></returns>
        public async Task<GetTransaction_Response> CreateSignedTransaction_Async(CreateSignedTransaction_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("create_signed_transaction", rpc.ToString());
            ActionResult<GetTransaction_Response> deserializationResult = GetTransaction_Response.LoadResponseFromString(responseJson);
            GetTransaction_Response response = new ();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
        }

        /// <summary>
        /// Create a signed transaction from the given wallet<br/><br/>
        /// WARNING: Due to lacking documentation may not be implemented correctly
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#create_signed_transaction"/></remarks>
        /// <returns></returns>
        public GetTransaction_Response CreateSignedTransaction_Sync(CreateSignedTransaction_RPC rpc)
        {
            Task<GetTransaction_Response> data = Task.Run(() => CreateSignedTransaction_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Delete notifications, with the option to specify IDs from which to delete
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#delete_notifications"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<Success_Response> DeleteNotifications_Async(DeleteNotifications_RPC rpc)
        {
            string rpcString = rpc?.ToString() ?? "";
            string responseJson = await SendCustomMessage_Async("delete_notifications", rpcString);
            ActionResult<Success_Response> deserializationResult = Success_Response.LoadResponseFromString(responseJson);
            Success_Response response = new ();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
        }

        /// <summary>
        /// Delete notifications, with the option to specify IDs from which to delete
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#delete_notifications"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public Success_Response DeleteNotifications_Sync(DeleteNotifications_RPC? rpc = null)
        {
            Task<Success_Response> data = Task.Run(() => DeleteNotifications_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Delete all transactions that have yet to be confirmed on the blockchain from the given wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#delete_unconfirmed_transactions"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<Success_Response> DeleteUnconfirmedTransactions_Async(WalletID_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("delete_unconfirmed_transactions", rpc.ToString());
            ActionResult<Success_Response> deserializationResult = Success_Response.LoadResponseFromString(responseJson);
            Success_Response response = new ();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
        }

        /// <summary>
        /// Delete all transactions that have yet to be confirmed on the blockchain from the given wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#delete_unconfirmed_transactions"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public Success_Response DeleteUnconfirmedTransactions_Sync(WalletID_RPC rpc)
        {
            Task<Success_Response> data = Task.Run(() => DeleteUnconfirmedTransactions_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Increase the derivation index
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#extend_derivation_index"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<Index_Response> ExtendDerivationIndex_Async(Index_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("extend_derivation_index", rpc.ToString());
            ActionResult<Index_Response> deserializationResult = Index_Response.LoadResponseFromString(responseJson);
            Index_Response response = new ();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
        }

        /// <summary>
        /// Increase the derivation index
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#extend_derivation_index"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public Index_Response ExtendDerivationIndex_Sync(Index_RPC rpc)
        {
            Task<Index_Response> data = Task.Run(() => ExtendDerivationIndex_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Obtain coin records from a list of coin names
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_coin_records_by_names"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetCoinRecords_Response> GetCoinRecordsByNames_Async(GetCoinRecordsByNames_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_coin_records_by_names", rpc.ToString());
            ActionResult<GetCoinRecords_Response> deserializationResult = GetCoinRecords_Response.LoadResponseFromString(responseJson);
            GetCoinRecords_Response response = new ();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
        }

        /// <summary>
        /// Obtain coin records from a list of coin names
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_coin_records_by_names"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetCoinRecords_Response GetCoinRecordsByNames_Sync(GetCoinRecordsByNames_RPC rpc)
        {
            Task<GetCoinRecords_Response> data = Task.Run(() => GetCoinRecordsByNames_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Obtain the current derivation index for the current wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_current_derivation_index"/></remarks>
        /// <returns></returns>
        public async Task<Index_Response> GetCurrentDerivationIndex_Async()
        {
            string responseJson = await SendCustomMessage_Async("get_current_derivation_index");
            ActionResult<Index_Response> deserializationResult = Index_Response.LoadResponseFromString(responseJson);
            Index_Response response = new ();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
        }

        /// <summary>
        /// Obtain the current derivation index for the current wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_current_derivation_index"/></remarks>
        /// <returns></returns>
        public Index_Response GetCurrentDerivationIndex_Sync()
        {
            Task<Index_Response> data = Task.Run(() => GetCurrentDerivationIndex_Async());
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Show the total amount that has been farmed
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_farmed_amount"/></remarks>
        /// <returns></returns>
        public async Task<GetFarmedAmount_Response> GetFarmedAmount_Async()
        {
            string responseJson = await SendCustomMessage_Async("get_farmed_amount");
            ActionResult<GetFarmedAmount_Response> deserializationResult = GetFarmedAmount_Response.LoadResponseFromString(responseJson);
            GetFarmedAmount_Response response = new ();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
        }

        /// <summary>
        /// Show the total amount that has been farmed
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_farmed_amount"/></remarks>
        /// <returns></returns>
        public GetFarmedAmount_Response GetFarmedAmount_Sync()
        {
            Task<GetFarmedAmount_Response> data = Task.Run(() => GetFarmedAmount_Async());
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Get the next address in the HD tree, with the option to show the latest address
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_next_address"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetNextAddress_Response> GetNextAddress_Async(GetNextAddress_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_next_address", rpc.ToString());
            ActionResult<GetNextAddress_Response> deserializationResult = GetNextAddress_Response.LoadResponseFromString(responseJson);
            GetNextAddress_Response response = new ();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
        }

        /// <summary>
        /// Get the next address in the HD tree, with the option to show the latest address
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_next_address"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetNextAddress_Response GetNextAddress_Sync(GetNextAddress_RPC rpc)
        {
            Task<GetNextAddress_Response> data = Task.Run(() => GetNextAddress_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /*
        public GetNextAddress_Response? GetWalletAddresses_Sync(GetWalletAddresses_RPC rpc)
        {
            Task<GetNextAddress_Response?> data = Task.Run(() => GetWalletAddresses_Async(rpc));
            data.Wait();
            return data.Data;
        }
        public async Task<GetNextAddress_Response?> GetWalletAddresses_Async(GetWalletAddresses_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_wallet_addresses", rpc.ToString());
            GetNextAddress_Response? deserializedObject = GetNextAddress_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        */
        /// <summary>
        /// Obtain current notifications
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_notifications"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetNotifications_Response> GetNotifications_Async(GetNotifications_RPC rpc)
        {
            string rpcString = rpc?.ToString() ?? "";
            string responseJson = await SendCustomMessage_Async("get_notifications", rpcString);
            ActionResult<GetNotifications_Response> deserializationResult = GetNotifications_Response.LoadResponseFromString(responseJson);
            GetNotifications_Response response = new ();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
        }

        /// <summary>
        /// Obtain current notifications
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_notifications"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetNotifications_Response GetNotifications_Sync(GetNotifications_RPC? rpc = null)
        {
            Task<GetNotifications_Response> data = Task.Run(() => GetNotifications_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Get all spendable coins, with various possible filters
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_spendable_coins"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetSpendableCoins_Response> GetSpendableCoins_Async(GetSpendableCoins_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_spendable_coins", rpc.ToString());
            ActionResult<GetSpendableCoins_Response> deserializationResult = GetSpendableCoins_Response.LoadResponseFromString(responseJson);
            GetSpendableCoins_Response response = new ();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
        }

        /// <summary>
        /// Get all spendable coins, with various possible filters
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_spendable_coins"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetSpendableCoins_Response GetSpendableCoins_Sync(GetSpendableCoins_RPC rpc)
        {
            Task<GetSpendableCoins_Response> data = Task.Run(() => GetSpendableCoins_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Get a transaction's details from its ID
        /// </summary>
        /// <remarks>
        /// <b>WARNING:</b> The transaction history is not deterministic due to heuristics we use to counter privacy features of the blockchain.<br/>
        /// this means, a couple of details cannot be fetched fully:<br/>
        /// - The transaction IDs can and will change if you resync the wallet<br/>
        /// - The transaction time is a <b>rough</b> estimate. When an offer is accepted, the individual offer transactions have different created times
        /// - For your offers that a 3rd Party accepted, the incoming coins are beeing marked as incoming transaction, not as incoming coin<br/>
        /// - When cancelling offers, the cancellation Transactions are beeing shown as transaction, not as trade<br/>
        /// - Offers are split into multiple transactions on the corresponding wallets.<br/>
        /// - Offer Transactions do not share the same ids. To match them up, it is best to keep the offer files.<br/>
        /// - Transactions which are not kept in XCH and have a fee will cause a second Transaction in the XCH Wallet<br/>
        /// For accurate records, you should keep a local record of transactions (TXs) and the Offer files made. <br/><br/>
        /// <see href="https://docs.chia.net/wallet-rpc#get_transaction"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetTransaction_Response> GetTransaction_Async(TransactionID_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_transaction", rpc.ToString());
            ActionResult<GetTransaction_Response> deserializationResult = GetTransaction_Response.LoadResponseFromString(responseJson);
            GetTransaction_Response response = new ();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
        }

        /// <summary>
        /// Get a transaction's details from its ID
        /// </summary>
        /// <remarks>
        /// <b>WARNING:</b> The transaction history is not deterministic due to heuristics we use to counter privacy features of the blockchain.<br/>
        /// this means, a couple of details cannot be fetched fully:<br/>
        /// - The transaction IDs can and will change if you resync the wallet<br/>
        /// - The transaction time is a <b>rough</b> estimate. When an offer is accepted, the individual offer transactions have different created times
        /// - For your offers that a 3rd Party accepted, the incoming coins are beeing marked as incoming transaction, not as incoming coin<br/>
        /// - When cancelling offers, the cancellation Transactions are beeing shown as transaction, not as trade<br/>
        /// - Offers are split into multiple transactions on the corresponding wallets.<br/>
        /// - Offer Transactions do not share the same ids. To match them up, it is best to keep the offer files.<br/>
        /// - Transactions which are not kept in XCH and have a fee will cause a second Transaction in the XCH Wallet<br/>
        /// For accurate records, you should keep a local record of transactions (TXs) and the Offer files made. <br/><br/>
        /// <see href="https://docs.chia.net/wallet-rpc#get_transaction"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetTransaction_Response GetTransaction_Sync(TransactionID_RPC rpc)
        {
            Task<GetTransaction_Response> data = Task.Run(() => GetTransaction_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Get all transactions for a given wallet
        /// </summary>
        /// <remarks>
        /// <b>WARNING:</b> The transaction history is not deterministic due to heuristics we use to counter privacy features of the blockchain.<br/>
        /// this means, a couple of details cannot be fetched fully:<br/>
        /// - The transaction IDs can and will change if you resync the wallet<br/>
        /// - The transaction time is a <b>rough</b> estimate. When an offer is accepted, the individual offer transactions have different created times
        /// - For your offers that a 3rd Party accepted, the incoming coins are beeing marked as incoming transaction, not as incoming coin<br/>
        /// - When cancelling offers, the cancellation Transactions are beeing shown as transaction, not as trade<br/>
        /// - Offers are split into multiple transactions on the corresponding wallets.<br/>
        /// - Offer Transactions do not share the same ids. To match them up, it is best to keep the offer files.<br/>
        /// - Transactions which are not kept in XCH and have a fee will cause a second Transaction in the XCH Wallet<br/>
        /// For accurate records, you should keep a local record of transactions (TXs) and the Offer files made. <br/><br/>
        /// <see href="https://docs.chia.net/wallet-rpc#get_transactions"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetTransactions_Response> GetTransactions_Async(GetTransactions_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_transactions", rpc.ToString());
            ActionResult<GetTransactions_Response> deserializationResult = GetTransactions_Response.LoadResponseFromString(responseJson);
            GetTransactions_Response response = new ();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
        }


        /// <summary>
        /// Get all transactions for a given wallet
        /// </summary>
        /// <remarks>
        /// <b>WARNING:</b> The transaction history is not deterministic due to heuristics we use to counter privacy features of the blockchain.<br/>
        /// this means, a couple of details cannot be fetched fully:<br/>
        /// - The transaction IDs can and will change if you resync the wallet<br/>
        /// - The transaction time is a <b>rough</b> estimate. When an offer is accepted, the individual offer transactions have different created times
        /// - For your offers that a 3rd Party accepted, the incoming coins are beeing marked as incoming transaction, not as incoming coin<br/>
        /// - When cancelling offers, the cancellation Transactions are beeing shown as transaction, not as trade<br/>
        /// - Offers are split into multiple transactions on the corresponding wallets.<br/>
        /// - Offer Transactions do not share the same ids. To match them up, it is best to keep the offer files.<br/>
        /// - Transactions which are not kept in XCH and have a fee will cause a second Transaction in the XCH Wallet<br/>
        /// For accurate records, you should keep a local record of transactions (TXs) and the Offer files made. <br/><br/>
        /// <see href="https://docs.chia.net/wallet-rpc#get_transactions"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetTransactions_Response GetTransactions_Sync(GetTransactions_RPC rpc)
        {
            Task<GetTransactions_Response> data = Task.Run(() => GetTransactions_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Get all transactions for a given wallet
        /// </summary>
        /// <remarks>
        /// <b>WARNING:</b> The transaction history is not deterministic due to heuristics we use to counter privacy features of the blockchain.<br/>
        /// this means, a couple of details cannot be fetched fully:<br/>
        /// - The transaction IDs can and will change if you resync the wallet<br/>
        /// - The transaction time is a <b>rough</b> estimate. When an offer is accepted, the individual offer transactions have different created times
        /// - For your offers that a 3rd Party accepted, the incoming coins are beeing marked as incoming transaction, not as incoming coin<br/>
        /// - When cancelling offers, the cancellation Transactions are beeing shown as transaction, not as trade<br/>
        /// - Offers are split into multiple transactions on the corresponding wallets.<br/>
        /// - Offer Transactions do not share the same ids. To match them up, it is best to keep the offer files.<br/>
        /// - Transactions which are not kept in XCH and have a fee will cause a second Transaction in the XCH Wallet<br/>
        /// For accurate records, you should keep a local record of transactions (TXs) and the Offer files made. <br/><br/>
        /// <see href="https://docs.chia.net/wallet-rpc#get_transactions"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetTransactionCount_Response> GetTransactionCount_Async(WalletID_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_transaction_count", rpc.ToString());
            ActionResult<GetTransactionCount_Response> deserializationResult = GetTransactionCount_Response.LoadResponseFromString(responseJson);
            GetTransactionCount_Response response = new ();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
        }

        /// <summary>
        /// Obtain the number of transactions for a wallet
        /// </summary>
        /// <remarks>
        /// <b>WARNING:</b> The transaction history is not deterministic due to heuristics we use to counter privacy features of the blockchain.<br/>
        /// this means, a couple of details cannot be fetched fully:<br/>
        /// - The transaction IDs can and will change if you resync the wallet<br/>
        /// - The transaction time is a <b>rough</b> estimate. When an offer is accepted, the individual offer transactions have different created times
        /// - For your offers that a 3rd Party accepted, the incoming coins are beeing marked as incoming transaction, not as incoming coin<br/>
        /// - When cancelling offers, the cancellation Transactions are beeing shown as transaction, not as trade<br/>
        /// - Offers are split into multiple transactions on the corresponding wallets.<br/>
        /// - Offer Transactions do not share the same ids. To match them up, it is best to keep the offer files.<br/>
        /// - Transactions which are not kept in XCH and have a fee will cause a second Transaction in the XCH Wallet<br/>
        /// For accurate records, you should keep a local record of transactions (TXs) and the Offer files made. <br/><br/>
        /// <see href="https://docs.chia.net/wallet-rpc#get_transaction_count"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetTransactionCount_Response GetTransactionCount_Sync(WalletID_RPC rpc)
        {
            Task<GetTransactionCount_Response> data = Task.Run(() => GetTransactionCount_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Obtain the memo for the specified transaction
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_transaction_memo"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<Memo_Response> GetTransactionMemo_Async(TransactionID_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_transaction_memo", rpc.ToString());
            ActionResult<Memo_Response> deserializationResult = Memo_Response.LoadResponseFromString(responseJson);
            Memo_Response response = new ();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
        }

        /// <summary>
        /// Obtain the memo for the specified transaction
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_transaction_memo"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public Memo_Response GetTransactionMemo_Sync(TransactionID_RPC rpc)
        {
            Task<Memo_Response> data = Task.Run(() => GetTransactionMemo_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Obtain the balance (and related info) from a wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_wallet_balance"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetWalletBalance_Response> GetWalletBalance_Async(WalletID_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_wallet_balance", rpc.ToString());
            ActionResult<GetWalletBalance_Response> deserializationResult = GetWalletBalance_Response.LoadResponseFromString(responseJson);
            GetWalletBalance_Response response = new ();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
        }

        /// <summary>
        /// Obtain the balance (and related info) from a wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_wallet_balance"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetWalletBalance_Response GetWalletBalance_Sync(WalletID_RPC rpc)
        {
            Task<GetWalletBalance_Response> data = Task.Run(() => GetWalletBalance_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Select coins from a given wallet that add up to at least the specified amount
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#select_coins"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<SelectCoins_Response> SelectCoins_Async(SelectCoins_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("select_coins", rpc.ToString());
            ActionResult<SelectCoins_Response> deserializationResult = SelectCoins_Response.LoadResponseFromString(responseJson);
            SelectCoins_Response response = new ();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
        }

        /// <summary>
        /// Select coins from a given wallet that add up to at least the specified amount
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#select_coins"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public SelectCoins_Response SelectCoins_Sync(SelectCoins_RPC rpc)
        {
            Task<SelectCoins_Response> data = Task.Run(() => SelectCoins_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Send a notification to a specified puzzle hash
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#send_notification"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<SendNotification_Response> SendNotification_Async(SendNotification_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("send_notification", rpc.ToString());
            ActionResult<SendNotification_Response> deserializationResult = SendNotification_Response.LoadResponseFromString(responseJson);
            SendNotification_Response response = new ();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
        }

        /// <summary>
        /// Send a notification to a specified puzzle hash
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#send_notification"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public SendNotification_Response SendNotification_Sync(SendNotification_RPC rpc)
        {
            Task<SendNotification_Response> data = Task.Run(() => SendNotification_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Send a (chia) transaction
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#send_transaction"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetTransaction_Response> SendTransaction_Async(SendTransaction_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("send_transaction", rpc.ToString());
            ActionResult<GetTransaction_Response> deserializationResult = GetTransaction_Response.LoadResponseFromString(responseJson);
            GetTransaction_Response response = new ();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
        }

        /// <summary>
        /// Send a (chia) transaction
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#send_transaction"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetTransaction_Response SendTransaction_Sync(SendTransaction_RPC rpc)
        {
            Task<GetTransaction_Response> data = Task.Run(() => SendTransaction_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Send multiple chia transactions from a given wallet<br/><br/>
        /// WARNING: due to missing documentation, endpoint may not be implemented correctly
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#send_transaction_multi"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetTransaction_Response> SendTransactionMulti_Async(SendTransactionMulti_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("send_transaction_multi", rpc.ToString());
            ActionResult<GetTransaction_Response> deserializationResult = GetTransaction_Response.LoadResponseFromString(responseJson);
            GetTransaction_Response response = new ();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
        }

        /// <summary>
        /// Send multiple chia transactions from a given wallet<br/><br/>
        /// WARNING: due to missing documentation, endpoint may not be implemented correctly
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#send_transaction_multi"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetTransaction_Response SendTransactionMulti_Sync(SendTransactionMulti_RPC rpc)
        {
            Task<GetTransaction_Response> data = Task.Run(() => SendTransactionMulti_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Sign a message using an XCH address without incurring an on-chain transaction
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#sign_message_by_address"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<SignMessage_Response> SignMessageByAddress_Async(SignMessageByAddress_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("sign_message_by_address", rpc.ToString());
            ActionResult<SignMessage_Response> deserializationResult = SignMessage_Response.LoadResponseFromString(responseJson);
            SignMessage_Response response = new ();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
        }

        /// <summary>
        /// Sign a message using an XCH address without incurring an on-chain transaction
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#sign_message_by_address"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public SignMessage_Response SignMessageByAddress_Sync(SignMessageByAddress_RPC rpc)
        {
            Task<SignMessage_Response> data = Task.Run(() => SignMessageByAddress_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Sign a message using a DID or NFT ID without incurring an on-chain transaction
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#sign_message_by_id"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<SignMessage_Response> SignMessageByID_Async(SignMessageByID_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("sign_message_by_id", rpc.ToString());
            ActionResult<SignMessage_Response> deserializationResult = SignMessage_Response.LoadResponseFromString(responseJson);
            SignMessage_Response response = new ();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
        }

        /// <summary>
        /// Sign a message using a DID or NFT ID without incurring an on-chain transaction
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#sign_message_by_id"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public SignMessage_Response SignMessageByID_Sync(SignMessageByID_RPC rpc)
        {
            Task<SignMessage_Response> data = Task.Run(() => SignMessageByID_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Given a public key, message and signature, verify if it is valid.<br/><br/>
        /// WARNING: Due to missing Documentation this endpoint is not implemented correctly
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#verify_signature"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<SignMessage_Response> VerifySignature_Async(VerifySignature_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("verify_signature", rpc.ToString());
            ActionResult<SignMessage_Response> deserializationResult = SignMessage_Response.LoadResponseFromString(responseJson);
            SignMessage_Response response = new ();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
        }

        /// <summary>
        /// Given a public key, message and signature, verify if it is valid.<br/><br/>
        /// WARNING: Due to missing Documentation this endpoint is not implemented correctly
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#verify_signature"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public SignMessage_Response VerifySignature_Sync(VerifySignature_RPC rpc)
        {
            Task<SignMessage_Response> data = Task.Run(() => VerifySignature_Async(rpc));
            data.Wait();
            return data.Result;
        }
        
    }
}
