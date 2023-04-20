using CHIA_API_Tests.Initialisation_NS;
using CHIA_RPC.Datalayer_NS;
using CHIA_RPC.Datalayer_NS.DatalayerObjects_NS;
using CHIA_RPC.General_NS;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using static CHIA_RPC.Datalayer_NS.DatalayerObjects_NS.DataStoreChange;

namespace CHIA_API_Tests.Datalayer_NS
{
    [Collection("Testnet_Datalayer_Wallet")]
    public class Datalayer_Tests
    {
        [Fact]
        [Trait("Category", "Manual")]
        public void CreateDataStore_Test()
        {
            CreateDataStore_RPC rpc = new CreateDataStore_RPC()
            {
                fee = 1000
            };
            CreateDataStore_Response response = Testnet_Datalayer.Datalayer_Client.CreateDataStore_Sync(rpc);
            Assert.True(response.success, response.error);
            Assert.Null(response.error);
            { }
        }
        [Fact]
        [Trait("Category", "Automatic")]
        public void GetOwnedStores_Test()
        {
            GetOwnedStores_Response response = Testnet_Datalayer.Datalayer_Client.GetOwnedStores_Sync();
            Assert.True(response.success, response.error);
            Assert.Null(response.error);
            { }
        }
        [Fact]
        [Trait("Category", "Manual")]
        public void BatchUpdate_Test()
        {
            // select store
            GetOwnedStores_Response owned_stores = Testnet_Datalayer.Datalayer_Client.GetOwnedStores_Sync();
            Assert.True(owned_stores.store_ids.Length > 0, "There are no stores in your wallet.");

            BatchUpdate_RPC batchUpdate = new BatchUpdate_RPC()
            {
                id = owned_stores.store_ids[0],
                fee = 1000,
                changelist = new DataStoreChange[]
                {
                    new DataStoreChange (DataStoreChangeAction.insert,"0003","abc123")
                }
            };
            TxID_Response response = Testnet_Datalayer.Datalayer_Client.BatchUpdate_Sync(batchUpdate);
            if (response.error == "Changelist resulted in no change to tree data") return;
            Assert.True(response.success, response.error);
            Assert.Null(response.error);
            { }
        }
        [Fact]
        [Trait("Category", "Automatic")]
        public void GetValue_Test()
        {
            GetOwnedStores_Response owned_stores = Testnet_Datalayer.Datalayer_Client.GetOwnedStores_Sync();
            Assert.True(owned_stores.store_ids.Length > 0, "There are no stores in your wallet.");
            GetLocalRoot_Response localRoot = Testnet_Datalayer.Datalayer_Client.GetLocalRoot_Sync(owned_stores);
            GetValue_RPC rpc = new GetValue_RPC()
            {
                id = owned_stores.store_ids[0],
                key = "0003",
                root_hash = localRoot.hash
            };
            GetValue_Response response = Testnet_Datalayer.Datalayer_Client.GetValue_Sync(rpc);
            Assert.True(response.success, response.error);
            Assert.Null(response.error);
            Assert.Equal("abc123", response.value);
            { }
        }

    }
}
