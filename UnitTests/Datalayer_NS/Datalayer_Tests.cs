using CHIA_API_Tests.Initialisation_NS;
using CHIA_RPC.Datalayer_NS;
using CHIA_RPC.Datalayer_NS.DatalayerObjects_NS;
using CHIA_RPC.General_NS;
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
        [Trait("Category", "Automatic")]
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
        [Fact]
        [Trait("Category", "Automatic")]
        public void GetKeys_Test()
        {
            GetOwnedStores_Response owned_stores = Testnet_Datalayer.Datalayer_Client.GetOwnedStores_Sync();
            Assert.True(owned_stores.store_ids.Length > 0, "There are no stores in your wallet.");
            GetKeys_RPC rpc = new GetKeys_RPC(owned_stores.store_ids[0]);
            GetKeys_Response response = Testnet_Datalayer.Datalayer_Client.GetKeys_Sync(rpc);
            Assert.True(response.success, response.error);
            Assert.Null(response.error);
            Assert.True(response.keys.Any(), "there are no keys present!");
            { }
        }
        [Fact]
        [Trait("Category", "Automatic")]
        public void GetKeysValues_Test()
        {
            GetOwnedStores_Response owned_stores = Testnet_Datalayer.Datalayer_Client.GetOwnedStores_Sync();
            Assert.True(owned_stores.store_ids.Length > 0, "There are no stores in your wallet.");
            GetKeys_RPC rpc = new GetKeys_RPC(owned_stores.store_ids[0]);
            GetKeysValues_Response response = Testnet_Datalayer.Datalayer_Client.GetKeysValues_Sync(rpc);
            Assert.True(response.success, response.error);
            Assert.Null(response.error);
            Assert.True(response.keys_values.Any(), "there are no keys present!");
            { }
        }
        [Fact]
        [Trait("Category", "Automatic")]
        public void GetAncestors_Test()
        {
            GetOwnedStores_Response owned_stores = Testnet_Datalayer.Datalayer_Client.GetOwnedStores_Sync();
            Assert.True(owned_stores.store_ids.Length > 0, "There are no stores in your wallet.");
            GetLocalRoot_Response localRoot = Testnet_Datalayer.Datalayer_Client.GetLocalRoot_Sync(owned_stores);
            GetAncestors_RPC rpc = new GetAncestors_RPC(owned_stores.store_ids[0], localRoot.hash);
            GetAncestors_Response response = Testnet_Datalayer.Datalayer_Client.GetAncestors_Sync(rpc);
            Assert.True(response.success, response.error);
            Assert.Null(response.error);
            { }
        }
        [Fact]
        [Trait("Category", "Automatic")]
        public void GetRoot_Test()
        {
            GetOwnedStores_Response owned_stores = Testnet_Datalayer.Datalayer_Client.GetOwnedStores_Sync();
            Assert.True(owned_stores.store_ids.Length > 0, "There are no stores in your wallet.");
            GetRoot_Response response = Testnet_Datalayer.Datalayer_Client.GetRoot_Sync(owned_stores);
            Assert.True(response.success, response.error);
            Assert.Null(response.error);
            { }
        }
        [Fact]
        [Trait("Category", "Automatic")]
        public void GetLocalRoot_Test()
        {
            GetOwnedStores_Response owned_stores = Testnet_Datalayer.Datalayer_Client.GetOwnedStores_Sync();
            Assert.True(owned_stores.store_ids.Length > 0, "There are no stores in your wallet.");
            GetLocalRoot_Response response = Testnet_Datalayer.Datalayer_Client.GetLocalRoot_Sync(owned_stores);
            Assert.True(response.success, response.error);
            Assert.Null(response.error);
            { }
        }
        [Fact]
        [Trait("Category", "Automatic")]
        public void GetRoots_Test()
        {
            GetOwnedStores_Response owned_stores = Testnet_Datalayer.Datalayer_Client.GetOwnedStores_Sync();
            Assert.True(owned_stores.store_ids.Length > 0, "There are no stores in your wallet.");
            GetRoots_Response response = Testnet_Datalayer.Datalayer_Client.GetRoots_Sync(owned_stores);
            Assert.True(response.success, response.error);
            Assert.Null(response.error);
            { }
        }
        [Fact]
        [Trait("Category", "Automatic")]
        public void DeleteKey_Test()
        {
            GetOwnedStores_Response owned_stores = Testnet_Datalayer.Datalayer_Client.GetOwnedStores_Sync();
            Assert.True(owned_stores.store_ids.Length > 0, "There are no stores in your wallet.");
            Insert_RPC prep = new Insert_RPC(owned_stores.store_ids[0], "000004", "abc123");
            TxID_Response prepresult = Testnet_Datalayer.Datalayer_Client.InsertOrUpdate_Async(prep).Result;
            DeleteKey_RPC rpc = new DeleteKey_RPC(owned_stores.store_ids[0], "000004");
            TxID_Response response = Testnet_Datalayer.Datalayer_Client.DeleteKey_Sync(rpc);
            Assert.True(response.success, response.error);
            Assert.Null(response.error);
            { }
        }
        [Fact]
        [Trait("Category", "Automatic")]
        public void InsertKey_Test()
        {
            GetOwnedStores_Response owned_stores = Testnet_Datalayer.Datalayer_Client.GetOwnedStores_Sync();
            Assert.True(owned_stores.store_ids.Length > 0, "There are no stores in your wallet.");
            Insert_RPC failRPC = new Insert_RPC(owned_stores.store_ids[0], "0003", "abc123");
            TxID_Response failResponse = Testnet_Datalayer.Datalayer_Client.Insert_Sync(failRPC);
            Assert.False(failResponse.success, "transaction should have failed!");
            DeleteKey_RPC rpc = new DeleteKey_RPC(owned_stores.store_ids[0], "000005");
            TxID_Response delresp = Testnet_Datalayer.Datalayer_Client.DeleteKey_Sync(rpc);
            Insert_RPC insert = new Insert_RPC(owned_stores.store_ids[0], "000005", "abc123");
            TxID_Response response = Testnet_Datalayer.Datalayer_Client.Insert_Sync(failRPC);
            Assert.True(response.success, response.error);
            Assert.Null(response.error);
            { }
        }
    }
}
