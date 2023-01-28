using CHIA_RPC.Wallet_RPC_NS.Wallet_NS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Wallet_RPC_NS
{
    public class WalletApi_Offer
    {
        [Fact]
        public void TestOffer()
        {
            Offer_RPC test = new Offer_RPC();
            test.offer.Add("3", 1);
            test.offer.Add("4", -1);
            string json = test.ToString();
            { }
        }
    }
}
