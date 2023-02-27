using Chia_Client_API.FullNodeAPI_NS;
using Xunit;

namespace UnitTests.FullNode_NS
{
    [Collection("Testnet")]
    public class FullNode_RPC_Tests
    {
        [Fact]
        public void GetAllMempoolItems_Test()
        {
            Testnet.Fullnode_Client.GetAllMempoolItems_Async().Wait();
        }
    }
}
