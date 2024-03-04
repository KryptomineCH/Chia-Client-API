using Chia_Client_API.WalletAPI_NS;
using CHIA_RPC.General_NS;
using CHIA_RPC.Objects_NS;
using CHIA_RPC.Wallet_NS.Wallet_NS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionTypeTests.Transactions_NS
{
    public class ManualTest_MultiTransaction
    {
        [Fact]
        [Trait("Category", "Manual")]
        public async void Test_SplitCoinManual()
        {
            string certificatePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".testnet", "testnetclientSsl");
            WalletRpcClient client = new WalletRpcClient(reportResponseErrors: true, targetApiAddress: "kryp-chiatestclient", targetCertificateBaseFolder: certificatePath);
            await client.LogIn_Async(2960367160);
            await client.AwaitWalletSync_Async(CancellationToken.None);
            var spendableCoins_Response = await client.GetSpendableCoins_Async(new WalletID_RPC(1));
            var splitResult = await client.SplitCoin(1, 5, 0.001m, spendableCoins_Response.confirmed_records[0].coin, 1000);
            Assert.True(splitResult.success);
            var spendableCoinsAfter_Response = await client.GetSpendableCoins_Async(new WalletID_RPC(1));
            Assert.True(spendableCoins_Response.confirmed_records.Length < spendableCoinsAfter_Response.confirmed_records.Length);
        }

        [Fact]
        [Trait("Category", "Manual")]
        public async void Test_CombineCoinsManual()
        {
            //string certificatePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".testnet", "testnetclientSsl");
            //WalletRpcClient client = new WalletRpcClient(reportResponseErrors: true, targetApiAddress: "kryp-chiatestclient" /*, targetCertificateBaseFolder: certificatePath*/);
            WalletRpcClient client = new WalletRpcClient(reportResponseErrors: true);
            //await client.LogIn_Async(2960367160);
            await client.AwaitWalletSync_Async(CancellationToken.None);
            var spendableCoins_Response = await client.GetSpendableCoins_Async(new WalletID_RPC(5));
            Coin[] sortedCoins = spendableCoins_Response.GetSortedCoins();
            var mergeResult = await client.CombineCoins( walletId: 5, feeMojos: 1000, coinsToCombine: sortedCoins);
            Assert.True(mergeResult.success);
            var spendableCoinsAfter_Response = await client.GetSpendableCoins_Async(new WalletID_RPC(1));
            Assert.True(spendableCoins_Response.confirmed_records.Length < spendableCoinsAfter_Response.confirmed_records.Length);
        }
        [Fact]
        [Trait("Category", "Manual")]
        public async void Test_DustWalletManual()
        {
            //string certificatePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".testnet", "testnetclientSsl");
            //WalletRpcClient client = new WalletRpcClient(reportResponseErrors: true, targetApiAddress: "kryp-chiatestclient" /*, targetCertificateBaseFolder: certificatePath*/);
            WalletRpcClient client = new WalletRpcClient(reportResponseErrors: true);
            //await client.LogIn_Async(2960367160);
            await client.AwaitWalletSync_Async(CancellationToken.None);
            GetTransaction_Response result = await client.CleanDust(CancellationToken.None, 5, 1000,maximumBatchSize:200);
            var spendableCoinsAfter_Response = await client.GetSpendableCoins_Async(new WalletID_RPC(5));
            { }
        }
    }
}
