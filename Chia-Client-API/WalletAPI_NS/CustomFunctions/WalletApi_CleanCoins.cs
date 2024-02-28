using Chia_Client_API.ChiaClient_NS;
using CHIA_RPC.FullNode_NS;
using CHIA_RPC.General_NS;
using CHIA_RPC.Objects_NS;
using CHIA_RPC.Wallet_NS.CATsAndTrading_NS;
using CHIA_RPC.Wallet_NS.Wallet_NS;
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
        public async Task<bool> CleanCoins(
            ulong minimumCoinAmount = 100, 
            ulong maximumCoinAmount = 1000, 
            ulong walletId = 0,
            double minCoinSize = 0.01,
            ulong xchFeeInMojos = 1000)
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
                ulong targetEqualizedBalance = (minimumCoinAmount + maximumCoinAmount) / 2;
                ulong finalEqualizedBalance = Math.Min(balance.wallet_balance.confirmed_wallet_balance, targetEqualizedBalance);
                GetNextAddress_Response address_Response = await GetNextAddress_Async(new GetNextAddress_RPC(wallet.id, false));
                if (!(bool)address_Response.success || string.IsNullOrEmpty(address_Response.address))
                    continue;
                while (true)
                {
                    GetSpendableCoins_Response coins_Response = await GetSpendableCoins_Async(wallet);
                    // check if target Balance is reached
                    if (coins_Response.confirmed_records == null ||
                        ((ulong)coins_Response.confirmed_records.Length >= minimumCoinAmount 
                        && (ulong)coins_Response.confirmed_records.Length <= maximumCoinAmount))
                        break;
                    Coin[] coins = coins_Response.GetSortedCoins(ascending: true);
                    if ((ulong)coins_Response.confirmed_records.Length < minimumCoinAmount)
                    {
                        // too little coins, split largest coins
                        ulong amount = (ulong)coins[^-1].amount;
                        SendTransaction_RPC transaction = new SendTransaction_RPC(
                            walletID_RPC: wallet,
                            address: address_Response.address,
                            amount_mojos: amount/2,
                            min_coin_amount: amount-1,
                            fee_mojos: xchFeeInMojos
                            );

                        CatSpend_RPC rpc = new CatSpend_RPC();
                        rpc.wallet_id = transaction.wallet_id;
                        rpc.amount = transaction.amount;
                        rpc.inner_address = transaction.address;
                        rpc.memos = new string[] { "Transfer" };
                        rpc.fee = transaction.fee;
                    }
                    else
                    {
                        // too many coins, merge smallest coins 
                    }
                }
                
                // exit logic
                if (walletId != 0)
                    break;
            }

            return false;
        }
    }
}
