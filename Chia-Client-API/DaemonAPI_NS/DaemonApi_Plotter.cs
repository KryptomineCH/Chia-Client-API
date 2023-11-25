using Chia_Client_API.Helpers_NS;
using CHIA_RPC.Daemon_NS.Plotter_NS;
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
    public partial class Daemon_RPC_Client
    {
        // get_keys_for_plotting
        /// <summary>
        /// Show the farmer_public_key and pool_public_key for one or more wallet fingerprints
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#get_keys_for_plotting"/></remarks>
        /// <returns><see cref="GetKeysForPlotting_Response"/></returns>
        public async Task<GetKeysForPlotting_Response> GetKeysForPlotting_Async(FingerPrints_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_keys_for_plotting", rpc.ToString());
            ActionResult<GetKeysForPlotting_Response> deserializationResult = GetKeysForPlotting_Response.LoadResponseFromString(responseJson);
            GetKeysForPlotting_Response response = new GetKeysForPlotting_Response();
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
        /// Show the farmer_public_key and pool_public_key for one or more wallet fingerprints (Synchronous)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#get_keys_for_plotting"/></remarks>
        /// <returns><see cref="GetKeysForPlotting_Response"/></returns>
        public GetKeysForPlotting_Response GetKeysForPlotting_Sync(FingerPrints_RPC rpc)
        {
            Task<GetKeysForPlotting_Response> data = Task.Run(() => GetKeysForPlotting_Async(rpc));
            data.Wait();
            return data.Result;
        }

        // get_plotters
        /// <summary>
        /// List all available plotters
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#get_plotters"/></remarks>
        /// <returns><see cref="GetPlotters_Response"/></returns>
        public async Task<GetPlotters_Response> GetPlotters_Async()
        {
            string responseJson = await SendCustomMessage_Async("get_plotters");
            ActionResult<GetPlotters_Response> deserializationResult = GetPlotters_Response.LoadResponseFromString(responseJson);
            GetPlotters_Response response = new GetPlotters_Response();
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
                    await ReportError.UploadFileAsync(new Error(response, "Daemon", "get_plotters"));
                }
            }
            response.RawContent = deserializationResult.RawJson;
            return response;
        }

        /// <summary>
        /// List all available plotters (Synchronous)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#get_plotters"/></remarks>
        /// <returns><see cref="GetPlotters_Response"/></returns>
        public GetPlotters_Response GetPlotters_Sync()
        {
            Task<GetPlotters_Response> data = Task.Run(() => GetPlotters_Async());
            data.Wait();
            return data.Result;
        }

        // start_plotting
        /// <summary>
        /// Create one or more plots with the desired plotter
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#start_plotting"/></remarks>
        /// <returns><see cref="Success_Response"/></returns>
        public async Task<Success_Response> StartPlotting_Async(StartPlotting_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("start_plotting", rpc.ToString());
            ActionResult<Success_Response> deserializationResult = Success_Response.LoadResponseFromString(responseJson);
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
                    await ReportError.UploadFileAsync(new Error(response, "Daemon", "start_plotting"));
                }
            }
            response.RawContent = deserializationResult.RawJson;
            return response;
        }

        /// <summary>
        /// Create one or more plots with the desired plotter (Synchronous)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#start_plotting"/></remarks>
        /// <returns><see cref="Success_Response"/></returns>
        public Success_Response StartPlotting_Sync(StartPlotting_RPC rpc)
        {
            Task<Success_Response> data = Task.Run(() => StartPlotting_Async(rpc));
            data.Wait();
            return data.Result;
        }

        // stop_plotting
        /// <summary>
        /// Stop creating a plot
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#stop_plotting"/></remarks>
        /// <returns><see cref="Success_Response"/></returns>
        public async Task<Success_Response> StopPlotting_Async(ID_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("stop_plotting", rpc.ToString());
            ActionResult<Success_Response> deserializationResult = Success_Response.LoadResponseFromString(responseJson);
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
                    await ReportError.UploadFileAsync(new Error(response, "Daemon", "stop_plotting"));
                }
            }
            response.RawContent = deserializationResult.RawJson;
            return response;
        }

        /// <summary>
        /// Stop creating a plot (Synchronous)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#stop_plotting"/></remarks>
        /// <returns><see cref="Success_Response"/></returns>
        public Success_Response StopPlotting_Sync(ID_RPC rpc)
        {
            Task<Success_Response> data = Task.Run(() => StopPlotting_Async(rpc));
            data.Wait();
            return data.Result;
        }

    }
}
