using CHIA_RPC.General_NS;
using Xunit;
using System.Threading.Tasks;
using CHIA_API_Tests.Initialisation_NS;

namespace CHIA_API_Tests.Wallet_NS
{
    [Collection("Testnet_Wallet")]
    public class WalletAPI_Wallet_CustomFunctions
    {
        [Fact]
        public async Task SeekBlockTransactionIndex_FindsFirstTransaction_Success()
        {
            // Arrange
            ulong block = 12345; // Example block number

            // Act
            ulong resultIndex = await Testnet_Wallet.Wallet_Client.SeekBlockTransactionIndex(block, 1);

            // Assert
            Assert.NotEqual(ulong.MaxValue, resultIndex); // Assuming ulong.MaxValue means 'not found'
            // Additional assertions as needed
        }

    }
}
