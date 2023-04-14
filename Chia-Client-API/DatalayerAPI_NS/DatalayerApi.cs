
using CHIA_RPC.Datalayer_NS;
using CHIA_RPC.General_NS;
using CHIA_RPC.Wallet_NS.RoutesAndConnections_NS;
using System.Text.Json;

namespace Chia_Client_API.DatalayerAPI_NS
{
    public partial class Datalayer_RPC_Client
    {
        /// <summary>
        /// Create a data store. Triggers a Chia transaction
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#create_data_store"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<CreateDataStore_Response> CreateDataStore_Async(CreateDataStore_RPC rpc)
        {

            string response = await SendCustomMessage_Async("create_data_store", rpc.ToString());
            CreateDataStore_Response deserializedObject = JsonSerializer.Deserialize<CreateDataStore_Response>(response);
            return deserializedObject;
        }
        /// <summary>
        /// Create a data store. Triggers a Chia transaction
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#create_data_store"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public CreateDataStore_Response CreateDataStore_Sync(CreateDataStore_RPC rpc)
        {
            Task<CreateDataStore_Response> data = Task.Run(() => CreateDataStore_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// List the id (store_id) of each data_store owned by this wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#get_owned_stores"/></remarks>
        /// <returns></returns>
        public async Task<GetOwnedStores_Response> GetOwnedStores_Async()
        {
            string response = await SendCustomMessage_Async("get_owned_stores");
            GetOwnedStores_Response deserializedObject = JsonSerializer.Deserialize<GetOwnedStores_Response>(response);
            return deserializedObject;
        }
        /// <summary>
        /// List the id (store_id) of each data_store owned by this wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#get_owned_stores"/></remarks>
        /// <returns></returns>
        public GetOwnedStores_Response GetOwnedStores_Sync()
        {
            Task<GetOwnedStores_Response> data = Task.Run(() => GetOwnedStores_Async());
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Apply multiple updates to a data store with a given changelist. Triggers a Chia transaction
        /// </summary>/
        /// <remarks>
        /// - The entire list must be formatted as a JSON array <br/>
        /// - There are two actions allowed: insert and delete <br/>
        /// - insert requires key and value flags <br/>
        /// - delete requires a key flag only <br/>
        /// - Keys and values must be written in hex format.Values can be derived from text or binary. <br/>
        /// - Labels, keys and values must all be enclosed in double quotes <br/>
        /// - Multiple inserts and deletes are allowed <br/>
        /// - The size of a single value flag is limited to 100 MiB.However, adding anything close to that size has not been tested and could produce unexpected results <br/><br/>
        /// <see href="https://docs.chia.net/datalayer-rpc#batch_update"/>
        /// </remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<TxID_Response> BatchUpdate_Async(BatchUpdate_RPC rpc)
        {
            string response = await SendCustomMessage_Async("batch_update", rpc.ToString());
            TxID_Response deserializedObject = JsonSerializer.Deserialize<TxID_Response>(response);
            return deserializedObject;
        }
        /// <summary>
        /// Apply multiple updates to a data store with a given changelist. Triggers a Chia transaction
        /// </summary>/
        /// <remarks>
        /// - The entire list must be formatted as a JSON array <br/>
        /// - There are two actions allowed: insert and delete <br/>
        /// - insert requires key and value flags <br/>
        /// - delete requires a key flag only <br/>
        /// - Keys and values must be written in hex format.Values can be derived from text or binary. <br/>
        /// - Labels, keys and values must all be enclosed in double quotes <br/>
        /// - Multiple inserts and deletes are allowed <br/>
        /// - The size of a single value flag is limited to 100 MiB.However, adding anything close to that size has not been tested and could produce unexpected results <br/><br/>
        /// <see href="https://docs.chia.net/datalayer-rpc#batch_update"/>
        /// </remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public TxID_Response BatchUpdate_Sync(BatchUpdate_RPC rpc)
        {
            Task<TxID_Response> data = Task.Run(() => BatchUpdate_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Given a key and the data store in which the key is located, return corresponding value
        /// </summary>
        /// <remarks>The root_hash parameter is recommended to be used each time you call this RPC. 
        /// If root_hash is not specified, there is no way to guarantee that the latest data is being shown (stale data may be shown instead). 
        /// This parameter is obtainable by calling the get_root RPC.<br/><br/>
        /// <see href="https://docs.chia.net/datalayer-rpc#get_value"/>
        /// </remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetValue_Response> GetValue_Async(GetValue_RPC rpc)
        {
            string response = await SendCustomMessage_Async("get_value", rpc.ToString());
            GetValue_Response deserializedObject = JsonSerializer.Deserialize<GetValue_Response>(response);
            return deserializedObject;
        }
        /// <summary>
        /// Given a key and the data store in which the key is located, return corresponding value
        /// </summary>
        /// <remarks>The root_hash parameter is recommended to be used each time you call this RPC. 
        /// If root_hash is not specified, there is no way to guarantee that the latest data is being shown (stale data may be shown instead). 
        /// This parameter is obtainable by calling the get_root RPC.<br/><br/>
        /// <see href="https://docs.chia.net/datalayer-rpc#get_value"/>
        /// </remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetValue_Response GetValue_Sync(GetValue_RPC rpc)
        {
            Task<GetValue_Response> data = Task.Run(() => GetValue_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Get all keys associated with a store_id
        /// </summary>
        /// <remarks>The root_hash parameter is recommended to be used each time you call this RPC. 
        /// If root_hash is not specified, there is no way to guarantee that the latest data is being shown (stale data may be shown instead). 
        /// This parameter is obtainable by calling the get_root RPC.<br/><br/>
        /// <see href="https://docs.chia.net/datalayer-rpc#get_keys"/>
        /// </remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetKeys_Response> GetKeys_Async(GetKeys_RPC rpc)
        {
            string response = await SendCustomMessage_Async("get_keys", rpc.ToString());
            GetKeys_Response deserializedObject = JsonSerializer.Deserialize<GetKeys_Response>(response);
            return deserializedObject;
        }
        /// <summary>
        /// Get all keys associated with a store_id
        /// </summary>
        /// <remarks>The root_hash parameter is recommended to be used each time you call this RPC. 
        /// If root_hash is not specified, there is no way to guarantee that the latest data is being shown (stale data may be shown instead). 
        /// This parameter is obtainable by calling the get_root RPC.<br/><br/>
        /// <see href="https://docs.chia.net/datalayer-rpc#get_keys"/>
        /// </remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetKeys_Response GetKeys_Sync(GetKeys_RPC rpc)
        {
            Task<GetKeys_Response> data = Task.Run(() => GetKeys_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Get all keys and values for a store. Must be subscribed to store ID
        /// </summary>
        /// <remarks>The root_hash parameter is recommended to be used each time you call this RPC. 
        /// If root_hash is not specified, there is no way to guarantee that the latest data is being shown (stale data may be shown instead). 
        /// This parameter is obtainable by calling the get_root RPC.<br/><br/>
        /// <see href="https://docs.chia.net/datalayer-rpc#get_keys_values"/>
        /// </remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetKeysValues_Response> GetKeysValues_Async(GetKeys_RPC rpc)
        {
            string response = await SendCustomMessage_Async("get_keys_values", rpc.ToString());
            GetKeysValues_Response deserializedObject = JsonSerializer.Deserialize<GetKeysValues_Response>(response);
            return deserializedObject;
        }
        /// <summary>
        /// Get all keys and values for a store. Must be subscribed to store ID
        /// </summary>
        /// <remarks>The root_hash parameter is recommended to be used each time you call this RPC. 
        /// If root_hash is not specified, there is no way to guarantee that the latest data is being shown (stale data may be shown instead). 
        /// This parameter is obtainable by calling the get_root RPC.<br/><br/>
        /// <see href="https://docs.chia.net/datalayer-rpc#get_keys_values"/>
        /// </remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetKeysValues_Response GetKeysValues_Sync(GetKeys_RPC rpc)
        {
            Task<GetKeysValues_Response> data = Task.Run(() => GetKeysValues_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Return all ancestors of a given hash
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#get_ancestors"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetAncestors_Response> GetAncestors_Async(GetAncestors_RPC rpc)
        {
            string response = await SendCustomMessage_Async("get_ancestors", rpc.ToString());
            GetAncestors_Response deserializedObject = JsonSerializer.Deserialize<GetAncestors_Response>(response);
            return deserializedObject;
        }
        /// <summary>
        /// Return all ancestors of a given hash
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#get_ancestors"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetAncestors_Response GetAncestors_Sync(GetAncestors_RPC rpc)
        {
            Task<GetAncestors_Response> data = Task.Run(() => GetAncestors_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Get the root hash and timestamp of a given store ID. 
        /// If it is a subscribed store, this command will return an invalid hash (see example). 
        /// In this case, use get_local_root instead
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#get_root"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetRoot_Response> GetRoot_Async(ID_RPC rpc)
        {
            string response = await SendCustomMessage_Async("get_root", rpc.ToString());
            GetRoot_Response deserializedObject = JsonSerializer.Deserialize<GetRoot_Response>(response);
            return deserializedObject;
        }
        /// <summary>
        /// Get the root hash and timestamp of a given store ID. 
        /// If it is a subscribed store, this command will return an invalid hash (see example). 
        /// In this case, use get_local_root instead
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#get_root"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetRoot_Response GetRoot_Sync(ID_RPC rpc)
        {
            Task<GetRoot_Response> data = Task.Run(() => GetRoot_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Get the root hash and timestamp of a store ID. Can be used for either owned or subscribed stores
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#get_local_root"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetLocalRoot_Response> GetLocalRoot_Async(ID_RPC rpc)
        {
            string response = await SendCustomMessage_Async("get_local_root", rpc.ToString());
            GetLocalRoot_Response deserializedObject = JsonSerializer.Deserialize<GetLocalRoot_Response>(response);
            return deserializedObject;
        }
        /// <summary>
        /// Get the root hash and timestamp of a store ID. Can be used for either owned or subscribed stores
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#get_local_root"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetLocalRoot_Response GetLocalRoot_Sync(ID_RPC rpc)
        {
            Task<GetLocalRoot_Response> data = Task.Run(() => GetLocalRoot_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Get the root hashes and timestamps from a list of stores. Note that an invalid hash will be returned for subscribed stores. Use get_local_root instead
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#get_roots"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetRoots_Response> GetRoots_Async(GetRoots_RPC rpc)
        {
            string response = await SendCustomMessage_Async("get_roots", rpc.ToString());
            GetRoots_Response deserializedObject = JsonSerializer.Deserialize<GetRoots_Response>(response);
            return deserializedObject;
        }
        /// <summary>
        /// Get the root hashes and timestamps from a list of stores. Note that an invalid hash will be returned for subscribed stores. Use get_local_root instead
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#get_roots"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetRoots_Response GetRoots_Sync(GetRoots_RPC rpc)
        {
            Task<GetRoots_Response> data = Task.Run(() => GetRoots_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Delete a key/value pair from a store that you control. Triggers a Chia transaction
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#delete_key"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<TxID_Response> DeleteKey_Async(DeleteKey_RPC rpc)
        {
            string response = await SendCustomMessage_Async("delete_key", rpc.ToString());
            TxID_Response deserializedObject = JsonSerializer.Deserialize<TxID_Response>(response);
            return deserializedObject;
        }
        /// <summary>
        /// Delete a key/value pair from a store that you control. Triggers a Chia transaction
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#delete_key"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public TxID_Response DeleteKey_Sync(DeleteKey_RPC rpc)
        {
            Task<TxID_Response> data = Task.Run(() => DeleteKey_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Insert a key/value pair into a store that you control. Triggers a Chia transaction
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#insert"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<TxID_Response> Insert_Async(Insert_RPC rpc)
        {
            string response = await SendCustomMessage_Async("insert", rpc.ToString());
            TxID_Response deserializedObject = JsonSerializer.Deserialize<TxID_Response>(response);
            return deserializedObject;
        }
        /// <summary>
        /// Insert a key/value pair into a store that you control. Triggers a Chia transaction
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#insert"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public TxID_Response Insert_Sync(Insert_RPC rpc)
        {
            Task<TxID_Response> data = Task.Run(() => Insert_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Subscribe to a store ID
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#subscribe"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<Success_Response> Subscribe_Async(Subscribe_RPC rpc)
        {
            string response = await SendCustomMessage_Async("subscribe", rpc.ToString());
            Success_Response deserializedObject = JsonSerializer.Deserialize<Success_Response>(response);
            return deserializedObject;
        }
        /// <summary>
        /// Subscribe to a store ID
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#subscribe"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public Success_Response Subscribe_Sync(Subscribe_RPC rpc)
        {
            Task<Success_Response> data = Task.Run(() => Subscribe_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Unsubscribe from a store ID
        /// </summary>
        /// <remarks>
        /// This RPC does not remove any data from the database or the filesystem.<br/><br/>
        /// <see href="https://docs.chia.net/datalayer-rpc#unsubscribe"/>
        /// </remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<Success_Response> Unsubscribe_Async(ID_RPC rpc)
        {
            string response = await SendCustomMessage_Async("unsubscribe", rpc.ToString());
            Success_Response deserializedObject = JsonSerializer.Deserialize<Success_Response>(response);
            return deserializedObject;
        }
        /// <summary>
        /// Unsubscribe from a store ID
        /// </summary>
        /// <remarks>
        /// This RPC does not remove any data from the database or the filesystem.<br/>
        /// <see href="https://docs.chia.net/datalayer-rpc#unsubscribe"/>
        /// </remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public Success_Response Unsubscribe_Sync(ID_RPC rpc)
        {
            Task<Success_Response> data = Task.Run(() => Unsubscribe_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// List the store ID for each current subscription
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#subscriptions"/></remarks>
        /// <returns></returns>
        public async Task<Subscriptions_Response> Subscriptions_Async()
        {
            string response = await SendCustomMessage_Async("subscriptions");
            Subscriptions_Response deserializedObject = JsonSerializer.Deserialize<Subscriptions_Response>(response);
            return deserializedObject;
        }
        /// <summary>
        /// List the store ID for each current subscription
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#subscriptions"/></remarks>
        /// <returns></returns>
        public Subscriptions_Response Subscriptions_Sync()
        {
            Task<Subscriptions_Response> data = Task.Run(() => Subscriptions_Async());
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Remove one or more URLs from a data store to which you subscribe.
        /// Note that this action will not remove the subscription to the data store itself. For that functionality, use unsubscribe
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#remove_subscriptions"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<Success_Response> RemoveSubscriptions_Async(Subscribe_RPC rpc)
        {
            string response = await SendCustomMessage_Async("remove_subscriptions", rpc.ToString());
            Success_Response deserializedObject = JsonSerializer.Deserialize<Success_Response>(response);
            return deserializedObject;
        }
        /// <summary>
        /// Remove one or more URLs from a data store to which you subscribe. 
        /// Note that this action will not remove the subscription to the data store itself. For that functionality, use unsubscribe
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#remove_subscriptions"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public Success_Response RemoveSubscriptions_Sync(Subscribe_RPC rpc)
        {
            Task<Success_Response> data = Task.Run(() => RemoveSubscriptions_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Use the database to restore all files for one or more owned data stores
        /// </summary>
        /// <remarks>
        /// For subscribed stores, this command will do nothing. Use unsubscribe and subscribe instead<br/><br/>
        /// <see href="https://docs.chia.net/datalayer-rpc#add_missing_files"/>
        /// </remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<Success_Response> AddMissingFiles_Async(AddMissingFiles_RPC rpc)
        {
            string response = await SendCustomMessage_Async("add_missing_files", rpc.ToString());
            Success_Response deserializedObject = JsonSerializer.Deserialize<Success_Response>(response);
            return deserializedObject;
        }
        /// <summary>
        /// Use the database to restore all files for one or more owned data stores
        /// </summary>
        /// <remarks>
        /// For subscribed stores, this command will do nothing. Use unsubscribe and subscribe instead<br/><br/>
        /// <see href="https://docs.chia.net/datalayer-rpc#add_missing_files"/>
        /// </remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public Success_Response AddMissingFiles_Sync(AddMissingFiles_RPC rpc)
        {
            Task<Success_Response> data = Task.Run(() => AddMissingFiles_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Get the kv diff between two hashes within the same store ID
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#get_kv_diff"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetKvDiff_Response> GetKVDiff_Async(GetKvDiff_RPC rpc)
        {
            string response = await SendCustomMessage_Async("get_kv_diff", rpc.ToString());
            GetKvDiff_Response deserializedObject = JsonSerializer.Deserialize<GetKvDiff_Response>(response);
            return deserializedObject;
        }
        /// <summary>
        /// Get the kv diff between two hashes within the same store ID
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#get_kv_diff"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetKvDiff_Response GetKVDiff_Sync(GetKvDiff_RPC rpc)
        {
            Task<GetKvDiff_Response> data = Task.Run(() => GetKVDiff_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Get a history of root hashes for a Store ID that you subscribe to
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#get_root_history"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetRootHistory_Response> GetRootHistory_Async(ID_RPC rpc)
        {
            string response = await SendCustomMessage_Async("get_root_history", rpc.ToString());
            GetRootHistory_Response deserializedObject = JsonSerializer.Deserialize<GetRootHistory_Response>(response);
            return deserializedObject;
        }
        /// <summary>
        /// Get a history of root hashes for a Store ID that you subscribe to
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#get_root_history"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetRootHistory_Response GetRootHistory_Sync(ID_RPC rpc)
        {
            Task<GetRootHistory_Response> data = Task.Run(() => GetRootHistory_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Add a new mirror from an owned or subscribed data store. Triggers a Chia transaction
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#add_mirror"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<Success_Response> AddMirror_Async(AddMirror_RPC rpc)
        {
            string response = await SendCustomMessage_Async("add_mirror", rpc.ToString());
            Success_Response deserializedObject = JsonSerializer.Deserialize<Success_Response>(response);
            return deserializedObject;
        }
        /// <summary>
        /// Add a new mirror from an owned or subscribed data store. Triggers a Chia transaction
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#add_mirror"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public Success_Response AddMirror_Sync(AddMirror_RPC rpc)
        {
            Task<Success_Response> data = Task.Run(() => AddMirror_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Delete a mirror, by coin_id. Triggers a Chia transaction
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#delete_mirror"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<Success_Response> DeleteMirror_Async(ID_RPC rpc)
        {
            string response = await SendCustomMessage_Async("delete_mirror", rpc.ToString());
            Success_Response deserializedObject = JsonSerializer.Deserialize<Success_Response>(response);
            return deserializedObject;
        }
        /// <summary>
        /// Delete a mirror, by coin_id. Triggers a Chia transaction
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#delete_mirror"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public Success_Response DeleteMirror_Sync(ID_RPC rpc)
        {
            Task<Success_Response> data = Task.Run(() => DeleteMirror_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// List all mirrors for a store ID
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#get_mirrors"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetMirrors_Response> GetMirrors_Async(ID_RPC rpc)
        {
            string response = await SendCustomMessage_Async("get_mirrors", rpc.ToString());
            GetMirrors_Response deserializedObject = JsonSerializer.Deserialize<GetMirrors_Response>(response);
            return deserializedObject;
        }
        /// <summary>
        /// List all mirrors for a store ID
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#get_mirrors"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetMirrors_Response GetMirrors_Sync(ID_RPC rpc)
        {
            Task<GetMirrors_Response> data = Task.Run(() => GetMirrors_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Make an offer to include one or more keys in exchange for a Taker including one or more keys
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#make_offer"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<MakeOffer_Response> MakeOffer_Async(MakeOffer_RPC rpc)
        {
            string response = await SendCustomMessage_Async("make_offer", rpc.ToString());
            MakeOffer_Response deserializedObject = JsonSerializer.Deserialize<MakeOffer_Response>(response);
            return deserializedObject;
        }
        /// <summary>
        /// Make an offer to include one or more keys in exchange for a Taker including one or more keys
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#make_offer"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public MakeOffer_Response MakeOffer_Sync(MakeOffer_RPC rpc)
        {
            Task<MakeOffer_Response> data = Task.Run(() => MakeOffer_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Accept an offer to create one or more keys in exchange for the Maker creating one or more keys. Triggers a Chia transaction
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#take_offer"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<TradeID_Response> TakeOffer_Async(TakeOffer_RPC rpc)
        {
            string response = await SendCustomMessage_Async("take_offer", rpc.ToString());
            TradeID_Response deserializedObject = JsonSerializer.Deserialize<TradeID_Response>(response);
            return deserializedObject;
        }
        /// <summary>
        /// Accept an offer to create one or more keys in exchange for the Maker creating one or more keys. Triggers a Chia transaction
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#take_offer"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public TradeID_Response TakeOffer_Sync(TakeOffer_RPC rpc)
        {
            Task<TradeID_Response> data = Task.Run(() => TakeOffer_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Verify that a DataLayer offer is well-formed
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#verify_offer"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<VerifyOffer_Response> VerifyOffer_Async(VerifyOffer_RPC rpc)
        {
            string response = await SendCustomMessage_Async("verify_offer", rpc.ToString());
            VerifyOffer_Response deserializedObject = JsonSerializer.Deserialize<VerifyOffer_Response>(response);
            return deserializedObject;
        }
        /// <summary>
        /// Verify that a DataLayer offer is well-formed
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#verify_offer"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public VerifyOffer_Response VerifyOffer_Sync(VerifyOffer_RPC rpc)
        {
            Task<VerifyOffer_Response> data = Task.Run(() => VerifyOffer_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Cancel a DataLayer offer
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#cancel_offer"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<Success_Response> CancelOffer_Async(CancelOffer_RPC rpc)
        {
            string response = await SendCustomMessage_Async("cancel_offer", rpc.ToString());
            Success_Response deserializedObject = JsonSerializer.Deserialize<Success_Response>(response);
            return deserializedObject;
        }
        /// <summary>
        /// Cancel a DataLayer offer
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#cancel_offer"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public Success_Response CancelOffer_Sync(CancelOffer_RPC rpc)
        {
            Task<Success_Response> data = Task.Run(() => CancelOffer_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Get a list of active connections
        /// </summary>
        /// <remarks>
        /// Note: Inherited from RPC Server<br/><br/>
        /// <see href="https://docs.chia.net/datalayer-rpc#get_connections"/>
        /// </remarks>
        /// <returns></returns>
        public async Task<GetConnections_Response> GetConnections_Async()
        {
            string response = await SendCustomMessage_Async("get_connections");
            GetConnections_Response deserializedObject = JsonSerializer.Deserialize<GetConnections_Response>(response);
            return deserializedObject;
        }
        /// <summary>
        /// Get a list of active connections
        /// </summary>
        /// <remarks>
        /// Note: Inherited from RPC Server<br/><br/>
        /// <see href="https://docs.chia.net/datalayer-rpc#get_connections"/>
        /// </remarks>
        /// <returns></returns>
        public GetConnections_Response GetConnections_Sync()
        {
            Task<GetConnections_Response> data = Task.Run(() => GetConnections_Async());
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Open a connection to another node
        /// </summary>
        /// <remarks>
        /// Note: Inherited from RPC Server<br/><br/>
        /// <see href="https://docs.chia.net/datalayer-rpc#open_connection"/>
        /// </remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<Success_Response> OpenConnection_Async(OpenConnection_RPC rpc)
        {
            string response = await SendCustomMessage_Async("open_connection", rpc.ToString());
            Success_Response deserializedObject = JsonSerializer.Deserialize<Success_Response>(response);
            return deserializedObject;
        }
        /// <summary>
        /// Open a connection to another node
        /// </summary>
        /// <remarks>
        /// Note: Inherited from RPC Server<br/><br/>
        /// <see href="https://docs.chia.net/datalayer-rpc#open_connection"/>
        /// </remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public Success_Response OpenConnection_Sync(OpenConnection_RPC rpc)
        {
            Task<Success_Response> data = Task.Run(() => OpenConnection_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Close an active connection
        /// </summary>
        /// <remarks>
        /// Note: Inherited from RPC Server<br/><br/>
        /// <see href="https://docs.chia.net/datalayer-rpc#close_connection"/>
        /// </remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<Success_Response> CloseConnection_Async(CloseConnection_RPC rpc)
        {
            string response = await SendCustomMessage_Async("close_connection", rpc.ToString());
            Success_Response deserializedObject = JsonSerializer.Deserialize<Success_Response>(response);
            return deserializedObject;
        }
        /// <summary>
        /// Close an active connection
        /// </summary>
        /// <remarks>
        /// Note: Inherited from RPC Server<br/><br/>
        /// <see href="https://docs.chia.net/datalayer-rpc#close_connection"/>
        /// </remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public Success_Response CloseConnection_Sync(CloseConnection_RPC rpc)
        {
            Task<Success_Response> data = Task.Run(() => CloseConnection_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Stop your local node
        /// </summary>
        /// <remarks>
        /// Note: Inherited from RPC Server<br/><br/>
        /// <see href="https://docs.chia.net/datalayer-rpc#stop_node"/>
        /// </remarks>
        /// <returns></returns>
        public async Task<Success_Response> StopNode_Async()
        {
            string response = await SendCustomMessage_Async("stop_node");
            Success_Response deserializedObject = JsonSerializer.Deserialize<Success_Response>(response);
            return deserializedObject;
        }
        /// <summary>
        /// Stop your local node
        /// </summary>
        /// <remarks>
        /// Note: Inherited from RPC Server<br/><br/>
        /// <see href="https://docs.chia.net/datalayer-rpc#stop_node"/>
        /// </remarks>
        /// <returns></returns>
        public Success_Response StopNode_Sync()
        {
            Task<Success_Response> data = Task.Run(() => StopNode_Async());
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Obtain the current sync status for a provided data store
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#get_sync_status"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetDatalayerSyncStatus_Response> GetSyncStatus_Async(ID_RPC rpc)
        {
            string response = await SendCustomMessage_Async("close_connection", rpc.ToString());
            GetDatalayerSyncStatus_Response deserializedObject = JsonSerializer.Deserialize<GetDatalayerSyncStatus_Response>(response);
            return deserializedObject;
        }
        /// <summary>
        /// Obtain the current sync status for a provided data store
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#get_sync_status"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetDatalayerSyncStatus_Response GetSyncStatus_Sync(ID_RPC rpc)
        {
            Task<GetDatalayerSyncStatus_Response> data = Task.Run(() => GetSyncStatus_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Show a comprehensive list of RPC routes for the DataLayer
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#get_routes"/></remarks>
        /// <returns></returns>
        public async Task<GetRoutes_Response> GetRoutes_Async()
        {
            string response = await SendCustomMessage_Async("close_connection");
            GetRoutes_Response deserializedObject = JsonSerializer.Deserialize<GetRoutes_Response>(response);
            return deserializedObject;
        }
        /// <summary>
        /// Show a comprehensive list of RPC routes for the DataLayer
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#get_routes"/></remarks>
        /// <returns></returns>
        public GetRoutes_Response GetRoutes_Sync()
        {
            Task<GetRoutes_Response> data = Task.Run(() => GetRoutes_Async());
            data.Wait();
            return data.Result;
        }
    }
}
