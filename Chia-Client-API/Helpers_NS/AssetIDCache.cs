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
        /// sets the wallet client to which to attach to
        /// </summary>
        /// <remarks>Switching wallet does not clear the cache automatically!</remarks>
        /// <param name="client"></param>
        public AssetIDCache(Wallet_RPC_Client client)
        {
            Client = client;
        }
        /// <summary>
        /// the client from which to pull Data if nessesary
        /// </summary>
        public Wallet_RPC_Client Client { get; set; }
        /// <summary>
        /// contains the raw cache
        /// </summary>
        public ConcurrentDictionary<ulong, string> AssetIDs = new ConcurrentDictionary<ulong, string>();
        /// <summary>
        /// useful for storing and obtaining asset ids by wallets fast.
        /// </summary>
        /// <remarks>
        /// IMPORTANT: Note that this is caching. You need to make sure to clear this class or make a new instance when you switch wallets or resync!
        /// </remarks>
        /// <param name="walletID">The wallet ID to look the asset ID up for</param>
        /// <param name="assetID">The asset ID which is beeing returned.<br/>
        /// note: in case of client error, the error will be filled here. MUST CHECK!</param>
        /// <param name="client">The Client which to query if the asset ID is not in cache</param>
        /// <returns>if the lookup was successful</returns>
        public bool GetOrObtainAssetID(ulong walletID, out string? assetID)
        {
            if (walletID == 1)
            {
                assetID = "xch";
                return true;
            }
            if (AssetIDs.TryGetValue(walletID, out assetID))
            {
                return true;
            }
            CatGetAssetId_Response? assetInfo = Client.CatGetAssetID_Sync(new WalletID_RPC(walletID));
            if (assetInfo == null)
            {
                assetID = "assetInfo was null!";
                return false;
            }
            if (!(bool)assetInfo.success || !string.IsNullOrEmpty(assetInfo.error) || string.IsNullOrEmpty(assetInfo.asset_id))
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
