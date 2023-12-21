using Chia_Client_API.DaemonAPI_NS;
using CHIA_RPC.Daemon_NS.Server_NS;
using CHIA_RPC.HelperFunctions_NS;

namespace Chia_Client_API.Helpers_NS
{
    public class Error
    {
        public Error(IResponseTemplate response, string endpoint, string function, DaemonRpcClient? client = null)
        {
            ApiVersion = GlobalVar.PackageVersion;
            RpcVersion = CHIA_RPC.General_NS.GlobalVar.PackageVersion;
            ErrorTime = DateTime.Now;
            ErrorText = response.error;
            Endpoint = endpoint;
            Function = function;
            RawServerResponse = response.RawContent;
            if (client != null)
            {
                try
                {
                    GetVersion_Response version = client.GetVersion_Sync();
                    if ((bool)version.success)
                    {
                        ChiaVersion = version.version;
                    }
                }
                catch { }
            }
        }
        public string? ChiaVersion { get; set; }
        public Version ApiVersion { get; set; }
        public Version RpcVersion { get; set; }
        public DateTime ErrorTime { get; set; }
        public string Endpoint { get; set; }
        public string Function { get; set; }
        public string? ErrorText { get; set; }
        public string? RawServerResponse { get; set; }
    }
}
