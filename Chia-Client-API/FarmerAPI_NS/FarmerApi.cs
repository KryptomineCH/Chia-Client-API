using Chia_Client_API.ChiaClient_NS;
using Chia_Client_API.Helpers_NS;
using CHIA_RPC.Farmer_NS;
using CHIA_RPC.Farmer_NS.FarmerObjects_NS;
using CHIA_RPC.General_NS;
using CHIA_RPC.HelperFunctions_NS;

namespace Chia_Client_API.FarmerAPI_NS
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
    public abstract partial class FarmerRpcBase : ChiaEndpointRouteBase
    {
        /// <summary>
        /// List all harvesters in your network, including all plots on each individual harvester
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/farmer-rpc#get_harvesters"/></remarks>
        /// <returns></returns>
        public async Task<GetHarvesters_Response> GetHarvesters_Async()
    {
        string responseJson = await SendCustomMessageAsync("get_harvesters");
        ActionResult<GetHarvesters_Response> deserializationResult = GetHarvesters_Response.LoadResponseFromString(responseJson,IncludeRawServerResponse);
        GetHarvesters_Response response = new GetHarvesters_Response();
        if (deserializationResult.Data != null)
        {
            response = deserializationResult.Data;
        }
        else
        {
            response.success = deserializationResult.Success;
            response.error = deserializationResult.Error;
            if (ReportResponseErrors && !(bool)response.success)
            {
                await ReportError.UploadFileAsync(new Error(response, "Farmer", "get_harvesters"));
            }
        }
        response.RawContent = deserializationResult.RawJson;
        return response;
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
            string responseJson = await SendCustomMessageAsync("get_harvesters_summary");
            ActionResult<GetHarvestersSummary_Response> deserializationResult = GetHarvestersSummary_Response.LoadResponseFromString(responseJson,IncludeRawServerResponse);
            GetHarvestersSummary_Response response = new GetHarvestersSummary_Response();
            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                if (ReportResponseErrors && !(bool)response.success)
                {
                    await ReportError.UploadFileAsync(new Error(response, "Farmer", "get_harvesters_summary"));
                }
            }
            response.RawContent = deserializationResult.RawJson;
            return response;
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
            string responseJson = await SendCustomMessageAsync("get_harvester_plots_duplicates", rpc.ToString());
            ActionResult<GetHarvesterPlots_Response> deserializationResult = GetHarvesterPlots_Response.LoadResponseFromString(responseJson,IncludeRawServerResponse);
            GetHarvesterPlots_Response response = new GetHarvesterPlots_Response();
            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                if (ReportResponseErrors && !(bool)response.success)
                {
                    await ReportError.UploadFileAsync(new Error(response, "Farmer", "get_harvester_plots_duplicates"));
                }
            }
            response.RawContent = deserializationResult.RawJson;
            return response;
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
            string responseJson = await SendCustomMessageAsync("get_harvester_plots_invalid", rpc.ToString());
            ActionResult<GetHarvesterPlotsInvalid_Response> deserializationResult = GetHarvesterPlotsInvalid_Response.LoadResponseFromString(responseJson,IncludeRawServerResponse);
            GetHarvesterPlotsInvalid_Response response = new GetHarvesterPlotsInvalid_Response();
            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                if (ReportResponseErrors && !(bool)response.success)
                {
                    await ReportError.UploadFileAsync(new Error(response, "Farmer", "get_harvester_plots_invalid"));
                }
            }
            response.RawContent = deserializationResult.RawJson;
            return response;
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
            string responseJson = await SendCustomMessageAsync("get_harvester_plots_keys_missing", rpc.ToString());
            ActionResult<GetHarvesterPlots_Response> deserializationResult = GetHarvesterPlots_Response.LoadResponseFromString(responseJson,IncludeRawServerResponse);
            GetHarvesterPlots_Response response = new GetHarvesterPlots_Response();
            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                if (ReportResponseErrors && !(bool)response.success)
                {
                    await ReportError.UploadFileAsync(new Error(response, "Farmer", "get_harvester_plots_keys_missing"));
                }
            }
            response.RawContent = deserializationResult.RawJson;
            return response;
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
            string responseJson = await SendCustomMessageAsync("get_harvester_plots_valid", rpc.ToString());
            ActionResult<GetHarvesterPlots_Response> deserializationResult = GetHarvesterPlots_Response.LoadResponseFromString(responseJson,IncludeRawServerResponse);
            GetHarvesterPlots_Response response = new GetHarvesterPlots_Response();
            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                if (ReportResponseErrors && !(bool)response.success)
                {
                    await ReportError.UploadFileAsync(new Error(response, "Farmer", "get_harvester_plots_valid"));
                }
            }
            response.RawContent = deserializationResult.RawJson;
            return response;
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
            string responseJson = await SendCustomMessageAsync("get_pool_login_link", rpc.ToString());
            ActionResult<GetPoolLoginLink_Response> deserializationResult = GetPoolLoginLink_Response.LoadResponseFromString(responseJson,IncludeRawServerResponse);
            GetPoolLoginLink_Response response = new GetPoolLoginLink_Response();
            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                if (ReportResponseErrors && !(bool)response.success)
                {
                    await ReportError.UploadFileAsync(new Error(response, "Farmer", "get_pool_login_link"));
                }
            }
            response.RawContent = deserializationResult.RawJson;
            return response;
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
            string responseJson = await SendCustomMessageAsync("get_pool_state");
            ActionResult<GetPoolState_Response> deserializationResult = GetPoolState_Response.LoadResponseFromString(responseJson,IncludeRawServerResponse);
            GetPoolState_Response response = new GetPoolState_Response();
            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                if (ReportResponseErrors && !(bool)response.success)
                {
                    await ReportError.UploadFileAsync(new Error(response, "Farmer", "get_pool_state"));
                }
            }
            response.RawContent = deserializationResult.RawJson;
            return response;
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
            string responseJson = await SendCustomMessageAsync("get_reward_targets", rpc.ToString());
            ActionResult<GetRewardTargets_Response> deserializationResult = GetRewardTargets_Response.LoadResponseFromString(responseJson,IncludeRawServerResponse);
            GetRewardTargets_Response response = new GetRewardTargets_Response();
            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                if (ReportResponseErrors && !(bool)response.success)
        {
                    await ReportError.UploadFileAsync(new Error(response, "Farmer", "get_reward_targets"));
                }
            }
            response.RawContent = deserializationResult.RawJson;
            return response;
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
            string responseJson = await SendCustomMessageAsync("get_routes");
            ActionResult<GetRoutes_Response> deserializationResult = GetRoutes_Response.LoadResponseFromString(responseJson,IncludeRawServerResponse);
            GetRoutes_Response response = new GetRoutes_Response();
            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                if (ReportResponseErrors &&!(bool)response.success)
        {
                    await ReportError.UploadFileAsync(new Error(response, "Farmer", "get_routes"));
                }
            }
            response.RawContent = deserializationResult.RawJson;
            return response;
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
            string responseJson = await SendCustomMessageAsync("get_signage_point", rpc.ToString());
            ActionResult<SignagePointWithProofs> deserializationResult = SignagePointWithProofs.LoadResponseFromString(responseJson,IncludeRawServerResponse);
            SignagePointWithProofs response = new SignagePointWithProofs();
            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                if (ReportResponseErrors &&!(bool)response.success)
        {
                    await ReportError.UploadFileAsync(new Error(response, "Farmer", "get_signage_point"));
                }
            }
            response.RawContent = deserializationResult.RawJson;
            return response;
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
            string responseJson = await SendCustomMessageAsync("get_signage_points");
            ActionResult<GetSignagePoints_Response> deserializationResult = GetSignagePoints_Response.LoadResponseFromString(responseJson,IncludeRawServerResponse);
            GetSignagePoints_Response response = new GetSignagePoints_Response();
            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                if (ReportResponseErrors && !(bool)response.success)
        {
                    await ReportError.UploadFileAsync(new Error(response, "Farmer", "get_signage_points"));
                }
            }
            response.RawContent = deserializationResult.RawJson;
            return response;
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
            string responseJson = await SendCustomMessageAsync("set_payout_instructions", rpc.ToString());
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
                if (ReportResponseErrors && !(bool)response.success)
                {
                    await ReportError.UploadFileAsync(new Error(response, "Farmer", "set_payout_instructions"));
                }
            }
            response.RawContent = deserializationResult.RawJson;
            return response;
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
            string responseJson = await SendCustomMessageAsync("set_reward_targets", rpc.ToString());
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
                if (ReportResponseErrors && !(bool)response.success)
                {
                    await ReportError.UploadFileAsync(new Error(response, "Farmer", "set_reward_targets"));
                }
            }
            response.RawContent = deserializationResult.RawJson;
            return response;
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
