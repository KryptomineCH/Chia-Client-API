using Chia_Client_API.Helpers_NS;
using CHIA_RPC.General_NS;
using CHIA_RPC.Wallet_NS.DLWallet_NS;
using System.Text.Json;

namespace Chia_Client_API.WalletAPI_NS
{
	public partial class Wallet_RPC_Client
	{
		/// <summary>
		/// Create a new DataLayer wallet
		/// </summary>
		/// <remarks><see href="https://docs.chia.net/wallet-rpc#create_new_dl"/></remarks>
		/// <param name="rpc"></param>
		/// <returns></returns>
		public async Task<CreateNewDL_Response?> CreateNewDL_Async(CreateNewDL_RPC rpc)
		{
			string responseJson = await SendCustomMessage_Async("create_new_dl", rpc.ToString());
			CreateNewDL_Response? deserializedObject = CreateNewDL_Response.LoadResponseFromString(responseJson);
            if (ReportResponseErrors && !(bool)deserializedObject.success)
            {
                await ReportError.UploadFileAsync(new Error(deserializedObject, "WalletApi_DataLayer", "create_new_dl"));
            }
            return deserializedObject;
		}
		/// <summary>
		/// Create a new DataLayer wallet
		/// </summary>
		/// <remarks><see href="https://docs.chia.net/wallet-rpc#create_new_dl"/></remarks>
		/// <param name="rpc"></param>
		/// <returns></returns>
		public CreateNewDL_Response? CreateNewDL_Sync(CreateNewDL_RPC rpc)
		{
			Task<CreateNewDL_Response?> data = Task.Run(() => CreateNewDL_Async(rpc));
			data.Wait();
			return data.Result;
		}

        /// <summary>
        /// Add a new on chain message for a specific singleton
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#dl_new_mirror"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public DlNewMirror_Response? DlNewMirror_Sync(DlNewMirror_RPC rpc)
        {
            Task<DlNewMirror_Response?> data = Task.Run(() => DlNewMirror_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Remove an existing mirror for a specific singleton
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#dl_delete_mirror"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<DlNewMirror_Response?> DlDeleteMirror_Async(DlDeleteMirror_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("dl_delete_mirror", rpc.ToString());
            DlNewMirror_Response? deserializedObject = DlNewMirror_Response.LoadResponseFromString(responseJson);
            if (ReportResponseErrors && !(bool)deserializedObject.success)
            {
                await ReportError.UploadFileAsync(new Error(deserializedObject, "WalletApi_DataLayer", "dl_delete_mirror"));
            }
            return deserializedObject;
        }

        /// <summary>
        /// Get all of the mirrors for a specific singleton
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#dl_get_mirrors"/></remarks>
        /// <returns></returns>
        public async Task<DlGetMirrors_Response?> DlGetMirrors_Async()
        {
            string responseJson = await SendCustomMessage_Async("dl_get_mirrors");
            DlGetMirrors_Response? deserializedObject = DlGetMirrors_Response.LoadResponseFromString(responseJson);
            if (ReportResponseErrors && !(bool)deserializedObject.success)
            {
                await ReportError.UploadFileAsync(new Error(deserializedObject, "WalletApi_DataLayer", "dl_get_mirrors"));
            }
            return deserializedObject;
        }
        /// <summary>
        /// Get all of the mirrors for a specific singleton
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#dl_get_mirrors"/></remarks>
        /// <returns></returns>
        public DlGetMirrors_Response? DlGetMirrors_Sync()
        {
            Task<DlGetMirrors_Response?> data = Task.Run(() => DlGetMirrors_Async());
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Show the history of a data store
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#dl_history"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<DlHistory_Response?> DlHistory_Async(DlHistory_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("dl_history", rpc.ToString());
            DlHistory_Response? deserializedObject = DlHistory_Response.LoadResponseFromString(responseJson);
            if (ReportResponseErrors && !(bool)deserializedObject.success)
            {
                await ReportError.UploadFileAsync(new Error(deserializedObject, "WalletApi_DataLayer", "dl_history"));
            }
            return deserializedObject;
        }
        /// <summary>
        /// Show the history of a data store
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#dl_history"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public DlHistory_Response? DlHistory_Sync(DlHistory_RPC rpc)
        {
            Task<DlHistory_Response?> data = Task.Run(() => DlHistory_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Get the singleton record for the latest singleton of a launcher ID
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#dl_latest_singleton"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<DlLatestSingleton_Response?> DlLatestSingleton_Async(DlLatestSingleton_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("dl_latest_singleton", rpc.ToString());
            DlLatestSingleton_Response? deserializedObject = DlLatestSingleton_Response.LoadResponseFromString(responseJson);
            if (ReportResponseErrors && !(bool)deserializedObject.success)
            {
                await ReportError.UploadFileAsync(new Error(deserializedObject, "WalletApi_DataLayer", "dl_latest_singleton"));
            }
            return deserializedObject;
        }
        /// <summary>
        /// Get the singleton record for the latest singleton of a launcher ID
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#dl_latest_singleton"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public DlLatestSingleton_Response? DlLatestSingleton_Sync(DlLatestSingleton_RPC rpc)
        {
            Task<DlLatestSingleton_Response?> data = Task.Run(() => DlLatestSingleton_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Add a new on chain message for a specific singleton
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#dl_new_mirror"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<DlNewMirror_Response?> DlNewMirror_Async(DlNewMirror_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("dl_new_mirror", rpc.ToString());
            DlNewMirror_Response? deserializedObject = DlNewMirror_Response.LoadResponseFromString(responseJson);
            if (ReportResponseErrors && !(bool)deserializedObject.success)
            {
                await ReportError.UploadFileAsync(new Error(deserializedObject, "WalletApi_DataLayer", "dl_new_mirror"));
            }
            return deserializedObject;
        }
        /// <summary>
        /// Remove an existing mirror for a specific singleton
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#dl_delete_mirror"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public DlNewMirror_Response? DlDeleteMirror_Sync(DlDeleteMirror_RPC rpc)
        {
            Task<DlNewMirror_Response?> data = Task.Run(() => DlDeleteMirror_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
		/// Get all owned singleton records
		/// </summary>
		/// <remarks><see href="https://docs.chia.net/wallet-rpc#dl_owned_singletons"/></remarks>
		/// <returns></returns>
		public async Task<DlOwnedSingletons_Response?> DlOwnedSingletons_Async()
        {
            string responseJson = await SendCustomMessage_Async("dl_owned_singletons");
            DlOwnedSingletons_Response? deserializedObject = DlOwnedSingletons_Response.LoadResponseFromString(responseJson);
            if (ReportResponseErrors && !(bool)deserializedObject.success)
            {
                await ReportError.UploadFileAsync(new Error(deserializedObject, "WalletApi_DataLayer", "dl_owned_singletons"));
            }
            return deserializedObject;
        }
        /// <summary>
        /// Get all owned singleton records
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#dl_owned_singletons"/></remarks>
        /// <returns></returns>
        public DlOwnedSingletons_Response? DlOwnedSingletons_Sync()
        {
            Task<DlOwnedSingletons_Response?> data = Task.Run(() => DlOwnedSingletons_Async());
            data.Wait();
            return data.Result;
        }

        /// <summary>
		/// Get the singleton records that contain the specified root
		/// </summary>
		/// <remarks><see href="https://docs.chia.net/wallet-rpc#dl_singletons_by_root"/></remarks>
		/// <param name="rpc"></param>
		/// <returns></returns>
		public async Task<DlSingletonsByRoot_Response?> DlSingletonsByRoot_Async(DlSingletonsByRoot_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("dl_singletons_by_root", rpc.ToString());
            DlSingletonsByRoot_Response? deserializedObject = DlSingletonsByRoot_Response.LoadResponseFromString(responseJson);
            if (ReportResponseErrors && !(bool)deserializedObject.success)
            {
                await ReportError.UploadFileAsync(new Error(deserializedObject, "WalletApi_DataLayer", "dl_singletons_by_root"));
            }
            return deserializedObject;
        }
        /// <summary>
        /// Get the singleton records that contain the specified root
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#dl_singletons_by_root"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public DlSingletonsByRoot_Response? DlSingletonsByRoot_Sync(DlSingletonsByRoot_RPC rpc)
        {
            Task<DlSingletonsByRoot_Response?> data = Task.Run(() => DlSingletonsByRoot_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
		/// Stop tracking a DataStore
		/// </summary>
		/// <remarks><see href="https://docs.chia.net/wallet-rpc#dl_stop_tracking"/></remarks>
		/// <param name="rpc"></param>
		/// <returns></returns>
		public async Task<Success_Response?> DlStopTracking_Async(LauncherID_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("dl_stop_tracking", rpc.ToString());
            Success_Response? deserializedObject = Success_Response.LoadResponseFromString(responseJson);
            if (ReportResponseErrors && !(bool)deserializedObject.success)
            {
                await ReportError.UploadFileAsync(new Error(deserializedObject, "WalletApi_DataLayer", "dl_stop_tracking"));
            }
            return deserializedObject;
        }
        /// <summary>
        /// Stop tracking a DataStore
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#dl_stop_tracking"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public Success_Response? DlStopTracking_Sync(LauncherID_RPC rpc)
        {
            Task<Success_Response?> data = Task.Run(() => DlStopTracking_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Begin tracking a DataStore
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#dl_track_new"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<Success_Response?> DlTrackNew_Async(LauncherID_RPC rpc)
		{
			string responseJson = await SendCustomMessage_Async("dl_track_new", rpc.ToString());
			Success_Response? deserializedObject = Success_Response.LoadResponseFromString(responseJson);
            if (ReportResponseErrors && !(bool)deserializedObject.success)
            {
                await ReportError.UploadFileAsync(new Error(deserializedObject, "WalletApi_DataLayer", "dl_track_new"));
            }
            return deserializedObject;
		}
		/// <summary>
		/// Begin tracking a DataStore
		/// </summary>
		/// <remarks><see href="https://docs.chia.net/wallet-rpc#dl_track_new"/></remarks>
		/// <param name="rpc"></param>
		/// <returns></returns>
		public Success_Response? DlTrackNew_Sync(LauncherID_RPC rpc)
		{
			Task<Success_Response?> data = Task.Run(() => DlTrackNew_Async(rpc));
			data.Wait();
			return data.Result;
		}

		/// <summary>
		/// Update the root of a data store to the given new root
		/// </summary>
		/// <remarks><see href="https://docs.chia.net/wallet-rpc#dl_update_root"/></remarks>
		/// <param name="rpc"></param>
		/// <returns></returns>
		public async Task<DlUpdateRoot_Response?> DlUpdateRoot_Async(DlUpdateRoot_RPC rpc)
		{
			string responseJson = await SendCustomMessage_Async("dl_update_root", rpc.ToString());
			DlUpdateRoot_Response? deserializedObject = DlUpdateRoot_Response.LoadResponseFromString(responseJson);
            if (ReportResponseErrors && !(bool)deserializedObject.success)
            {
                await ReportError.UploadFileAsync(new Error(deserializedObject, "WalletApi_DataLayer", "dl_update_root"));
            }
            return deserializedObject;
		}
		/// <summary>
		/// Update the root of a data store to the given new root
		/// </summary>
		/// <remarks><see href="https://docs.chia.net/wallet-rpc#dl_update_root"/></remarks>
		/// <param name="rpc"></param>
		/// <returns></returns>
		public DlUpdateRoot_Response? DlUpdateRoot_Sync(DlUpdateRoot_RPC rpc)
		{
			Task<DlUpdateRoot_Response?> data = Task.Run(() => DlUpdateRoot_Async(rpc));
			data.Wait();
			return data.Result;
		}
	}
}
