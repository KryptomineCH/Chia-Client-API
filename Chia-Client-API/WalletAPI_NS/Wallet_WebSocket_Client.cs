﻿using Chia_Client_API.ChiaClient_NS;
using CHIA_RPC.Daemon_NS.Server_NS.ServerObjects_NS;
using CHIA_RPC.General_NS;
using CHIA_RPC.Wallet_NS.WalletNode_NS;

namespace Chia_Client_API.WalletAPI_NS
{
    /// <summary>
    /// NOTE: set `daemon_allow_tls_1_2: True`  in chia config!
    /// </summary>
    public class WalletWebSocketClient : WalletRpcBase
    {
        private WebsocketClient _WebSocketClient;
        /// <summary>
        /// specifies if errors in the response should be reported to kryptomine.ch for api improvements.
        /// </summary>
        /// <remarks>Reported data is anonymous</remarks>
        public bool ReportResponseErrors { get; set; }
        /// <summary>
        /// Initializes a new instance of the Wallet_RPC_Client class that can be used to interact with a Chia wallet.<br/><br/>
        /// NOTE: set daemon_allow_tls_1_2: True  in chia config!
        /// </summary>
        /// <param name="reportResponseErrors">sends the following information with asymmetric rsa 4096 and AES encryption to kryptomine.ch for improving the API:<br/>
        /// - ChiaVersion<br/>
        /// - ApiVersion<br/>
        /// - RpcVersion<br/>
        /// - ErrorTime<br/>
        /// - ErrorText<br/>
        /// - RawServerResponse<br/>
        /// Preferably use on testnet or with a test wallet<br/>
        /// Any responses with potential key / authentication information are omitted</param>
        /// <param name="targetApiAddress">The IP address of the Chia wallet's RPC server. Defaults to 'localhost'.</param>
        /// <param name="targetApiPort">The port number of the Chia wallet's RPC server. Defaults to 9256.</param>
        /// <param name="targetCertificateBaseFolder">The base directory for the SSL/TLS certificate. This certificate is used to establish a secure connection with the Chia wallet's RPC server. If null, the default certificate location will be used.</param>
        /// <param name="timeout">the timeout after which the client cancels requests</param>
        /// <remarks>
        /// This constructor configures the connection details for the Chia wallet's RPC server. It is important to ensure the correctness of these details for successful communication with the wallet. 
        /// The 'localhost' default for the targetApiAddress parameter is suitable for scenarios where the wallet and the application are running on the same machine. 
        /// For remote wallets, provide the appropriate IP address.
        /// The default targetApiPort is the default port number where the Chia Wallet RPC server is configured to listen for incoming requests.
        /// The targetCertificateBaseFolder parameter needs to point to a directory containing a valid SSL/TLS certificate. This is required to establish a secure (HTTPS) connection to the wallet's RPC server. If left null, it assumes the certificate is in the default location.
        /// </remarks>
        public WalletWebSocketClient(
            bool reportResponseErrors, 
            string targetApiAddress = "localhost", int targetApiPort = 55400, 
            string? targetCertificateBaseFolder = null, 
            TimeSpan? timeout = null)
        {
            ReportResponseErrors = reportResponseErrors;
            _WebSocketClient = new WebsocketClient(
                CHIA_RPC.General_NS.Endpoint.wallet,
                ChiaService.wallet_ui,
                targetApiAddress, targetApiPort, 
                targetCertificateBaseFolder, 
                timeout);
        }
        /// <summary>
        /// Asynchronously sends a custom message to the daemon API.
        /// </summary>
        /// <param name="function">The RPC function name.</param>
        /// <param name="json">The JSON string to send.</param>
        /// <returns>A Task that represents the asynchronous send operation, yielding the response string.</returns>
        public async override Task<string> SendCustomMessageAsync(string function, string json = " { } ")
        {
            return await _WebSocketClient.SendCustomMessageAsync(function, json);
        }

        /// <summaWallry>
        /// Synchronously sends a custom message to the daemon API.
        /// </summary>
        /// <param name="function">The RPC function name.</param>
        /// <param name="json">The JSON string to send.</param>
        /// <returns>The response string from the daemon API.</returns>
        public override string SendCustomMessageSync(string function, string json = " { } ")
        {
            return _WebSocketClient.SendCustomMessageSync(function, json);
        }
        
        /// <summary>
        /// waits until the wallet is fully synced with the blockchain
        /// </summary>
        /// <param name="cancellation"></param>
        /// <param name="timeoutSeconds"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> AwaitWalletSync_Async(CancellationToken cancellation,
            double timeoutSeconds = 60.0)
        {
            TimeSpan timeOut = TimeSpan.FromSeconds(timeoutSeconds);
            DateTime timeOutTracker = DateTime.Now;
            bool success = false;
            while (!cancellation.IsCancellationRequested)
            {
                GetWalletSyncStatus_Response? syncStatus = await GetSyncStatus_Async();
                if (syncStatus == null || !(syncStatus.success ?? false))
                {
                    if (syncStatus != null && syncStatus.error == "wallet state manager not assigned")
                    {
                        throw new Exception("no wallet is logged in!");
                    }
                    throw new Exception("Error while obtaining sync status!");
                }
                else if (!syncStatus.syncing && !syncStatus.synced)
                {
                    // not synced and not syncing, check timeout
                    if (timeOutTracker + timeOut < DateTime.Now)
                    {
                        return false;
                    }
                    // timeout is not met, wait for the wallet to start syncing
                }
                else if ((syncStatus.success ?? false) && (syncStatus.syncing || syncStatus.synced))
                {
                    // currently syncing, so we want to extend the timeout
                    timeOutTracker = DateTime.Now;
                }
                if (syncStatus.synced) return true;
                await Task.Delay(1000); // wait 1 second
            }
            return success;
        }
        /// <summary>
        /// waits until the wallet is fully synced with the blockchain
        /// </summary>
        /// <param name="timeoutSeconds"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool AwaitWalletSync_Sync(double timeoutSeconds = 60.0)
        {
            Task<bool> data = Task.Run(() => AwaitWalletSync_Async(CancellationToken.None, timeoutSeconds));
            data.Wait();
            return data.Result;
        }
    }
}
