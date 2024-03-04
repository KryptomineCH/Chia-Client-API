using Chia_Client_API.Helpers_NS;
using CHIA_RPC.General_NS;
using CHIA_RPC.HelperFunctions_NS;
using CHIA_RPC.Wallet_NS.PoolWallet_NS;
using System.Text.Json;
using Chia_Client_API.ChiaClient_NS;

namespace Chia_Client_API.WalletAPI_NS
{
    public abstract partial class WalletRpcBase : ChiaEndpointRouteBase
    {
        /// <summary>
        /// Absorb unspent coinbase rewards to a pool wallet<br/><br/>
        /// WARNING: Documentation incomplete so endpoint is likely implemented incorrectly
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#pw_absorb_rewards"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<Success_Response> PwAbsorbRewards_Async(PwAbsorbRewards_RPC rpc)
        {
            string responseJson = await SendCustomMessageAsync("pw_absorb_rewards", rpc.ToString());
            ActionResult<Success_Response> deserializationResult = Success_Response.LoadResponseFromString(responseJson,IncludeRawServerResponse);
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
        /// Absorb unspent coinbase rewards to a pool wallet<br/><br/>
        /// WARNING: Documentation incomplete so endpoint is likely implemented incorrectly
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#pw_absorb_rewards"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public Success_Response PwAbsorbRewards_Sync(PwAbsorbRewards_RPC rpc)
        {
            Task<Success_Response> data = Task.Run(() => PwAbsorbRewards_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Join a pool
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#pw_join_pool"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<PwJoinPool_Response> PwJoinPool_Async(PwJoinPool_RPC rpc)
        {
            string responseJson = await SendCustomMessageAsync("pw_join_pool", rpc.ToString());
            ActionResult<PwJoinPool_Response> deserializationResult = PwJoinPool_Response.LoadResponseFromString(responseJson,IncludeRawServerResponse);
            PwJoinPool_Response response = new ();

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
        /// Join a pool
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#pw_join_pool"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public PwJoinPool_Response PwJoinPool_Sync(PwJoinPool_RPC rpc)
        {
            Task<PwJoinPool_Response> data = Task.Run(() => PwJoinPool_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Leave a pool and begin self-pooling
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#pw_self_pool"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<PwJoinPool_Response> PwSelfPool_Async(PwSelfPool_RPC rpc)
        {
            string responseJson = await SendCustomMessageAsync("pw_self_pool", rpc.ToString());
            ActionResult<PwJoinPool_Response> deserializationResult = PwJoinPool_Response.LoadResponseFromString(responseJson,IncludeRawServerResponse);
            PwJoinPool_Response response = new ();

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
        /// Leave a pool and begin self-pooling
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#pw_self_pool"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public PwJoinPool_Response PwSelfPool_Sync(PwSelfPool_RPC rpc)
        {
            Task<PwJoinPool_Response> data = Task.Run(() => PwSelfPool_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Obtain the status of a pooling wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#pw_status"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<PwStatus_Response> PwStatus_Async(WalletID_RPC rpc)
        {
            string responseJson = await SendCustomMessageAsync("pw_status", rpc.ToString());
            ActionResult<PwStatus_Response> deserializationResult = PwStatus_Response.LoadResponseFromString(responseJson,IncludeRawServerResponse);
            PwStatus_Response response = new ();

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
        /// Obtain the status of a pooling wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#pw_status"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public PwStatus_Response PwStatus_Sync(WalletID_RPC rpc)
        {
            Task<PwStatus_Response> data = Task.Run(() => PwStatus_Async(rpc));
            data.Wait();
            return data.Result;
        }
    }
}
