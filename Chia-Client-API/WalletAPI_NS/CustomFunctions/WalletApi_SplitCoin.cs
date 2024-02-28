using Chia_Client_API.ChiaClient_NS;
using CHIA_RPC.FullNode_NS;
using CHIA_RPC.General_NS;
using CHIA_RPC.Objects_NS;
using CHIA_RPC.Wallet_NS.Wallet_NS;
using CHIA_RPC.Wallet_NS.WalletManagement_NS;
using System.Diagnostics;

namespace Chia_Client_API.WalletAPI_NS
{
    public partial class WalletRpcClient
    {
        public async Task<GetTransaction_Response> SplitCoin(
            ulong walletId, ulong feeMojos, ulong numberOfCoins, decimal amountPerCoin, Coin coinToSplit)
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
            if (isXchTransaction)
                totalMojoAmount += feeMojos;
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
