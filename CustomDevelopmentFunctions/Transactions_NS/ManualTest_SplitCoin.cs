using Chia_Client_API.WalletAPI_NS;
using CHIA_RPC.General_NS;
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
            var splitResult = await client.SplitCoin(1, 1000, 5, 0.1m, spendableCoins_Response.confirmed_records[0].coin);
            Assert.True(splitResult.success);
            var spendableCoinsAfter_Response = await client.GetSpendableCoins_Async(new WalletID_RPC(1));
            Assert.True(spendableCoins_Response.confirmed_records.Length < spendableCoinsAfter_Response.confirmed_records.Length);
        }
    }
}
