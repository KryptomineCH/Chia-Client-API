using Chia_Client_API.Helpers_NS;
using CHIA_RPC.Daemon_NS.Server_NS;
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
    public abstract partial class DaemonRPCBase
    {
        // exit
        /// <summary>
        /// Stop the daemon and all of its running services
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#exit"/></remarks>
        /// <returns><see cref="Exit_Response"/></returns>
        public async Task<Exit_Response> Exit_Async()
        {
            string responseJson = await SendCustomMessageAsync("exit");
            ActionResult<Exit_Response> deserializationResult = Exit_Response.LoadResponseFromString(responseJson);
            Exit_Response response = new Exit_Response();
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
                    await ReportError.UploadFileAsync(new Error(response, "Daemon", "exit"));
                }
            }
            response.RawContent = deserializationResult.RawJson;
            return response;
        }

        /// <summary>
        /// Stop the daemon and all of its running services (Synchronous)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#exit"/></remarks>
        /// <returns><see cref="Exit_Response"/></returns>
        public Exit_Response Exit_Sync()
        {
            Task<Exit_Response> data = Task.Run(() => Exit_Async());
            data.Wait();
            return data.Result;
        }

        // get_routes
        /// <summary>
        /// List all available Daemon RPC routes
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#get_routes"/></remarks>
        /// <returns><see cref="GetRoutes_Response"/></returns>
        public async Task<GetRoutes_Response> GetRoutes_Async()
        {
            string responseJson = await SendCustomMessageAsync("get_routes");
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
                if (ReportResponseErrors && !(bool)response.success)
                {
                    await ReportError.UploadFileAsync(new Error(response, "Daemon", "get_routes"));
                }
            }
            response.RawContent = deserializationResult.RawJson;
            return response;
        }

        /// <summary>
        /// List all available Daemon RPC routes (Synchronous)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#get_routes"/></remarks>
        /// <returns><see cref="GetRoutes_Response"/></returns>
        public GetRoutes_Response GetRoutes_Sync()
        {
            Task<GetRoutes_Response> data = Task.Run(() => GetRoutes_Async());
            data.Wait();
            return data.Result;
        }

        // get_status
        /// <summary>
        /// Show the status of the daemon
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#get_status"/></remarks>
        /// <returns><see cref="GetStatus_Response"/></returns>
        public async Task<GetStatus_Response> GetStatus_Async()
        {
            string responseJson = await SendCustomMessageAsync("get_status");
            ActionResult<GetStatus_Response> deserializationResult = GetStatus_Response.LoadResponseFromString(responseJson);
            GetStatus_Response response = new GetStatus_Response();
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
                    await ReportError.UploadFileAsync(new Error(response, "Daemon", "get_status"));
                }
            }
            response.RawContent = deserializationResult.RawJson;
            return response;
        }

        /// <summary>
        /// Show the status of the daemon (Synchronous)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#get_status"/></remarks>
        /// <returns><see cref="GetStatus_Response"/></returns>
        public GetStatus_Response GetStatus_Sync()
        {
            Task<GetStatus_Response> data = Task.Run(() => GetStatus_Async());
            data.Wait();
            return data.Result;
        }

        // get_version
        /// <summary>
        /// Show the version of the daemon
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#get_version"/></remarks>
        /// <returns><see cref="GetVersion_Response"/></returns>
        public async Task<GetVersion_Response> GetVersion_Async()
        {
            string responseJson = await SendCustomMessageAsync("get_version");
            ActionResult<GetVersion_Response> deserializationResult = GetVersion_Response.LoadResponseFromString(responseJson);
            GetVersion_Response response = new GetVersion_Response();
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
                    await ReportError.UploadFileAsync(new Error(response, "Daemon", "get_version"));
                }
            }
            response.RawContent = deserializationResult.RawJson;
            return response;
        }

        /// <summary>
        /// Show the version of the daemon (Synchronous)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#get_version"/></remarks>
        /// <returns><see cref="GetVersion_Response"/></returns>
        public GetVersion_Response GetVersion_Sync()
        {
            Task<GetVersion_Response> data = Task.Run(() => GetVersion_Async());
            data.Wait();
            return data.Result;
        }

        // get_wallet_addresses
        /// <summary>
        /// List one or more addresses from one or more keys
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#get_wallet_addresses"/></remarks>
        /// <param name="rpc">Parameters for the request</param>
        /// <returns><see cref="GetWalletAddresses_Response"/></returns>
        public async Task<GetWalletAddresses_Response> GetWalletAddresses_Async(GetWalletAddresses_RPC rpc)
        {
            string responseJson = await SendCustomMessageAsync("get_wallet_addresses", rpc.ToString());
            ActionResult<GetWalletAddresses_Response> deserializationResult = GetWalletAddresses_Response.LoadResponseFromString(responseJson);
            GetWalletAddresses_Response response = new GetWalletAddresses_Response();
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
                    await ReportError.UploadFileAsync(new Error(response, "Daemon", "get_wallet_addresses"));
                }
            }
            response.RawContent = deserializationResult.RawJson;
            return response;
        }

        /// <summary>
        /// List one or more addresses from one or more keys (Synchronous)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#get_wallet_addresses"/></remarks>
        /// <param name="rpc">Parameters for the request</param>
        /// <returns><see cref="GetWalletAddresses_Response"/></returns>
        public GetWalletAddresses_Response GetWalletAddresses_Sync(GetWalletAddresses_RPC rpc)
        {
            Task<GetWalletAddresses_Response> data = Task.Run(() => GetWalletAddresses_Async(rpc));
            data.Wait();
            return data.Result;
        }

        // is_keyring_locked
        /// <summary>
        /// Show whether the keyring is locked
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#is_keyring_locked"/></remarks>
        /// <returns><see cref="IsKeyringLocked_Response"/></returns>
        public async Task<IsKeyringLocked_Response> IsKeyringLocked_Async()
        {
            string responseJson = await SendCustomMessageAsync("is_keyring_locked");
            ActionResult<IsKeyringLocked_Response> deserializationResult = IsKeyringLocked_Response.LoadResponseFromString(responseJson);
            IsKeyringLocked_Response response = new IsKeyringLocked_Response();
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
        /// Show whether the keyring is locked (Synchronous)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#is_keyring_locked"/></remarks>
        /// <returns><see cref="IsKeyringLocked_Response"/></returns>
        public IsKeyringLocked_Response IsKeyringLocked_Sync()
        {
            Task<IsKeyringLocked_Response> data = Task.Run(() => IsKeyringLocked_Async());
            data.Wait();
            return data.Result;
        }

        // is_running
        /// <summary>
        /// Show if a specific service is running
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#is_running"/></remarks>
        /// <param name="rpc">Parameters for the request</param>
        /// <returns><see cref="IsRunning_Response"/></returns>
        public async Task<IsRunning_Response> IsRunning_Async(Service_RPC rpc)
        {
            string responseJson = await SendCustomMessageAsync("is_running", rpc.ToString());
            ActionResult<IsRunning_Response> deserializationResult = IsRunning_Response.LoadResponseFromString(responseJson);
            IsRunning_Response response = new IsRunning_Response();
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
                    await ReportError.UploadFileAsync(new Error(response, "Daemon", "is_running"));
                }
            }
            response.RawContent = deserializationResult.RawJson;
            return response;
        }

        /// <summary>
        /// Show if a specific service is running (Synchronous)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#is_running"/></remarks>
        /// <param name="rpc">Parameters for the request</param>
        /// <returns><see cref="IsRunning_Response"/></returns>
        public IsRunning_Response IsRunning_Sync(Service_RPC rpc)
        {
            Task<IsRunning_Response> data = Task.Run(() => IsRunning_Async(rpc));
            data.Wait();
            return data.Result;
        }

        // keyring_status
        /// <summary>
        /// Show a snapshot of the keyring status
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#keyring_status"/></remarks>
        /// <returns><see cref="KeyringStatus_Response"/></returns>
        public async Task<KeyringStatus_Response> KeyringStatus_Async()
        {
            string responseJson = await SendCustomMessageAsync("keyring_status");
            ActionResult<KeyringStatus_Response> deserializationResult = KeyringStatus_Response.LoadResponseFromString(responseJson);
            KeyringStatus_Response response = new KeyringStatus_Response();
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
        /// Show a snapshot of the keyring status (Synchronous)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#keyring_status"/></remarks>
        /// <returns><see cref="KeyringStatus_Response"/></returns>
        public KeyringStatus_Response KeyringStatus_Sync()
        {
            Task<KeyringStatus_Response> data = Task.Run(() => KeyringStatus_Async());
            data.Wait();
            return data.Result;
        }

        // register_service
        /// <summary>
        /// Register a service
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#register_service"/></remarks>
        /// <returns><see cref="Success_Response"/></returns>
        public async Task<Success_Response> RegisterService_Async(Service_RPC rpc)
        {
            string responseJson = await SendCustomMessageAsync("register_service", rpc.ToString());
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
                    await ReportError.UploadFileAsync(new Error(response, "Daemon", "register_service"));
                }
            }
            response.RawContent = deserializationResult.RawJson;
            return response;
        }

        /// <summary>
        /// Register a service (Synchronous)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#register_service"/></remarks>
        /// <returns><see cref="Success_Response"/></returns>
        public Success_Response RegisterService_Sync(Service_RPC rpc)
        {
            Task<Success_Response> data = Task.Run(() => RegisterService_Async(rpc));
            data.Wait();
            return data.Result;
        }

        // remove_keyring_passphrase
        /// <summary>
        /// Remove a passphrase from the keyring
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#remove_keyring_passphrase"/></remarks>
        /// <returns><see cref="Success_Response"/></returns>
        public async Task<Success_Response> RemoveKeyringPassphrase_Async(RemoveKeyringPassphrase_RPC rpc)
        {
            string responseJson = await SendCustomMessageAsync("remove_keyring_passphrase", rpc.ToString());
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
            }
            response.RawContent = deserializationResult.RawJson;
            return response;
        }

        /// <summary>
        /// Remove a passphrase from the keyring (Synchronous)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#remove_keyring_passphrase"/></remarks>
        /// <returns><see cref="Success_Response"/></returns>
        public Success_Response RemoveKeyringPassphrase_Sync(RemoveKeyringPassphrase_RPC rpc)
        {
            Task<Success_Response> data = Task.Run(() => RemoveKeyringPassphrase_Async(rpc));
            data.Wait();
            return data.Result;
        }

        // running_services
        /// <summary>
        /// Show all services that are currently running
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#running_services"/></remarks>
        /// <returns><see cref="RunningServices_Response"/></returns>
        public async Task<RunningServices_Response> RunningServices_Async()
        {
            string responseJson = await SendCustomMessageAsync("running_services");
            ActionResult<RunningServices_Response> deserializationResult = RunningServices_Response.LoadResponseFromString(responseJson);
            RunningServices_Response response = new RunningServices_Response();
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
                    await ReportError.UploadFileAsync(new Error(response, "Daemon", "running_services"));
                }
            }
            response.RawContent = deserializationResult.RawJson;
            return response;
        }

        /// <summary>
        /// Show all services that are currently running (Synchronous)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#running_services"/></remarks>
        /// <returns><see cref="RunningServices_Response"/></returns>
        public RunningServices_Response RunningServices_Sync()
        {
            Task<RunningServices_Response> data = Task.Run(() => RunningServices_Async());
            data.Wait();
            return data.Result;
        }

        // set_keyring_passphrase
        /// <summary>
        /// Set keyring passphrase
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#set_keyring_passphrase"/></remarks>
        /// <returns><see cref="Success_Response"/></returns>
        public async Task<Success_Response> SetKeyringPassphrase_Async(SetKeyringPassphrase_RPC rpc)
        {
            string responseJson = await SendCustomMessageAsync("set_keyring_passphrase", rpc.ToString());
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
            }
            response.RawContent = deserializationResult.RawJson;
            return response;
        }

        /// <summary>
        /// Set keyring passphrase (Synchronous)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#set_keyring_passphrase"/></remarks>
        /// <returns><see cref="Success_Response"/></returns>
        public Success_Response SetKeyringPassphrase_Sync(SetKeyringPassphrase_RPC rpc)
        {
            Task<Success_Response> data = Task.Run(() => SetKeyringPassphrase_Async(rpc));
            data.Wait();
            return data.Result;
        }

        // start_service
        /// <summary>
        /// Start a service
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#start_service"/></remarks>
        /// <returns><see cref="Service_Response"/></returns>
        public async Task<Service_Response> StartService_Async(Service_RPC rpc)
        {
            string responseJson = await SendCustomMessageAsync("start_service", rpc.ToString());
            ActionResult<Service_Response> deserializationResult = Service_Response.LoadResponseFromString(responseJson);
            Service_Response response = new Service_Response();
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
                    await ReportError.UploadFileAsync(new Error(response, "Daemon", "start_service"));
                }
            }
            response.RawContent = deserializationResult.RawJson;
            return response;
        }

        /// <summary>
        /// Start a service (Synchronous)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#start_service"/></remarks>
        /// <returns><see cref="Service_Response"/></returns>
        public Service_Response StartService_Sync(Service_RPC rpc)
        {
            Task<Service_Response> data = Task.Run(() => StartService_Async(rpc));
            data.Wait();
            return data.Result;
        }

        // stop_service
        /// <summary>
        /// Stop a service
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#stop_service"/></remarks>
        /// <returns><see cref="Service_Response"/></returns>
        public async Task<Service_Response> StopService_Async(Service_RPC rpc)
        {
            string responseJson = await SendCustomMessageAsync("stop_service", rpc.ToString());
            ActionResult<Service_Response> deserializationResult = Service_Response.LoadResponseFromString(responseJson);
            Service_Response response = new Service_Response();
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
                    await ReportError.UploadFileAsync(new Error(response, "Daemon", "stop_service"));
                }
            }
            response.RawContent = deserializationResult.RawJson;
            return response;
        }

        /// <summary>
        /// Stop a service (Synchronous)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#stop_service"/></remarks>
        /// <returns><see cref="Service_Response"/></returns>
        public Service_Response StopService_Sync(Service_RPC rpc)
        {
            Task<Service_Response> data = Task.Run(() => StopService_Async(rpc));
            data.Wait();
            return data.Result;
        }

        // unlock_keyring
        /// <summary>
        /// Unlock the keyring
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#unlock_keyring"/></remarks>
        /// <returns><see cref="Success_Response"/></returns>
        public async Task<Success_Response> UnlockKeyring_Async(Key_RPC rpc)
        {
            string responseJson = await SendCustomMessageAsync("unlock_keyring", rpc.ToString());
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
            }
            response.RawContent = deserializationResult.RawJson;
            return response;
        }

        /// <summary>
        /// Unlock the keyring (Synchronous)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#unlock_keyring"/></remarks>
        /// <returns><see cref="Success_Response"/></returns>
        public Success_Response UnlockKeyring_Sync(Key_RPC rpc)
        {
            Task<Success_Response> data = Task.Run(() => UnlockKeyring_Async(rpc));
            data.Wait();
            return data.Result;
        }

        // validate_keyring_passphrase
        /// <summary>
        /// Validate keyring passphrase
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#validate_keyring_passphrase"/></remarks>
        /// <returns><see cref="Success_Response"/></returns>
        public async Task<Success_Response> ValidateKeyringPassphrase_Async(Key_RPC rpc)
        {
            string responseJson = await SendCustomMessageAsync("validate_keyring_passphrase", rpc.ToString());
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
            }
            response.RawContent = deserializationResult.RawJson;
            return response;
        }

        /// <summary>
        /// Validate keyring passphrase (Synchronous)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#validate_keyring_passphrase"/></remarks>
        /// <param name="rpc">Parameters for the request</param>
        /// <returns><see cref="Success_Response"/></returns>
        public Success_Response ValidateKeyringPassphrase_Sync(Key_RPC rpc)
        {
            Task<Success_Response> data = Task.Run(() => ValidateKeyringPassphrase_Async(rpc));
            data.Wait();
            return data.Result;
        }
    }
}
