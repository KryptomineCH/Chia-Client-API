using Chia_Client_API.WalletAPI_NS;
using CHIA_RPC.General_NS;
using CHIA_RPC.Objects_NS;
using CHIA_RPC.Wallet_NS.CATsAndTrading_NS;
using CHIA_RPC.Wallet_NS.Wallet_NS;
using System;
namespace TransactionTypeTests.Transactions_NS
{
    public class ManualTest_CleanExpiredOffers
    {
        [Fact]
        [Trait("Category", "Manual")]
        public async void Test_CleanExpiredOffers()
        {
            //string certificatePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".testnet", "testnetclientSsl");
            //WalletRpcClient client = new WalletRpcClient(reportResponseErrors: true, targetApiAddress: "kryp-chiatestclient", targetCertificateBaseFolder: certificatePath);
            WalletRpcClient client = new WalletRpcClient(reportResponseErrors: true);
            Success_Response success = await client.CleanExpiredOffers();
        }
    }
}
