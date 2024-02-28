using CHIA_RPC.Objects_NS;
using CHIA_RPC.Wallet_NS.Wallet_NS;
using CHIA_RPC.Wallet_NS.WalletManagement_NS;

namespace Chia_Client_API.WalletAPI_NS
{
    public partial class WalletRpcClient
    {
        /// <summary>
        /// this function takes an array of coins and combines them within the same wallet
        /// </summary>
        /// <param name="walletId">the wallet where the coins are in</param>
        /// <param name="feeMojos">an optional blockchain fee in xch mojos</param>
        /// <param name="coinsToCombine">the coins which are to be combined into one large coin</param>
        /// <returns></returns>
        public async Task<GetTransaction_Response> CombineCoins(
            ulong walletId, Coin[] coinsToCombine, ulong? feeMojos)
        {
            GetTransaction_Response response = new GetTransaction_Response();
            response.success = false;
            response.error = "unknown";
            // check if we are not trying to create too many coins
            if (coinsToCombine.Length > 500)
            {
                response.error = $"{coinsToCombine.Length} coins is greater than the maximum limit of 500 coins.";
                return response;
            }
            // get wallet info
            Wallets_info walletInfo_Response = await GetWalletInfo_Async(walletId);
            bool isXchTransaction = walletInfo_Response.type == WalletType.STANDARD_WALLET;
            // verify wallet is of correct type
            if (!isXchTransaction && walletInfo_Response.type != WalletType.CAT)
            {
                response.error = $"Can only split coins of xch or cat Wallet!";
                return response;
            }
            // calculate transaction amount            
            ulong totalMojoAmount = 0;
            foreach(Coin coin in coinsToCombine)
            {
                totalMojoAmount += coin.amount.Value;
            }
            if (isXchTransaction && feeMojos != null)
                totalMojoAmount -= feeMojos.Value;

            // Generate addition
            GetNextAddress_Response address_Response = await GetNextAddress_Async(new GetNextAddress_RPC(walletId, true));
            MultiTransactionRecipient[] additions = new[] { new MultiTransactionRecipient(totalMojoAmount, address_Response.address) };
            
            // send multi transaction
            SendTransactionMulti_RPC sendRpc = new SendTransactionMulti_RPC(walletId, additions,coinsToCombine, feeMojos);
            
            await AwaitWalletSync_Async(CancellationToken.None);
            response = await SendTransactionMulti_Async(sendRpc);
            return response;
        }
    }
}
