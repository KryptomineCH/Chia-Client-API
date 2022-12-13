
using Chia_Client_API.Wallet_NS.WalletApiResponses_NS;
using CHIA_RPC.General;
using CHIA_RPC.Wallet_RPC_NS.Wallet;
using System.Text.Json;

namespace Chia_Client_API.Wallet_NS.WalletAPI_NS
{
    public static partial class WalletApi
    {
        public async static Task<get_wallet_balance_info> GetWalletBalance(WalletID_RPC walletID_RPC)
        {
            string response = await SendCustomMessage("get_wallet_balance", walletID_RPC.ToString());
            get_wallet_balance_info json = JsonSerializer.Deserialize<get_wallet_balance_info>(response);
            return json;
        }
        public async static Task<GetTransaction_Response> GetTransaction(TransactionID_RPC transactionID_RPC)
        {
            string response = await SendCustomMessage("get_transaction", transactionID_RPC.ToString());
            GetTransaction_Response json = JsonSerializer.Deserialize<GetTransaction_Response>(response);
            return json;
        }
        public async static Task<GetTransactions_Response> GetTransactions(WalletID_RPC walletID_RPC)
        {
            string response = await SendCustomMessage("get_transactions", walletID_RPC.ToString());
            GetTransactions_Response json = JsonSerializer.Deserialize<GetTransactions_Response>(response);
            return json;
        }
        public async static Task<get_transaction_count_info> GetTransactionCount(WalletID_RPC walletID_RPC)
        {
            string response = await SendCustomMessage("get_transaction_count", walletID_RPC.ToString());
            get_transaction_count_info json = JsonSerializer.Deserialize<get_transaction_count_info>(response);
            return json;
        }
        public async static Task<GetNextAddress_Response> GetNextAddress(GetNextAddress_RPC getNextAddress_RPC)
        {
            string response = await SendCustomMessage("get_next_address", getNextAddress_RPC.ToString());
            GetNextAddress_Response json = JsonSerializer.Deserialize<GetNextAddress_Response>(response);
            return json;
        }
        public async static Task<GetTransaction_Response> SendTransaction(SendXCH_RPC wallet_Send_XCH_RPC)
        {
            string response = await SendCustomMessage("send_transaction", wallet_Send_XCH_RPC.ToString());
            GetTransaction_Response json = JsonSerializer.Deserialize<GetTransaction_Response>(response);
            return json;
        }
        public async static Task<GetTransactions_Response> SendTransactionMulti(SendXCH_RPC wallet_Send_XCH_RPC)
        {
            throw new NotImplementedException("not implemented yet due to documentation uncertainties. Please use SendTransaction instead");
            string response = await SendCustomMessage("send_transaction", wallet_Send_XCH_RPC.ToString());
            GetTransactions_Response json = JsonSerializer.Deserialize<GetTransactions_Response>(response);
            return json;
        }
        public async static Task<get_farmed_amount_info> GetFarmedAmount()
        {
            string response = await SendCustomMessage("get_farmed_amount");
            get_farmed_amount_info json = JsonSerializer.Deserialize<get_farmed_amount_info>(response);
            return json;
        }
        /// <summary>
        /// not yet implemented
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async static Task<get_farmed_amount_info> CreateSignedTransaction()
        {
            throw new NotImplementedException();
            string response = await SendCustomMessage("get_farmed_amount");
            get_farmed_amount_info json = JsonSerializer.Deserialize<get_farmed_amount_info>(response);
            return json;
        }
        public async static Task<Success_Response> DeleteUnconfirmedTransactions(ulong walletID)
        {
            string response = await SendCustomMessage("delete_unconfirmed_transactions", "{\"wallet_id\": "+walletID+"}");
            Success_Response success = JsonSerializer.Deserialize<Success_Response>(response);
            return success;
        }
        public async static Task<SelectCoins_Response> SelectCoins(SelectCoins_RPC selectCoins_RPC)
        {
            string response = await SendCustomMessage("select_coins", selectCoins_RPC.ToString());
            SelectCoins_Response json = JsonSerializer.Deserialize<SelectCoins_Response>(response);
            return json;
        }
        public async static Task<GetSpendableCoins_Response> GetSpendableCoins(GetSpendableCoins_RPC getSpendableCoins_RPC)
        {
            string response = await SendCustomMessage("get_spendable_coins", getSpendableCoins_RPC.ToString());
            GetSpendableCoins_Response json = JsonSerializer.Deserialize<GetSpendableCoins_Response>(response);
            return json;
        }
        public async static Task<GetCoinRecordsByNames_Response> GetCoinRecordsByNames(GetCoinRecordsByNames_RPC getCoinRecordsByNames_RPC)
        {
            string response = await SendCustomMessage("get_coin_records_by_names", getCoinRecordsByNames_RPC.ToString());
            GetCoinRecordsByNames_Response json = JsonSerializer.Deserialize<GetCoinRecordsByNames_Response>(response);
            return json;
        }
        public async static Task<get_current_derivation_index_info> GetCurrentDerivationIndex()
        {
            string response = await SendCustomMessage("get_current_derivation_index");
            get_current_derivation_index_info json = JsonSerializer.Deserialize<get_current_derivation_index_info>(response);
            return json;
        }
        public async static Task<ExtendDerivationIndex_Response> ExtendDerivationIndex(ExtendDerivationIndex_RPC extendDerivationIndex_RPC)
        {
            string response = await SendCustomMessage("extend_derivation_index", extendDerivationIndex_RPC.ToString());
            ExtendDerivationIndex_Response json = JsonSerializer.Deserialize<ExtendDerivationIndex_Response>(response);
            return json;
        }
        public async static Task<GetNotifications_Response> GetNotifications(GetNotifications_RPC getNotifications_RPC = null)
        {
            string response;
            if (getNotifications_RPC != null) { response = await SendCustomMessage("get_notifications", getNotifications_RPC.ToString()); }
            else { response = await SendCustomMessage("get_notifications"); }
            GetNotifications_Response json = JsonSerializer.Deserialize<GetNotifications_Response>(response);
            return json;
        }
        public async static Task<Success_Response> DeleteNotifications(DeleteNotifications_RPC deleteNotifications_RPC = null)
        {
            string response;
            if (deleteNotifications_RPC != null) { response = await SendCustomMessage("delete_notifications", deleteNotifications_RPC.ToString()); }
            else { response = await SendCustomMessage("delete_notifications"); }
            Success_Response json = JsonSerializer.Deserialize<Success_Response>(response);
            return json;
        }
        public async static Task<SendNotification_Response> SendNotification(SendNotification_RPC sendNotification_RPC)
        {
            string response = await SendCustomMessage("send_notification", sendNotification_RPC.ToString());
            SendNotification_Response success = JsonSerializer.Deserialize<SendNotification_Response>(response);
            return success;
        }
        public async static Task<SignMessage_Response> SignMessageByAddress(SignMessageByAddress_RPC signMessageByAddress_RPC)
        {
            string response = await SendCustomMessage("sign_message_by_address", signMessageByAddress_RPC.ToString());
            SignMessage_Response success = JsonSerializer.Deserialize<SignMessage_Response>(response);
            return success;
        }
        public async static Task<SignMessage_Response> SignMessageByID(SignMessageByID_RPC signMessageByID_RPC)
        {
            string response = await SendCustomMessage("sign_message_by_id", signMessageByID_RPC.ToString());
            SignMessage_Response success = JsonSerializer.Deserialize<SignMessage_Response>(response);
            return success;
        }
        public async static Task<> VerifySignature (VerifySignature_RPC verifySignature_RPC)
        {
            throw new NotImplementedException("due to incomplete documentation, this function is not yet implemented");
        }
    }
}
