using CHIA_RPC.Daemon_NS.Plotter_NS;
using CHIA_RPC.Daemon_NS.Plotter_NS.PlotterObjects_NS;
using CHIA_RPC.Daemon_NS.Server_NS;
using CHIA_RPC.General_NS;

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
        // exit
        /// <summary>
        /// Stop the daemon and all of its running services
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#exit"/></remarks>
        /// <returns><see cref="Exit_Response"/></returns>
        public async Task<Exit_Response?> Exit_Async()
        {
            string responseJson = await SendCustomMessage_Async("exit");
            Exit_Response? deserializedObject = Exit_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Stop the daemon and all of its running services (Synchronous)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#exit"/></remarks>
        /// <returns><see cref="Exit_Response"/></returns>
        public Exit_Response? Exit_Sync()
        {
            Task<Exit_Response?> data = Task.Run(() => Exit_Async());
            data.Wait();
            return data.Result;
        }

        // get_routes
        /// <summary>
        /// List all available Daemon RPC routes
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#get_routes"/></remarks>
        /// <returns><see cref="GetRoutes_Response"/></returns>
        public async Task<GetRoutes_Response?> GetRoutes_Async()
        {
            string responseJson = await SendCustomMessage_Async("get_routes");
            GetRoutes_Response? deserializedObject = GetRoutes_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// List all available Daemon RPC routes (Synchronous)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#get_routes"/></remarks>
        /// <returns><see cref="GetRoutes_Response"/></returns>
        public GetRoutes_Response? GetRoutes_Sync()
        {
            Task<GetRoutes_Response?> data = Task.Run(() => GetRoutes_Async());
            data.Wait();
            return data.Result;
        }

        // get_status
        /// <summary>
        /// Show the status of the daemon
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#get_status"/></remarks>
        /// <returns><see cref="GetStatus_Response"/></returns>
        public async Task<GetStatus_Response?> GetStatus_Async()
        {
            string responseJson = await SendCustomMessage_Async("get_status");
            GetStatus_Response? deserializedObject = GetStatus_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Show the status of the daemon (Synchronous)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#get_status"/></remarks>
        /// <returns><see cref="GetStatus_Response"/></returns>
        public GetStatus_Response? GetStatus_Sync()
        {
            Task<GetStatus_Response?> data = Task.Run(() => GetStatus_Async());
            data.Wait();
            return data.Result;
        }

        // get_version
        /// <summary>
        /// Show the version of the daemon
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#get_version"/></remarks>
        /// <returns><see cref="GetVersion_Response"/></returns>
        public async Task<GetVersion_Response?> GetVersion_Async()
        {
            string responseJson = await SendCustomMessage_Async("get_version");
            GetVersion_Response? deserializedObject = GetVersion_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Show the version of the daemon (Synchronous)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#get_version"/></remarks>
        /// <returns><see cref="GetVersion_Response"/></returns>
        public GetVersion_Response? GetVersion_Sync()
        {
            Task<GetVersion_Response?> data = Task.Run(() => GetVersion_Async());
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
        public async Task<GetWalletAddresses_Response?> GetWalletAddresses_Async(GetWalletAddresses_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_wallet_addresses", rpc.ToString());
            GetWalletAddresses_Response? deserializedObject = GetWalletAddresses_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// List one or more addresses from one or more keys (Synchronous)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#get_wallet_addresses"/></remarks>
        /// <param name="rpc">Parameters for the request</param>
        /// <returns><see cref="GetWalletAddresses_Response"/></returns>
        public GetWalletAddresses_Response? GetWalletAddresses_Sync(GetWalletAddresses_RPC rpc)
        {
            Task<GetWalletAddresses_Response?> data = Task.Run(() => GetWalletAddresses_Async(rpc));
            data.Wait();
            return data.Result;
        }

        // is_keyring_locked
        /// <summary>
        /// Show whether the keyring is locked
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#is_keyring_locked"/></remarks>
        /// <returns><see cref="IsKeyringLocked_Response"/></returns>
        public async Task<IsKeyringLocked_Response?> IsKeyringLocked_Async()
        {
            string responseJson = await SendCustomMessage_Async("is_keyring_locked");
            IsKeyringLocked_Response? deserializedObject = IsKeyringLocked_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Show whether the keyring is locked (Synchronous)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#is_keyring_locked"/></remarks>
        /// <returns><see cref="IsKeyringLocked_Response"/></returns>
        public IsKeyringLocked_Response? IsKeyringLocked_Sync()
        {
            Task<IsKeyringLocked_Response?> data = Task.Run(() => IsKeyringLocked_Async());
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
        public async Task<IsRunning_Response?> IsRunning_Async(Service_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("is_running", rpc.ToString());
            IsRunning_Response? deserializedObject = IsRunning_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Show if a specific service is running (Synchronous)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#is_running"/></remarks>
        /// <param name="rpc">Parameters for the request</param>
        /// <returns><see cref="IsRunning_Response"/></returns>
        public IsRunning_Response? IsRunning_Sync(Service_RPC rpc)
        {
            Task<IsRunning_Response?> data = Task.Run(() => IsRunning_Async(rpc));
            data.Wait();
            return data.Result;
        }

        // keyring_status
        /// <summary>
        /// Show a snapshot of the keyring status
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#keyring_status"/></remarks>
        /// <returns><see cref="KeyringStatus_Response"/></returns>
        public async Task<KeyringStatus_Response?> KeyringStatus_Async()
        {
            string responseJson = await SendCustomMessage_Async("keyring_status");
            KeyringStatus_Response? deserializedObject = KeyringStatus_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Show a snapshot of the keyring status (Synchronous)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#keyring_status"/></remarks>
        /// <returns><see cref="KeyringStatus_Response"/></returns>
        public KeyringStatus_Response? KeyringStatus_Sync()
        {
            Task<KeyringStatus_Response?> data = Task.Run(() => KeyringStatus_Async());
            data.Wait();
            return data.Result;
        }

        // register_service
        /// <summary>
        /// Register a service
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#register_service"/></remarks>
        /// <returns><see cref="Success_Response"/></returns>
        public async Task<Success_Response?> RegisterService_Async(Service_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("register_service", rpc.ToString());
            Success_Response? deserializedObject = Success_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Register a service (Synchronous)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#register_service"/></remarks>
        /// <returns><see cref="Success_Response"/></returns>
        public Success_Response? RegisterService_Sync(Service_RPC rpc)
        {
            Task<Success_Response?> data = Task.Run(() => RegisterService_Async(rpc));
            data.Wait();
            return data.Result;
        }

        // remove_keyring_passphrase
        /// <summary>
        /// Remove a passphrase from the keyring
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#remove_keyring_passphrase"/></remarks>
        /// <returns><see cref="Success_Response"/></returns>
        public async Task<Success_Response?> RemoveKeyringPassphrase_Async(RemoveKeyringPassphrase_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("remove_keyring_passphrase", rpc.ToString());
            Success_Response? deserializedObject = Success_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Remove a passphrase from the keyring (Synchronous)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#remove_keyring_passphrase"/></remarks>
        /// <returns><see cref="Success_Response"/></returns>
        public Success_Response? RemoveKeyringPassphrase_Sync(RemoveKeyringPassphrase_RPC rpc)
        {
            Task<Success_Response?> data = Task.Run(() => RemoveKeyringPassphrase_Async(rpc));
            data.Wait();
            return data.Result;
        }

        // running_services
        /// <summary>
        /// Show all services that are currently running
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#running_services"/></remarks>
        /// <returns><see cref="RunningServices_Response"/></returns>
        public async Task<RunningServices_Response?> RunningServices_Async()
        {
            string responseJson = await SendCustomMessage_Async("running_services");
            RunningServices_Response? deserializedObject = RunningServices_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Show all services that are currently running (Synchronous)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#running_services"/></remarks>
        /// <returns><see cref="RunningServices_Response"/></returns>
        public RunningServices_Response? RunningServices_Sync()
        {
            Task<RunningServices_Response?> data = Task.Run(() => RunningServices_Async());
            data.Wait();
            return data.Result;
        }

        // set_keyring_passphrase
        /// <summary>
        /// Set keyring passphrase
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#set_keyring_passphrase"/></remarks>
        /// <returns><see cref="Success_Response"/></returns>
        public async Task<Success_Response?> SetKeyringPassphrase_Async(SetKeyringPassphrase_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("set_keyring_passphrase", rpc.ToString());
            Success_Response? deserializedObject = Success_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Set keyring passphrase (Synchronous)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#set_keyring_passphrase"/></remarks>
        /// <returns><see cref="Success_Response"/></returns>
        public Success_Response? SetKeyringPassphrase_Sync(SetKeyringPassphrase_RPC rpc)
        {
            Task<Success_Response?> data = Task.Run(() => SetKeyringPassphrase_Async(rpc));
            data.Wait();
            return data.Result;
        }

        // start_service
        /// <summary>
        /// Start a service
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#start_service"/></remarks>
        /// <returns><see cref="Service_Response"/></returns>
        public async Task<Service_Response?> StartService_Async(Service_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("start_service", rpc.ToString());
            Service_Response? deserializedObject = Service_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Start a service (Synchronous)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#start_service"/></remarks>
        /// <returns><see cref="Service_Response"/></returns>
        public Service_Response? StartService_Sync(Service_RPC rpc)
        {
            Task<Service_Response?> data = Task.Run(() => StartService_Async(rpc));
            data.Wait();
            return data.Result;
        }

        // stop_service
        /// <summary>
        /// Stop a service
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#stop_service"/></remarks>
        /// <returns><see cref="Service_Response"/></returns>
        public async Task<Service_Response?> StopService_Async(Service_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("stop_service", rpc.ToString());
            Service_Response? deserializedObject = Service_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Stop a service (Synchronous)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#stop_service"/></remarks>
        /// <returns><see cref="Service_Response"/></returns>
        public Service_Response? StopService_Sync(Service_RPC rpc)
        {
            Task<Service_Response?> data = Task.Run(() => StopService_Async(rpc));
            data.Wait();
            return data.Result;
        }

        // unlock_keyring
        /// <summary>
        /// Unlock the keyring
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#unlock_keyring"/></remarks>
        /// <returns><see cref="Success_Response"/></returns>
        public async Task<Success_Response?> UnlockKeyring_Async(Key_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("unlock_keyring", rpc.ToString());
            Success_Response? deserializedObject = Success_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Unlock the keyring (Synchronous)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#unlock_keyring"/></remarks>
        /// <returns><see cref="Success_Response"/></returns>
        public Success_Response? UnlockKeyring_Sync(Key_RPC rpc)
        {
            Task<Success_Response?> data = Task.Run(() => UnlockKeyring_Async(rpc));
            data.Wait();
            return data.Result;
        }

        // validate_keyring_passphrase
        /// <summary>
        /// Validate keyring passphrase
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#validate_keyring_passphrase"/></remarks>
        /// <returns><see cref="Success_Response"/></returns>
        public async Task<Success_Response?> ValidateKeyringPassphrase_Async(Key_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("validate_keyring_passphrase", rpc.ToString());
            Success_Response? deserializedObject = Success_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Validate keyring passphrase (Synchronous)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/daemon-rpc#validate_keyring_passphrase"/></remarks>
        /// <param name="rpc">Parameters for the request</param>
        /// <returns><see cref="Success_Response"/></returns>
        public Success_Response? ValidateKeyringPassphrase_Sync(Key_RPC rpc)
        {
            Task<Success_Response?> data = Task.Run(() => ValidateKeyringPassphrase_Async(rpc));
            data.Wait();
            return data.Result;
        }
    }
}
