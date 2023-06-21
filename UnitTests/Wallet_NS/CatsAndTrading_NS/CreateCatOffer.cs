using CHIA_API_Tests.Initialisation_NS;
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
                    { }
                }
            }
        }
    }
}
