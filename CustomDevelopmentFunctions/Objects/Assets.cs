using CHIA_RPC.General_NS;
using CHIA_RPC.Wallet_NS.CATsAndTrading_NS;
using CHIA_RPC.Wallet_NS.WalletManagement_NS;

namespace TransactionTypeTests.Objects
{
    internal class AssetWrapper
    {
        internal static Dictionary<string, Asset> Assets = new Dictionary<string, Asset>
        {
            { "xch", new Asset() {AssetType = AssetType.XCH, AssetID = "1" }},
            {"btf", new Asset() {AssetType = AssetType.CAT, AssetID = "fbaac130d4fe250cd5896e1106d810246feb41a07638e108881fd89f2cf19a74" }},
            {"tdbx", new Asset() {AssetType = AssetType.CAT, AssetID = "d82dd03f8a9ad2f84353cd953c4de6b21dbaaf7de3ba3f4ddd9abe31ecba80ad" }},
            {"tcred",new Asset() {AssetType = AssetType.CAT, AssetID = "e3becac2ebef17570cf29af1e25afefafbe63f6daea017ba411bf6537c902477" }}
        };
        internal static WalletID_RPC GetAssetWallet(Asset asset, FingerPrint_RPC fingerprint)
        {
            if (asset.AssetID == "1") return new WalletID_RPC(1);
            GetWallets_Response walletsResponse = Clients.Wallet_Client.GetWallets_Sync(new GetWallets_RPC(include_data: true));
            foreach (Wallets_info wallet in walletsResponse.wallets)
            {
                if (asset.AssetType == AssetType.NFT && wallet.type == CHIA_RPC.Objects_NS.WalletType.NFT)
                {
                    return wallet.GetWalletID_RPC();
                }
                if (asset.AssetType == AssetType.CAT && wallet.type == CHIA_RPC.Objects_NS.WalletType.CAT)
                {
                    CatGetAssetId_Response walletAssetId = Clients.Wallet_Client.CatGetAssetID_Sync(wallet.GetWalletID_RPC());
                    if (walletAssetId.asset_id == asset.AssetID)
                    {
                        return wallet.GetWalletID_RPC();
                    }
                }

            }
            throw new InvalidOperationException($"could not identify wallet for asset {asset.AssetType} with id {asset.AssetID}");
        }

    }

    internal struct Asset
    {
        public AssetType AssetType;
        public string AssetID;
    }
    internal enum AssetType
    {
        XCH,
        CAT,
        NFT
    }

}