using System.Collections.Concurrent;
using Chia_Client_API.WalletAPI_NS;
using CHIA_RPC.General_NS;
using CHIA_RPC.Wallet_NS.CATsAndTrading_NS;

namespace Chia_Client_API.Helpers_NS
{

    /// <summary>
    /// useful for storing and obtaining asset ids by wallets fast.<br/>
    /// Note that this is caching. You need to make sure to clear this class or makei a new instance when you switch wallets
    /// </summary>
    public class AssetIDCache
    {
        /// <summary>
        /// contains the raw cache
        /// </summary>
        ConcurrentDictionary<ulong, string> AssetIDs = new ConcurrentDictionary<ulong, string>();
        /// <summary>
        /// useful for storing and obtaining asset ids by wallets fast.
        /// Note that this is caching. You need to make sure to clear this class or makei a new instance when you switch wallets
        /// </summary>
        /// <param name="walletID">The wallet ID to look the asset ID up for</param>
        /// <param name="assetID">The asset ID which is beeing returned.<br/>
        /// note: in case of client error, the error will be filled here. MUST CHECK!</param>
        /// <param name="client">The Client which to query if the asset ID is not in cache</param>
        /// <returns>if the lookup was successful</returns>
        public bool GetOrObtainAssetID(ulong walletID, out string? assetID, Wallet_RPC_Client client)
        {
            if (walletID == 1)
            {
                assetID = "1";
                return true;
            }
            if (AssetIDs.TryGetValue(walletID, out assetID))
            {
                return true;
            }
            CatGetAssetId_Response? assetInfo = client.CatGetAssetID_Sync(new WalletID_RPC(walletID));
            if (assetInfo == null) return false;
            if (assetInfo.success ?? false == false || !string.IsNullOrEmpty(assetInfo.error) || string.IsNullOrEmpty(assetInfo.asset_id))
            {
                assetID = assetInfo.error;
                return false;
            }
            assetID = assetInfo.asset_id;
            AssetIDs[walletID] = assetInfo.asset_id!;
            return true;
        }
        /// <summary>
        /// Clears the cache
        /// </summary>
        public void Clear()
        {
            this.AssetIDs.Clear();
        }
    }
}
