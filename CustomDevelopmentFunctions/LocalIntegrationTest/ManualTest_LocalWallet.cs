using Chia_Client_API.WalletAPI_NS;
using CHIA_RPC.Objects_NS;
using System.Diagnostics;

namespace CustomDevelopmentFunctions.LocalIntegrationTest
{
    public class ManualTest_LocalWallet
    {
        [Fact]
        public void Test_ConnectLocalWallet()
        {
            WalletRpcClient wallet = new WalletRpcClient(false);
            var response = wallet.GetWalletBalance_Sync(1);
            Assert.True(response.success);
        }
    }
}
