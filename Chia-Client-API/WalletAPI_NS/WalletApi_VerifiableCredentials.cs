using Chia_Client_API.Helpers_NS;
using CHIA_RPC.General_NS;
using CHIA_RPC.HelperFunctions_NS;
using CHIA_RPC.Wallet_NS.VerifiableCredentials_NS;
using System.Text.Json;

namespace Chia_Client_API.WalletAPI_NS
{
    public partial class Wallet_RPC_Client
    {
        // vc_add_proofs
        /// <summary>
        /// Add a set of proofs to the DB that can be used when spending a VC
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#vc_add_proofs"/></remarks>
        /// <returns><see cref="Success_Response"/></returns>
        public async Task<Success_Response> VCAddProofs_Async(VcAddProofs_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("vc_add_proofs", rpc.ToString());
            ActionResult<Success_Response> deserializationResult = Success_Response.LoadResponseFromString(responseJson);
            Success_Response response = new ();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
        }

        /// <summary>
        /// Synchronous wrapper for VCAddProofs_Async.
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#vc_add_proofs"/></remarks>
        /// <returns><see cref="Success_Response"/></returns>
        public Success_Response VCAddProofs_Sync(VcAddProofs_RPC rpc)
        {
            Task<Success_Response> data = Task.Run(() => VCAddProofs_Async(rpc));
            data.Wait();
            return data.Result;
        }

        // vc_get
        /// <summary>
        /// Given a launcher ID, get the Verifiable Credential
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#vc_get"/></remarks>
        /// <returns><see cref="VcGet_Response"/></returns>
        public async Task<VcGet_Response> VcGet_Async(VcID_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("vc_get", rpc.ToString());
            ActionResult<VcGet_Response> deserializationResult = VcGet_Response.LoadResponseFromString(responseJson);
            VcGet_Response response = new ();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
        }

        /// <summary>
        /// Synchronous wrapper for VcGet_Async.
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#vc_get"/></remarks>
        /// <returns><see cref="VcGet_Response"/></returns>
        public VcGet_Response VcGet_Sync(VcID_RPC rpc)
        {
            Task<VcGet_Response> data = Task.Run(() => VcGet_Async(rpc));
            data.Wait();
            return data.Result;
        }

        // vc_get_list
        /// <summary>
        /// Get a list of Verifiable Credentials
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#vc_get_list"/></remarks>
        /// <returns><see cref="VcGetList_Response"/></returns>
        public async Task<VcGetList_Response> VcGetList_Async(VcGetList_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("vc_get_list", rpc.ToString());
            ActionResult<VcGetList_Response> deserializationResult = VcGetList_Response.LoadResponseFromString(responseJson);
            VcGetList_Response response = new ();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
        }

        /// <summary>
        /// Synchronous wrapper for VcGetList_Async.
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#vc_get_list"/></remarks>
        /// <returns><see cref="VcGetList_Response"/></returns>
        public VcGetList_Response VcGetList_Sync(VcGetList_RPC rpc)
        {
            Task<VcGetList_Response> data = Task.Run(() => VcGetList_Async(rpc));
            data.Wait();
            return data.Result;
        }


        // vc_get_proofs_for_root
        /// <summary>
        /// Given a specified VC root, get any proofs associated with that root
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#vc_get_proofs_for_root"/></remarks>
        /// <returns><see cref="VcGetProofsForRoot_Response"/></returns>
        public async Task<VcGetProofsForRoot_Response> VCGetProofsForRoot_Async(VcGetProofsForRoot_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("vc_get_proofs_for_root", rpc.ToString());
            ActionResult<VcGetProofsForRoot_Response> deserializationResult = VcGetProofsForRoot_Response.LoadResponseFromString(responseJson);
            VcGetProofsForRoot_Response response = new ();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
        }

        /// <summary>
        /// Synchronous wrapper for VCGetProofsForRoot_Async.
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#vc_get_proofs_for_root"/></remarks>
        /// <returns><see cref="VcGetProofsForRoot_Response"/></returns>
        public VcGetProofsForRoot_Response VCGetProofsForRoot_Sync(VcGetProofsForRoot_RPC rpc)
        {
            Task<VcGetProofsForRoot_Response> data = Task.Run(() => VCGetProofsForRoot_Async(rpc));
            data.Wait();
            return data.Result;
        }


        // vc_mint
        /// <summary>
        /// Mint a Verifiable Credential using the assigned DID
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#vc_mint"/></remarks>
        /// <returns><see cref="VcMint_Response"/></returns>
        public async Task<VcMint_Response> VcMint_Async(VcMint_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("vc_mint", rpc.ToString());
            ActionResult<VcMint_Response> deserializationResult = VcMint_Response.LoadResponseFromString(responseJson);
            VcMint_Response response = new ();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
        }

        /// <summary>
        /// Synchronous wrapper for VcMint_Async.
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#vc_mint"/></remarks>
        /// <returns><see cref="VcMint_Response"/></returns>
        public VcMint_Response VcMint_Sync(VcMint_RPC rpc)
        {
            Task<VcMint_Response> data = Task.Run(() => VcMint_Async(rpc));
            data.Wait();
            return data.Result;
        }

        // vc_revoke
        /// <summary>
        /// Revoke an on-chain VC provided the correct DID is available
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#vc_revoke"/></remarks>
        /// <returns><see cref="Success_Response"/></returns>
        public async Task<Success_Response> VcRevoke_Async(VcRevoke_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("vc_revoke", rpc.ToString());
            ActionResult<Success_Response> deserializationResult = Success_Response.LoadResponseFromString(responseJson);
            Success_Response response = new ();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
        }

        /// <summary>
        /// Synchronous wrapper for VcRevoke_Async.
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#vc_revoke"/></remarks>
        /// <returns><see cref="Success_Response"/></returns>
        public Success_Response VcRevoke_Sync(VcRevoke_RPC rpc)
        {
            Task<Success_Response> data = Task.Run(() => VcRevoke_Async(rpc));
            data.Wait();
            return data.Result;
        }

    }
}
