﻿using Chia_Client_API.ChiaClient_NS;
using CHIA_RPC.Daemon_NS.KeychainServer_NS;
using CHIA_RPC.General_NS;
using CHIA_RPC.HelperFunctions_NS;

namespace Chia_Client_API.DaemonAPI_NS
{
    /// <summary>
    /// Provides a client for making RPC (Remote Procedure Call) requests to a Chia Farmer node.
    /// </summary>
    /// <remarks>
    /// A Chia Farmer node is responsible for farming operations including creating plots, looking for proofs of space, and more. 
    /// The Farmer_RPC_Client class abstracts the details of the RPC communication, allowing higher-level code to use simple method calls instead of dealing directly with the RPC protocol.
    /// This class includes methods corresponding to each available RPC call that the Farmer node can handle. For example, you can get the state of the farmer, get the reward targets and set the reward targets.
    /// Each method in this class sends an RPC request to the Farmer node, and then waits for and returns the response.
    /// </remarks>
    public abstract partial class DaemonRPCBase : ChiaEndpointRouteBase
    {
        // add_private_key
        /// <summary>
        /// Add a new private key from a mnemonic word list.
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#add_private_key"/></remarks>
        /// <returns><see cref="FingerPrint_Response"/></returns>
        public async Task<FingerPrint_Response> AddPrivateKey_Async(AddPrivateKey_RPC rpc)
        {
            string responseJson = await SendCustomMessageAsync("add_private_key", rpc.ToString());
            ActionResult<FingerPrint_Response> deserialisationResult = FingerPrint_Response.LoadResponseFromString(responseJson,IncludeRawServerResponse);
            FingerPrint_Response response = new FingerPrint_Response();
            if (deserialisationResult.Data != null)
            {
                response = deserialisationResult.Data;
            }
            else
            {
                response.success = deserialisationResult.Success;
                response.error = deserialisationResult.Error;
            }
            response.RawContent = deserialisationResult.RawJson;
            return response;
        }
        /// <summary>
        /// Add a new private key from a mnemonic word list.
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#add_private_key"/></remarks>
        /// <returns><see cref="FingerPrint_Response"/></returns>
        public FingerPrint_Response AddPrivateKey_Sync(AddPrivateKey_RPC rpc)
        {
            Task<FingerPrint_Response> data = Task.Run(() => AddPrivateKey_Async(rpc));
            data.Wait();
            return data.Result;
        }

        // check_keys
        /// <summary>
        /// functionality: unknown (refer to documentation)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#check_keys"/></remarks>
        /// <returns>unknown</returns>
        public async Task<Success_Response> CheckKeys_Async(CheckKeys_RPC rpc)
        {
            string responseJson = await SendCustomMessageAsync("check_keys", rpc.ToString());
            ActionResult<Success_Response> deserializationResult = Success_Response.LoadResponseFromString(responseJson,IncludeRawServerResponse);
            Success_Response response = new Success_Response();
            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
            }
            response.RawContent = deserializationResult.RawJson;
            return response;
        }

        /// <summary>
        /// functionality: unknown (refer to documentation)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#check_keys"/></remarks>
        /// <returns>unknown</returns>
        public Success_Response CheckKeys_Sync(CheckKeys_RPC rpc)
        {
            Task<Success_Response> data = Task.Run(() => CheckKeys_Async(rpc));
            data.Wait();
            return data.Result;
        }

        // delete_all_keys
        /// <summary>
        /// Delete all keys.
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#delete_all_keys"/></remarks>
        /// <returns><see cref="Success_Response"/></returns>
        public async Task<Success_Response> DeleteAllKeys_Async()
        {
            string responseJson = await SendCustomMessageAsync("delete_all_keys");
            ActionResult<Success_Response> deserializationResult = Success_Response.LoadResponseFromString(responseJson,IncludeRawServerResponse);
            Success_Response response = new Success_Response();
            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
            }
            response.RawContent = deserializationResult.RawJson;
            return response;
        }

        /// <summary>
        /// Delete all keys.
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#delete_all_keys"/></remarks>
        /// <returns><see cref="Success_Response"/></returns>
        public Success_Response DeleteAllKeys_Sync()
        {
            Task<Success_Response> data = Task.Run(() => DeleteAllKeys_Async());
            data.Wait();
            return data.Result;
        }

        // delete_key_by_fingerprint
        /// <summary>
        /// Delete the key corresponding to the input fingerprint.
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#delete_key_by_fingerprint"/></remarks>
        /// <returns><see cref="Success_Response"/></returns>
        public async Task<Success_Response> DeleteKeyByFingerprint_Async(FingerPrint_RPC rpc)
        {
            string responseJson = await SendCustomMessageAsync("delete_key_by_fingerprint", rpc.ToString());
            ActionResult<Success_Response> deserializationResult = Success_Response.LoadResponseFromString(responseJson,IncludeRawServerResponse);
            Success_Response response = new Success_Response();
            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
            }
            response.RawContent = deserializationResult.RawJson;
            return response;
        }

        /// <summary>
        /// Delete the key corresponding to the input fingerprint.
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#delete_key_by_fingerprint"/></remarks>
        /// <returns><see cref="Success_Response"/></returns>
        public Success_Response DeleteKeyByFingerprint_Sync(FingerPrint_RPC rpc)
        {
            Task<Success_Response> data = Task.Run(() => DeleteKeyByFingerprint_Async(rpc));
            data.Wait();
            return data.Result;
        }

        // get_all_private_keys
        /// <summary>
        /// List all private keys, along with their respective entropies.
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#get_all_private_keys"/></remarks>
        /// <returns>List of private keys and entropies.</returns>
        public async Task<GetAllPrivateKeys_Response> GetAllPrivateKeys_Async()
        {
            string responseJson = await SendCustomMessageAsync("get_all_private_keys");
            ActionResult<GetAllPrivateKeys_Response> deserializationResult = GetAllPrivateKeys_Response.LoadResponseFromString(responseJson,IncludeRawServerResponse);
            GetAllPrivateKeys_Response response = new GetAllPrivateKeys_Response();
            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
            }
            response.RawContent = deserializationResult.RawJson;
            return response;
        }

        /// <summary>
        /// List all private keys, along with their respective entropies.
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#get_all_private_keys"/></remarks>
        /// <returns>List of private keys and entropies.</returns>
        public GetAllPrivateKeys_Response GetAllPrivateKeys_Sync()
        {
            Task<GetAllPrivateKeys_Response> data = Task.Run(() => GetAllPrivateKeys_Async());
            data.Wait();
            return data.Result;
        }

        // get_first_private_key
        /// <summary>
        /// Obtain the first private key, along with its entropy.
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#get_first_private_key"/></remarks>
        /// <returns>First private key and entropy.</returns>
        public async Task<PrivateKey_Response> GetFirstPrivateKey_Async()
        {
            string responseJson = await SendCustomMessageAsync("get_first_private_key");
            ActionResult<PrivateKey_Response> deserializationResult = PrivateKey_Response.LoadResponseFromString(responseJson,IncludeRawServerResponse);
            PrivateKey_Response response = new PrivateKey_Response();
            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
            }
            response.RawContent = deserializationResult.RawJson;
            return response;
        }

        /// <summary>
        /// Obtain the first private key, along with its entropy.
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#get_first_private_key"/></remarks>
        /// <returns>First private key and entropy.</returns>
        public PrivateKey_Response GetFirstPrivateKey_Sync()
        {
            Task<PrivateKey_Response> data = Task.Run(() => GetFirstPrivateKey_Async());
            data.Wait();
            return data.Result;
        }

        // get_key_for_fingerprint
        /// <summary>
        /// Given a fingerprint, list the corresponding private key and entropy.
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#get_key_for_fingerprint"/></remarks>
        /// <returns>Private key and entropy for given fingerprint.</returns>
        public async Task<PrivateKey_Response> GetKeyForFingerprint_Async(FingerPrint_RPC rpc)
        {
            string responseJson = await SendCustomMessageAsync("get_key_for_fingerprint", rpc.ToString());
            ActionResult<PrivateKey_Response> deserializationResult = PrivateKey_Response.LoadResponseFromString(responseJson,IncludeRawServerResponse);
            PrivateKey_Response response = new PrivateKey_Response();
            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
            }
            response.RawContent = deserializationResult.RawJson;
            return response;
        }

        /// <summary>
        /// Given a fingerprint, list the corresponding private key and entropy.
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#get_key_for_fingerprint"/></remarks>
        /// <returns>Private key and entropy for given fingerprint.</returns>
        public PrivateKey_Response GetKeyForFingerprint_Sync(FingerPrint_RPC rpc)
        {
            Task<PrivateKey_Response> data = Task.Run(() => GetKeyForFingerprint_Async(rpc));
            data.Wait();
            return data.Result;
        }

        // get_key
        /// <summary>
        /// Retrieve the corresponding key for a given fingerprint
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#get_key"/></remarks>
        /// <param name="rpc">The fingerprint whose key you want to look up</param>
        /// <returns><see cref="GetKey_Response"/></returns>
        public async Task<GetKey_Response> GetKey_Async(GetKeys_RPC rpc)
        {
            string responseJson = await SendCustomMessageAsync("get_key", rpc.ToString());
            ActionResult<GetKey_Response> deserializationResult = GetKey_Response.LoadResponseFromString(responseJson,IncludeRawServerResponse);
            GetKey_Response response = new GetKey_Response();
            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
            }
            response.RawContent = deserializationResult.RawJson;
            return response;
        }

        /// <summary>
        /// Retrieve the corresponding key for a given fingerprint
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#get_key"/></remarks>
        /// <param name="rpc">The fingerprint whose key you want to look up</param>
        /// <returns><see cref="GetKey_Response"/></returns>
        public GetKey_Response GetKey_Sync(GetKeys_RPC rpc)
        {
            Task<GetKey_Response> data = Task.Run(() => GetKey_Async(rpc));
            data.Wait();
            return data.Result;
        }

        // get_keys
        /// <summary>
        /// Get all public keys with the option of including secrets
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#get_keys"/></remarks>
        /// <param name="rpc">Set to true to include secrets in the response [Default: false]</param>
        /// <returns><see cref="GetKeys_Response"/></returns>
        public async Task<GetKeys_Response> GetKeys_Async(GetKeys_RPC rpc)
        {
            string responseJson = await SendCustomMessageAsync("get_keys", rpc.ToString());
            ActionResult<GetKeys_Response> deserializationResult = GetKeys_Response.LoadResponseFromString(responseJson,IncludeRawServerResponse);
            GetKeys_Response response = new GetKeys_Response();
            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
            }
            response.RawContent = deserializationResult.RawJson;
            return response;
        }

        /// <summary>
        /// Get all public keys with the option of including secrets
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#get_keys"/></remarks>
        /// <param name="rpc">Set to true to include secrets in the response [Default: false]</param>
        /// <returns><see cref="GetKeys_Response"/></returns>
        public GetKeys_Response GetKeys_Sync(GetKeys_RPC rpc)
        {
            Task<GetKeys_Response> data = Task.Run(() => GetKeys_Async(rpc));
            data.Wait();
            return data.Result;
        }

        // get_public_key
        /// <summary>
        /// Get the public key from a specified fingerprint
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#get_public_key"/></remarks>
        /// <param name="rpc">The fingerprint whose key you want to look up</param>
        /// <returns><see cref="GetKey_Response"/></returns>
        public async Task<GetKey_Response> GetPublicKey_Async(FingerPrint_RPC rpc)
        {
            string responseJson = await SendCustomMessageAsync("get_public_key", rpc.ToString());
            ActionResult<GetKey_Response> deserializationResult = GetKey_Response.LoadResponseFromString(responseJson,IncludeRawServerResponse);
            GetKey_Response response = new GetKey_Response();
            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
            }
            response.RawContent = deserializationResult.RawJson;
            return response;
        }

        /// <summary>
        /// Get the public key from a specified fingerprint
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#get_public_key"/></remarks>
        /// <param name="rpc">The fingerprint whose key you want to look up</param>
        /// <returns><see cref="GetKey_Response"/></returns>
        public GetKey_Response GetPublicKey_Sync(FingerPrint_RPC rpc)
        {
            Task<GetKey_Response> data = Task.Run(() => GetPublicKey_Async(rpc));
            data.Wait();
            return data.Result;
        }

        // get_public_keys
        /// <summary>
        /// Get all public keys
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#get_public_keys"/></remarks>
        /// <returns><see cref="GetKeys_Response"/></returns>
        public async Task<GetKeys_Response> GetPublicKeys_Async()
        {
            string responseJson = await SendCustomMessageAsync("get_public_keys");
            ActionResult<GetKeys_Response> deserializationResult = GetKeys_Response.LoadResponseFromString(responseJson,IncludeRawServerResponse);
            GetKeys_Response response = new GetKeys_Response();
            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
            }
            response.RawContent = deserializationResult.RawJson;
            return response;
        }

        /// <summary>
        /// Get all public keys
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#get_public_keys"/></remarks>
        /// <returns><see cref="GetKeys_Response"/></returns>
        public GetKeys_Response GetPublicKeys_Sync()
        {
            Task<GetKeys_Response> data = Task.Run(() => GetPublicKeys_Async());
            data.Wait();
            return data.Result;
        }

        // set_label
        /// <summary>
        /// Set the label for a specified key
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#set_label"/></remarks>
        /// <param name="rpc">The fingerprint whose label you want to set</param>
        /// <returns><see cref="Success_Response"/></returns>
        public async Task<Success_Response> SetLabel_Async(SetLabel_RPC rpc)
        {
            string responseJson = await SendCustomMessageAsync("set_label", rpc.ToString());
            ActionResult<Success_Response> deserializationResult = Success_Response.LoadResponseFromString(responseJson,IncludeRawServerResponse);
            Success_Response response = new Success_Response();
            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
            }
            response.RawContent = deserializationResult.RawJson;
            return response;
        }

        /// <summary>
        /// Set the label for a specified key
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#set_label"/></remarks>
        /// <param name="rpc">The fingerprint whose label you want to set</param>
        /// <returns><see cref="Success_Response"/></returns>
        public Success_Response SetLabel_Sync(SetLabel_RPC rpc)
        {
            Task<Success_Response> data = Task.Run(() => SetLabel_Async(rpc));
            data.Wait();
            return data.Result;
        }

        // delete_label
        /// <summary>
        /// Delete the label for a specified key
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#delete_label"/></remarks>
        /// <param name="rpc">The fingerprint whose label you want to delete</param>
        /// <returns><see cref="Success_Response"/></returns>
        public async Task<Success_Response> DeleteLabel_Async(FingerPrint_RPC rpc)
        {
            string responseJson = await SendCustomMessageAsync("delete_label", rpc.ToString());
            ActionResult<Success_Response> deserializationResult = Success_Response.LoadResponseFromString(responseJson,IncludeRawServerResponse);
            Success_Response response = new Success_Response();
            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
            }
            response.RawContent = deserializationResult.RawJson;
            return response;
        }

        /// <summary>
        /// Delete the label for a specified key
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#delete_label"/></remarks>
        /// <param name="rpc">The fingerprint whose label you want to delete</param>
        /// <returns><see cref="Success_Response"/></returns>
        public Success_Response DeleteLabel_Sync(FingerPrint_RPC rpc)
        {
            Task<Success_Response> data = Task.Run(() => DeleteLabel_Async(rpc));
            data.Wait();
            return data.Result;
        }
    }
}
