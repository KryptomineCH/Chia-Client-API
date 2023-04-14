
using CHIA_RPC.FullNode_NS;
using CHIA_RPC.FullNode_NS.Simulator_NS;
using System.Text.Json;

namespace Chia_Client_API.FullNodeAPI_NS
{
    public partial class FullNode_RPC_Client
    {
        /// <summary>
        /// Return a list of all blocks in the blockhain
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/simulator-rpc#get_all_blocks"/></remarks>
        /// <returns></returns>
        public async Task<GetBlocks_Response> Simulator_GetAllBlocks_Async()
        {
            string responseJson = await SendCustomMessage_Async("get_all_blocks");
            GetBlocks_Response deserializedObject = JsonSerializer.Deserialize<GetBlocks_Response>(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Return a list of all blocks in the blockhain
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/simulator-rpc#get_all_blocks"/></remarks>
        /// <returns></returns>
        public GetBlocks_Response Simulator_GetAllBlocks_Sync()
        {
            Task<GetBlocks_Response> data = Task.Run(() => Simulator_GetAllBlocks_Async());
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Farm one or more blocks. Can ensure farming a transaction block if required
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/simulator-rpc#farm_block"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<FarmBlock_Response> Simulator_FarmBlock_Async(FarmBlock_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("farm_block", rpc.ToString());
            FarmBlock_Response deserializedObject = JsonSerializer.Deserialize<FarmBlock_Response>(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Farm one or more blocks. Can ensure farming a transaction block if required
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/simulator-rpc#farm_block"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public FarmBlock_Response Simulator_FarmBlock_Sync(FarmBlock_RPC rpc)
        {
            Task<FarmBlock_Response> data = Task.Run(() => Simulator_FarmBlock_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Set whether to auto farm (Boolean)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/simulator-rpc#set_auto_farming"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<AutoFarming_Response> Simulator_SetAutoFarming_Async(SetAutoFarming_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("set_auto_farming", rpc.ToString());
            AutoFarming_Response deserializedObject = JsonSerializer.Deserialize<AutoFarming_Response>(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Set whether to auto farm (Boolean)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/simulator-rpc#set_auto_farming"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public AutoFarming_Response Simulator_SetAutoFarming_Sync(SetAutoFarming_RPC rpc)
        {
            Task<AutoFarming_Response> data = Task.Run(() => Simulator_SetAutoFarming_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Returns a Boolean to indicate whether auto farming is enabled
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/simulator-rpc#get_auto_farming"/></remarks>
        /// <returns></returns>
        public async Task<AutoFarming_Response> Simulator_GetAutoFarming_Async()
        {
            string responseJson = await SendCustomMessage_Async("get_auto_farming");
            AutoFarming_Response deserializedObject = JsonSerializer.Deserialize<AutoFarming_Response>(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Returns a Boolean to indicate whether auto farming is enabled
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/simulator-rpc#get_auto_farming"/></remarks>
        /// <returns></returns>
        public AutoFarming_Response Simulator_GetAutoFarming_Sync()
        {
            Task<AutoFarming_Response> data = Task.Run(() => Simulator_GetAutoFarming_Async());
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Get the puzzle hash used by the farmer
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/simulator-rpc#get_farming_ph"/></remarks>
        /// <returns></returns>
        public async Task<GetFarmingPh_Response> Simulator_GetFarmingPh_Async()
        {
            string responseJson = await SendCustomMessage_Async("get_farming_ph");
            GetFarmingPh_Response deserializedObject = JsonSerializer.Deserialize<GetFarmingPh_Response>(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Get the puzzle hash used by the farmer
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/simulator-rpc#get_farming_ph"/></remarks>
        /// <returns></returns>
        public GetFarmingPh_Response Simulator_GetFarmingPh_Sync()
        {
            Task<GetFarmingPh_Response> data = Task.Run(() => Simulator_GetFarmingPh_Async());
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Get all coins
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/simulator-rpc#get_all_coins"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetAllCoins_Response> Simulator_GetAllCoins_Async(GetAllCoins_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_all_coins", rpc.ToString());
            GetAllCoins_Response deserializedObject = JsonSerializer.Deserialize<GetAllCoins_Response>(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Get all coins
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/simulator-rpc#get_all_coins"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetAllCoins_Response Simulator_GetAllCoins_Sync(GetAllCoins_RPC rpc)
        {
            Task<GetAllCoins_Response> data = Task.Run(() => Simulator_GetAllCoins_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Get all puzzle hashes used in this blockchain
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/simulator-rpc#get_all_puzzle_hashes"/></remarks>
        /// <returns></returns>
        public async Task<GetAllPuzzleHashes_Response> Simulator_GetAllPuzzleHashes_Async()
        {
            string responseJson = await SendCustomMessage_Async("get_all_puzzle_hashes\r\n");
            GetAllPuzzleHashes_Response deserializedObject = JsonSerializer.Deserialize<GetAllPuzzleHashes_Response>(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Get all puzzle hashes used in this blockchain
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/simulator-rpc#get_all_puzzle_hashes"/></remarks>
        /// <returns></returns>
        public GetAllPuzzleHashes_Response Simulator_GetAllPuzzleHashes_Sync()
        {
            Task<GetAllPuzzleHashes_Response> data = Task.Run(() => Simulator_GetAllPuzzleHashes_Async());
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Revert a customizable number of blocks
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/simulator-rpc#revert_blocks"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<NewPeakHeight_Response> Simulator_RevertBlocks_Async(RevertBlocks_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("revert_blocks", rpc.ToString());
            NewPeakHeight_Response deserializedObject = JsonSerializer.Deserialize<NewPeakHeight_Response>(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Revert a customizable number of blocks
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/simulator-rpc#revert_blocks"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public NewPeakHeight_Response Simulator_RevertBlocks_Sync(RevertBlocks_RPC rpc)
        {
            Task<NewPeakHeight_Response> data = Task.Run(() => Simulator_RevertBlocks_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Initiate a reorg or a customizable number of blocks
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/simulator-rpc#reorg_blocks"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<NewPeakHeight_Response> Simulator_ReorgBlocks_Async(ReorgBlocks_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("reorg_blocks", rpc.ToString());
            NewPeakHeight_Response deserializedObject = JsonSerializer.Deserialize<NewPeakHeight_Response>(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Initiate a reorg or a customizable number of blocks
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/simulator-rpc#reorg_blocks"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public NewPeakHeight_Response Simulator_ReorgBlocks_Sync(ReorgBlocks_RPC rpc)
        {
            Task<NewPeakHeight_Response> data = Task.Run(() => Simulator_ReorgBlocks_Async(rpc));
            data.Wait();
            return data.Result;
        }
    }
}
