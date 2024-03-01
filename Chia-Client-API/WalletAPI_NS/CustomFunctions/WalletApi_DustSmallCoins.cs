using CHIA_RPC.General_NS;
using CHIA_RPC.Objects_NS;
using CHIA_RPC.Wallet_NS.Wallet_NS;
using CHIA_RPC.Wallet_NS.WalletManagement_NS;

namespace Chia_Client_API.WalletAPI_NS
{
    public partial class WalletRpcClient
    {
        /// <summary>
        /// accumulates small coin amounts on the wallet into larger coins
        /// </summary>
        /// <remarks>
        /// Luckily I tested this and have the answer 😉 <see href="https://docs.chia.net/coin-set-costs/#estimated-transaction-costs"/><br/><br/> 
        /// a 500 coin combination = clvm cost of 3,100,000,000 so the minimum effective fee would be 15,500,000,000 mojo(.0155 xch) any amount less than this would be seen by the network as a 1 mojo fee<br/><br/> 
        /// please note that this is 56% of a full block, so the fee is considerable
        /// </remarks>
        /// <param name="walletId">the wallet that should be Dusted</param>
        /// <param name="minCoinSize">the minimum size of coin to have</param>
        /// <param name="minimumBatchSize">the minimum amount of small coins to justify a dusting run</param>
        /// <param name="maximumBatchSize">the maximum amount of small coins to dust at once. In theory, this is 500. But the fee will be tremendous, since you would need to occupy half a Block and kick out many other Transactions</param>
        /// <param name="xchFeeInMojosPerCoin">the fee per dusting action. Please note that this fee is per coin. recommended is 31,000,000 (29. feb 2024)</param>
        /// <returns></returns>
        public async Task<GetTransaction_Response> CleanDust(
            CancellationToken cancellation,
            ulong walletId = 1,
            decimal minCoinSize = 0.01m,
            int minimumBatchSize = 10,
            int maximumBatchSize = 25,
            ulong xchFeeInMojosPerCoin = 70000000
            )
        {
            GetTransaction_Response response = new GetTransaction_Response();
            response.success = false;
            response.error = "unknown";
            // check wallet type
            Wallets_info walletInfo = await GetWalletInfo_Async(walletId);
            bool isXch = walletInfo.type == WalletType.STANDARD_WALLET;
            if (!isXch && walletInfo.type != WalletType.CAT)
            {
                response.error = "Wallet type is neither of type xch or cat! cancelling";
                return response;
            }
            // get balance
            var balance = await GetWalletBalance_Async(walletId);
            decimal currencyFactor = CHIA_RPC.General_NS.GlobalVar.OneChiaInMojos;
            if (!isXch)
                currencyFactor = CHIA_RPC.General_NS.GlobalVar.OneCatInMojos;

            // fetch Coins and build batches for the small coins
            GetSpendableCoins_RPC coinsRpc = new GetSpendableCoins_RPC(walletId,null);
            GetSpendableCoins_Response coins_Response = await GetSpendableCoins_Async(coinsRpc);

            while (true)
            {
                Coin[] allCoins = coins_Response.GetSortedCoins(ascending: true);
                List<Coin> smallCoinbatch = new List<Coin>();
                foreach (Coin coin in allCoins)
                {
                    if (coin.amount.Value / currencyFactor <= minCoinSize)
                    {
                        smallCoinbatch.Add(coin);
                        if (smallCoinbatch.Count >= maximumBatchSize)
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                response.success = true;
                response.error = null;
                if (smallCoinbatch.Count > minimumBatchSize)
                {
                    response = await CombineCoins(walletId, smallCoinbatch.ToArray(), xchFeeInMojosPerCoin * (ulong)smallCoinbatch.Count);
                    if (!(bool)response.success || !string.IsNullOrEmpty(response.error))
                        return response;
                    else
                    {
                        TransactionID_RPC rpc = new TransactionID_RPC(response);
                        await AwaitTransactionToConfirm_Async(rpc, cancellation, 60);
                    }
                }
                else
                {
                    return response;
                }
            }
        }
    }
}
