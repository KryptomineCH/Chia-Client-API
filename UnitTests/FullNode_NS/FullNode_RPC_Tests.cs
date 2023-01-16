using Chia_Client_API.FullNode_NS;
using Chia_Client_API.Wallet_NS.WalletAPI_NS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.FullNode_NS
{
    [Collection("Testnet")]
    public class FullNode_RPC_Tests
    {
        [Fact]
        public void GetAllMempoolItems_Test()
        {
            FullNodeApi.GetAllMempoolItems_Async().Wait();
        }
    }
}
