using CHIA_RPC.FullNode_NS;
using CHIA_RPC.General_NS;
using CHIA_RPC.Wallet_NS.NFT_NS;
using System.Text.Json;

namespace Chia_Client_API.WalletAPI_NS
{
    public partial class Wallet_RPC_Client
    {
        /// <summary>
        /// This function is used to check if an NFT minting operation has completed successfully. 
        /// The function repeatedly checks the status of the minting operation by calling 
        /// the GetCoinRecordsByNames_Async method and comparing it to the original mintCoinInfo. <br/>
        /// If the minting is successful or the timeout is reached, 
        /// the function will return a boolean indicating the success or failure of the operation.
        /// </summary>
        /// <param name="nftMint">The responseJson from the NFT minting operation</param>
        /// <param name="cancel">CancellationToken used to cancel the operation if necessary</param>
        /// <param name="timeOutInMinutes">Timeout for the operation in minutes</param>
        /// <returns>A boolean indicating the success or failure of the operation</returns>
        public async Task<NftGetInfo_Response> NftAwaitMintComplete_Async(
            NftMintNFT_Response nftMint, CancellationToken cancel, double timeOutInMinutes = 15.0, int refreshInterwallSeconds = 60)
        {
            // set timeout
            DateTime timeOut = DateTime.Now + TimeSpan.FromMinutes(timeOutInMinutes);
            // obtain request data to validate transaction
            string coinID = nftMint.spend_bundle.coin_solutions[0].coin.GetCoinID();
            
            GetCoinRecordsByNames_RPC rpc = new GetCoinRecordsByNames_RPC()
            {
                names = new[] { coinID },
                include_spent_coins = true
            };
            GetCoinRecordByName_RPC rpc2 = new GetCoinRecordByName_RPC()
            {
                name = coinID
            };
            // check if mint has been completed sucessfully
            NftGetInfo_Response nftInfo = new NftGetInfo_Response();
            while (!cancel.IsCancellationRequested && DateTime.Now < timeOut)
            {
                nftInfo = await VerifyMint(nftMint).ConfigureAwait(false);
                
                if (nftInfo.success)
                {
                    return nftInfo;
                }
                //rpc.start_height = heightInfo.height;
                await Task.Delay(refreshInterwallSeconds * 1000);
            }
            return nftInfo;
        }
        /// <summary>
        /// This function is used to check if an NFT minting operation has completed successfully. 
        /// The function repeatedly checks the status of the minting operation by calling 
        /// the GetCoinRecordsByNames_Async method and comparing it to the original mintCoinInfo. <br/>
        /// If the minting is successful or the timeout is reached, 
        /// the function will return a boolean indicating the success or failure of the operation.
        /// </summary>
        /// <param name="nftMint">The responseJson from the NFT minting operation</param>
        /// <param name="cancel">CancellationToken used to cancel the operation if necessary</param>
        /// <param name="timeOutInMinutes">Timeout for the operation in minutes</param>
        /// <returns>A boolean indicating the success or failure of the operation</returns>
        public async Task<NftGetInfo_Response> NftAwaitMintComplete_Sync(
            NftMintNFT_Response nftMint, CancellationToken cancel, double timeOutInMinutes = 15.0)
        {
            Task<NftGetInfo_Response> data = Task.Run(() => NftAwaitMintComplete_Async(nftMint, cancel, timeOutInMinutes));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// this function takes a spend bundle, which is returned from NftMintNFT.
        /// it converts the coin into a coin ID which can be used to draft an NftGetInfo_Requests.
        /// </summary>
        /// <param name="nftMint">spend bundle, which is returned from NftMintNFT</param>
        /// <returns>It returns the NFT Info of the nft which was/is to be minted.</returns>
        public async Task<NftGetInfo_Response> VerifyMint(NftMintNFT_Response nftMint)
        {
            string coinID = nftMint.spend_bundle.coin_solutions[0].coin.GetCoinID();
            NftGetInfo_RPC nftRequest = new NftGetInfo_RPC
            {
                coin_id = coinID,
                wallet_id = nftMint.wallet_id
            };
            return await NftGetInfo_Async(nftRequest).ConfigureAwait(false);
        }
        /// <summary>
        /// This method asynchronously sends an "nft_mint_nft" message to mint an nft. 
        /// It then deserializes the responseJson into an NftMintNFT_Response object and returns it. 
        /// It takes an NftMintNFT_RPC object as an argument.
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/nft-rpc#nft_mint_nft"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<NftMintNFT_Response> NftMintNft_Async(NftMintNFT_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("nft_mint_nft", rpc.ToString());
            NftMintNFT_Response deserializedObject = JsonSerializer.Deserialize<NftMintNFT_Response>(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// This method asynchronously sends an "nft_mint_nft" message to mint an nft. 
        /// It then deserializes the responseJson into an NftMintNFT_Response object and returns it. 
        /// It takes an NftMintNFT_RPC object as an argument.
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/nft-rpc#nft_mint_nft"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public NftMintNFT_Response NftMintNft_Sync(NftMintNFT_RPC rpc)
        {
            Task<NftMintNFT_Response> data = Task.Run(() => NftMintNft_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Count the number of NFTs in a wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/nft-rpc#nft_count_nfts"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<NftCountNfts_Response> NftCountNfts_Async(WalletID_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("nft_count_nfts", rpc.ToString());
            NftCountNfts_Response deserializedObject = JsonSerializer.Deserialize<NftCountNfts_Response>(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Count the number of NFTs in a wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/nft-rpc#nft_count_nfts"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public NftCountNfts_Response NftCountNfts_Sync(WalletID_RPC rpc)
        {
            Task<NftCountNfts_Response> data = Task.Run(() => NftCountNfts_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Show all NFTs in a given wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/nft-rpc#nft_get_nfts"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<NftGetNfts_Response> NftGetNfts_Async(WalletID_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("nft_get_nfts", rpc.ToString());
            NftGetNfts_Response deserializedObject = JsonSerializer.Deserialize<NftGetNfts_Response>(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Show all NFTs in a given wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/nft-rpc#nft_get_nfts"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public NftGetNfts_Response NftGetNfts_Sync(WalletID_RPC rpc)
        {
            Task<NftGetNfts_Response> data = Task.Run(() => NftGetNfts_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Show the NFT wallet associated with a DID
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/nft-rpc#nft_get_by_did"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<WalletID_Response> NftGetByDID_Async(DidID_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("nft_get_by_did", rpc.ToString());
            WalletID_Response deserializedObject = JsonSerializer.Deserialize<WalletID_Response>(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Show the NFT wallet associated with a DID
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/nft-rpc#nft_get_by_did"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public WalletID_Response NftGetByDID_Sync(DidID_RPC rpc)
        {
            Task<WalletID_Response> data = Task.Run(() => NftGetByDID_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Set the DID for a specific NFT (the NFT must support DIDs)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/nft-rpc#nft_set_nft_did"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<NftSetNftDID_Response> NftSetNftDID_Async(NftSetNftDID_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("nft_set_nft_did", rpc.ToString());
            NftSetNftDID_Response deserializedObject = JsonSerializer.Deserialize<NftSetNftDID_Response>(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Set the DID for a specific NFT (the NFT must support DIDs)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/nft-rpc#nft_set_nft_did"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public NftSetNftDID_Response NftSetNftDID_Sync(NftSetNftDID_RPC rpc)
        {
            Task<NftSetNftDID_Response> data = Task.Run(() => NftSetNftDID_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Set the transaction status of an NFT
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/nft-rpc#nft_set_nft_status"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<Success_Response> NftSetNftStatus_Async(NftSetNftStatus_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("nft_set_nft_status", rpc.ToString());
            Success_Response deserializedObject = JsonSerializer.Deserialize<Success_Response>(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Set the transaction status of an NFT
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/nft-rpc#nft_set_nft_status"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public Success_Response NftSetNftStatus_Sync(NftSetNftStatus_RPC rpc)
        {
            Task<Success_Response> data = Task.Run(() => NftSetNftStatus_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Get the DID associated with an NFT wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/nft-rpc#nft_get_wallet_did"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<DidID_Response> NftGetWalletDID_Async(WalletID_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("nft_get_wallet_did", rpc.ToString());
            DidID_Response deserializedObject = JsonSerializer.Deserialize<DidID_Response>(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Get the DID associated with an NFT wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/nft-rpc#nft_get_wallet_did"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public DidID_Response NftGetWalletDID_Sync(WalletID_RPC rpc)
        {
            Task<DidID_Response> data = Task.Run(() => NftGetWalletDID_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Show all NFT wallets that are associated with DIDs
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/nft-rpc#nft_get_wallets_with_dids"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<NftGetWalletsWithDIDs_Response> NftGetWalletsWithDIDs_Async()
        {
            string responseJson = await SendCustomMessage_Async("nft_get_wallets_with_dids");
            NftGetWalletsWithDIDs_Response deserializedObject = JsonSerializer.Deserialize<NftGetWalletsWithDIDs_Response>(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Show all NFT wallets that are associated with DIDs
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/nft-rpc#nft_get_wallets_with_dids"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public NftGetWalletsWithDIDs_Response NftGetWalletsWithDIDs_Sync()
        {
            Task<NftGetWalletsWithDIDs_Response> data = Task.Run(() => NftGetWalletsWithDIDs_Async());
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Get info about an NFT
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/nft-rpc#nft_get_info"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<NftGetInfo_Response> NftGetInfo_Async(NftGetInfo_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("nft_get_info", rpc.ToString());
            NftGetInfo_Response deserializedObject = JsonSerializer.Deserialize<NftGetInfo_Response>(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Get info about an NFT
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/nft-rpc#nft_get_info"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public NftGetInfo_Response NftGetInfo_Sync(NftGetInfo_RPC rpc)
        {
            Task<NftGetInfo_Response> data = Task.Run(() => NftGetInfo_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Transfer an NFT to a new wallet address
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/nft-rpc#nft_transfer_nft"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<SpendBundle_Response> NftTransferNft_Async(NftTransferNft_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("nft_transfer_nft", rpc.ToString());
            SpendBundle_Response deserializedObject = JsonSerializer.Deserialize<SpendBundle_Response>(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Transfer an NFT to a new wallet address
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/nft-rpc#nft_transfer_nft"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public SpendBundle_Response NftTransferNft_Sync(NftTransferNft_RPC rpc)
        {
            Task<SpendBundle_Response> data = Task.Run(() => NftTransferNft_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Add a new URI to the location URI list
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/nft-rpc#nft_add_uri"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<SpendBundle_Response> NftAddURI_Async(NftAddURI_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("nft_add_uri", rpc.ToString());
            SpendBundle_Response deserializedObject = JsonSerializer.Deserialize<SpendBundle_Response>(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Add a new URI to the location URI list
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/nft-rpc#nft_add_uri"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public SpendBundle_Response NftAddURI_Sync(NftAddURI_RPC rpc)
        {
            Task<SpendBundle_Response> data = Task.Run(() => NftAddURI_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Given one or more NFTs to be exchanged for one or more fungible assets, calculate the correct royalty payments.
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/nft-rpc#nft_calculate_royalties"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<NftCalculateRoyalties_Response> NftCalculateRoyalties_Async(NftCalculateRoyalties_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("nft_calculate_royalties", rpc.ToString());
            NftCalculateRoyalties_Response deserializedObject = JsonSerializer.Deserialize<NftCalculateRoyalties_Response>(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Given one or more NFTs to be exchanged for one or more fungible assets, calculate the correct royalty payments.
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/nft-rpc#nft_calculate_royalties"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public NftCalculateRoyalties_Response NftCalculateRoyalties_Sync(NftCalculateRoyalties_RPC rpc)
        {
            Task<NftCalculateRoyalties_Response> data = Task.Run(() => NftCalculateRoyalties_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Create a spend bundle to mint multiple NFTs. (all the same)
        /// Note that this command does not push the spend bundle to the blockchain. 
        /// See our documentation for push_tx for info to accomplish this.
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/nft-rpc#nft_mint_bulk"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<SpendBundle_Response> NftMintBulk_Async(NftMintBulk_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("nft_mint_bulk", rpc.ToString());
            SpendBundle_Response deserializedObject = JsonSerializer.Deserialize<SpendBundle_Response>(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Create a spend bundle to mint multiple NFTs. (all the same)
        /// Note that this command does not push the spend bundle to the blockchain. 
        /// See our documentation for push_tx for info to accomplish this.
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/nft-rpc#nft_mint_bulk"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public SpendBundle_Response NftMintBulk_Sync(NftMintBulk_RPC rpc)
        {
            Task<SpendBundle_Response> data = Task.Run(() => NftMintBulk_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Bulk set DID for NFTs across different wallets.
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/nft-rpc#nft_set_did_bulk"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<SpendBundle_MultiWallet_Response> NftSetDIDBulk_Async(NftSetDidBulk_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("nft_set_did_bulk", rpc.ToString());
            SpendBundle_MultiWallet_Response deserializedObject = JsonSerializer.Deserialize<SpendBundle_MultiWallet_Response>(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Bulk set DID for NFTs across different wallets.
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/nft-rpc#nft_set_did_bulk"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public SpendBundle_MultiWallet_Response NftSetDIDBulk_Sync(NftSetDidBulk_RPC rpc)
        {
            Task<SpendBundle_MultiWallet_Response> data = Task.Run(() => NftSetDIDBulk_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Bulk transfer NFTs to an address.
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/nft-rpc#nft_transfer_bulk"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<SpendBundle_MultiWallet_Response> NftTransferBulk_Async(NftTransferBulk_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("nft_transfer_bulk", rpc.ToString());
            SpendBundle_MultiWallet_Response deserializedObject = JsonSerializer.Deserialize<SpendBundle_MultiWallet_Response>(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Bulk transfer NFTs to an address.
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/nft-rpc#nft_transfer_bulk"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public SpendBundle_MultiWallet_Response NftTransferBulk_Sync(NftTransferBulk_RPC rpc)
        {
            Task<SpendBundle_MultiWallet_Response> data = Task.Run(() => NftTransferBulk_Async(rpc));
            data.Wait();
            return data.Result;
        }
    }
}
