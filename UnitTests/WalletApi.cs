using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class WalletApi
    {
        [Fact]
        public void GetAllWallets()
        {
            Chia_Client_API.Wallet_NS.WalletAPI_NS.WalletApi.GetPublicKeys().Wait();
            { }
        }
    }
}