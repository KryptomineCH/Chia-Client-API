using CHIA_RPC.Farmer_NS;
using CHIA_RPC.Farmer_NS.FarmerObjects_NS;
using CHIA_RPC.General_NS;
using System.Text.Json;

namespace Chia_Client_API.FarmerAPI_NS
{
    public partial class Farmer_RPC_Client
    {
        /// <summary>
        /// List all harvesters in your network, including all plots on each individual harvester
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/farmer-rpc#get_harvesters"/></remarks>
        /// <returns></returns>
        public async Task<GetHarvesters_Response> GetHarvesters_Async()
        {
            string responseJson = await SendCustomMessage_Async("get_harvesters");
            GetHarvesters_Response deserializedObject = JsonSerializer.Deserialize<GetHarvesters_Response>(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// List all harvesters in your network, including all plots on each individual harvester
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/farmer-rpc#get_harvesters"/></remarks>
        /// <returns></returns>
        public GetHarvesters_Response GetHarvesters_Sync()
        {
            Task<GetHarvesters_Response> data = Task.Run(() => GetHarvesters_Async());
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// List all harvesters in your network, including the number of plots (but not the individual plots)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/farmer-rpc#get_harvesters_summary"/></remarks>
        /// <returns></returns>
        public async Task<GetHarvestersSummary_Response> GetHarvestersSummary_Async()
        {
            string responseJson = await SendCustomMessage_Async("get_harvesters_summary");
            GetHarvestersSummary_Response deserializedObject = JsonSerializer.Deserialize<GetHarvestersSummary_Response>(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// List all harvesters in your network, including the number of plots (but not the individual plots)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/farmer-rpc#get_harvesters_summary"/></remarks>
        /// <returns></returns>
        public GetHarvestersSummary_Response GetHarvestersSummary_Sync()
        {
            Task<GetHarvestersSummary_Response> data = Task.Run(() => GetHarvestersSummary_Async());
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// List duplicate plots
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/farmer-rpc#get_harvester_plots_duplicates"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetHarvesterPlots_Response> GetHarvesterPlotsDuplicates_Async(GetHarvesterPlots_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_harvester_plots_duplicates", rpc.ToString());
            GetHarvesterPlots_Response deserializedObject = JsonSerializer.Deserialize<GetHarvesterPlots_Response>(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// List duplicate plots
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/farmer-rpc#get_harvester_plots_duplicates"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetHarvesterPlots_Response GetHarvesterPlotsDuplicates_Sync(GetHarvesterPlots_RPC rpc)
        {
            Task<GetHarvesterPlots_Response> data = Task.Run(() => GetHarvesterPlotsDuplicates_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// List invalid plots in your local network
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/farmer-rpc#get_harvester_plots_invalid"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetHarvesterPlotsInvalid_Response> GetHarvesterPlotsInvalid_Async(GetHarvesterPlots_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_harvester_plots_invalid", rpc.ToString());
            GetHarvesterPlotsInvalid_Response deserializedObject = JsonSerializer.Deserialize<GetHarvesterPlotsInvalid_Response>(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// List invalid plots in your local network
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/farmer-rpc#get_harvester_plots_invalid"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetHarvesterPlotsInvalid_Response GetHarvesterPlotsInvalid_Sync(GetHarvesterPlots_RPC rpc)
        {
            Task<GetHarvesterPlotsInvalid_Response> data = Task.Run(() => GetHarvesterPlotsInvalid_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// List plots from your plot directories that have missing keys / are not associated with the current node_id
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/farmer-rpc#get_harvester_plots_keys_missing"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetHarvesterPlots_Response> GetHarvesterPlotsKeysMissing_Async(GetHarvesterPlots_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_harvester_plots_keys_missing", rpc.ToString());
            GetHarvesterPlots_Response deserializedObject = JsonSerializer.Deserialize<GetHarvesterPlots_Response>(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// List plots from your plot directories that have missing keys / are not associated with the current node_id
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/farmer-rpc#get_harvester_plots_keys_missing"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetHarvesterPlots_Response GetHarvesterPlotsKeysMissing_Sync(GetHarvesterPlots_RPC rpc)
        {
            Task<GetHarvesterPlots_Response> data = Task.Run(() => GetHarvesterPlotsKeysMissing_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// List valid plots in your local network
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/farmer-rpc#get_harvester_plots_valid"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetHarvesterPlots_Response> GetHarvesterPlotsValid_Async(GetHarvesterPlots_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_harvester_plots_valid", rpc.ToString());
            GetHarvesterPlots_Response deserializedObject = JsonSerializer.Deserialize<GetHarvesterPlots_Response>(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// List valid plots in your local network
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/farmer-rpc#get_harvester_plots_valid"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetHarvesterPlots_Response GetHarvesterPlotsValid_Sync(GetHarvesterPlots_RPC rpc)
        {
            Task<GetHarvesterPlots_Response> data = Task.Run(() => GetHarvesterPlotsValid_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Get a URI to view your pool info
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/farmer-rpc#get_pool_login_link"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetPoolLoginLink_Response> GetPoolLoginLink_Async(LauncherID_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_pool_login_link", rpc.ToString());
            GetPoolLoginLink_Response deserializedObject = JsonSerializer.Deserialize<GetPoolLoginLink_Response>(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Get a URI to view your pool info
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/farmer-rpc#get_pool_login_link"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetPoolLoginLink_Response GetPoolLoginLink_Sync(LauncherID_RPC rpc)
        {
            Task<GetPoolLoginLink_Response> data = Task.Run(() => GetPoolLoginLink_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// If pooling is enabled, show all pool info, such as p2_singleton_puzzle_hash and plot_count
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/farmer-rpc#get_pool_state"/></remarks>
        /// <returns></returns>
        public async Task<GetPoolState_Response> GetPoolState_Async()
        {
            string responseJson = await SendCustomMessage_Async("get_pool_state");
            GetPoolState_Response deserializedObject = JsonSerializer.Deserialize<GetPoolState_Response>(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// If pooling is enabled, show all pool info, such as p2_singleton_puzzle_hash and plot_count
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/farmer-rpc#get_pool_state"/></remarks>
        /// <returns></returns>
        public GetPoolState_Response GetPoolState_Sync()
        {
            Task<GetPoolState_Response> data = Task.Run(() => GetPoolState_Async());
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// List the payout targets for the farmer (1/8 of the reward) and pool (7/8)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/farmer-rpc#get_reward_targets"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetRewardTargets_Response> GetRewardTargets_Async(GetRewardTargets_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_reward_targets", rpc.ToString());
            GetRewardTargets_Response deserializedObject = JsonSerializer.Deserialize<GetRewardTargets_Response>(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// List the payout targets for the farmer (1/8 of the reward) and pool (7/8)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/farmer-rpc#get_reward_targets"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetRewardTargets_Response GetRewardTargets_Sync(GetRewardTargets_RPC rpc)
        {
            Task<GetRewardTargets_Response> data = Task.Run(() => GetRewardTargets_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// List all available RPC routes
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/farmer-rpc#get_routes"/></remarks>
        /// <returns></returns>
        public async Task<GetRoutes_Response> GetRoutes_Async()
        {
            string responseJson = await SendCustomMessage_Async("get_routes");
            GetRoutes_Response deserializedObject = JsonSerializer.Deserialize<GetRoutes_Response>(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// List all available RPC routes
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/farmer-rpc#get_routes"/></remarks>
        /// <returns></returns>
        public GetRoutes_Response GetRoutes_Sync()
        {
            Task<GetRoutes_Response> data = Task.Run(() => GetRoutes_Async());
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Given a signage point's hash, list the details of that signage point
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/farmer-rpc#get_signage_point"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<SignagePointWithProofs> GetSignagePoint_Async(GetSignagePoint_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_signage_point", rpc.ToString());
            SignagePointWithProofs deserializedObject = JsonSerializer.Deserialize<SignagePointWithProofs>(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Given a signage point's hash, list the details of that signage point
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/farmer-rpc#get_signage_point"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public SignagePointWithProofs GetSignagePoint_Sync(GetSignagePoint_RPC rpc)
        {
            Task<SignagePointWithProofs> data = Task.Run(() => GetSignagePoint_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// List details for all signage points going back several challenges
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/farmer-rpc#get_signage_points"/></remarks>
        /// <returns></returns>
        public async Task<GetSignagePoints_Response> GetSignagePoints_Async()
        {
            string responseJson = await SendCustomMessage_Async("get_signage_points");
            GetSignagePoints_Response deserializedObject = JsonSerializer.Deserialize<GetSignagePoints_Response>(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// List details for all signage points going back several challenges
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/farmer-rpc#get_signage_points"/></remarks>
        /// <returns></returns>
        public GetSignagePoints_Response GetSignagePoints_Sync()
        {
            Task<GetSignagePoints_Response> data = Task.Run(() => GetSignagePoints_Async());
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Set the payout_instructions parameter for your pool configuration
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/farmer-rpc#set_payout_instructions"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<Success_Response> SetPayoutInstructions_Async(SetPayoutInstructions_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("set_payout_instructions", rpc.ToString());
            Success_Response deserializedObject = JsonSerializer.Deserialize<Success_Response>(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Set the payout_instructions parameter for your pool configuration
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/farmer-rpc#set_payout_instructions"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public Success_Response SetPayoutInstructions_Sync(SetPayoutInstructions_RPC rpc)
        {
            Task<Success_Response> data = Task.Run(() => SetPayoutInstructions_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Set the farmer and/or pool reward target address(es)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/farmer-rpc#set_reward_targets"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<Success_Response> SetRewardTargets_Async(SetRewardTargets_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("set_reward_targets", rpc.ToString());
            Success_Response deserializedObject = JsonSerializer.Deserialize<Success_Response>(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Set the farmer and/or pool reward target address(es)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/farmer-rpc#set_reward_targets"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public Success_Response SetRewardTargets_Sync(SetRewardTargets_RPC rpc)
        {
            Task<Success_Response> data = Task.Run(() => SetRewardTargets_Async(rpc));
            data.Wait();
            return data.Result;
        }
    }
}
