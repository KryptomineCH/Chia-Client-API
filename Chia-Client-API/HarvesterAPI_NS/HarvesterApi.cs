using CHIA_RPC.General_NS;
using CHIA_RPC.Harvester_NS;
using System.Text.Json;

namespace Chia_Client_API.HarvesterAPI_NS
{
    public partial class Harvester_RPC_Client
    {
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
            GetPlots_Response deserializedObject = JsonSerializer.Deserialize<GetPlots_Response>(responseJson);
            return deserializedObject;
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
        /// Refresh all plots from the harvester<br/><br/>
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/harvester-rpc#refresh_plots"/></remarks>
        /// <returns></returns>
        public async Task<Success_Response> RefreshPlots_Async()
        {
            string responseJson = await SendCustomMessage_Async("refresh_plots");
            Success_Response deserializedObject = JsonSerializer.Deserialize<Success_Response>(responseJson);
            return deserializedObject;
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
        /// Delete a single plot<br/><br/>
        /// IMPORTANT: As long as this command includes the required filename flag, it will always output "success": true, even if the filename was invalid
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/harvester-rpc#delete_plot"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<Success_Response> DeletePlot_Async(DeletePlot_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("delete_plot", rpc.ToString());
            Success_Response deserializedObject = JsonSerializer.Deserialize<Success_Response>(responseJson);
            return deserializedObject;
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
        /// Add a new plot directory<br/><br/>
        /// IMPORTANT: Note that the new directory must already exist on the system
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/harvester-rpc#add_plot_directory"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<Success_Response> AddPlotDirectory_Async(AddPlotDirectory_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("add_plot_directory", rpc.ToString());
            Success_Response deserializedObject = JsonSerializer.Deserialize<Success_Response>(responseJson);
            return deserializedObject;
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
        /// List all plot directories
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/harvester-rpc#get_plot_directories"/></remarks>
        /// <returns></returns>
        public async Task<GetPlotDirectories_Response> GetPlotDirectories_Async()
        {
            string responseJson = await SendCustomMessage_Async("get_plot_directories");
            GetPlotDirectories_Response deserializedObject = JsonSerializer.Deserialize<GetPlotDirectories_Response>(responseJson);
            return deserializedObject;
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
        /// Remove a directory from the harvester's list of plot directories<br/><br/>
        /// IMPORTANT: As long as this command includes the required dirname flag, it will always output "success": true, even if the dirname is not in the directory list
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/harvester-rpc#remove_plot_directory"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<Success_Response> RemovePlotDirectory_Async(RemovePlotDirectory_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("remove_plot_directory", rpc.ToString());
            Success_Response deserializedObject = JsonSerializer.Deserialize<Success_Response>(responseJson);
            return deserializedObject;
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
        /// <summary>
        /// List all available RPC routes
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/harvester-rpc#get_routes"/></remarks>
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
        /// <remarks><see href="https://docs.chia.net/harvester-rpc#get_routes"/></remarks>
        /// <returns></returns>
        public GetRoutes_Response GetRoutes_Sync()
        {
            Task<GetRoutes_Response> data = Task.Run(() => GetRoutes_Async());
            data.Wait();
            return data.Result;
        }
    }
}
