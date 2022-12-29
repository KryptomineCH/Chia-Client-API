using CHIA_RPC.General;
using CHIA_RPC.Objects_NS;
using CHIA_RPC.Wallet_RPC_NS.Wallet_NS;
using System.Text.Json;

namespace Chia_Client_API.Wallet_NS.WalletAPI_NS
{
    public static partial class WalletApi
    {
        /// <summary>
        /// Obtain the balance (and related info) from a wallet
        /// </summary>
        /// <param name="walletID_RPC"></param>
        /// <returns></returns>
        public async static Task<GetWalletBalance_Response> GetWalletBalance(WalletID_RPC walletID_RPC)
        {
            string response = await SendCustomMessage("get_wallet_balance", walletID_RPC.ToString());
            GetWalletBalance_Response json = JsonSerializer.Deserialize<GetWalletBalance_Response>(response);
            return json;
        }
        /// <summary>
        /// Get a transaction's details from its ID
        /// </summary>
        /// <param name="transactionID_RPC"></param>
        /// <returns></returns>
        public async static Task<GetTransaction_Response> GetTransaction(TransactionID_RPC transactionID_RPC)
        {
            string response = await SendCustomMessage("get_transaction", transactionID_RPC.ToString());
            GetTransaction_Response json = JsonSerializer.Deserialize<GetTransaction_Response>(response);
            return json;
        }
        /// <summary>
        /// Get all transactions for a given wallet
        /// </summary>
        /// <param name="walletID_RPC"></param>
        /// <returns></returns>
        public async static Task<GetTransactions_Response> GetTransactions(WalletID_RPC walletID_RPC)
        {
            string response = await SendCustomMessage("get_transactions", walletID_RPC.ToString());
            GetTransactions_Response json = JsonSerializer.Deserialize<GetTransactions_Response>(response);
            return json;
        }
        /// <summary>
        /// Obtain the number of transactions for a wallet
        /// </summary>
        /// <param name="walletID_RPC"></param>
        /// <returns></returns>
        public async static Task<GetTransactionCount_Response> GetTransactionCount(WalletID_RPC walletID_RPC)
        {
            string response = await SendCustomMessage("get_transaction_count", walletID_RPC.ToString());
            GetTransactionCount_Response json = JsonSerializer.Deserialize<GetTransactionCount_Response>(response);
            return json;
        }
        /// <summary>
        /// Get the next address in the HD tree, with the option to show the latest address
        /// </summary>
        /// <param name="getNextAddress_RPC"></param>
        /// <returns></returns>
        public async static Task<GetNextAddress_Response> GetNextAddress(GetNextAddress_RPC getNextAddress_RPC)
        {
            string response = await SendCustomMessage("get_next_address", getNextAddress_RPC.ToString());
            GetNextAddress_Response json = JsonSerializer.Deserialize<GetNextAddress_Response>(response);
            return json;
        }
        /// <summary>
        /// Send a (chia) transaction
        /// </summary>
        /// <param name="wallet_Send_XCH_RPC"></param>
        /// <returns></returns>
        public async static Task<GetTransaction_Response> SendTransaction(SendXCH_RPC wallet_Send_XCH_RPC)
        {
            string response = await SendCustomMessage("send_transaction", wallet_Send_XCH_RPC.ToString());
            GetTransaction_Response json = JsonSerializer.Deserialize<GetTransaction_Response>(response);
            return json;
        }
        public static Task<GetTransaction_Response> AwaitTransactionToComplete(
            Transaction transaction,
            CancellationToken cancellation, double timeoutInMinutes = 5)
        {
            TransactionID_RPC transactionID_RPC = new TransactionID_RPC
            {
                transaction_id = transaction.name
            };
            return AwaitTransactionToComplete(transactionID_RPC, cancellation, timeoutInMinutes);
        }
        public async static Task<GetTransaction_Response> AwaitTransactionToComplete(
            TransactionID_RPC transactionID_RPC,
            CancellationToken cancellation, double timeoutInMinutes = 5)
        {
            DateTime startTime = DateTime.Now;
            TimeSpan timeOut = TimeSpan.FromMinutes(timeoutInMinutes);
            GetTransaction_Response response = null;
            while (!cancellation.IsCancellationRequested)
            {
                response = GetTransaction(transactionID_RPC).Result;
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
        /// not well documented. pleas use custom rpc
        /// </summary>
        /// <param name="wallet_Send_XCH_RPC"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async static Task<GetTransactions_Response> SendTransactionMulti(SendXCH_RPC wallet_Send_XCH_RPC)
        {
            throw new NotImplementedException("not implemented yet due to documentation uncertainties. Please use SendTransaction instead");
            string response = await SendCustomMessage("send_transaction", wallet_Send_XCH_RPC.ToString());
            GetTransactions_Response json = JsonSerializer.Deserialize<GetTransactions_Response>(response);
            return json;
        }
        /// <summary>
        /// Show the total amount that has been farmed
        /// </summary>
        /// <returns></returns>
        public async static Task<GetFarmedAmount_Response> GetFarmedAmount()
        {
            string response = await SendCustomMessage("get_farmed_amount");
            GetFarmedAmount_Response json = JsonSerializer.Deserialize<GetFarmedAmount_Response>(response);
            return json;
        }
        /// <summary>
        /// not yet implemented
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async static Task<GetFarmedAmount_Response> CreateSignedTransaction()
        {
            throw new NotImplementedException();
            string response = await SendCustomMessage("get_farmed_amount");
            GetFarmedAmount_Response json = JsonSerializer.Deserialize<GetFarmedAmount_Response>(response);
            return json;
        }
        /// <summary>
        /// Delete all transactions that have yet to be confirmed on the blockchain from the given wallet
        /// </summary>
        /// <param name="walletID"></param>
        /// <returns></returns>
        public async static Task<Success_Response> DeleteUnconfirmedTransactions(ulong walletID)
        {
            string response = await SendCustomMessage("delete_unconfirmed_transactions", "{\"wallet_id\": "+walletID+"}");
            Success_Response success = JsonSerializer.Deserialize<Success_Response>(response);
            return success;
        }
        /// <summary>
        /// Select coins from a given wallet that add up to at least the specified amount
        /// </summary>
        /// <param name="selectCoins_RPC"></param>
        /// <returns></returns>
        public async static Task<SelectCoins_Response> SelectCoins(SelectCoins_RPC selectCoins_RPC)
        {
            string response = await SendCustomMessage("select_coins", selectCoins_RPC.ToString());
            SelectCoins_Response json = JsonSerializer.Deserialize<SelectCoins_Response>(response);
            return json;
        }
        /// <summary>
        /// Get all spendable coins, with various possible filters
        /// </summary>
        /// <param name="getSpendableCoins_RPC"></param>
        /// <returns></returns>
        public async static Task<GetSpendableCoins_Response> GetSpendableCoins(GetSpendableCoins_RPC getSpendableCoins_RPC)
        {
            string response = await SendCustomMessage("get_spendable_coins", getSpendableCoins_RPC.ToString());
            GetSpendableCoins_Response json = JsonSerializer.Deserialize<GetSpendableCoins_Response>(response);
            return json;
        }
        /// <summary>
        /// Obtain coin records from a list of coin names
        /// </summary>
        /// <param name="getCoinRecordsByNames_RPC"></param>
        /// <returns></returns>
        public async static Task<GetCoinRecordsByNames_Response> GetCoinRecordsByNames(GetCoinRecordsByNames_RPC getCoinRecordsByNames_RPC)
        {
            string response = await SendCustomMessage("get_coin_records_by_names", getCoinRecordsByNames_RPC.ToString());
            GetCoinRecordsByNames_Response json = JsonSerializer.Deserialize<GetCoinRecordsByNames_Response>(response);
            return json;
        }
        /// <summary>
        /// Obtain the current derivation index for the current wallet
        /// </summary>
        /// <returns></returns>
        public async static Task<GetCurrentDerivationIndec_Response> GetCurrentDerivationIndex()
        {
            string response = await SendCustomMessage("get_current_derivation_index");
            GetCurrentDerivationIndec_Response json = JsonSerializer.Deserialize<GetCurrentDerivationIndec_Response>(response);
            return json;
        }
        /// <summary>
        /// Increase the derivation index
        /// </summary>
        /// <param name="extendDerivationIndex_RPC"></param>
        /// <returns></returns>
        public async static Task<ExtendDerivationIndex_Response> ExtendDerivationIndex(ExtendDerivationIndex_RPC extendDerivationIndex_RPC)
        {
            string response = await SendCustomMessage("extend_derivation_index", extendDerivationIndex_RPC.ToString());
            ExtendDerivationIndex_Response json = JsonSerializer.Deserialize<ExtendDerivationIndex_Response>(response);
            return json;
        }
        /// <summary>
        /// Obtain current notifications
        /// </summary>
        /// <param name="getNotifications_RPC"></param>
        /// <returns></returns>
        public async static Task<GetNotifications_Response> GetNotifications(GetNotifications_RPC getNotifications_RPC = null)
        {
            string response;
            if (getNotifications_RPC != null) { response = await SendCustomMessage("get_notifications", getNotifications_RPC.ToString()); }
            else { response = await SendCustomMessage("get_notifications"); }
            GetNotifications_Response json = JsonSerializer.Deserialize<GetNotifications_Response>(response);
            return json;
        }
        /// <summary>
        /// Delete notifications, with the option to specify IDs from which to delete
        /// </summary>
        /// <param name="deleteNotifications_RPC"></param>
        /// <returns></returns>
        public async static Task<Success_Response> DeleteNotifications(DeleteNotifications_RPC deleteNotifications_RPC = null)
        {
            string response;
            if (deleteNotifications_RPC != null) { response = await SendCustomMessage("delete_notifications", deleteNotifications_RPC.ToString()); }
            else { response = await SendCustomMessage("delete_notifications"); }
            Success_Response json = JsonSerializer.Deserialize<Success_Response>(response);
            return json;
        }
        /// <summary>
        /// Send a notification to a specified puzzle hash
        /// </summary>
        /// <param name="sendNotification_RPC"></param>
        /// <returns></returns>
        public async static Task<SendNotification_Response> SendNotification(SendNotification_RPC sendNotification_RPC)
        {
            string response = await SendCustomMessage("send_notification", sendNotification_RPC.ToString());
            SendNotification_Response success = JsonSerializer.Deserialize<SendNotification_Response>(response);
            return success;
        }
        /// <summary>
        /// Sign a message using an XCH address without incurring an on-chain transaction
        /// </summary>
        /// <param name="signMessageByAddress_RPC"></param>
        /// <returns></returns>
        public async static Task<SignMessage_Response> SignMessageByAddress(SignMessageByAddress_RPC signMessageByAddress_RPC)
        {
            string response = await SendCustomMessage("sign_message_by_address", signMessageByAddress_RPC.ToString());
            SignMessage_Response success = JsonSerializer.Deserialize<SignMessage_Response>(response);
            return success;
        }
        /// <summary>
        /// Sign a message using a DID or NFT ID without incurring an on-chain transaction
        /// </summary>
        /// <param name="signMessageByID_RPC"></param>
        /// <returns></returns>
        public async static Task<SignMessage_Response> SignMessageByID(SignMessageByID_RPC signMessageByID_RPC)
        {
            string response = await SendCustomMessage("sign_message_by_id", signMessageByID_RPC.ToString());
            SignMessage_Response success = JsonSerializer.Deserialize<SignMessage_Response>(response);
            return success;
        }
        /// <summary>
        /// Given a public key, message and signature, verify if it is valid.
        /// </summary>
        /// <param name="verifySignature_RPC"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async static Task VerifySignature (VerifySignature_RPC verifySignature_RPC)
        {
            throw new NotImplementedException("due to incomplete documentation, this function is not yet implemented");
        }
    }
}
