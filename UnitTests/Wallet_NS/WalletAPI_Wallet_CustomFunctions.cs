using CHIA_RPC.General_NS;
using Xunit;
using System.Threading.Tasks;
using CHIA_API_Tests.Initialisation_NS;
using System.IO;
using CHIA_RPC.Wallet_NS.Wallet_NS;

namespace CHIA_API_Tests.Wallet_NS
{
    [Collection("Testnet_Wallet")]
    public class WalletAPI_Wallet_CustomFunctions
    {
        [Fact]
        public async Task SeekBlockTransactionIndex_FindsFirstTransaction_Success()
        {
            // Arrange
            GetTransactions_RPC rpc = new GetTransactions_RPC(1);
            GetTransactions_Response response = await Testnet_Wallet.Wallet_Client.GetTransactions_Async(rpc);

            // Act
            ulong resultIndex = await Testnet_Wallet.Wallet_Client.SeekBlockTransactionIndex((ulong)response.transactions[10].confirmed_at_height, rpc);

            // Assert
            Assert.NotEqual(ulong.MaxValue, resultIndex); // Assuming ulong.MaxValue means 'not found'
            // Additional assertions as needed
        }
        [Fact]
        public async Task PullTransactionHistory()
        {
            FingerPrint_RPC? login_rpc = FingerPrint_RPC.LoadRpcFromFile("testfingerprint.rpc");
            DirectoryInfo directory = new DirectoryInfo("TestTransactionHistory");
            //if (directory.Exists) 
                //directory.Delete();

            //Chia_Client_API.Helpers_NS.WalletTransactionHistory history = new (Testnet_Wallet.Wallet_Client, login_rpc, directory);
            //await history.PullNewTransactions();
        }
    }
}
