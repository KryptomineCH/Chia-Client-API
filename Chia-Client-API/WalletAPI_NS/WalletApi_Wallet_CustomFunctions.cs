using Chia_Client_API.Helpers_NS;
using CHIA_RPC.General_NS;
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
                SendTransaction_RPC rpc = new SendTransaction_RPC(wallet_id: 1, targetAddress, amount_xch: amount, fee);
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
                SpendBundle_Response spendBundle = await NftTransferNft_Async(rpc);
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
                    SendTransaction_RPC rpc = new SendTransaction_RPC(wallet_id: 1, targetAddress, amount, fee);
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
                SpendBundle_Response spendBundle = await NftTransferNft_Async(rpc);
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
        public async Task<GetTransaction_Response> AwaitTransactionToConfirm_Async(
            TransactionID_RPC transactionID_RPC,
            CancellationToken cancellation, TimeSpan timeOut)
        {
            DateTime startTime = DateTime.Now;
            GetTransaction_Response responseJson = null;
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
        public async Task<GetTransaction_Response> AwaitTransactionToConfirm_Async(
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
        public GetTransaction_Response AwaitTransactionToConfirm_Sync(
            TransactionID_RPC transactionID_RPC,
            CancellationToken cancellation, double timeoutInMinutes = 5)
        {
            TimeSpan timeout = TimeSpan.FromMinutes(timeoutInMinutes);
            Task<GetTransaction_Response> data = Task.Run(() => AwaitTransactionToConfirm_Async(transactionID_RPC, cancellation, timeout));
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
        public GetTransaction_Response AwaitTransactionToConfirm_Sync(
            TransactionID_RPC transactionID_RPC,
            CancellationToken cancellation, TimeSpan timeout)
        {
            Task<GetTransaction_Response> data = Task.Run(() => AwaitTransactionToConfirm_Async(transactionID_RPC, cancellation, timeout));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// waits for an offer to fully execute or fail
        /// </summary>
        /// <param name="offer"></param>
        /// <param name="cancellation"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<CheckOfferValidity_Response> AwaitOfferToExecuteOrCancel_Async(
            OfferFile offer,
            CancellationToken cancellation, TimeSpan timeOut)
        {
            if (offer.offer == null || offer.error != null)
            {
                CheckOfferValidity_Response response = new CheckOfferValidity_Response();
                response.success = offer.success;
                response.error = offer.error;
                response.valid = false;
                return response;
            }
            DateTime startTime = DateTime.Now;
            CheckOfferValidity_Response responseJson = null;
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
        /// <param name="offer"></param>
        /// <param name="cancellation"></param>
        /// <param name="timeoutInMinutes"></param>
        /// <returns></returns>
        public CheckOfferValidity_Response AwaitOfferToExecuteOrCancel_Sync(
            OfferFile offer,
            CancellationToken cancellation, double timeoutInMinutes = 5)
        {
            TimeSpan timeout = TimeSpan.FromMinutes(timeoutInMinutes);
            Task<CheckOfferValidity_Response> data = Task.Run(() => AwaitOfferToExecuteOrCancel_Async(offer, cancellation, timeout));
            data.Wait();
            return data.Result;
        } 
    }
}
