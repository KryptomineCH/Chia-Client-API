using Chia_Client_API.Wallet_NS.WalletAPI_NS;
using CHIA_RPC.General;
using CHIA_RPC.Wallet_RPC_NS.NFT;
using Xunit;

namespace UnitTests.Wallet_RPC_NS
{
    [Collection("Testnet")]
    public class WalletAPI_NFT
    {
        [Fact]
        public void GetNFTs_Test()
        {
            string didWallet = CommonTestFunctions.TestDidWallet.name;
            WalletID_Response nftWallet = WalletApi.NftGetByDID(new DidID_RPC { did_id = didWallet }).Result;
            { }
        }
        [Fact]
        public void MintNft()
        {
            NftMintNFT_RPC rpc = new NftMintNFT_RPC(
                walletID: CommonTestFunctions.TestNftWallet.id,
                nftLinks: new[] { 
                    "https://bafybeih5423cflj3jpo7qzs65ozgs2jtdcf4wkvdga5d2ht57wzyeufq3q.ipfs.nftstorage.link/",
                    "https://nft.kryptomine.ch/kryptomine_testcollection/final/god_under_the_shower.png"},
                metadataLinks: new[] {
                    "https://bafkreifvwkzpf7unauzpcnydnmfu2jwhuhj3kyhgliyps44oipw3ebbqpm.ipfs.nftstorage.link/",
                    "https://nft.kryptomine.ch/kryptomine_testcollection/metadata/god_under_the_shower.json"
                }, 
                licenseLinks: new[]
                {
                    "https://bafkreicc3peq64kpclsssu344iroadtsvbloo7ofbkzdyyrqhybhvmblve.ipfs.nftstorage.link/",
                    "https://bafkreicc3peq64kpclsssu344iroadtsvbloo7ofbkzdyyrqhybhvmblve.ipfs.nftstorage.link/"
                },
                royaltyFee: 190,
                royaltyAddress: CommonTestFunctions.TestAdress,
                mintingFee_Mojos: 0);
            NftMintNFT_Response response = WalletApi.NftMintNft(rpc).Result;
            { }
        }
    }
}
