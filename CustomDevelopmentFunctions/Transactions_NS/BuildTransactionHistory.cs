using Chia_Client_API.WalletAPI_NS;
using CHIA_RPC.Wallet_NS.CustomTransactionHistoryObjects_NS;

namespace CustomDevelopmentFunctions.Transactions_NS
{
    public class BuildTransactionHistory
    {
        [Fact]
        public void Test_BuildTransactionHistoryManual()
        {
            DirectoryInfo testDir = new DirectoryInfo(@"TestAssets\transactionHistory");
            //if (testDir.Exists) testDir.Delete(true);
            string certificatePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), @".testnet\testnetclientSsl\");
            Wallet_RPC_Client client = new Wallet_RPC_Client(reportResponseErrors: true, targetApiAddress: "kryp-chiatestclient", targetCertificateBaseFolder: certificatePath);
            CustomChiaTransactionBundle[] bundle =  client.BuildtransactionHistory_Async(testDir).Result;
        }
    }
}
