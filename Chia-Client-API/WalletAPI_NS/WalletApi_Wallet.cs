using CHIA_RPC.General;
using CHIA_RPC.Objects_NS;
using CHIA_RPC.Wallet_RPC_NS.Wallet_NS;
using System.Text.Json;

namespace Chia_Client_API.WalletAPI_NS
{
    public partial class Wallet_RPC_Client
    {
        /// <summary>
        /// Obtain the balance (and related info) from a wallet
        /// </summary>
        /// <param name="walletID_RPC"></param>
        /// <returns></returns>
        public async Task<GetWalletBalance_Response> GetWalletBalance_Async(WalletID_RPC walletID_RPC)
        {
            string response = await SendCustomMessage_Async("get_wallet_balance", walletID_RPC.ToString());
            GetWalletBalance_Response json = JsonSerializer.Deserialize<GetWalletBalance_Response>(response);
            return json;
        }
        /// <summary>
        /// Obtain the balance (and related info) from a wallet
        /// </summary>
        /// <param name="walletID_RPC"></param>
        /// <returns></returns>
        public GetWalletBalance_Response GetWalletBalance_Sync(WalletID_RPC walletID_RPC)
        {
            Task<GetWalletBalance_Response> data = Task.Run(() => GetWalletBalance_Async(walletID_RPC));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Get a transaction's details from its ID
        /// </summary>
        /// <param name="transactionID_RPC"></param>
        /// <returns></returns>
        public async Task<GetTransaction_Response> GetTransaction_Async(TransactionID_RPC transactionID_RPC)
        {
            string response = await SendCustomMessage_Async("get_transaction", transactionID_RPC.ToString());
            GetTransaction_Response json = JsonSerializer.Deserialize<GetTransaction_Response>(response);
            return json;
        }
        /// <summary>
        /// Get a transaction's details from its ID
        /// </summary>
        /// <param name="transactionID_RPC"></param>
        /// <returns></returns>
        public GetTransaction_Response GetTransaction_Sync(TransactionID_RPC transactionID_RPC)
        {
            Task<GetTransaction_Response> data = Task.Run(() => GetTransaction_Async(transactionID_RPC));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Get all transactions for a given wallet
        /// </summary>
        /// <param name="walletID_RPC"></param>
        /// <returns></returns>
        public async Task<GetTransactions_Response> GetTransactions_Async(WalletID_RPC walletID_RPC)
        {
            string response = await SendCustomMessage_Async("get_transactions", walletID_RPC.ToString());
            GetTransactions_Response json = JsonSerializer.Deserialize<GetTransactions_Response>(response);
            return json;
        }
        /// <summary>
        /// Get all transactions for a given wallet
        /// </summary>
        /// <param name="walletID_RPC"></param>
        /// <returns></returns>
        public GetTransactions_Response GetTransactions_Sync(WalletID_RPC walletID_RPC)
        {
            Task<GetTransactions_Response> data = Task.Run(() => GetTransactions_Async(walletID_RPC));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Obtain the number of transactions for a wallet
        /// </summary>
        /// <param name="walletID_RPC"></param>
        /// <returns></returns>
        public async Task<GetTransactionCount_Response> GetTransactionCount_Async(WalletID_RPC walletID_RPC)
        {
            string response = await SendCustomMessage_Async("get_transaction_count", walletID_RPC.ToString());
            GetTransactionCount_Response json = JsonSerializer.Deserialize<GetTransactionCount_Response>(response);
            return json;
        }
        /// <summary>
        /// Obtain the number of transactions for a wallet
        /// </summary>
        /// <param name="walletID_RPC"></param>
        /// <returns></returns>
        public GetTransactionCount_Response GetTransactionCount_Sync(WalletID_RPC walletID_RPC)
        {
            Task<GetTransactionCount_Response> data = Task.Run(() => GetTransactionCount_Async(walletID_RPC));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Get the next address in the HD tree, with the option to show the latest address
        /// </summary>
        /// <param name="getNextAddress_RPC"></param>
        /// <returns></returns>
        public async Task<GetNextAddress_Response> GetNextAddress_Async(GetNextAddress_RPC getNextAddress_RPC)
        {
            string response = await SendCustomMessage_Async("get_next_address", getNextAddress_RPC.ToString());
            GetNextAddress_Response json = JsonSerializer.Deserialize<GetNextAddress_Response>(response);
            return json;
        }
        /// <summary>
        /// Get the next address in the HD tree, with the option to show the latest address
        /// </summary>
        /// <param name="getNextAddress_RPC"></param>
        /// <returns></returns>
        public GetNextAddress_Response GetNextAddress_Sync(GetNextAddress_RPC getNextAddress_RPC)
        {
            Task<GetNextAddress_Response> data = Task.Run(() => GetNextAddress_Async(getNextAddress_RPC));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Send a (chia) transaction
        /// </summary>
        /// <param name="wallet_Send_XCH_RPC"></param>
        /// <returns></returns>
        public async Task<GetTransaction_Response> SendTransaction_Async(SendXCH_RPC wallet_Send_XCH_RPC)
        {
            string response = await SendCustomMessage_Async("send_transaction", wallet_Send_XCH_RPC.ToString());
            GetTransaction_Response json = JsonSerializer.Deserialize<GetTransaction_Response>(response);
            return json;
        }
        /// <summary>
        /// Send a (chia) transaction
        /// </summary>
        /// <param name="wallet_Send_XCH_RPC"></param>
        /// <returns></returns>
        public GetTransaction_Response SendTransaction_Sync(SendXCH_RPC wallet_Send_XCH_RPC)
        {
            Task<GetTransaction_Response> data = Task.Run(() => SendTransaction_Async(wallet_Send_XCH_RPC));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// waits for a transaction to fully execute or fail
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="cancellation"></param>
        /// <param name="timeoutInMinutes"></param>
        /// <returns></returns>
        public async Task<GetTransaction_Response> AwaitTransactionToComplete_Async(
            Transaction transaction,
            CancellationToken cancellation, double timeoutInMinutes = 5)
        {
            TransactionID_RPC transactionID_RPC = new TransactionID_RPC
            {
                transaction_id = transaction.name
            };
            return await AwaitTransactionToComplete_Async(transactionID_RPC, cancellation, timeoutInMinutes).ConfigureAwait(false);
        }
        /// <summary>
        /// waits for a transaction to fully execute or fail
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="cancellation"></param>
        /// <param name="timeoutInMinutes"></param>
        /// <returns></returns>
        public GetTransaction_Response AwaitTransactionToComplete_Sync(
            Transaction transaction, 
            CancellationToken cancellation, double timeoutInMinutes = 5)
        {
            Task<GetTransaction_Response> data = Task.Run(() => AwaitTransactionToComplete_Async(transaction, cancellation, timeoutInMinutes));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// waits for a transaction to fully execute or fail
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="cancellation"></param>
        /// <param name="timeoutInMinutes"></param>
        /// <returns></returns>
        public async Task<GetTransaction_Response> AwaitTransactionToComplete_Async(
            TransactionID_RPC transactionID_RPC,
            CancellationToken cancellation, double timeoutInMinutes = 5)
        {
            DateTime startTime = DateTime.Now;
            TimeSpan timeOut = TimeSpan.FromMinutes(timeoutInMinutes);
            GetTransaction_Response response = null;
            while (!cancellation.IsCancellationRequested)
            {
                response = await GetTransaction_Async(transactionID_RPC);
                if (response.success && response.transaction.confirmed)
                {
                    return response;
                }
                Task.Delay(1000, cancellation);
                if (DateTime.Now > startTime + timeOut)
                {
                    break;
                }
            }
            return response;
        }
        /// <summary>
        /// waits for a transaction to fully execute or fail
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="cancellation"></param>
        /// <param name="timeoutInMinutes"></param>
        /// <returns></returns>
        public GetTransaction_Response AwaitTransactionToComplete_Sync(
            TransactionID_RPC transactionID_RPC, 
            CancellationToken cancellation, double timeoutInMinutes = 5)
        {
            Task<GetTransaction_Response> data = Task.Run(() => AwaitTransactionToComplete_Async(transactionID_RPC, cancellation, timeoutInMinutes));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// not well documented. pleas use custom rpc
        /// </summary>
        /// <param name="wallet_Send_XCH_RPC"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<GetTransactions_Response> SendTransactionMulti_Async(SendXCH_RPC wallet_Send_XCH_RPC)
        {
            throw new NotImplementedException("not implemented yet due to documentation uncertainties. Please use SendTransaction instead");
            string response = await SendCustomMessage_Async("send_transaction", wallet_Send_XCH_RPC.ToString());
            GetTransactions_Response json = JsonSerializer.Deserialize<GetTransactions_Response>(response);
            return json;
        }
        /// <summary>
        /// not well documented. pleas use custom rpc
        /// </summary>
        /// <param name="wallet_Send_XCH_RPC"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public GetTransactions_Response SendTransactionMulti_Sync(SendXCH_RPC wallet_Send_XCH_RPC)
        {
            Task<GetTransactions_Response> data = Task.Run(() => SendTransactionMulti_Async(wallet_Send_XCH_RPC));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Show the total amount that has been farmed
        /// </summary>
        /// <returns></returns>
        public async Task<GetFarmedAmount_Response> GetFarmedAmount_Async()
        {
            string response = await SendCustomMessage_Async("get_farmed_amount");
            GetFarmedAmount_Response json = JsonSerializer.Deserialize<GetFarmedAmount_Response>(response);
            return json;
        }
        /// <summary>
        /// Show the total amount that has been farmed
        /// </summary>
        /// <returns></returns>
        public GetFarmedAmount_Response GetFarmedAmount_Sync()
        {
            Task<GetFarmedAmount_Response> data = Task.Run(() => GetFarmedAmount_Async());
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// not yet implemented
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<GetFarmedAmount_Response> CreateSignedTransaction_Async()
        {
            throw new NotImplementedException();
            string response = await SendCustomMessage_Async("get_farmed_amount");
            GetFarmedAmount_Response json = JsonSerializer.Deserialize<GetFarmedAmount_Response>(response);
            return json;
        }
        /// <summary>
        /// not yet implemented
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public GetFarmedAmount_Response CreateSignedTransaction_Sync()
        {
            Task<GetFarmedAmount_Response> data = Task.Run(() => CreateSignedTransaction_Async());
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Delete all transactions that have yet to be confirmed on the blockchain from the given wallet
        /// </summary>
        /// <param name="walletID"></param>
        /// <returns></returns>
        public async Task<Success_Response> DeleteUnconfirmedTransactions_Async(ulong walletID)
        {
            string response = await SendCustomMessage_Async("delete_unconfirmed_transactions", "{\"wallet_id\": "+walletID+"}");
            Success_Response success = JsonSerializer.Deserialize<Success_Response>(response);
            return success;
        }
        /// <summary>
        /// Delete all transactions that have yet to be confirmed on the blockchain from the given wallet
        /// </summary>
        /// <param name="walletID"></param>
        /// <returns></returns>
        public Success_Response DeleteUnconfirmedTransactions_Sync(ulong walletID)
        {
            Task<Success_Response> data = Task.Run(() => DeleteUnconfirmedTransactions_Async(walletID));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Select coins from a given wallet that add up to at least the specified amount
        /// </summary>
        /// <param name="selectCoins_RPC"></param>
        /// <returns></returns>
        public async Task<SelectCoins_Response> SelectCoins_Async(SelectCoins_RPC selectCoins_RPC)
        {
            string response = await SendCustomMessage_Async("select_coins", selectCoins_RPC.ToString());
            SelectCoins_Response json = JsonSerializer.Deserialize<SelectCoins_Response>(response);
            return json;
        }
        /// <summary>
        /// Select coins from a given wallet that add up to at least the specified amount
        /// </summary>
        /// <param name="selectCoins_RPC"></param>
        /// <returns></returns>
        public SelectCoins_Response SelectCoins_Sync(SelectCoins_RPC selectCoins_RPC)
        {
            Task<SelectCoins_Response> data = Task.Run(() => SelectCoins_Async(selectCoins_RPC));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Get all spendable coins, with various possible filters
        /// </summary>
        /// <param name="getSpendableCoins_RPC"></param>
        /// <returns></returns>
        public async Task<GetSpendableCoins_Response> GetSpendableCoins_Async(GetSpendableCoins_RPC getSpendableCoins_RPC)
        {
            string response = await SendCustomMessage_Async("get_spendable_coins", getSpendableCoins_RPC.ToString());
            GetSpendableCoins_Response json = JsonSerializer.Deserialize<GetSpendableCoins_Response>(response);
            return json;
        }
        /// <summary>
        /// Get all spendable coins, with various possible filters
        /// </summary>
        /// <param name="getSpendableCoins_RPC"></param>
        /// <returns></returns>
        public GetSpendableCoins_Response GetSpendableCoins_Sync(GetSpendableCoins_RPC getSpendableCoins_RPC)
        {
            Task<GetSpendableCoins_Response> data = Task.Run(() => GetSpendableCoins_Async(getSpendableCoins_RPC));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Obtain coin records from a list of coin names
        /// </summary>
        /// <param name="getCoinRecordsByNames_RPC"></param>
        /// <returns></returns>
        public async Task<GetCoinRecordsByNames_Response> GetCoinRecordsByNames_Async(GetCoinRecordsByNames_RPC getCoinRecordsByNames_RPC)
        {
            string response = await SendCustomMessage_Async("get_coin_records_by_names", getCoinRecordsByNames_RPC.ToString());
            GetCoinRecordsByNames_Response json = JsonSerializer.Deserialize<GetCoinRecordsByNames_Response>(response);
            return json;
        }
        /// <summary>
        /// Obtain coin records from a list of coin names
        /// </summary>
        /// <param name="getCoinRecordsByNames_RPC"></param>
        /// <returns></returns>
        public GetCoinRecordsByNames_Response GetCoinRecordsByNames_Sync(GetCoinRecordsByNames_RPC getCoinRecordsByNames_RPC)
        {
            Task<GetCoinRecordsByNames_Response> data = Task.Run(() => GetCoinRecordsByNames_Async(getCoinRecordsByNames_RPC));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Obtain the current derivation index for the current wallet
        /// </summary>
        /// <returns></returns>
        public async Task<GetCurrentDerivationIndec_Response> GetCurrentDerivationIndex_Async()
        {
            string response = await SendCustomMessage_Async("get_current_derivation_index");
            GetCurrentDerivationIndec_Response json = JsonSerializer.Deserialize<GetCurrentDerivationIndec_Response>(response);
            return json;
        }
        /// <summary>
        /// Obtain the current derivation index for the current wallet
        /// </summary>
        /// <returns></returns>
        public GetCurrentDerivationIndec_Response GetCurrentDerivationIndex_Sync()
        {
            Task<GetCurrentDerivationIndec_Response> data = Task.Run(() => GetCurrentDerivationIndex_Async());
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Increase the derivation index
        /// </summary>
        /// <param name="extendDerivationIndex_RPC"></param>
        /// <returns></returns>
        public async Task<ExtendDerivationIndex_Response> ExtendDerivationIndex_Async(ExtendDerivationIndex_RPC extendDerivationIndex_RPC)
        {
            string response = await SendCustomMessage_Async("extend_derivation_index", extendDerivationIndex_RPC.ToString());
            ExtendDerivationIndex_Response json = JsonSerializer.Deserialize<ExtendDerivationIndex_Response>(response);
            return json;
        }
        /// <summary>
        /// Increase the derivation index
        /// </summary>
        /// <param name="extendDerivationIndex_RPC"></param>
        /// <returns></returns>
        public ExtendDerivationIndex_Response ExtendDerivationIndex_Sync(ExtendDerivationIndex_RPC extendDerivationIndex_RPC)
        {
            Task<ExtendDerivationIndex_Response> data = Task.Run(() => ExtendDerivationIndex_Async(extendDerivationIndex_RPC));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Obtain current notifications
        /// </summary>
        /// <param name="getNotifications_RPC"></param>
        /// <returns></returns>
        public async Task<GetNotifications_Response> GetNotifications_Async(GetNotifications_RPC getNotifications_RPC = null)
        {
            string response;
            if (getNotifications_RPC != null) { response = await SendCustomMessage_Async("get_notifications", getNotifications_RPC.ToString()); }
            else { response = await SendCustomMessage_Async("get_notifications"); }
            GetNotifications_Response json = JsonSerializer.Deserialize<GetNotifications_Response>(response);
            return json;
        }
        /// <summary>
        /// Obtain current notifications
        /// </summary>
        /// <param name="getNotifications_RPC"></param>
        /// <returns></returns>
        public GetNotifications_Response GetNotifications_Sync(GetNotifications_RPC getNotifications_RPC = null)
        {
            Task<GetNotifications_Response> data = Task.Run(() => GetNotifications_Async(getNotifications_RPC));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Delete notifications, with the option to specify IDs from which to delete
        /// </summary>
        /// <param name="deleteNotifications_RPC"></param>
        /// <returns></returns>
        public async Task<Success_Response> DeleteNotifications_Async(DeleteNotifications_RPC deleteNotifications_RPC = null)
        {
            string response;
            if (deleteNotifications_RPC != null) { response = await SendCustomMessage_Async("delete_notifications", deleteNotifications_RPC.ToString()); }
            else { response = await SendCustomMessage_Async("delete_notifications"); }
            Success_Response json = JsonSerializer.Deserialize<Success_Response>(response);
            return json;
        }
        /// <summary>
        /// Delete notifications, with the option to specify IDs from which to delete
        /// </summary>
        /// <param name="deleteNotifications_RPC"></param>
        /// <returns></returns>
        public Success_Response DeleteNotifications_Sync(DeleteNotifications_RPC deleteNotifications_RPC = null)
        {
            Task<Success_Response> data = Task.Run(() => DeleteNotifications_Async(deleteNotifications_RPC));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Send a notification to a specified puzzle hash
        /// </summary>
        /// <param name="sendNotification_RPC"></param>
        /// <returns></returns>
        public async Task<SendNotification_Response> SendNotification_Async(SendNotification_RPC sendNotification_RPC)
        {
            string response = await SendCustomMessage_Async("send_notification", sendNotification_RPC.ToString());
            SendNotification_Response success = JsonSerializer.Deserialize<SendNotification_Response>(response);
            return success;
        }
        /// <summary>
        /// Send a notification to a specified puzzle hash
        /// </summary>
        /// <param name="sendNotification_RPC"></param>
        /// <returns></returns>
        public SendNotification_Response SendNotification_Sync(SendNotification_RPC sendNotification_RPC)
        {
            Task<SendNotification_Response> data = Task.Run(() => SendNotification_Async(sendNotification_RPC));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Sign a message using an XCH address without incurring an on-chain transaction
        /// </summary>
        /// <param name="signMessageByAddress_RPC"></param>
        /// <returns></returns>
        public async Task<SignMessage_Response> SignMessageByAddress_Async(SignMessageByAddress_RPC signMessageByAddress_RPC)
        {
            string response = await SendCustomMessage_Async("sign_message_by_address", signMessageByAddress_RPC.ToString());
            SignMessage_Response success = JsonSerializer.Deserialize<SignMessage_Response>(response);
            return success;
        }
        /// <summary>
        /// Sign a message using an XCH address without incurring an on-chain transaction
        /// </summary>
        /// <param name="signMessageByAddress_RPC"></param>
        /// <returns></returns>
        public SignMessage_Response SignMessageByAddress_Sync(SignMessageByAddress_RPC signMessageByAddress_RPC)
        {
            Task<SignMessage_Response> data = Task.Run(() => SignMessageByAddress_Async(signMessageByAddress_RPC));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Sign a message using a DID or NFT ID without incurring an on-chain transaction
        /// </summary>
        /// <param name="signMessageByID_RPC"></param>
        /// <returns></returns>
        public async Task<SignMessage_Response> SignMessageByID_Async(SignMessageByID_RPC signMessageByID_RPC)
        {
            string response = await SendCustomMessage_Async("sign_message_by_id", signMessageByID_RPC.ToString());
            SignMessage_Response success = JsonSerializer.Deserialize<SignMessage_Response>(response);
            return success;
        }
        /// <summary>
        /// Sign a message using a DID or NFT ID without incurring an on-chain transaction
        /// </summary>
        /// <param name="signMessageByID_RPC"></param>
        /// <returns></returns>
        public SignMessage_Response SignMessageByID_Sync(SignMessageByID_RPC signMessageByID_RPC)
        {
            Task<SignMessage_Response> data = Task.Run(() => SignMessageByID_Async(signMessageByID_RPC));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Given a public key, message and signature, verify if it is valid.
        /// </summary>
        /// <param name="verifySignature_RPC"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task VerifySignature_Async(VerifySignature_RPC verifySignature_RPC)
        {
            throw new NotImplementedException("due to incomplete documentation, this function is not yet implemented");
        }
        /// <summary>
        /// Given a public key, message and signature, verify if it is valid.
        /// </summary>
        /// <param name="verifySignature_RPC"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public void VerifySignature_Sync(VerifySignature_RPC verifySignature_RPC)
        {
            Task.Run(() => VerifySignature_Async(verifySignature_RPC)).Wait();
        }
    }
}
