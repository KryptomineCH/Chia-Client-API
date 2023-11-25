using Chia_Client_API.Helpers_NS;
using CHIA_RPC.General_NS;
using CHIA_RPC.Harvester_NS;
using CHIA_RPC.HelperFunctions_NS;

namespace Chia_Client_API.HarvesterAPI_NS
{
    ///<summary>
    /// The Harvester_RPC_Client class is a part of the Remote Procedure Call (RPC) client that interacts with the harvester service of the Chia network. 
    /// This class contains all the necessary methods to interact with the harvester service using RPC.
    /// </summary>
    /// <remarks>
    /// The Harvester is responsible for managing the farming of plots, including reading and responding to challenges from the Chia blockchain. 
    /// It ensures that proof of space is available for a given plot when the challenge comes in. 
    /// The RPC client allows the user to perform a variety of tasks related to managing and interfacing with the harvester service.
    /// </remarks>
    public partial class Harvester_RPC_Client
    {
        /// <summary>
        /// Add a new plot directory<br/><br/>
        /// IMPORTANT: Note that the new directory must already exist on the system
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/harvester-rpc#add_plot_directory"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<Success_Response> AddPlotDirectory_Async(AddPlotDirectory_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("add_plot_directory", rpc.ToString());
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
                response.RawContent = deserializationResult.RawJson;
                if (ReportResponseErrors && !(bool)response.success)
                {
                    await ReportError.UploadFileAsync(new Error(response, "Harvester", "add_plot_directory"));
                }
            }
            return response;
        }

        /// <summary>
        /// Add a new plot directory<br/><br/>
        /// IMPORTANT: Note that the new directory must already exist on the system
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/harvester-rpc#add_plot_directory"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public Success_Response AddPlotDirectory_Sync(AddPlotDirectory_RPC rpc)
        {
            Task<Success_Response> data = Task.Run(() => AddPlotDirectory_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Delete a single plot<br/><br/>
        /// IMPORTANT: As long as this command includes the required filename flag, it will always output "success": true, even if the filename was invalid
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/harvester-rpc#delete_plot"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<Success_Response> DeletePlot_Async(DeletePlot_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("delete_plot", rpc.ToString());
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
                response.RawContent = deserializationResult.RawJson;
                if (ReportResponseErrors && !(bool)response.success)
                {
                    await ReportError.UploadFileAsync(new Error(response, "Harvester", "delete_plot"));
                }
            }
            return response;
        }

        /// <summary>
        /// Delete a single plot<br/><br/>
        /// IMPORTANT: As long as this command includes the required filename flag, it will always output "success": true, even if the filename was invalid
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/harvester-rpc#delete_plot"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public Success_Response DeletePlot_Sync(DeletePlot_RPC rpc)
        {
            Task<Success_Response> data = Task.Run(() => DeletePlot_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// List all local plots<br/><br/>
        /// NOTE: The plots will be grouped into three categories:<br/>
        /// - failed_to_open_filenames - plots that the harvester was unable to open; these plots may be incomplete, corrupted or otherwise damaged<br/>
        /// - not_found_filenames - typically these are plots that exist and are readable, but were created under a different key than the current one<br/>
        /// - plots - a listing of all valid plots that were created with the current key used by the harvester
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/harvester-rpc#get_plots"/></remarks>
        /// <returns></returns>
        public async Task<GetPlots_Response> GetPlots_Async()
        {
            string responseJson = await SendCustomMessage_Async("get_plots");
            ActionResult<GetPlots_Response> deserializationResult = GetPlots_Response.LoadResponseFromString(responseJson);
            GetPlots_Response response = new GetPlots_Response();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
                if (ReportResponseErrors && !(bool)response.success)
                {
                    await ReportError.UploadFileAsync(new Error(response, "Harvester", "get_plots"));
                }
            }
            return response;
        }

        /// <summary>
        /// List all local plots<br/><br/>
        /// NOTE: The plots will be grouped into three categories:<br/>
        /// - failed_to_open_filenames - plots that the harvester was unable to open; these plots may be incomplete, corrupted or otherwise damaged<br/>
        /// - not_found_filenames - typically these are plots that exist and are readable, but were created under a different key than the current one<br/>
        /// - plots - a listing of all valid plots that were created with the current key used by the harvester
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/harvester-rpc#get_plots"/></remarks>
        /// <returns></returns>
        public GetPlots_Response GetPlots_Sync()
        {
            Task<GetPlots_Response> data = Task.Run(() => GetPlots_Async());
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// List all plot directories
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/harvester-rpc#get_plot_directories"/></remarks>
        /// <returns></returns>
        public async Task<GetPlotDirectories_Response> GetPlotDirectories_Async()
        {
            string responseJson = await SendCustomMessage_Async("get_plot_directories");
            ActionResult<GetPlotDirectories_Response> deserializationResult = GetPlotDirectories_Response.LoadResponseFromString(responseJson);
            GetPlotDirectories_Response response = new GetPlotDirectories_Response();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
                if (ReportResponseErrors && !(bool)response.success)
                {
                    await ReportError.UploadFileAsync(new Error(response, "Harvester", "get_plot_directories"));
                }
            }
            return response;
        }

        /// <summary>
        /// List all plot directories
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/harvester-rpc#get_plot_directories"/></remarks>
        /// <returns></returns>
        public GetPlotDirectories_Response GetPlotDirectories_Sync()
        {
            Task<GetPlotDirectories_Response> data = Task.Run(() => GetPlotDirectories_Async());
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// List all available RPC routes
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/harvester-rpc#get_routes"/></remarks>
        /// <returns></returns>
        public async Task<GetRoutes_Response> GetRoutes_Async()
        {
            string responseJson = await SendCustomMessage_Async("get_routes");
            ActionResult<GetRoutes_Response> deserializationResult = GetRoutes_Response.LoadResponseFromString(responseJson);
            GetRoutes_Response response = new GetRoutes_Response();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
                if (ReportResponseErrors && !(bool)response.success)
                {
                    await ReportError.UploadFileAsync(new Error(response, "Fullnode", "get_routes"));
                }
            }
            return response;
        }
        /// <summary>
        /// List all available RPC routes
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/harvester-rpc#get_routes"/></remarks>
        /// <returns></returns>
        public GetRoutes_Response GetRoutes_Sync()
        {
            Task<GetRoutes_Response> data = Task.Run(() => GetRoutes_Async());
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Refresh all plots from the harvester<br/><br/>
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/harvester-rpc#refresh_plots"/></remarks>
        /// <returns></returns>
        public async Task<Success_Response> RefreshPlots_Async()
        {
            string responseJson = await SendCustomMessage_Async("refresh_plots");
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
                response.RawContent = deserializationResult.RawJson;
                if (ReportResponseErrors && !(bool)response.success)
                {
                    await ReportError.UploadFileAsync(new Error(response, "Harvester", "refresh_plots"));
                }
            }
            return response;
        }

        /// <summary>
        /// Refresh all plots from the harvester<br/><br/>
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/harvester-rpc#refresh_plots"/></remarks>
        /// <returns></returns>
        public Success_Response RefreshPlots_Sync()
        {
            Task<Success_Response> data = Task.Run(() => RefreshPlots_Async());
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Remove a directory from the harvester's list of plot directories<br/><br/>
        /// IMPORTANT: As long as this command includes the required dirname flag, it will always output "success": true, even if the dirname is not in the directory list
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/harvester-rpc#remove_plot_directory"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<Success_Response> RemovePlotDirectory_Async(RemovePlotDirectory_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("remove_plot_directory", rpc.ToString());
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
                response.RawContent = deserializationResult.RawJson;
                if (ReportResponseErrors && !(bool)response.success)
                {
                    await ReportError.UploadFileAsync(new Error(response, "Harvester", "remove_plot_directory"));
                }
            }
            return response;
        }


        /// <summary>
        /// Remove a directory from the harvester's list of plot directories<br/><br/>
        /// IMPORTANT: As long as this command includes the required dirname flag, it will always output "success": true, even if the dirname is not in the directory list
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/harvester-rpc#remove_plot_directory"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public Success_Response RemovePlotDirectory_Sync(RemovePlotDirectory_RPC rpc)
        {
            Task<Success_Response> data = Task.Run(() => RemovePlotDirectory_Async(rpc));
            data.Wait();
            return data.Result;
        }
    }
}
