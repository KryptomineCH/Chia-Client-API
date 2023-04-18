using CHIA_API_Tests.Initialisation_NS;
using CHIA_RPC.FullNode_NS;
using CHIA_RPC.General_NS;
using System.Threading.Tasks;
using Xunit;

namespace CHIA_API_Tests.FullNode_NS
{
    [Collection("Testnet_FullNode")]
    public class Fullnode_Tests
    {
        [Fact]
        public void GetAllMempoolItems_Test()
        {
            Task<GetAllMempoolItems_Response> response = Testnet_FullNode.Fullnode_Client.GetAllMempoolItems_Async();
            GetAllMempoolItems_Response results = response.Result;

            { }
        }
        [Fact]
        public void CheckHealthz_Test()
        {
            Success_Response test1 = Testnet_FullNode.Fullnode_Client.HealthZ_Sync();
            Task<Success_Response> response = Testnet_FullNode.Fullnode_Client.HealthZ_Async();
            Success_Response results = response.Result;

            { }
        }
    }
}
