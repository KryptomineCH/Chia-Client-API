using CHIA_RPC.Objects_NS;
using CHIA_RPC.Wallet_NS.Wallet_NS;
using CHIA_RPC.Wallet_NS.WalletManagement_NS;

namespace Chia_Client_API.WalletAPI_NS
{
    public partial class WalletRpcClient
    {
        /// <summary>
        /// splits a given coin into multiple smaller ones, as per request
        /// </summary>
        /// <remarks>
        /// numberOfCoins * amountPerCoin + feeMojos must be smaller than coinToSplit.amount<br/>
        /// if there is a rest, you will get one additional coin which contains all the restValue
        /// </remarks>
        /// <param name="walletId">thes wallet where the coins are</param>
        /// <param name="feeMojos">an optional blockchain fee in mojos</param>
        /// <param name="numberOfCoins">the number of coins to create</param>
        /// <param name="amountPerCoin">the size of each subcoin</param>
        /// <param name="coinToSplit">the input coin which will be consumed</param>
        /// <returns></returns>
        public async Task<GetTransaction_Response> SplitCoin(
            ulong walletId,  ulong numberOfCoins, decimal amountPerCoin, Coin coinToSplit, ulong? feeMojos = null)
        {
            GetTransaction_Response response = new GetTransaction_Response();
            response.success = true;
            response.error = "unknown";
            // check if we are not trying to create too many coins
            if (numberOfCoins > 500)
            {
                response.error = $"{numberOfCoins} coins is greater than the maximum limit of 500 coins.";
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
            // calculate transaction amounts
            decimal mojosPerUnit = CHIA_RPC.General_NS.GlobalVar.OneChiaInMojos;
            if (!isXchTransaction)
                mojosPerUnit = CHIA_RPC.General_NS.GlobalVar.OneCatInMojos;
            ulong finalMojoAmountPerCoin = (ulong)(amountPerCoin * mojosPerUnit);
            ulong totalMojoAmount = finalMojoAmountPerCoin * numberOfCoins;
            if (isXchTransaction && feeMojos != null)
                totalMojoAmount += feeMojos.Value;
            // verify the coin is large enough to split
            if (coinToSplit.amount < totalMojoAmount)
            {
                response.error = $"The Coin is too small to split as requested!";
                return response;
            }

            // Generate additions
            GetNextAddress_RPC addressRpc = new GetNextAddress_RPC(walletId, true);
            List<MultiTransactionRecipient> additions = new List<MultiTransactionRecipient>();
            for (ulong i = 0; i < numberOfCoins; i++)
            {
                GetNextAddress_Response address_Response = await GetNextAddress_Async(addressRpc);
                additions.Add(new MultiTransactionRecipient(finalMojoAmountPerCoin, address_Response.address));
            }
            
            // send multi transaction
            SendTransactionMulti_RPC sendRpc = new SendTransactionMulti_RPC(walletId, additions.ToArray(),new[] { coinToSplit }, feeMojos);
            
            await AwaitWalletSync_Async(CancellationToken.None);
            response = await SendTransactionMulti_Async(sendRpc);
            return response;
        }
    }
}
