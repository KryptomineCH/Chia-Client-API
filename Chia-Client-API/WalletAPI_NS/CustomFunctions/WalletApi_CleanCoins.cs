using Chia_Client_API.ChiaClient_NS;
using CHIA_RPC.General_NS;
using CHIA_RPC.Objects_NS;
using CHIA_RPC.Wallet_NS.WalletManagement_NS;

namespace Chia_Client_API.WalletAPI_NS
{
    public partial class WalletRpcBase : ChiaEndpointRouteBase
    {
        /// <summary>
        /// This function aims to merge small coins and Split large Coins in order to get to the target coin amount.
        /// this effectively Cleans Dust storms and Allows for transactions to not Lock up the entire Wallet
        /// </summary>
        /// <param name="TargetCoinAmount"></param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException"></exception>
        public async Task<bool> CleanCoins(ulong minimumCoinAmount = 100, ulong maximumCounAmount = 1000, ulong walletId = 0)
        {
            var transactionsByBlock = new Dictionary<ulong, List<Transaction_DictMemos>>();
            var transactionsByAssetIDValue = new Dictionary<string, List<Transaction_DictMemos>>();

            // fetch subwallets
            GetWallets_Response walletsResponse = await GetWallets_Async();
            foreach (Wallets_info wallet in walletsResponse.wallets)
            {
                // filter logic
                if (wallet.type != WalletType.STANDARD_WALLET &&  wallet.type != WalletType.CAT)
                    continue;
                if (walletId != 0 && wallet.id != walletId)
                    continue;
                // check if target state is reachable
                var balance = await GetWalletBalance_Async(wallet);
                if (balance.wallet_balance.confirmed_wallet_balance < minimumCoinAmount)
                {
                    // If the target state is not reachable, and a specific walletId was being searched,
                    // we should break out of the loop to avoid inefficiency.
                    if (walletId != 0)
                        break;
                    else
                        continue; // If no specific walletId is targeted, just continue with the next wallet.
                }

                // cleanup logic
                ulong targetEqualizedBalance = (minimumCoinAmount + maximumCounAmount) / 2;
                ulong finalEqualizedBalance = Math.Min(balance.wallet_balance.confirmed_wallet_balance, targetEqualizedBalance);

                GetSpendableCoins_Async(wallet);

                // exit logic
                if (walletId != 0)
                    break;
            }

            return false;
        }
    }
}
