﻿using CHIA_API_Tests.Initialisation_NS;
using CHIA_RPC.Datalayer_NS;
using CHIA_RPC.General_NS;
using Xunit;

namespace CHIA_API_Tests.Datalayer_NS
{
    [Collection("Testnet_Datalayer_Wallet")]
    public class Datalayer_Connections_Tests
    {
        [Fact]
        [Trait("Category", "Automatic")]
        public void GetConnections_Test()
        {
            GetConnections_Response? response = Testnet_Datalayer.Datalayer_Client.GetConnections_Sync();
            Assert.NotNull(response);
            Assert.True(response!.success, response.error);
            Assert.Null(response.error);
            { }
        }
        [Fact]
        [Trait("Category", "Automatic")]
        public void GetRoutes_Test()
        {
            GetRoutes_Response? response = Testnet_Datalayer.Datalayer_Client.GetRoutes_Sync();
            Assert.NotNull(response);
            Assert.True(response!.success, response.error);
            Assert.Null(response.error);
            { }
        }
    }
}
