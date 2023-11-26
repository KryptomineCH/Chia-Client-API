using Chia_Client_API.WalletAPI_NS;
using CHIA_RPC.Wallet_NS.CustomTransactionHistoryObjects_NS;

namespace CustomDevelopmentFunctions.Transactions_NS
{
    public class BuildTransactionHistory
    {
        [Fact]
        public async void Test_BuildTransactionHistoryManual()
        {
            DirectoryInfo testDir = new DirectoryInfo(Path.Combine("TestAssets","ransactionHistory"));
            //if (testDir.Exists) testDir.Delete(true);
            string certificatePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".testnet","testnetclientSsl");
            Wallet_RPC_Client client = new Wallet_RPC_Client(reportResponseErrors: true, targetApiAddress: "kryp-chiatestclient", targetCertificateBaseFolder: certificatePath);
            CustomChiaTransactionBundle[] bundle =  await client.BuildtransactionHistory_Async(testDir);
        }
    }
}
