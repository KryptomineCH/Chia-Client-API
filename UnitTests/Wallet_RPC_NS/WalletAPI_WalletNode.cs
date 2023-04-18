using CHIA_API_Tests.Initialisation_NS;
using Chia_Client_API.WalletAPI_NS;
using CHIA_RPC.General_NS;
using CHIA_RPC.Wallet_NS.WalletNode_NS;
using Xunit;

namespace CHIA_API_Tests.Wallet_RPC_NS
{
    [Collection("Testnet_Wallet")]
    public class WalletAPI_WalletNode
    {
        [Fact]
        public void GetNetworkInfo_Test()
        {
            GetNetworkInfo_Response info = Testnet_Wallet.Wallet_Client.GetNetworkInfo_Async().Result;
            if (!info.success) throw new System.Exception("api call was uncucessful!");
            if (!info.network_name.StartsWith("testnet")) throw new System.Exception("warning: we are not in testnet!");
            if (info.network_prefix != "txch") throw new System.Exception("network prefix does not ");
        }
        [Fact]
        public void GetSyncStatus_Test()
        {
            GetWalletSyncStatus_Response info = Testnet_Wallet.Wallet_Client.GetSyncStatus_Async().Result;
            if (!info.success) throw new System.Exception("api call was uncucessful!");
            if (!info.genesis_initialized) throw new System.Exception("genesis is not initialized!");
            if (info.synced && info.syncing) throw new System.Exception("cannot be synced and syncing at the same time!");
            if (!info.synced && !info.syncing) throw new System.Exception("does not seem to be syncing!");
        }
        [Fact]
        public void GetHeightInfo_Test()
        {
            GetHeightInfo_Response info = Testnet_Wallet.Wallet_Client.GetHeightInfo_Async().Result;
            if (!info.success) throw new System.Exception("api call was uncucessful!");
            if (info.height <= 0) throw new System.Exception("height seems inplausible!");
            { }
        }
    }
}
