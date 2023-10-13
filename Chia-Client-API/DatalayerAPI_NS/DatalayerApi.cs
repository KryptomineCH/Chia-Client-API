﻿using CHIA_RPC.Datalayer_NS;
using CHIA_RPC.Datalayer_NS.DatalayerObjects_NS;
using CHIA_RPC.General_NS;
using CHIA_RPC.Wallet_NS.DLWallet_NS;
using CHIA_RPC.Wallet_NS.RoutesAndConnections_NS;
using System.Security.Cryptography;
using System.Text.Json;

namespace Chia_Client_API.DatalayerAPI_NS
{
    public partial class Datalayer_RPC_Client
    {
        /// <summary>
        /// Add a new mirror from an owned or subscribed data store. Triggers a Chia transaction
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#add_mirror"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<Success_Response?> AddMirror_Async(AddMirror_RPC rpc)
        {
            string response = await SendCustomMessage_Async("add_mirror", rpc.ToString());
            Success_Response? deserializedObject = Success_Response.LoadResponseFromString(response);
            return deserializedObject;
        }

        /// <summary>
        /// Add a new mirror from an owned or subscribed data store. Triggers a Chia transaction
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#add_mirror"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public Success_Response? AddMirror_Sync(AddMirror_RPC rpc)
        {
            Task<Success_Response?> data = Task.Run(() => AddMirror_Async(rpc));
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
        public async Task<Success_Response?> AddMissingFiles_Async(AddMissingFiles_RPC rpc)
        {
            string response = await SendCustomMessage_Async("add_missing_files", rpc.ToString());
            Success_Response? deserializedObject = Success_Response.LoadResponseFromString(response);
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
        public Success_Response? AddMissingFiles_Sync(AddMissingFiles_RPC rpc)
        {
            Task<Success_Response?> data = Task.Run(() => AddMissingFiles_Async(rpc));
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
        /// <param name="auto">ensures that keys are updated, otherwise command will fail if trying to add a key which already exists or deleting a non existant key</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<TxID_Response?> BatchUpdate_Async(BatchUpdate_RPC rpc, bool auto = true)
        {
            if (rpc.id == null)
            {
                throw new ArgumentNullException(nameof(rpc.id));
            }
            if (rpc.changelist == null)
            {
                throw new ArgumentNullException(nameof(rpc.changelist));
            }
            // Process DataStoreChange objects if 'auto' is true
            if (auto)
            {
                // Initialize a list for changes and a HashSet to store affected keys
                List<DataStoreChange> changes = new List<DataStoreChange>();
                HashSet<string> affectedKeys = new HashSet<string>();

                // Populate the affectedKeys HashSet from rpc.changelist
                
                foreach (DataStoreChange change in rpc.changelist)
                {
                    if (change.key == null) continue;
                    if (!affectedKeys.Contains(change.key))
                        affectedKeys.Add(change.key);
                }

                // Check which keys in affectedKeys exist in the datastore
                Dictionary<string, bool> keysExist = KeysExist(rpc.id, affectedKeys.ToList());

                // Process DataStoreChange objects in rpc.changelist based on their actions and key existence
                foreach (DataStoreChange change in rpc.changelist)
                {
                    if (change.key == null)
                    {
                        throw new ArgumentNullException(nameof(rpc.changelist), "a change.key in changelist is null!");
                    }
                    bool exists = keysExist[change.key];

                    if (change.action == DataStoreChange.DataStoreChangeAction.insert && exists)
                    {
                        // Delete the existing key and then insert the new one
                        changes.Add(new DataStoreChange(DataStoreChange.DataStoreChangeAction.delete, change.key));
                        changes.Add(change);
                    }
                    else if (change.action == DataStoreChange.DataStoreChangeAction.insert)
                    {
                        // Add the new key
                        changes.Add(change);
                        keysExist[change.key] = true;
                    }
                    else if (change.action == DataStoreChange.DataStoreChangeAction.delete && !exists)
                    {
                        // Skip deletion if the key does not exist
                    }
                    else
                    {
                        // Delete the existing key
                        changes.Add(change);
                        keysExist[change.key] = false;
                    }
                }

                // Update rpc.changelist with the processed changes
                rpc.changelist = changes.ToArray();
            }

            string response = await SendCustomMessage_Async("batch_update", rpc.ToString());
            TxID_Response? deserializedObject = TxID_Response.LoadResponseFromString(response);
            return deserializedObject;
        }

        /// <summary>
        /// Apply multiple updates to a data store with a given changelist. Triggers a Chia transaction
        /// </summary>/
        /// <remarks>
        /// - There are two actions allowed: insert and delete <br/>
        /// - insert requires key and value flags <br/>
        /// - delete requires a key flag only <br/>
        /// - Keys and values must be written in hex format. Values can be derived from text or binary. <br/>
        /// - Multiple inserts and deletes are allowed <br/>
        /// - The size of a single value flag is limited to 100 MiB.However, adding anything close to that size has not been tested and could produce unexpected results <br/><br/>
        /// <see href="https://docs.chia.net/datalayer-rpc#batch_update"/>
        /// </remarks>
        /// <param name="rpc"></param>
        /// /// <param name="auto">ensures that keys are updated, otherwise command will fail if trying to add a key which already exists or deleting a non existant key</param>
        /// <returns></returns>
        public TxID_Response? BatchUpdate_Sync(BatchUpdate_RPC rpc, bool auto = true)
        {
            Task<TxID_Response?> data = Task.Run(() => BatchUpdate_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Cancel a DataLayer offer
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#cancel_offer"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<Success_Response?> CancelOffer_Async(CancelOffer_RPC rpc)
        {
            string response = await SendCustomMessage_Async("cancel_offer", rpc.ToString());
            Success_Response? deserializedObject = Success_Response.LoadResponseFromString(response);
            return deserializedObject;
        }

        /// <summary>
        /// Cancel a DataLayer offer
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#cancel_offer"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public Success_Response? CancelOffer_Sync(CancelOffer_RPC rpc)
        {
            Task<Success_Response?> data = Task.Run(() => CancelOffer_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Get information about configured uploader/downloader plugins
        /// </summary>
        /// <returns>CheckPlugins_Response</returns>
        public async Task<CheckPlugins_Response?> CheckPlugins_Async()
        {
            string response = await SendCustomMessage_Async("check_plugins");
            CheckPlugins_Response? deserializedObject = CheckPlugins_Response.LoadResponseFromString(response);
            return deserializedObject;
        }

        /// <summary>
        /// Get information about configured uploader/downloader plugins
        /// </summary>
        /// <returns>CheckPlugins_Response</returns>
        public CheckPlugins_Response? CheckPlugins_Sync()
        {
            Task<CheckPlugins_Response?> data = Task.Run(() => CheckPlugins_Async());
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Clear pending roots that will not be published, associated data may not be recoverable
        /// </summary>
        /// <returns>ClearPendingRoots_Response</returns>
        public async Task<ClearPendingRoots_Response?> ClearPendingRoots_Async(ClearPendingRoots_RPC rpc)
        {
            string response = await SendCustomMessage_Async("check_plugins", rpc.ToString());
            ClearPendingRoots_Response? deserializedObject = ClearPendingRoots_Response.LoadResponseFromString(response);
            return deserializedObject;
        }

        /// <summary>
        /// Clear pending roots that will not be published, associated data may not be recoverable
        /// </summary>
        /// <returns>ClearPendingRoots_Response</returns>
        public ClearPendingRoots_Response? ClearPendingRoots_Sync(ClearPendingRoots_RPC rpc)
        {
            Task<ClearPendingRoots_Response?> data = Task.Run(() => ClearPendingRoots_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Create a data store. Triggers a Chia transaction
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#create_data_store"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<CreateDataStore_Response?> CreateDataStore_Async(CreateDataStore_RPC rpc)
        {

            string response = await SendCustomMessage_Async("create_data_store", rpc.ToString());
            CreateDataStore_Response? deserializedObject = CreateDataStore_Response.LoadResponseFromString(response);
            return deserializedObject;
        }

        /// <summary>
        /// Create a data store. Triggers a Chia transaction
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#create_data_store"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public CreateDataStore_Response? CreateDataStore_Sync(CreateDataStore_RPC rpc)
        {
            Task<CreateDataStore_Response?> data = Task.Run(() => CreateDataStore_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Delete a key/value pair from a store that you control. Triggers a Chia transaction
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#delete_key"/></remarks>
        /// <param name="rpc"></param>
        /// /// <param name="errorOnNonExistientKey">specify if it is considered an error to delete a non existing key</param>
        /// <returns></returns>
        public async Task<TxID_Response?> DeleteKey_Async(DeleteKey_RPC rpc, bool errorOnNonExistientKey = false)
        {
            string response = await SendCustomMessage_Async("delete_key", rpc.ToString());
            TxID_Response? deserializedObject = TxID_Response.LoadResponseFromString(response);
            if (deserializedObject == null)
            {
                throw new NullReferenceException(nameof(deserializedObject));
            }
            if (!(deserializedObject.success ?? false) && !errorOnNonExistientKey && deserializedObject.error == "Changelist resulted in no change to tree data")
            {
                deserializedObject.success = true;
            }
            return deserializedObject;
        }

        /// <summary>
        /// Delete a key/value pair from a store that you control. Triggers a Chia transaction
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#delete_key"/></remarks>
        /// <param name="rpc"></param>
        /// <param name="errorOnNonExistientKey">specify if it is considered an error to delete a non existing key</param>
        /// <returns></returns>
        public TxID_Response? DeleteKey_Sync(DeleteKey_RPC rpc, bool errorOnNonExistientKey = false)
        {
            Task<TxID_Response?> data = Task.Run(() => DeleteKey_Async(rpc, errorOnNonExistientKey));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Delete a mirror, by coin_id. Triggers a Chia transaction
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#delete_mirror"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<Success_Response?> DeleteMirror_Async(ID_RPC rpc)
        {
            string response = await SendCustomMessage_Async("delete_mirror", rpc.ToString());
            Success_Response? deserializedObject = Success_Response.LoadResponseFromString(response);
            return deserializedObject;
        }

        /// <summary>
        /// Delete a mirror, by coin_id. Triggers a Chia transaction
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#delete_mirror"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public Success_Response? DeleteMirror_Sync(ID_RPC rpc)
        {
            Task<Success_Response?> data = Task.Run(() => DeleteMirror_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Return all ancestors of a given hash
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#get_ancestors"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetAncestors_Response?> GetAncestors_Async(GetAncestors_RPC rpc)
        {
            string response = await SendCustomMessage_Async("get_ancestors", rpc.ToString());
            GetAncestors_Response? deserializedObject = GetAncestors_Response.LoadResponseFromString(response);
            return deserializedObject;
        }

        /// <summary>
        /// Return all ancestors of a given hash
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#get_ancestors"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetAncestors_Response? GetAncestors_Sync(GetAncestors_RPC rpc)
        {
            Task<GetAncestors_Response?> data = Task.Run(() => GetAncestors_Async(rpc));
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
        public async Task<GetKeys_Response?> GetKeys_Async(GetKeys_RPC rpc)
        {
            string response = await SendCustomMessage_Async("get_keys", rpc.ToString());
            GetKeys_Response? deserializedObject = GetKeys_Response.LoadResponseFromString(response);
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
        public GetKeys_Response? GetKeys_Sync(GetKeys_RPC rpc)
        {
            Task<GetKeys_Response?> data = Task.Run(() => GetKeys_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Get all keys and values for a store. Must be subscribed to store ID
        /// </summary>
        /// <remarks>The root_hash parameter is recommended to be used each time you call this RPC. 
        /// If root_hash is not specified, there is no way to guarantee that the latest data is being shown (stale data may be shown instead). 
        /// This parameter is obtainable by calling the <see cref="GetRoot_Async(ID_RPC)"/>.<br/><br/>
        /// <see href="https://docs.chia.net/datalayer-rpc#get_keys_values"/>
        /// </remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetKeysValues_Response?> GetKeysValues_Async(GetKeys_RPC rpc)
        {
            string response = await SendCustomMessage_Async("get_keys_values", rpc.ToString());
            GetKeysValues_Response? deserializedObject = GetKeysValues_Response.LoadResponseFromString(response);
            return deserializedObject;
        }

        /// <summary>
        /// Get all keys and values for a store. Must be subscribed to store ID
        /// </summary>
        /// <remarks>The root_hash parameter is recommended to be used each time you call this RPC. 
        /// If root_hash is not specified, there is no way to guarantee that the latest data is being shown (stale data may be shown instead). 
        /// This parameter is obtainable by calling the <see cref="GetRoot_Sync(ID_RPC)"/>.<br/><br/>
        /// <see href="https://docs.chia.net/datalayer-rpc#get_keys_values"/>
        /// </remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetKeysValues_Response? GetKeysValues_Sync(GetKeys_RPC rpc)
        {
            Task<GetKeysValues_Response?> data = Task.Run(() => GetKeysValues_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Get the kv diff between two hashes within the same store ID
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#get_kv_diff"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetKvDiff_Response?> GetKVDiff_Async(GetKvDiff_RPC rpc)
        {
            string response = await SendCustomMessage_Async("get_kv_diff", rpc.ToString());
            GetKvDiff_Response? deserializedObject = GetKvDiff_Response.LoadResponseFromString(response);
            return deserializedObject;
        }

        /// <summary>
        /// Get the kv diff between two hashes within the same store ID
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#get_kv_diff"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetKvDiff_Response? GetKVDiff_Sync(GetKvDiff_RPC rpc)
        {
            Task<GetKvDiff_Response?> data = Task.Run(() => GetKVDiff_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Get the root hash and timestamp of a store ID. Can be used for either owned or subscribed stores
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#get_local_root"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetLocalRoot_Response?> GetLocalRoot_Async(ID_RPC rpc)
        {
            string response = await SendCustomMessage_Async("get_local_root", rpc.ToString());
            GetLocalRoot_Response? deserializedObject = GetLocalRoot_Response.LoadResponseFromString(response);
            return deserializedObject;
        }

        /// <summary>
        /// Get the root hash and timestamp of a store ID. Can be used for either owned or subscribed stores
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#get_local_root"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetLocalRoot_Response? GetLocalRoot_Sync(ID_RPC rpc)
        {
            Task<GetLocalRoot_Response?> data = Task.Run(() => GetLocalRoot_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// List all mirrors for a store ID
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#get_mirrors"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetMirrors_Response?> GetMirrors_Async(ID_RPC rpc)
        {
            string response = await SendCustomMessage_Async("get_mirrors", rpc.ToString());
            GetMirrors_Response? deserializedObject = GetMirrors_Response.LoadResponseFromString(response);
            return deserializedObject;
        }

        /// <summary>
        /// List all mirrors for a store ID
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#get_mirrors"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetMirrors_Response? GetMirrors_Sync(ID_RPC rpc)
        {
            Task<GetMirrors_Response?> data = Task.Run(() => GetMirrors_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// List the id (store_id) of each data_store owned by this wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#get_owned_stores"/></remarks>
        /// <returns></returns>
        public async Task<GetOwnedStores_Response?> GetOwnedStores_Async()
        {
            string response = await SendCustomMessage_Async("get_owned_stores");
            GetOwnedStores_Response? deserializedObject = GetOwnedStores_Response.LoadResponseFromString(response);
            return deserializedObject;
        }

        /// <summary>
        /// List the id (store_id) of each data_store owned by this wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#get_owned_stores"/></remarks>
        /// <returns></returns>
        public GetOwnedStores_Response? GetOwnedStores_Sync()
        {
            Task<GetOwnedStores_Response?> data = Task.Run(() => GetOwnedStores_Async());
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Get the root hash and timestamp of a given store ID. 
        /// If it is a subscribed store, this command will return an invalid hash (see example). 
        /// In this case, use <see cref="GetLocalRoot_Async(ID_RPC)"/> instead
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#get_root"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetRoot_Response?> GetRoot_Async(ID_RPC rpc)
        {
            string response = await SendCustomMessage_Async("get_root", rpc.ToString());
            GetRoot_Response? deserializedObject = GetRoot_Response.LoadResponseFromString(response);
            return deserializedObject;
        }

        /// <summary>
        /// Get the root hash and timestamp of a given store ID. 
        /// If it is a subscribed store, this command will return an invalid hash (see example). 
        /// In this case, use <see cref="GetLocalRoot_Sync(ID_RPC)"/> instead
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#get_root"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetRoot_Response? GetRoot_Sync(ID_RPC rpc)
        {
            Task<GetRoot_Response?> data = Task.Run(() => GetRoot_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Get the root hashes and timestamps from a list of stores. Note that an invalid hash will be returned for subscribed stores. <br/>
        /// Use <see cref="GetLocalRoot_Async(ID_RPC)"/> instead
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#get_roots"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetRoots_Response?> GetRoots_Async(GetRoots_RPC rpc)
        {
            string response = await SendCustomMessage_Async("get_roots", rpc.ToString());
            GetRoots_Response? deserializedObject = GetRoots_Response.LoadResponseFromString(response);
            return deserializedObject;
        }

        /// <summary>
        /// Get the root hashes and timestamps from a list of stores. Note that an invalid hash will be returned for subscribed stores. <br/>
        /// Use <see cref="GetLocalRoot_Sync(ID_RPC)"/> instead
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#get_roots"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetRoots_Response? GetRoots_Sync(GetRoots_RPC rpc)
        {
            Task<GetRoots_Response?> data = Task.Run(() => GetRoots_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Get a history of root hashes for a Store ID that you subscribe to
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#get_root_history"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetRootHistory_Response?> GetRootHistory_Async(ID_RPC rpc)
        {
            string response = await SendCustomMessage_Async("get_root_history", rpc.ToString());
            GetRootHistory_Response? deserializedObject = GetRootHistory_Response.LoadResponseFromString(response);
            return deserializedObject;
        }

        /// <summary>
        /// Get a history of root hashes for a Store ID that you subscribe to
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#get_root_history"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetRootHistory_Response? GetRootHistory_Sync(ID_RPC rpc)
        {
            Task<GetRootHistory_Response?> data = Task.Run(() => GetRootHistory_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Show a comprehensive list of RPC routes for the DataLayer
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#get_routes"/></remarks>
        /// <returns></returns>
        public async Task<GetRoutes_Response?> GetRoutes_Async()
        {
            string response = await SendCustomMessage_Async("get_routes");
            GetRoutes_Response? deserializedObject = GetRoutes_Response.LoadResponseFromString(response);
            return deserializedObject;
        }

        /// <summary>
        /// Show a comprehensive list of RPC routes for the DataLayer
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#get_routes"/></remarks>
        /// <returns></returns>
        public GetRoutes_Response? GetRoutes_Sync()
        {
            Task<GetRoutes_Response?> data = Task.Run(() => GetRoutes_Async());
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Obtain the current sync status for a provided data store
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#get_sync_status"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetDatalayerSyncStatus_Response?> GetSyncStatus_Async(ID_RPC rpc)
        {
            string response = await SendCustomMessage_Async("get_sync_status", rpc.ToString());
            GetDatalayerSyncStatus_Response? deserializedObject = GetDatalayerSyncStatus_Response.LoadResponseFromString(response);
            return deserializedObject;
        }

        /// <summary>
        /// Obtain the current sync status for a provided data store
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#get_sync_status"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetDatalayerSyncStatus_Response? GetSyncStatus_Sync(ID_RPC rpc)
        {
            Task<GetDatalayerSyncStatus_Response?> data = Task.Run(() => GetSyncStatus_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Given a key and the data store in which the key is located, return corresponding value
        /// </summary>
        /// <remarks>The root_hash parameter is recommended to be used each time you call this RPC. 
        /// If root_hash is not specified, there is no way to guarantee that the latest data is being shown (stale data may be shown instead). 
        /// This parameter is obtainable by calling the <see cref="GetRoot_Async(ID_RPC)"/>.<br/><br/>
        /// <see href="https://docs.chia.net/datalayer-rpc#get_value"/>
        /// </remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetValue_Response?> GetValue_Async(GetValue_RPC rpc)
        {
            string response = await SendCustomMessage_Async("get_value", rpc.ToString());
            GetValue_Response? deserializedObject = GetValue_Response.LoadResponseFromString(response);
            return deserializedObject;
        }

        /// <summary>
        /// Given a key and the data store in which the key is located, return corresponding value
        /// </summary>
        /// <remarks>The root_hash parameter is recommended to be used each time you call this RPC. 
        /// If root_hash is not specified, there is no way to guarantee that the latest data is being shown (stale data may be shown instead). 
        /// This parameter is obtainable by calling the <see cref="GetRoot_Sync(ID_RPC)"/>.<br/><br/>
        /// <see href="https://docs.chia.net/datalayer-rpc#get_value"/>
        /// </remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetValue_Response? GetValue_Sync(GetValue_RPC rpc)
        {
            Task<GetValue_Response?> data = Task.Run(() => GetValue_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Insert a key/value pair into a store that you control. Triggers a Chia transaction
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#insert"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<TxID_Response?> Insert_Async(Insert_RPC rpc)
        {
            string response = await SendCustomMessage_Async("insert", rpc.ToString());
            TxID_Response? deserializedObject = TxID_Response.LoadResponseFromString(response);
            return deserializedObject;
        }

        /// <summary>
        /// Insert a key/value pair into a store that you control. Triggers a Chia transaction
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#insert"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public TxID_Response? Insert_Sync(Insert_RPC rpc)
        {
            Task<TxID_Response?> data = Task.Run(() => Insert_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Make an offer to include one or more keys in exchange for a Taker including one or more keys
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#make_offer"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<MakeOffer_Response?> MakeOffer_Async(MakeOffer_RPC rpc)
        {
            string response = await SendCustomMessage_Async("make_offer", rpc.ToString());
            MakeOffer_Response? deserializedObject = MakeOffer_Response.LoadResponseFromString(response);
            return deserializedObject;
        }

        /// <summary>
        /// Make an offer to include one or more keys in exchange for a Taker including one or more keys
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#make_offer"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public MakeOffer_Response? MakeOffer_Sync(MakeOffer_RPC rpc)
        {
            Task<MakeOffer_Response?> data = Task.Run(() => MakeOffer_Async(rpc));
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
        [Obsolete("WARNING: This method works differenrt than expected. Consultation incorrect. Use at your own risk. From my understanding it replaces the urls")]
        public async Task<Success_Response?> RemoveSubscriptions_Async(Subscribe_RPC rpc)
        {
            string response = await SendCustomMessage_Async("remove_subscriptions", rpc.ToString());
            Success_Response? deserializedObject = Success_Response.LoadResponseFromString(response);
            return deserializedObject;
        }

        /// <summary>
        /// Remove one or more URLs from a data store to which you subscribe. 
        /// Note that this action will not remove the subscription to the data store itself. For that functionality, use unsubscribe
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#remove_subscriptions"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        [Obsolete("WARNING: This method works differenrt than expected. Consultation incorrect. Use at your own risk. From my understanding it replaces the urls")]
        public Success_Response? RemoveSubscriptions_Sync(Subscribe_RPC rpc)
        {
            Task<Success_Response?> data = Task.Run(() => RemoveSubscriptions_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Subscribe to a store ID
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#subscribe"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<Success_Response?> Subscribe_Async(Subscribe_RPC rpc)
        {
            string response = await SendCustomMessage_Async("subscribe", rpc.ToString());
            Success_Response? deserializedObject = Success_Response.LoadResponseFromString(response);
            return deserializedObject;
        }

        /// <summary>
        /// Subscribe to a store ID
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#subscribe"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public Success_Response? Subscribe_Sync(Subscribe_RPC rpc)
        {
            Task<Success_Response?> data = Task.Run(() => Subscribe_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// List the store ID for each current subscription
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#subscriptions"/></remarks>
        /// <returns></returns>
        public async Task<Subscriptions_Response?> Subscriptions_Async()
        {
            string response = await SendCustomMessage_Async("subscriptions");
            Subscriptions_Response? deserializedObject = Subscriptions_Response.LoadResponseFromString(response);
            return deserializedObject;
        }

        /// <summary>
        /// List the store ID for each current subscription
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#subscriptions"/></remarks>
        /// <returns></returns>
        public Subscriptions_Response? Subscriptions_Sync()
        {
            Task<Subscriptions_Response?> data = Task.Run(() => Subscriptions_Async());
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Accept an offer to create one or more keys in exchange for the Maker creating one or more keys. Triggers a Chia transaction
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#take_offer"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<TradeID_Response?> TakeOffer_Async(TakeOffer_RPC rpc)
        {
            string response = await SendCustomMessage_Async("take_offer", rpc.ToString());
            TradeID_Response? deserializedObject = TradeID_Response.LoadResponseFromString(response);
            return deserializedObject;
        }

        /// <summary>
        /// Accept an offer to create one or more keys in exchange for the Maker creating one or more keys. Triggers a Chia transaction
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#take_offer"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public TradeID_Response? TakeOffer_Sync(TakeOffer_RPC rpc)
        {
            Task<TradeID_Response?> data = Task.Run(() => TakeOffer_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Unsubscribe from a store ID
        /// </summary>
        /// <remarks>
        /// The `unsubscribe` RPC may or may not delete any data, depending on which version of Chia you are running<br/>
        /// - Prior to version 2.1, the command did not delete the.dat files, nor did it delete from the database.<br/>
        /// - As of version 2.1, the command deletes the .dat files, but does not delete from the database.<br/>
        /// - In a future release, the command will also delete from the database<br/>
        /// <see href="https://docs.chia.net/datalayer-rpc#unsubscribe"/>
        /// </remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<Success_Response?> Unsubscribe_Async(ID_RPC rpc)
        {
            string response = await SendCustomMessage_Async("unsubscribe", rpc.ToString());
            Success_Response? deserializedObject = Success_Response.LoadResponseFromString(response);
            return deserializedObject;
        }

        /// <summary>
        /// Unsubscribe from a store ID
        /// </summary>
        /// <remarks>
        /// The `unsubscribe` RPC may or may not delete any data, depending on which version of Chia you are running<br/>
        /// - Prior to version 2.1, the command did not delete the.dat files, nor did it delete from the database.<br/>
        /// - As of version 2.1, the command deletes the .dat files, but does not delete from the database.<br/>
        /// - In a future release, the command will also delete from the database<br/>
        /// <see href="https://docs.chia.net/datalayer-rpc#unsubscribe"/>
        /// </remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public Success_Response? Unsubscribe_Sync(ID_RPC rpc)
        {
            Task<Success_Response?> data = Task.Run(() => Unsubscribe_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Verify that a DataLayer offer is well-formed
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#verify_offer"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<VerifyOffer_Response?> VerifyOffer_Async(VerifyOffer_RPC rpc)
        {
            string response = await SendCustomMessage_Async("verify_offer", rpc.ToString());
            VerifyOffer_Response? deserializedObject = VerifyOffer_Response.LoadResponseFromString(response);
            return deserializedObject;
        }
        
        /// <summary>
        /// Verify that a DataLayer offer is well-formed
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/datalayer-rpc#verify_offer"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public VerifyOffer_Response? VerifyOffer_Sync(VerifyOffer_RPC rpc)
        {
            Task<VerifyOffer_Response?> data = Task.Run(() => VerifyOffer_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Request that the wallet service be logged in to the specified fingerprint
        /// </summary>
        /// <remarks>
        /// <see href="https://docs.chia.net/datalayer-rpc#wallet_log_in"/>
        /// </remarks>
        /// <returns></returns>
        public async Task<Success_Response?> WalletLogIn_Async(FingerPrint_RPC rpc)
        {
            string response = await SendCustomMessage_Async("wallet_log_in", rpc.ToString());
            Success_Response? deserializedObject = Success_Response.LoadResponseFromString(response);
            return deserializedObject;
        }

        /// <summary>
        /// Request that the wallet service be logged in to the specified fingerprint
        /// </summary>
        /// <remarks>
        /// <see href="https://docs.chia.net/datalayer-rpc#wallet_log_in"/>
        /// </remarks>
        /// <returns></returns>
        public Success_Response? WalletLogIn_Sync(FingerPrint_RPC rpc)
        {
            Task<Success_Response?> data = Task.Run(() => WalletLogIn_Async(rpc));
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
        public async Task<Success_Response?> CloseConnection_Async(CloseConnection_RPC rpc)
        {
            string response = await SendCustomMessage_Async("close_connection", rpc.ToString());
            Success_Response? deserializedObject = Success_Response.LoadResponseFromString(response);
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
        public Success_Response? CloseConnection_Sync(CloseConnection_RPC rpc)
        {
            Task<Success_Response?> data = Task.Run(() => CloseConnection_Async(rpc));
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
        public async Task<GetConnections_Response?> GetConnections_Async()
        {
            string response = await SendCustomMessage_Async("get_connections");
            GetConnections_Response? deserializedObject = GetConnections_Response.LoadResponseFromString(response);
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
        public GetConnections_Response? GetConnections_Sync()
        {
            Task<GetConnections_Response?> data = Task.Run(() => GetConnections_Async());
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
        public async Task<Success_Response?> OpenConnection_Async(OpenConnection_RPC rpc)
        {
            string response = await SendCustomMessage_Async("open_connection", rpc.ToString());
            Success_Response? deserializedObject = Success_Response.LoadResponseFromString(response);
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
        public Success_Response? OpenConnection_Sync(OpenConnection_RPC rpc)
        {
            Task<Success_Response?> data = Task.Run(() => OpenConnection_Async(rpc));
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
        public async Task<Success_Response?> StopNode_Async()
        {
            string response = await SendCustomMessage_Async("stop_node");
            Success_Response? deserializedObject = Success_Response.LoadResponseFromString(response);
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
        public Success_Response? StopNode_Sync()
        {
            Task<Success_Response?> data = Task.Run(() => StopNode_Async());
            data.Wait();
            return data.Result;
        }
        
    }
}
