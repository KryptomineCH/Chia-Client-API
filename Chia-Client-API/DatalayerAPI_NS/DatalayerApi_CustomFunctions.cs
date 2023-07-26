using CHIA_RPC.Datalayer_NS;
using System.Text.Json;

namespace Chia_Client_API.DatalayerAPI_NS
{
    /// <summary>
    /// this class can be used to query the chia datalayer
    /// </summary>
    public partial class Datalayer_RPC_Client
    {
        /// <summary>
        /// Checks if the given key exists in the store.
        /// </summary>
        /// <param name="storeID">The ID of the store to check for the key.</param>
        /// <param name="questionableKey">The key whose existence needs to be checked.</param>
        /// <param name="rootHash">The root hash for the key store (optional).</param>
        /// <returns>A boolean indicating whether the key exists in the store or not.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public bool KeyExists(string storeID, string questionableKey, string? rootHash = null)
        {
            // If rootHash is not provided, fetch the local root hash
            if (string.IsNullOrEmpty(rootHash))
            {
                GetLocalRoot_Response? localRoot = GetLocalRoot_Sync(new CHIA_RPC.General_NS.ID_RPC(storeID));
                if (localRoot == null)
                {
                    throw new InvalidOperationException("local root could not be fetched!");
                }
                rootHash = localRoot.hash;
            }

            // Perform GetKeys RPC call
            GetKeys_RPC rpc = new GetKeys_RPC(storeID, rootHash);
            GetKeys_Response? resp = GetKeys_Sync(rpc);
            if (resp == null)
            {
                throw new InvalidOperationException("Keys could not be fetched!");
            }
            if (resp.keys == null)
            {
                throw new InvalidOperationException("Keys could not be fetched!");
            }
            // Iterate over the keys returned by the GetKeys RPC call
            foreach (string existingKey in resp.keys)
            {
                string key = existingKey;
                if (existingKey.StartsWith("0x")) key = existingKey.Substring(2);
                // If the key lengths are not equal, continue to the next iteration
                if (questionableKey.Length != key.Length) continue;

                // Assume the key exists until proven otherwise
                bool exists = true;

                // Compare the characters of the questionable key and the existing key
                for (int i = questionableKey.Length - 1; i >= 0; i--)
                {
                    // If the characters are different, set 'exists' to false and break the loop
                    if (questionableKey[i] != key[i])
                    {
                        exists = false;
                        break;
                    }
                }

                // If the key exists, return true
                if (exists) { return true; }
            }

            // If the key was not found, return false
            return false;
        }

        /// <summary>
        /// Checks the existence of the given keys in the store and returns a dictionary indicating their presence.
        /// </summary>
        /// <param name="storeID">The ID of the store to check for keys.</param>
        /// <param name="questionableKeys">A list of keys whose existence needs to be checked.</param>
        /// <param name="rootHash">The root hash for the key store (optional).</param>
        /// <returns>A dictionary with the key as the string and a boolean indicating its existence in the store.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public Dictionary<string, bool> KeysExist(string storeID, List<string> questionableKeys, string? rootHash = null)
        {
            // If rootHash is not provided, fetch the local root hash
            if (string.IsNullOrEmpty(rootHash))
            {
                GetLocalRoot_Response? localRoot = GetLocalRoot_Sync(new CHIA_RPC.General_NS.ID_RPC(storeID));
                if (localRoot == null)
                {
                    throw new InvalidOperationException("local root could not be fetched!");
                }
                rootHash = localRoot.hash;
            }

            // Perform GetKeys RPC call to receive existing keys
            GetKeys_RPC rpc = new GetKeys_RPC(storeID, rootHash);
            GetKeys_Response? resp = GetKeys_Sync(rpc);
            if (resp == null)
            {
                throw new InvalidOperationException("Keys could not be fetched!");
            }
            // Create a dictionary to store the results of the key check
            Dictionary<string, bool> keys = new Dictionary<string, bool>();
            if (resp.keys == null)
            {
                throw new InvalidOperationException("Keys could not be fetched!");
            }
            // Iterate over the keys returned by the GetKeys RPC call
            foreach (string existingKey in resp.keys)
            {
                string key = existingKey;
                if (existingKey.StartsWith("0x")) key = existingKey.Substring(2);
                // Iterate over the list of questionable keys
                for (int keyIndex = questionableKeys.Count - 1; keyIndex >= 0; keyIndex--)
                {
                    // If the key lengths are not equal, continue to the next iteration
                    if (questionableKeys[keyIndex].Length != key.Length) continue;

                    // Assume the key exists until proven otherwise
                    bool exists = true;

                    // Compare the characters of the questionable key and the existing key
                    for (int charIndex = key.Length - 1; charIndex >= 0; charIndex--)
                    {
                        // If the characters are different, set 'exists' to false and break the character comparison loop
                        if (questionableKeys[keyIndex][charIndex] != key[charIndex])
                        {
                            exists = false;
                            break;
                        }
                    }

                    // If the key exists, add it to the result dictionary and remove it from the questionable keys list
                    if (exists)
                    {
                        keys.Add(key, true);
                        questionableKeys.RemoveAt(keyIndex);
                    }
                }

                // If all questionable keys have been found existing, break the loop early
                if (questionableKeys.Count <= 0) break;
            }

            // Add the remaining questionable keys to the result dictionary as non-existent keys
            foreach (string key in questionableKeys)
            {
                keys[key] = false;
            }

            return keys;
        }
        /// <summary>
        /// This asynchronous method is used to insert or update a key-value pair in a specific data store.
        /// </summary>
        /// <param name="rpc">The <see cref="Insert_RPC"/> object containing the ID of the data store, the key, and the value to be inserted or updated.</param>
        /// <returns>A <see cref="CHIA_RPC.General_NS.TxID_Response"/> object indicating the result of the operation.
        /// This object contains a success flag and an error message in case of failure.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the ID or key provided in the <see cref="Insert_RPC"/> object is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the value could not be fetched for a given key.</exception>
        /// <remarks>
        /// If the key already exists in the data store, the method will first compare the existing value with the new value.
        /// If they are different, the key will be deleted before the new key-value pair is inserted.
        /// If they are the same, the method will return a success response without modifying the data store.
        /// </remarks>
        public async Task<CHIA_RPC.General_NS.TxID_Response?> InsertOrUpdate_Async(Insert_RPC rpc)
        {
            if (rpc.id == null)
            {
                throw new ArgumentNullException(nameof(rpc.id));
            }
            if (rpc.key == null)
            {
                throw new ArgumentNullException(nameof(rpc.key));
            }
            if (KeyExists(rpc.id,rpc.key)) {
                GetValue_RPC getValueRpc = new GetValue_RPC(rpc.id, rpc.key);
                GetValue_Response? getValueResponse = GetValue_Sync(getValueRpc);
                if (getValueResponse == null)
                {
                    throw new InvalidOperationException("value could not be fetched!");
                }
                if (rpc.value != getValueResponse.value)
                {
                    DeleteKey_RPC deleteRPC = new DeleteKey_RPC(rpc.id, rpc.key);
                    DeleteKey_Sync(deleteRPC);
                }
                else
                {
                    return new CHIA_RPC.General_NS.TxID_Response
                    {
                        success = true,
                        error = "the exact same value already exists"
                    };        
                }
            }
            string response = await SendCustomMessage_Async("insert", rpc.ToString());
            CHIA_RPC.General_NS.TxID_Response? deserializedObject = JsonSerializer.Deserialize<CHIA_RPC.General_NS.TxID_Response>(response);
            return deserializedObject;
        }
    }
}
