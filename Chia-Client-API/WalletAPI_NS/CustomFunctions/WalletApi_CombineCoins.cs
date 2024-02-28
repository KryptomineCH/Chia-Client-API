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
        public async Task<GetTransaction_Response> CombineCoins(
            ulong walletId, ulong feeMojos, Coin[] coinsToCombine)
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
            if (isXchTransaction)
                totalMojoAmount -= feeMojos;

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
