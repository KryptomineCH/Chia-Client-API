using CHIA_API_Tests.Initialisation_NS;
using CHIA_RPC.FullNode_NS;
using CHIA_RPC.General_NS;
using CHIA_RPC.Objects_NS;
using CHIA_RPC.Wallet_NS.NFT_NS;
using CHIA_RPC.Wallet_NS.Offer_NS;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CHIA_API_Tests.Wallet_NS
{
    [Collection("Testnet_Wallet")]
    public class WalletAPI_NFT
    {
        [Fact]
        public void NftMintNft_SaveLoadRPC()
        {
            NftMintNFT_RPC rpc = new NftMintNFT_RPC(
                walletID: 8,
                nftLinks: new[] 
                { 
                    "https://nft.kryptomine.ch/Kryptomine_Basecollection/KryptoMine_Logo.png",
                    "https://bafybeicwmyirgdb7cul5jy66rls73ww4fhyb5y7y6qfjjz54qtvepfausu.ipfs.nftstorage.link/"
                },
                metadataLinks: new[] 
                {
                    "https://nft.kryptomine.ch/Kryptomine_Basecollection/KryptoMine_Logo.json",
                    "https://bafkreidycvdxqbnsevhsngzq2gkbo6sf6hhwvqsnkpkyufvkdjfxol2q5y.ipfs.nftstorage.link/"
                },
                licenseLinks: new[] 
                {
                    "https://nft.kryptomine.ch/Kryptomine_Basecollection/KryptoMine_Logo_License.txt",
                    "https://bafkreigj4vrboii67oj2ygmgrjzz4bmig5vfnzlxwfzbkxajhumk7qro2q.ipfs.nftstorage.link/"
                },
                royaltyFee: 1234567890,
                royaltyAddress: "xch1e426jf55z7npqnw7ae7h9ap0gez7wrljvqqeskx97p928jshfapq2w7p5l",
                targetAddress: "xch1e426jf55z7npqnw7ae7h9ap0gez7wrljvqqeskx97p928jshfapq2w7p5l",
                mintingFee_Mojos: 2441556
                );
            rpc.SaveRpcToFile("NftMintNft_SaveLoadRPC");
            NftMintNFT_RPC? loadedRpc = NftMintNFT_RPC.LoadRpcFromFile("NftMintNft_SaveLoadRPC");
            if (loadedRpc == null)
            {
                throw new NullReferenceException("NftMintNft_SaveLoadRPC could not be loaded!");
            }
            if (rpc.wallet_id != loadedRpc.wallet_id)
            {
                throw new System.Exception("Wallet Ids do not match!");
            }
            // check if uris match

            if (rpc.uris != null && loadedRpc.uris != null)
            {
                bool urismatch = rpc.uris.Count == loadedRpc.uris.Count;

                if (urismatch)
                {
                    for (int i = 0; i < rpc.uris.Count; i++)
                    {
                        if (rpc.uris[i] != loadedRpc.uris[i])
                        {
                            urismatch = false;
                            break;
                        }
                    }
                }
                if (!urismatch)
                {
                    throw new System.Exception("uris dont match!");
                }
            }
            else if (!(rpc.uris == null && loadedRpc.uris == null))
            {
                throw new System.Exception("uris dont match!");
            }
            // check if metadata uris match
            bool metaUriMatch = true;
            if ((rpc.meta_uris == null || loadedRpc.meta_uris == null) && (rpc.meta_uris != null || loadedRpc.meta_uris != null))
            {
                // one meta is null, the other isnt!
                metaUriMatch = false;
            }
            if (metaUriMatch && rpc.meta_uris != null && loadedRpc.meta_uris != null)
            {
                // meta != null
                metaUriMatch = rpc.meta_uris.Count == loadedRpc.meta_uris.Count;
                if (metaUriMatch)
                {
                    for (int i = 0; i < rpc.meta_uris.Count; i++)
                    {
                        if (rpc.meta_uris[i] != loadedRpc.meta_uris[i])
                        {
                            metaUriMatch = false;
                            break;
                        }
                    }
                }
            }
            if (!metaUriMatch)
            {
                throw new System.Exception("metaUri dont match!");
            }
            // check if license uris match
            bool licenseUriMatch = true;
            if ((rpc.license_uris == null || loadedRpc.license_uris == null) && (rpc.license_uris != null || loadedRpc.license_uris != null))
            {
                // one meta is null, the other isnt!
                licenseUriMatch = false;
            }
            if (licenseUriMatch && rpc.license_uris != null && loadedRpc.license_uris != null)
            {
                // meta != null
                licenseUriMatch = rpc.license_uris.Count == loadedRpc.license_uris.Count;
                if (licenseUriMatch)
                {
                    for (int i = 0; i < rpc.license_uris.Count; i++)
                    {
                        if (rpc.license_uris[i] != loadedRpc.license_uris[i])
                        {
                            licenseUriMatch = false;
                            break;
                        }
                    }
                }
            }
            if (licenseUriMatch)
            {
                throw new System.Exception("licenseUri dont match!");
            }
            // check hashes
            if (rpc.hash != loadedRpc.hash || rpc.meta_hash != loadedRpc.meta_hash || rpc.license_hash != loadedRpc.license_hash)
            {
                throw new System.Exception("uris dont match!");
            }
            if (rpc.royalty_address != loadedRpc.royalty_address || rpc.target_address != loadedRpc.target_address)
            {
                throw new System.Exception("addresses dont match!");
            }
            if (rpc.edition_number != loadedRpc.edition_number || rpc.edition_total != loadedRpc.edition_total)
            {
                throw new System.Exception("edition numbers do not match!");
            }
            if (rpc.fee != loadedRpc.fee || rpc.royalty_percentage != loadedRpc.royalty_percentage)
            {
                throw new System.Exception("fees dont match!");
            }
        }
        [Fact]
        public void GetNftInfo()
        {
            Assert.NotNull(CommonTestFunctions.TestDidWallet);
            Assert.NotNull(CommonTestFunctions.TestDidWallet!.name);
            string didWallet = CommonTestFunctions.TestDidWallet.name!;
            WalletID_Response? nftWallet = Testnet_Wallet.Wallet_Client.NftGetByDID_Async(new DidID_RPC { did_id = didWallet }).Result;
            Assert.NotNull(nftWallet);
            NftGetInfo_Response? info = Testnet_Wallet.Wallet_Client.NftGetInfo_Async(new NftGetInfo_RPC
            {
                wallet_id= nftWallet!.wallet_id,
                coin_id = "0x76ccbfe8323435d23d37cb7e2f09c5b6a0fdaa372bd5601635f4a0d1e01777a7"
            }).Result;
            { }
        }
        [Fact]
        public void GetNFTs_Test()
        {
            Assert.NotNull(CommonTestFunctions.TestDidWallet);
            Assert.NotNull(CommonTestFunctions.TestDidWallet!.name);
            string didWallet = CommonTestFunctions.TestDidWallet.name!;
            WalletID_Response? nftWallet = Testnet_Wallet.Wallet_Client.NftGetByDID_Async(new DidID_RPC { did_id = didWallet }).Result;
            Assert.NotNull(nftWallet);
            NftGetNfts_Response? response = Testnet_Wallet.Wallet_Client.NftGetNfts_Async(
                new WalletID_RPC { wallet_id = nftWallet!.wallet_id }).Result;
            { }
        }

        [Fact]
        public async void MintNft()
        {
            Assert.NotNull(CommonTestFunctions.TestNftWallet);
            NftMintNFT_RPC rpc = new NftMintNFT_RPC(
                walletID: CommonTestFunctions.TestNftWallet!.id,
                nftLinks: new[]
                {
                    "https://bafybeih5423cflj3jpo7qzs65ozgs2jtdcf4wkvdga5d2ht57wzyeufq3q.ipfs.nftstorage.link/",
                    "https://nft.kryptomine.ch/kryptomine_testcollection/final/god_under_the_shower.png"
                },
                metadataLinks: new[]
                {
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
                mintingFee_Mojos: 10000);
            NftMintNFT_Response mintNftResponse  =
                await Testnet_Wallet.Wallet_Client.NftMintNft_Async(rpc).ConfigureAwait(false);
            Assert.NotNull(mintNftResponse);
            Assert.True(mintNftResponse.success);
            NftGetInfo_Response nftMintInfo = await Testnet_Wallet.Wallet_Client
                .NftAwaitMintComplete_Async(mintNftResponse, cancel: System.Threading.CancellationToken.None,
                    refreshInterwallSeconds: 15).ConfigureAwait(false);
            Assert.NotNull(nftMintInfo);
            Assert.True(nftMintInfo.success);

            Offer_RPC offer = new Offer_RPC();
            offer.offer.Add("1", 1); // 1 mojo
            OfferFile? offerFile = Testnet_Wallet.Wallet_Client.CreateOfferForIds_Sync(offer);
            Assert.NotNull(offerFile);
            offerFile!.SaveObjectToFile("testoffer");
        }

        [Fact]
        public void TestGetCoinInfo()
        {
            GetCoinRecordByName_RPC rpc = new GetCoinRecordByName_RPC()
            {
                name = "0x2d858b0476d8972a023265ab4bfd362b894ac82c4465e77556e320def8d5c932"
            };
            GetCoinRecordByName_Response? response = Testnet_FullNode.Fullnode_Client.GetCoinRecordByName_Async(rpc).Result;
            Assert.NotNull(response);
            if (!(response!.success ?? false))
            {
                throw new System.Exception(response.error);
            }
        }
        [Fact]
        public void TestGetCoinInfo2()
        {
            GetCoinRecordsByNames_RPC rpc = new GetCoinRecordsByNames_RPC()
            {
                names = new[] { "0x2d858b0476d8972a023265ab4bfd362b894ac82c4465e77556e320def8d5c932" },
                include_spent_coins = true,
            };
            GetCoinRecords_Response? response = Testnet_FullNode.Fullnode_Client.GetCoinRecordsByNames_Async(rpc).Result;
            Assert.NotNull(response);
            GetCoinRecords_Response? success2 = Testnet_Wallet.Wallet_Client.GetCoinRecordsByNames_Async(rpc).Result;
            if (!(response!.success ?? false))
            {
                throw new System.Exception(response.error);
            }
        }
    }
}
