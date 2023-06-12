using CHIA_RPC.General_NS;
using CHIA_RPC.Wallet_NS.PoolWallet_NS;
using System.Text.Json;

namespace Chia_Client_API.WalletAPI_NS
{
    public partial class Wallet_RPC_Client
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
            string responseJson = await SendCustomMessage_Async("pw_absorb_rewards", rpc.ToString());
            Success_Response deserializedObject = Success_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
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
            string responseJson = await SendCustomMessage_Async("pw_join_pool", rpc.ToString());
            PwJoinPool_Response deserializedObject = PwJoinPool_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
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
            string responseJson = await SendCustomMessage_Async("pw_self_pool", rpc.ToString());
            PwJoinPool_Response deserializedObject = PwJoinPool_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
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
            string responseJson = await SendCustomMessage_Async("pw_status", rpc.ToString());
            PwStatus_Response deserializedObject = PwStatus_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
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
