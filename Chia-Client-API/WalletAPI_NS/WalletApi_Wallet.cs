﻿using CHIA_RPC.General_NS;
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
        // Custom Functions

        /// <summary>
        /// Asynchronously performs a simple transfer operation, handling multiple transaction types.
        /// </summary>
        /// <remarks>
        /// - This is a custom function. If it fails, please use the official endpoints<br/>
        /// - If you pass an nft, the performance is improved if you can provide a wallet id<br/>
        /// - when passing an nft, the return does not contain a transaction id, but the nft coin id
        /// - WARNING: This is a custom function. Test on testnet first. (as you should anyways when managing financials)
        /// </remarks>
        /// <param name="targetAddress">The destination address for the transaction.</param>
        /// <param name="wallet">An optional wallet ID. If not specified, the function will use the default wallet.</param>
        /// <param name="nft">An optional NFT object. If not specified, a standard Chia transaction will be made.</param>
        /// <param name="amount">The amount to be transferred. Defaults to 1.</param>
        /// <param name="fee">The fee for the transaction in Mojos. Defaults to 0.</param>
        /// <param name="awaitTransactionToComplete">Whether to wait for the transaction to be confirmed. Defaults to true.</param>
        /// <returns>
        /// A <see cref="GetTransaction_Response"/> object containing details of the transaction. 
        /// The <c>success</c> property indicates the outcome.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the <paramref name="amount"/> is less than or equal to zero.</exception>
        /// <exception cref="ArgumentException">Thrown when an unsupported <see cref="WalletType"/> is encountered or both <paramref name="wallet"/> and <paramref name="nft"/> are null.</exception>
        public async Task<GetTransaction_Response> SimpleTransfer_Async(
            string targetAddress, 
            WalletID_RPC? wallet = null, 
            Nft? nft = null, 
            decimal amount = 1, 
            decimal fee = 0, 
            bool awaitTransactionToComplete = true)
        {
            ulong feeMojos = (ulong)(fee * CHIA_RPC.General_NS.GlobalVar.OneCatInMojos);
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException("amount must be bigger than 0 for sending!");
            }
            if (wallet == null && nft == null)
            {
                // std chia transaction
                SendTransaction_RPC rpc = new SendTransaction_RPC(1, targetAddress, amount, fee);
                GetTransaction_Response transaction = await SendTransaction_Async(rpc);
                if (!(bool)transaction.success)
                {
                    GetTransaction_Response error = new GetTransaction_Response()
                    {
                        success = false,
                        error = transaction.error,
                        RawContent = transaction.RawContent
                    };
                    return error;
                }
                if (awaitTransactionToComplete)
                {
                    transaction = await AwaitTransactionToConfirm_Async(transaction, CancellationToken.None, timeoutInMinutes: 60);
                }
                return transaction;
            }
            else if (wallet != null && nft != null)
            {
                // nft transaction with known wallet
                NftTransferNft_RPC rpc = new NftTransferNft_RPC(wallet.wallet_id, targetAddress, nft.nft_coin_id, feeMojos, reuse_puzhash: null);
                SpendBundle_Response? spendBundle = await NftTransferNft_Async(rpc);
                if (!(bool)spendBundle.success)
                {
                    GetTransaction_Response error = new GetTransaction_Response()
                    {
                        success = false,
                        error = spendBundle.error,
                        RawContent = spendBundle.RawContent
                    };
                    return error;
                }
                if (awaitTransactionToComplete)
                {
                    bool success = await NftAwaitTransferComplete_Async(wallet.wallet_id, nft, TimeSpan.FromMinutes(60), 1000);
                    GetTransaction_Response response = new GetTransaction_Response()
                    {
                        success = success,
                        transaction_id = nft.nft_coin_id
                    };
                    return response;
                }
            }
            else if (wallet != null)
            {
                Wallets_info walletInfo = await GetWalletInfo_Async(wallet);
                if (walletInfo.type == WalletType.STANDARD_WALLET)
                {
                    // std chia transaction
                    SendTransaction_RPC rpc = new SendTransaction_RPC(1, targetAddress, amount, fee);
                    GetTransaction_Response transaction = await SendTransaction_Async(rpc);
                    if (!(bool)transaction.success)
                    {
                        GetTransaction_Response error = new GetTransaction_Response()
                        {
                            success = false,
                            error = transaction.error,
                            RawContent = transaction.RawContent
                        };
                        return error;
                    }
                    if (awaitTransactionToComplete)
                    {
                        transaction = await AwaitTransactionToConfirm_Async(transaction, CancellationToken.None, TimeSpan.FromMinutes(60));
                    }
                    return transaction;
                }
                else if (walletInfo.type == WalletType.CAT)
                {
                    CatSpend_RPC rpc = new CatSpend_RPC();
                    rpc.wallet_id = wallet.wallet_id;
                    rpc.amount = (ulong)(amount * CHIA_RPC.General_NS.GlobalVar.OneCatInMojos);
                    rpc.inner_address = targetAddress;
                    rpc.fee = feeMojos;

                    CatSpend_Response? record = await CatSpend_Async(rpc);
                    if (!(bool)record.success)
                    {
                        GetTransaction_Response error = new GetTransaction_Response()
                        {
                            success = false,
                            error = record.error,
                            RawContent = record.RawContent
                        };
                        return error;
                    }

                    GetTransaction_Response? transactionDetails;
                    if (awaitTransactionToComplete)
                    {
                        transactionDetails = await AwaitTransactionToConfirm_Async(record.transaction, CancellationToken.None, TimeSpan.FromMinutes(60));
                    }
                    else
                    {
                        transactionDetails = await GetTransaction_Async(record.transaction);
                    }
                    return transactionDetails;
                }
                else
                {
                    throw new ArgumentException($"WalletType {walletInfo.type} is not supported!");
                }
            }
            else
            {
                // nft transaction with unknown wallet
                WalletID_Response walletId = await NftGetwallet_Async(nft);
                if (!(bool)walletId.success)
                {
                    GetTransaction_Response error = new GetTransaction_Response()
                    {
                        success = false,
                        error = walletId.error,
                        RawContent = walletId.RawContent
                    };
                    return error;
                }
                NftTransferNft_RPC rpc = new NftTransferNft_RPC(walletId.wallet_id, targetAddress, nft.nft_coin_id, feeMojos,reuse_puzhash: null);
                SpendBundle_Response? spendBundle = await NftTransferNft_Async(rpc);
                if (!(bool)spendBundle.success)
                {
                    GetTransaction_Response error = new GetTransaction_Response()
                    {
                        success = false,
                        error = spendBundle.error,
                        RawContent = spendBundle.RawContent
                    };
                    return error;
                }
                if (awaitTransactionToComplete)
                {
                    bool success = await NftAwaitTransferComplete_Async(walletId.wallet_id, nft, TimeSpan.FromMinutes(60), 1000);
                    GetTransaction_Response response = new GetTransaction_Response()
                    {
                        success = success,
                        transaction_id = nft.nft_coin_id
                    };
                    return response;
                }
            }
            throw new InvalidOperationException("Operation could not be identified from the parameters");
        }
        /// <summary>
        /// simple function for accessing the wallet info of a secific wallet ID.<br/>
        /// Note that this function pulls all wallets. So if you need it often or are in for performance, perhaps caching the value is better.
        /// </summary>
        /// <param name="wallet"></param>
        /// <returns></returns>
        public Wallets_info GetWalletInfo_Sync(WalletID_RPC wallet)
        {
            Task<Wallets_info> data = Task.Run(() => GetWalletInfo_Async(wallet));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// simple function for accessing the wallet info of a secific wallet ID.<br/>
        /// Note that this function pulls all wallets. So if you need it often or are in for performance, perhaps caching the value is better.
        /// </summary>
        /// <param name="wallet"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="InvalidDataException"></exception>
        public async Task<Wallets_info> GetWalletInfo_Async(WalletID_RPC wallet)
        {
            if (wallet.wallet_id < 1)
            {
                throw new ArgumentOutOfRangeException("Minimum Wallet ID must be 1!");
;            }
            GetWallets_Response wallets = await GetWallets_Async();
            for(int i = 0; i < wallets.wallets.Length; i++)
            {
                if (wallets.wallets[i].id == wallet.wallet_id)
                {
                    return wallets.wallets[i];
                }
            }
            throw new InvalidDataException($"Wallet with id {wallet.wallet_id} could not be found!");
        }
        /// <summary>
        /// waits for a transaction to fully execute or fail
        /// </summary>
        /// <param name="transactionID_RPC"></param>
        /// <param name="cancellation"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<GetTransaction_Response?> AwaitTransactionToConfirm_Async(
            TransactionID_RPC transactionID_RPC,
            CancellationToken cancellation, TimeSpan timeOut)
        {
            DateTime startTime = DateTime.Now;
            GetTransaction_Response? responseJson = null;
            while (!cancellation.IsCancellationRequested)
            {
                responseJson = await GetTransaction_Async(transactionID_RPC);
                if (responseJson == null)
                {
                    throw new InvalidOperationException("unable to fetch GetTransaction_Response!");
                }
                if ((responseJson.success ?? false) && responseJson.transaction != null && (responseJson.transaction.confirmed ?? false))
                {
                    return responseJson;
                }
                await Task.Delay(1000, cancellation);
                if (DateTime.Now > startTime + timeOut)
                {
                    break;
                }
            }
            return responseJson;
        }
        
        /// <summary>
        /// waits for a transaction to fully execute or fail
        /// </summary>
        /// <param name="transactionID_RPC"></param>
        /// <param name="cancellation"></param>
        /// <param name="timeoutInMinutes"></param>
        /// <returns></returns>
        public async Task<GetTransaction_Response?> AwaitTransactionToConfirm_Async(
            TransactionID_RPC transactionID_RPC,
            CancellationToken cancellation, double timeoutInMinutes = 5)
        {
            return await AwaitTransactionToConfirm_Async(transactionID_RPC, cancellation, timeOut: TimeSpan.FromMinutes(timeoutInMinutes));
        }
        /// <summary>
        /// waits for a transaction to fully execute or fail
        /// </summary>
        /// <param name="transactionID_RPC"></param>
        /// <param name="cancellation"></param>
        /// <param name="timeoutInMinutes"></param>
        /// <returns></returns>
        public GetTransaction_Response? AwaitTransactionToConfirm_Sync(
            TransactionID_RPC transactionID_RPC,
            CancellationToken cancellation, double timeoutInMinutes = 5)
        {
            TimeSpan timeout = TimeSpan.FromMinutes(timeoutInMinutes);
            Task<GetTransaction_Response?> data = Task.Run(() => AwaitTransactionToConfirm_Async(transactionID_RPC, cancellation, timeout));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// waits for a transaction to fully execute or fail
        /// </summary>
        /// <param name="transactionID_RPC"></param>
        /// <param name="cancellation"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public GetTransaction_Response? AwaitTransactionToConfirm_Sync(
            TransactionID_RPC transactionID_RPC,
            CancellationToken cancellation, TimeSpan timeout)
        {
            Task<GetTransaction_Response?> data = Task.Run(() => AwaitTransactionToConfirm_Async(transactionID_RPC, cancellation, timeout));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// waits for an offer to fully execute or fail
        /// </summary>
        /// <param name="transactionID_RPC"></param>
        /// <param name="cancellation"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<CheckOfferValidity_Response?> AwaitOfferToExecuteOrCancel_Async(
            OfferFile offer,
            CancellationToken cancellation, TimeSpan timeOut)
        {
            DateTime startTime = DateTime.Now;
            CheckOfferValidity_Response? responseJson = null;
            while (!cancellation.IsCancellationRequested)
            {
                
                responseJson = await CheckOfferValidity_Async(offer);
                if (responseJson == null)
                {
                    throw new InvalidOperationException("unable to fetch GetTransaction_Response!");
                }
                if ((responseJson.success ?? false) && !(responseJson.valid ?? false))
                {
                    return responseJson;
                }
                await Task.Delay(1000, cancellation);
                if (DateTime.Now > startTime + timeOut)
                {
                    break;
                }
            }
            return responseJson;
        }
        /// <summary>
        /// waits for a offer to fully execute or fail
        /// </summary>
        /// <param name="transactionID_RPC"></param>
        /// <param name="cancellation"></param>
        /// <param name="timeoutInMinutes"></param>
        /// <returns></returns>
        public CheckOfferValidity_Response? AwaitOfferToExecuteOrCancel_Sync(
            OfferFile offer,
            CancellationToken cancellation, double timeoutInMinutes = 5)
        {
            TimeSpan timeout = TimeSpan.FromMinutes(timeoutInMinutes);
            Task<CheckOfferValidity_Response?> data = Task.Run(() => AwaitOfferToExecuteOrCancel_Async(offer, cancellation, timeout));
            data.Wait();
            return data.Result;
        }

        // Functions according to Documentation
        /// <summary>
        /// Create a signed transaction from the given wallet<br/><br/>
        /// WARNING: Due to lacking documentation may not be implemented correctly
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#create_signed_transaction"/></remarks>
        /// <returns></returns>
        public async Task<GetTransaction_Response?> CreateSignedTransaction_Async(CreateSignedTransaction_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_farmed_amount", rpc.ToString());
            GetTransaction_Response? deserializedObject = GetTransaction_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Create a signed transaction from the given wallet<br/><br/>
        /// WARNING: Due to lacking documentation may not be implemented correctly
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#create_signed_transaction"/></remarks>
        /// <returns></returns>
        public GetTransaction_Response? CreateSignedTransaction_Sync(CreateSignedTransaction_RPC rpc)
        {
            Task<GetTransaction_Response?> data = Task.Run(() => CreateSignedTransaction_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Delete notifications, with the option to specify IDs from which to delete
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#delete_notifications"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<Success_Response?> DeleteNotifications_Async(DeleteNotifications_RPC? rpc)
        {
            string? rpcString = rpc?.ToString() ?? null;
            string responseJson = await SendCustomMessage_Async("delete_notifications", rpcString);
            Success_Response? deserializedObject = Success_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Delete notifications, with the option to specify IDs from which to delete
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#delete_notifications"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public Success_Response? DeleteNotifications_Sync(DeleteNotifications_RPC? rpc = null)
        {
            Task<Success_Response?> data = Task.Run(() => DeleteNotifications_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Delete all transactions that have yet to be confirmed on the blockchain from the given wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#delete_unconfirmed_transactions"/></remarks>
        /// <param name="walletID"></param>
        /// <returns></returns>
        public async Task<Success_Response?> DeleteUnconfirmedTransactions_Async(ulong walletID)
        {
            string responseJson = await SendCustomMessage_Async("delete_unconfirmed_transactions", "{\"wallet_id\": " + walletID + "}");
            Success_Response? success = Success_Response.LoadResponseFromString(responseJson);
            return success;
        }
        /// <summary>
        /// Delete all transactions that have yet to be confirmed on the blockchain from the given wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#delete_unconfirmed_transactions"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<Success_Response?> DeleteUnconfirmedTransactions_Async(WalletID_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("delete_unconfirmed_transactions", rpc.ToString());
            Success_Response? success = Success_Response.LoadResponseFromString(responseJson);
            return success;
        }
        /// <summary>
        /// Delete all transactions that have yet to be confirmed on the blockchain from the given wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#delete_unconfirmed_transactions"/></remarks>
        /// <param name="walletID"></param>
        /// <returns></returns>
        public Success_Response? DeleteUnconfirmedTransactions_Sync(ulong walletID)
        {
            Task<Success_Response?> data = Task.Run(() => DeleteUnconfirmedTransactions_Async(walletID));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Delete all transactions that have yet to be confirmed on the blockchain from the given wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#delete_unconfirmed_transactions"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public Success_Response? DeleteUnconfirmedTransactions_Sync(WalletID_RPC rpc)
        {
            Task<Success_Response?> data = Task.Run(() => DeleteUnconfirmedTransactions_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Increase the derivation index
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#extend_derivation_index"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<Index_Response?> ExtendDerivationIndex_Async(Index_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("extend_derivation_index", rpc.ToString());
            Index_Response? deserializedObject = Index_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Increase the derivation index
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#extend_derivation_index"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public Index_Response? ExtendDerivationIndex_Sync(Index_RPC rpc)
        {
            Task<Index_Response?> data = Task.Run(() => ExtendDerivationIndex_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Obtain coin records from a list of coin names
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_coin_records_by_names"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetCoinRecords_Response?> GetCoinRecordsByNames_Async(GetCoinRecordsByNames_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_coin_records_by_names", rpc.ToString());
            GetCoinRecords_Response? deserializedObject = GetCoinRecords_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Obtain coin records from a list of coin names
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_coin_records_by_names"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetCoinRecords_Response? GetCoinRecordsByNames_Sync(GetCoinRecordsByNames_RPC rpc)
        {
            Task<GetCoinRecords_Response?> data = Task.Run(() => GetCoinRecordsByNames_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Obtain the current derivation index for the current wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_current_derivation_index"/></remarks>
        /// <returns></returns>
        public async Task<Index_Response?> GetCurrentDerivationIndex_Async()
        {
            string responseJson = await SendCustomMessage_Async("get_current_derivation_index");
            Index_Response? deserializedObject = Index_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Obtain the current derivation index for the current wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_current_derivation_index"/></remarks>
        /// <returns></returns>
        public Index_Response? GetCurrentDerivationIndex_Sync()
        {
            Task<Index_Response?> data = Task.Run(() => GetCurrentDerivationIndex_Async());
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Show the total amount that has been farmed
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_farmed_amount"/></remarks>
        /// <returns></returns>
        public async Task<GetFarmedAmount_Response?> GetFarmedAmount_Async()
        {
            string responseJson = await SendCustomMessage_Async("get_farmed_amount");
            GetFarmedAmount_Response? deserializedObject = GetFarmedAmount_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Show the total amount that has been farmed
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_farmed_amount"/></remarks>
        /// <returns></returns>
        public GetFarmedAmount_Response? GetFarmedAmount_Sync()
        {
            Task<GetFarmedAmount_Response?> data = Task.Run(() => GetFarmedAmount_Async());
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Get the next address in the HD tree, with the option to show the latest address
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_next_address"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetNextAddress_Response?> GetNextAddress_Async(GetNextAddress_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_next_address", rpc.ToString());
            GetNextAddress_Response? deserializedObject = GetNextAddress_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Get the next address in the HD tree, with the option to show the latest address
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_next_address"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetNextAddress_Response? GetNextAddress_Sync(GetNextAddress_RPC rpc)
        {
            Task<GetNextAddress_Response?> data = Task.Run(() => GetNextAddress_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /*
        public GetNextAddress_Response? GetWalletAddresses_Sync(GetWalletAddresses_RPC rpc)
        {
            Task<GetNextAddress_Response?> data = Task.Run(() => GetWalletAddresses_Async(rpc));
            data.Wait();
            return data.Result;
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
        public async Task<GetNotifications_Response?> GetNotifications_Async(GetNotifications_RPC? rpc = null)
        {
            string? rpcString = rpc?.ToString() ?? null;
            string responseJson = await SendCustomMessage_Async("get_notifications", rpcString);
            GetNotifications_Response? deserializedObject = GetNotifications_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Obtain current notifications
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_notifications"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetNotifications_Response? GetNotifications_Sync(GetNotifications_RPC? rpc = null)
        {
            Task<GetNotifications_Response?> data = Task.Run(() => GetNotifications_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Get all spendable coins, with various possible filters
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_spendable_coins"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetSpendableCoins_Response?> GetSpendableCoins_Async(GetSpendableCoins_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_spendable_coins", rpc.ToString());
            GetSpendableCoins_Response? deserializedObject = GetSpendableCoins_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Get all spendable coins, with various possible filters
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_spendable_coins"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetSpendableCoins_Response? GetSpendableCoins_Sync(GetSpendableCoins_RPC rpc)
        {
            Task<GetSpendableCoins_Response?> data = Task.Run(() => GetSpendableCoins_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Get a transaction's details from its ID
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_transaction"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetTransaction_Response?> GetTransaction_Async(TransactionID_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_transaction", rpc.ToString());
            GetTransaction_Response? deserializedObject = GetTransaction_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Get a transaction's details from its ID
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_transaction"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetTransaction_Response? GetTransaction_Sync(TransactionID_RPC rpc)
        {
            Task<GetTransaction_Response?> data = Task.Run(() => GetTransaction_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Get all transactions for a given wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_transactions"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        [Obsolete("Transaction history is not deterministic due to heuristics we use to counter privacy features of the blockchain. " +
            "The wallet does its best effort. We are striving to improve this going forward. " +
            "For accurate records, you should keep a local record of transactions (TXs) made.")]
        public async Task<GetTransactions_Response?> GetTransactions_Async(WalletID_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_transactions", rpc.ToString());
            //GetTransactions_Response? deserializedObject = JsonSerializer.Deserialize<GetTransactions_Response?>(responseJson);
            GetTransactions_Response? deserializedObject = GetTransactions_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }

        /// <summary>
        /// Get all transactions for a given wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_transactions"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        [Obsolete("Transaction history is not deterministic due to heuristics we use to counter privacy features of the blockchain. " +
            "The wallet does its best effort. We are striving to improve this going forward. " +
            "For accurate records, you should keep a local record of transactions (TXs) made.")]
        public GetTransactions_Response? GetTransactions_Sync(WalletID_RPC rpc)
        {
            Task<GetTransactions_Response?> data = Task.Run(() => GetTransactions_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Get all transactions for a given wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_transactions"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        [Obsolete("Transaction history is not deterministic due to heuristics we use to counter privacy features of the blockchain. " +
            "The wallet does its best effort. We are striving to improve this going forward. " +
            "For accurate records, you should keep a local record of transactions (TXs) made.")]
        public async Task<GetTransactions_Response?> GetTransactions_Async(GetTransactions_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_transactions", rpc.ToString());
            //GetTransactions_Response? deserializedObject = JsonSerializer.Deserialize<GetTransactions_Response?>(responseJson);
            GetTransactions_Response? deserializedObject = GetTransactions_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Get all transactions for a given wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_transactions"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        [Obsolete("Transaction history is not deterministic due to heuristics we use to counter privacy features of the blockchain. " +
            "The wallet does its best effort. We are striving to improve this going forward. " +
            "For accurate records, you should keep a local record of transactions (TXs) made.")]
        public GetTransactions_Response? GetTransactions_Sync(GetTransactions_RPC rpc)
        {
            Task<GetTransactions_Response?> data = Task.Run(() => GetTransactions_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Obtain the number of transactions for a wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_transaction_count"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        [Obsolete("Transaction history is not deterministic due to heuristics we use to counter privacy features of the blockchain. " +
            "The wallet does its best effort. We are striving to improve this going forward. " +
            "For accurate records, you should keep a local record of transactions (TXs) made.")]
        public async Task<GetTransactionCount_Response?> GetTransactionCount_Async(WalletID_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_transaction_count", rpc.ToString());
            GetTransactionCount_Response? deserializedObject = GetTransactionCount_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Obtain the number of transactions for a wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_transaction_count"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        [Obsolete("Transaction history is not deterministic due to heuristics we use to counter privacy features of the blockchain. " +
            "The wallet does its best effort. We are striving to improve this going forward. " +
            "For accurate records, you should keep a local record of transactions (TXs) made.")]
        public GetTransactionCount_Response? GetTransactionCount_Sync(WalletID_RPC rpc)
        {
            Task<GetTransactionCount_Response?> data = Task.Run(() => GetTransactionCount_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Obtain the memo for the specified transaction
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_transaction_memo"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<Memo_Response?> GetTransactionMemo_Async(TransactionID_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_transaction_memo", rpc.ToString());
            Memo_Response? success = Memo_Response.LoadResponseFromString(responseJson);
            return success;
        }
        /// <summary>
        /// Obtain the memo for the specified transaction
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_transaction_memo"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public Memo_Response? GetTransactionMemo_Sync(TransactionID_RPC rpc)
        {
            Task<Memo_Response?> data = Task.Run(() => GetTransactionMemo_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Obtain the balance (and related info) from a wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_wallet_balance"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetWalletBalance_Response?> GetWalletBalance_Async(WalletID_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_wallet_balance", rpc.ToString());
            GetWalletBalance_Response? deserializedObject = GetWalletBalance_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Obtain the balance (and related info) from a wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_wallet_balance"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetWalletBalance_Response? GetWalletBalance_Sync(WalletID_RPC rpc)
        {
            Task<GetWalletBalance_Response?> data = Task.Run(() => GetWalletBalance_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Select coins from a given wallet that add up to at least the specified amount
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#select_coins"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<SelectCoins_Response?> SelectCoins_Async(SelectCoins_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("select_coins", rpc.ToString());
            SelectCoins_Response? deserializedObject = SelectCoins_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Select coins from a given wallet that add up to at least the specified amount
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#select_coins"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public SelectCoins_Response? SelectCoins_Sync(SelectCoins_RPC rpc)
        {
            Task<SelectCoins_Response?> data = Task.Run(() => SelectCoins_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Send a notification to a specified puzzle hash
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#send_notification"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<SendNotification_Response?> SendNotification_Async(SendNotification_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("send_notification", rpc.ToString());
            SendNotification_Response? success = SendNotification_Response.LoadResponseFromString(responseJson);
            return success;
        }
        /// <summary>
        /// Send a notification to a specified puzzle hash
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#send_notification"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public SendNotification_Response? SendNotification_Sync(SendNotification_RPC rpc)
        {
            Task<SendNotification_Response?> data = Task.Run(() => SendNotification_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Send a (chia) transaction
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#send_transaction"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetTransaction_Response?> SendTransaction_Async(SendTransaction_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("send_transaction", rpc.ToString());
            GetTransaction_Response? deserializedObject = GetTransaction_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Send a (chia) transaction
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#send_transaction"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetTransaction_Response? SendTransaction_Sync(SendTransaction_RPC rpc)
        {
            Task<GetTransaction_Response?> data = Task.Run(() => SendTransaction_Async(rpc));
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
        public async Task<GetTransaction_Response?> SendTransactionMulti_Async(SendTransactionMulti_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("send_transaction_multi", rpc.ToString());
            GetTransaction_Response? deserializedObject = GetTransaction_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Send multiple chia transactions from a given wallet<br/><br/>
        /// WARNING: due to missing documentation, endpoint may not be implemented correctly
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#send_transaction_multi"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetTransaction_Response? SendTransactionMulti_Sync(SendTransactionMulti_RPC rpc)
        {
            Task<GetTransaction_Response?> data = Task.Run(() => SendTransactionMulti_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Sign a message using an XCH address without incurring an on-chain transaction
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#sign_message_by_address"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<SignMessage_Response?> SignMessageByAddress_Async(SignMessageByAddress_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("sign_message_by_address", rpc.ToString());
            SignMessage_Response? success = SignMessage_Response.LoadResponseFromString(responseJson);
            return success;
        }
        /// <summary>
        /// Sign a message using an XCH address without incurring an on-chain transaction
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#sign_message_by_address"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public SignMessage_Response? SignMessageByAddress_Sync(SignMessageByAddress_RPC rpc)
        {
            Task<SignMessage_Response?> data = Task.Run(() => SignMessageByAddress_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Sign a message using a DID or NFT ID without incurring an on-chain transaction
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#sign_message_by_id"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<SignMessage_Response?> SignMessageByID_Async(SignMessageByID_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("sign_message_by_id", rpc.ToString());
            SignMessage_Response? success = SignMessage_Response.LoadResponseFromString(responseJson);
            return success;
        }
        /// <summary>
        /// Sign a message using a DID or NFT ID without incurring an on-chain transaction
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#sign_message_by_id"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public SignMessage_Response? SignMessageByID_Sync(SignMessageByID_RPC rpc)
        {
            Task<SignMessage_Response?> data = Task.Run(() => SignMessageByID_Async(rpc));
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
        public async Task<SignMessage_Response?> VerifySignature_Async(VerifySignature_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("verify_signature", rpc.ToString());
            SignMessage_Response? success = SignMessage_Response.LoadResponseFromString(responseJson);
            return success;
        }
        /// <summary>
        /// Given a public key, message and signature, verify if it is valid.<br/><br/>
        /// WARNING: Due to missing Documentation this endpoint is not implemented correctly
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#verify_signature"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public SignMessage_Response? VerifySignature_Sync(VerifySignature_RPC rpc)
        {
            Task<SignMessage_Response?> data = Task.Run(() => VerifySignature_Async(rpc));
            data.Wait();
            return data.Result;
        }
        
    }
}
