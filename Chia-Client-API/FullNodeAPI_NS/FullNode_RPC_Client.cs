using Chia_Client_API.ChiaClient_NS;
using CHIA_RPC.General_NS;

namespace Chia_Client_API.FullNodeAPI_NS
{
    public partial class FullNodeRpcClient : FullNodeRpcBase
    {
        private RpcClient _rpcClient;
        /// <summary>
        /// Initializes a new instance of the full node_RPC_Client class that can be used to interact with a Chia full node.
        /// </summary>
        /// <param name="reportResponseErrors">sends the following information with asymmetric rsa 4096 and AES encryption to kryptomine.ch for improving the API:<br/>
        /// - ChiaVersion<br/>
        /// - ApiVersion<br/>
        /// - RpcVersion<br/>
        /// - ErrorTime<br/>
        /// - ErrorText<br/>
        /// - RawServerResponse<br/>
        /// Preferably use on testnet or with a test full node<br/>
        /// Any responses with potential key / authentication information are omitted</param>
        /// <param name="targetApiAddress">The IP address of the Chia full node's RPC server. Defaults to 'localhost'.</param>
        /// <param name="targetApiPort">The port number of the Chia full node's RPC server. Defaults to 8555.</param>
        /// <param name="targetCertificateBaseFolder">The base directory for the SSL/TLS certificate. This certificate is used to establish a secure connection with the Chia full node's RPC server. If null, the default certificate location will be used.</param>
        /// <param name="timeout">the timeout after which the client cancels requests</param>
        /// <remarks>
        /// This constructor configures the connection details for the Chia full node's RPC server. It is important to ensure the correctness of these details for successful communication with the full node. 
        /// The 'localhost' default for the targetApiAddress parameter is suitable for scenarios where the full node and the application are running on the same machine. 
        /// For remote full nodes, provide the appropriate IP address.
        /// The default targetApiPort is the default port number where the Chia full node RPC server is configured to listen for incoming requests.
        /// The targetCertificateBaseFolder parameter needs to point to a directory containing a valid SSL/TLS certificate. This is required to establish a secure (HTTPS) connection to the full node's RPC server. If left null, it assumes the certificate is in the default location.
        /// </remarks>
        public FullNodeRpcClient(
            bool reportResponseErrors, 
            string targetApiAddress = "localhost", int targetApiPort = 8555, 
            string? targetCertificateBaseFolder = null, 
            TimeSpan? timeout = null)
        {
            ReportResponseErrors = reportResponseErrors;
            _rpcClient = new RpcClient(
                Endpoint.full_node,
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
            return await _rpcClient.SendCustomMessageAsync(function, json);
        }

        /// <summary>
        /// Synchronously sends a custom message to the daemon API.
        /// </summary>
        /// <param name="function">The RPC function name.</param>
        /// <param name="json">The JSON string to send.</param>
        /// <returns>The response string from the daemon API.</returns>
        public override string SendCustomMessageSync(string function, string json = " { } ")
        {
            return _rpcClient.SendCustomMessageSync(function, json);
        }
        // todo: add full node sync await function
    }
}
