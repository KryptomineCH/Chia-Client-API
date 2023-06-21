using CHIA_API_Tests.Initialisation_NS;
using CHIA_RPC.General_NS;
using CHIA_RPC.Objects_NS;
using CHIA_RPC.Wallet_NS.CATsAndTrading_NS;
using CHIA_RPC.Wallet_NS.Offer_NS;
using CHIA_RPC.Wallet_NS.WalletManagement_NS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CHIA_API_Tests.Wallet_NS.CatsAndTrading_NS
{
    [Collection("Testnet_Wallet")]
    public class CreateCatOffer
    {
        [Fact]
        public void CreateCatOffer_Test()
        {
            // get all wallets
            GetWallets_Response wallets = Testnet_Wallet.Wallet_Client.GetWallets_Sync(includeData: true);
            // get apropriate wallet
            foreach (Wallets_info wallet in wallets.wallets)
            {
                if (wallet.name == "BTF-TEST")
                {
                    CatGetAssetId_Response assetId = Testnet_Wallet.Wallet_Client.CatGetAssetID_Sync(new WalletID_RPC(wallet.id));
                    CreateOfferForIds_RPC offer_rpc = new CreateOfferForIds_RPC();
                    offer_rpc.offer.Add("1", -50000);
                    offer_rpc.offer.Add(assetId.asset_id, 500);
                    OfferFile offer = Testnet_Wallet.Wallet_Client.CreateOfferForIds_Sync(offer_rpc);
                    offer.Export("btftestoffer");

                    Testnet_Wallet.Wallet_Client.CancelOffers_Sync(new CancelCatOffers_RPC(secure: true, cancel_all: true, batch_fee: 100000));
                    { }
                }
            }
        }
    }
}
