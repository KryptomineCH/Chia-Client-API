using Chia_Client_API.ChiaClient_NS;
using CHIA_RPC.General_NS;
using CHIA_RPC.Objects_NS;
using CHIA_RPC.Wallet_NS.CATsAndTrading_NS;
using CHIA_RPC.Wallet_NS.Wallet_NS;
using CHIA_RPC.Wallet_NS.WalletManagement_NS;
using System.Diagnostics;

namespace Chia_Client_API.WalletAPI_NS
{
    public abstract partial class WalletRpcBase : ChiaEndpointRouteBase
    {
        public async Task<
            (Dictionary<ulong, List<Transaction_DictMemos>> TransactionsOrganizedByHeight, Dictionary<string, List<Transaction_DictMemos>> TransactionsOrganizedByAssetID)> 
            FetchAndOrganizeTransactions(ulong startHeight)
        {
            var transactionsByBlock = new Dictionary<ulong, List<Transaction_DictMemos>>();
            var transactionsByAssetIDValue = new Dictionary<string, List<Transaction_DictMemos>>();

            GetWallets_Response walletsResponse = await GetWallets_Async();
            foreach (Wallets_info wallet in walletsResponse.wallets)
            {
                GetTransactions_RPC rpc = new GetTransactions_RPC(wallet.id, start: startHeight, end: long.MaxValue, reverse: true);
                GetTransactions_Response transactions = await GetTransactions_Async(rpc);

                foreach (Transaction_DictMemos transaction in transactions.transactions)
                {
                    if (transaction.confirmed ?? false)
                    {
                        // Security checks
                        if (transaction.confirmed_at_height == null || transaction.confirmed_at_height == 0)
                        {
                            throw new InvalidDataException("Transaction height is null or zero!");
                        }

                        // filter non XCH or CAt transactions
                        Wallets_info transactionWallet = await GetWalletInfo_Async(transaction.wallet_id);
                        if (transactionWallet.type != WalletType.STANDARD_WALLET &&
                            transactionWallet.type != WalletType.CAT)
                            continue;

                        // Add to transactions by block height
                        if (!transactionsByBlock.TryGetValue(transaction.confirmed_at_height.Value, out var transactionsListBlock))
                        {
                            transactionsListBlock = new List<Transaction_DictMemos>();
                            transactionsByBlock[transaction.confirmed_at_height.Value] = transactionsListBlock;
                        }
                        transactionsListBlock.Add(transaction);

                        // Add to transactions by asset ID value
                        string identifier = "xch";
                        
                        if (transactionWallet.type == WalletType.CAT)
                        {
                            CatGetAssetId_Response response = await CatGetAssetID_Async(transaction);
                            identifier = response.asset_id;
                            if (identifier is null)
                                Debugger.Break();
                        }
                        
                        if (!transactionsByAssetIDValue.TryGetValue(identifier, out var transactionsListAssetID))
                        {
                            transactionsListAssetID = new List<Transaction_DictMemos>();
                            transactionsByAssetIDValue[identifier] = transactionsListAssetID;
                        }
                        transactionsListAssetID.Add(transaction);
                    }
                }
            }

            return (transactionsByBlock, transactionsByAssetIDValue);
        }
    }
}
