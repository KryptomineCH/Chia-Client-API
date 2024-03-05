using Chia_Client_API.DaemonAPI_NS;
using CHIA_RPC.Daemon_NS.Server_NS;
using Xunit;

namespace CHIA_API_Tests.Daemon_NS
{
    public class Daemon_Tests
    {
        [Fact]
        public async void Sample_Test()
        {
            DaemonRpcClient DaemonApi = new DaemonRpcClient(reportResponseErrors: false);
            var rpc = new GetWalletAddresses_RPC();
            rpc.fingerprints = new ulong[] { 2419455740 };
            rpc.index = 0;
            rpc.count = 10;
            var response = await DaemonApi.GetWalletAddresses_Async(rpc);
            { }
        }
    }
}
