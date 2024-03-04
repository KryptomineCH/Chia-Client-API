using Chia_Client_API.Helpers_NS;
using CHIA_RPC.General_NS;
using CHIA_RPC.HelperFunctions_NS;
using CHIA_RPC.Wallet_NS.DLWallet_NS;
using System.Text.Json;
using Chia_Client_API.ChiaClient_NS;

namespace Chia_Client_API.WalletAPI_NS
{
    public abstract partial class WalletRpcBase : ChiaEndpointRouteBase
	{
        /// <summary>
        /// Create a new DataLayer wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#create_new_dl"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<CreateNewDL_Response> CreateNewDL_Async(CreateNewDL_RPC rpc)
        {
            string responseJson = await SendCustomMessageAsync("create_new_dl", rpc.ToString());
            ActionResult<CreateNewDL_Response> deserializationResult = CreateNewDL_Response.LoadResponseFromString(responseJson,IncludeRawServerResponse);
            CreateNewDL_Response response = new CreateNewDL_Response();

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
                    await ReportError.UploadFileAsync(new Error(response, "WalletApi_DataLayer", "create_new_dl"));
                }
            }
            return response;
        }

        /// <summary>
        /// Create a new DataLayer wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#create_new_dl"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public CreateNewDL_Response CreateNewDL_Sync(CreateNewDL_RPC rpc)
		{
			Task<CreateNewDL_Response> data = Task.Run(() => CreateNewDL_Async(rpc));
			data.Wait();
			return data.Result;
		}

        /// <summary>
        /// Add a new on chain message for a specific singleton
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#dl_new_mirror"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<DlNewMirror_Response> DlNewMirror_Async(DlNewMirror_RPC rpc)
        {
            string responseJson = await SendCustomMessageAsync("dl_new_mirror", rpc.ToString());
            ActionResult<DlNewMirror_Response> deserializationResult = DlNewMirror_Response.LoadResponseFromString(responseJson,IncludeRawServerResponse);
            DlNewMirror_Response response = new DlNewMirror_Response();

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
                    await ReportError.UploadFileAsync(new Error(response, "WalletApi_DataLayer", "dl_new_mirror"));
                }
            }
            return response;
        }

        /// <summary>
        /// Add a new on chain message for a specific singleton
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#dl_new_mirror"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public DlNewMirror_Response DlNewMirror_Sync(DlNewMirror_RPC rpc)
        {
            Task<DlNewMirror_Response> data = Task.Run(() => DlNewMirror_Async(rpc));
            data.Wait();
            return data.Result;
        }
        

        /// <summary>
        /// Get all of the mirrors for a specific singleton
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#dl_get_mirrors"/></remarks>
        /// <returns></returns>
        public async Task<DlGetMirrors_Response> DlGetMirrors_Async()
        {
            string responseJson = await SendCustomMessageAsync("dl_get_mirrors");
            ActionResult<DlGetMirrors_Response> deserializationResult = DlGetMirrors_Response.LoadResponseFromString(responseJson,IncludeRawServerResponse);
            DlGetMirrors_Response response = new DlGetMirrors_Response();

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
                    await ReportError.UploadFileAsync(new Error(response, "WalletApi_DataLayer", "dl_get_mirrors"));
                }
            }
            return response;
        }

        /// <summary>
        /// Get all of the mirrors for a specific singleton
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#dl_get_mirrors"/></remarks>
        /// <returns></returns>
        public DlGetMirrors_Response DlGetMirrors_Sync()
        {
            Task<DlGetMirrors_Response> data = Task.Run(() => DlGetMirrors_Async());
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Show the history of a data store
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#dl_history"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<DlHistory_Response> DlHistory_Async(DlHistory_RPC rpc)
        {
            string responseJson = await SendCustomMessageAsync("dl_history", rpc.ToString());
            ActionResult<DlHistory_Response> deserializationResult = DlHistory_Response.LoadResponseFromString(responseJson,IncludeRawServerResponse);
            DlHistory_Response response = new DlHistory_Response();

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
                    await ReportError.UploadFileAsync(new Error(response, "WalletApi_DataLayer", "dl_history"));
                }
            }
            return response;
        }

        /// <summary>
        /// Show the history of a data store
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#dl_history"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public DlHistory_Response DlHistory_Sync(DlHistory_RPC rpc)
        {
            Task<DlHistory_Response> data = Task.Run(() => DlHistory_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Get the singleton record for the latest singleton of a launcher ID
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#dl_latest_singleton"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<DlLatestSingleton_Response> DlLatestSingleton_Async(DlLatestSingleton_RPC rpc)
        {
            string responseJson = await SendCustomMessageAsync("dl_latest_singleton", rpc.ToString());
            ActionResult<DlLatestSingleton_Response> deserializationResult = DlLatestSingleton_Response.LoadResponseFromString(responseJson,IncludeRawServerResponse);
            DlLatestSingleton_Response response = new DlLatestSingleton_Response();

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
                    await ReportError.UploadFileAsync(new Error(response, "WalletApi_DataLayer", "dl_latest_singleton"));
                }
            }
            return response;
        }

        /// <summary>
        /// Get the singleton record for the latest singleton of a launcher ID
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#dl_latest_singleton"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public DlLatestSingleton_Response DlLatestSingleton_Sync(DlLatestSingleton_RPC rpc)
        {
            Task<DlLatestSingleton_Response> data = Task.Run(() => DlLatestSingleton_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Remove an existing mirror for a specific singleton
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#dl_delete_mirror"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<DlNewMirror_Response> DlDeleteMirror_Async(DlDeleteMirror_RPC rpc)
        {
            string responseJson = await SendCustomMessageAsync("dl_delete_mirror", rpc.ToString());
            ActionResult<DlNewMirror_Response> deserializationResult = DlNewMirror_Response.LoadResponseFromString(responseJson,IncludeRawServerResponse);
            DlNewMirror_Response response = new DlNewMirror_Response();

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
                    await ReportError.UploadFileAsync(new Error(response, "WalletApi_DataLayer", "dl_delete_mirror"));
                }
            }
            return response;
        }

        /// <summary>
        /// Remove an existing mirror for a specific singleton
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#dl_delete_mirror"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public DlNewMirror_Response DlDeleteMirror_Sync(DlDeleteMirror_RPC rpc)
        {
            Task<DlNewMirror_Response> data = Task.Run(() => DlDeleteMirror_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
		/// Get all owned singleton records
		/// </summary>
		/// <remarks><see href="https://docs.chia.net/wallet-rpc#dl_owned_singletons"/></remarks>
		/// <returns></returns>
		public async Task<DlOwnedSingletons_Response> DlOwnedSingletons_Async()
        {
            string responseJson = await SendCustomMessageAsync("dl_owned_singletons");
            ActionResult<DlOwnedSingletons_Response> deserializationResult = DlOwnedSingletons_Response.LoadResponseFromString(responseJson,IncludeRawServerResponse);
            DlOwnedSingletons_Response response = new DlOwnedSingletons_Response();

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
                    await ReportError.UploadFileAsync(new Error(response, "WalletApi_DataLayer", "dl_owned_singletons"));
                }
            }
            return response;
        }

        /// <summary>
        /// Get all owned singleton records
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#dl_owned_singletons"/></remarks>
        /// <returns></returns>
        public DlOwnedSingletons_Response DlOwnedSingletons_Sync()
        {
            Task<DlOwnedSingletons_Response> data = Task.Run(() => DlOwnedSingletons_Async());
            data.Wait();
            return data.Result;
        }

        /// <summary>
		/// Get the singleton records that contain the specified root
		/// </summary>
		/// <remarks><see href="https://docs.chia.net/wallet-rpc#dl_singletons_by_root"/></remarks>
		/// <param name="rpc"></param>
		/// <returns></returns>
		public async Task<DlSingletonsByRoot_Response> DlSingletonsByRoot_Async(DlSingletonsByRoot_RPC rpc)
        {
            string responseJson = await SendCustomMessageAsync("dl_singletons_by_root", rpc.ToString());
            ActionResult<DlSingletonsByRoot_Response> deserializationResult = DlSingletonsByRoot_Response.LoadResponseFromString(responseJson,IncludeRawServerResponse);
            DlSingletonsByRoot_Response response = new DlSingletonsByRoot_Response();

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
                    await ReportError.UploadFileAsync(new Error(response, "WalletApi_DataLayer", "dl_singletons_by_root"));
                }
            }
            return response;
        }

        /// <summary>
        /// Get the singleton records that contain the specified root
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#dl_singletons_by_root"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public DlSingletonsByRoot_Response DlSingletonsByRoot_Sync(DlSingletonsByRoot_RPC rpc)
        {
            Task<DlSingletonsByRoot_Response> data = Task.Run(() => DlSingletonsByRoot_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
		/// Stop tracking a DataStore
		/// </summary>
		/// <remarks><see href="https://docs.chia.net/wallet-rpc#dl_stop_tracking"/></remarks>
		/// <param name="rpc"></param>
		/// <returns></returns>
		public async Task<Success_Response> DlStopTracking_Async(LauncherID_RPC rpc)
        {
            string responseJson = await SendCustomMessageAsync("dl_stop_tracking", rpc.ToString());
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
                response.RawContent = deserializationResult.RawJson;
                if (ReportResponseErrors && !(bool)response.success)
                {
                    await ReportError.UploadFileAsync(new Error(response, "WalletApi_DataLayer", "dl_stop_tracking"));
                }
            }
            return response;
        }

        /// <summary>
        /// Stop tracking a DataStore
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#dl_stop_tracking"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public Success_Response DlStopTracking_Sync(LauncherID_RPC rpc)
        {
            Task<Success_Response> data = Task.Run(() => DlStopTracking_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Begin tracking a DataStore
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#dl_track_new"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<Success_Response> DlTrackNew_Async(LauncherID_RPC rpc)
        {
            string responseJson = await SendCustomMessageAsync("dl_track_new", rpc.ToString());
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
                response.RawContent = deserializationResult.RawJson;
                if (ReportResponseErrors && !(bool)response.success)
                {
                    await ReportError.UploadFileAsync(new Error(response, "WalletApi_DataLayer", "dl_track_new"));
                }
            }
            return response;
        }

        /// <summary>
        /// Begin tracking a DataStore
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#dl_track_new"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public Success_Response DlTrackNew_Sync(LauncherID_RPC rpc)
		{
			Task<Success_Response> data = Task.Run(() => DlTrackNew_Async(rpc));
			data.Wait();
			return data.Result;
		}

        /// <summary>
        /// Update the root of a data store to the given new root
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#dl_update_root"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<DlUpdateRoot_Response> DlUpdateRoot_Async(DlUpdateRoot_RPC rpc)
        {
            string responseJson = await SendCustomMessageAsync("dl_update_root", rpc.ToString());
            ActionResult<DlUpdateRoot_Response> deserializationResult = DlUpdateRoot_Response.LoadResponseFromString(responseJson,IncludeRawServerResponse);
            DlUpdateRoot_Response response = new DlUpdateRoot_Response();

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
                    await ReportError.UploadFileAsync(new Error(response, "WalletApi_DataLayer", "dl_update_root"));
                }
            }
            return response;
        }

        /// <summary>
        /// Update the root of a data store to the given new root
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#dl_update_root"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public DlUpdateRoot_Response DlUpdateRoot_Sync(DlUpdateRoot_RPC rpc)
		{
			Task<DlUpdateRoot_Response> data = Task.Run(() => DlUpdateRoot_Async(rpc));
			data.Wait();
			return data.Result;
		}
	}
}
