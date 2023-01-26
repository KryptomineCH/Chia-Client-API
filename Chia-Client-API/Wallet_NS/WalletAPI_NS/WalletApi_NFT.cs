using Chia_Client_API.FullNode_NS;
using CHIA_RPC.FullNode_RPC_NS;
using CHIA_RPC.General;
using CHIA_RPC.Wallet_RPC_NS.NFT;
using CHIA_RPC.Wallet_RPC_NS.Wallet_NS;
using CHIA_RPC.Wallet_RPC_NS.WalletNode_NS;
using System;
using System.Text.Json;

namespace Chia_Client_API.Wallet_NS.WalletAPI_NS
{
    public static partial class WalletApi
    {
        /// <summary>
        /// This function is used to check if an NFT minting operation has completed successfully. 
        /// The function repeatedly checks the status of the minting operation by calling 
        /// the GetCoinRecordsByNames_Async method and comparing it to the original mintCoinInfo. 
        /// If the minting is successful or the timeout is reached, 
        /// the function will return a boolean indicating the success or failure of the operation.
        /// </summary>
        /// <param name="nftMint">The response from the NFT minting operation</param>
        /// <param name="cancel">CancellationToken used to cancel the operation if necessary</param>
        /// <param name="timeOutInMinutes">Timeout for the operation in minutes</param>
        /// <returns>A boolean indicating the success or failure of the operation</returns>
        public async static Task<NftGetInfo_Response> NftAwaitMintComplete_Async(
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
                await Task.Delay(refreshInterwallSeconds * 1000).ConfigureAwait(false);
            }
            return nftInfo;
        }
        /// <summary>
        /// this function takes a spend bundle, which is retiurned from NftMintNFT.
        /// it converts the coin into a coin ID which can be used to draft an NftGetInfo_Requests.
        /// 
        /// </summary>
        /// <param name="nftMint">spend bundle, which is retiurned from NftMintNFT</param>
        /// <returns>It returns the NFT Info of the nft which was/is to be minted.</returns>
        public static async Task<NftGetInfo_Response> VerifyMint(NftMintNFT_Response nftMint)
        {
            NftGetInfo_RPC nftRequest = nftMint.Get_NftGetInfo_Rpc();
            return await WalletApi.NftGetInfo_Async(nftRequest);
        }
        /// <summary>
        /// This function is used to check if an NFT minting operation has completed successfully. 
        /// The function repeatedly checks the status of the minting operation by calling 
        /// the GetCoinRecordsByNames_Async method and comparing it to the original mintCoinInfo. 
        /// If the minting is successful or the timeout is reached, 
        /// the function will return a boolean indicating the success or failure of the operation.
        /// </summary>
        /// <param name="nftMint">The response from the NFT minting operation</param>
        /// <param name="cancel">CancellationToken used to cancel the operation if necessary</param>
        /// <param name="timeOutInMinutes">Timeout for the operation in minutes</param>
        /// <returns>A boolean indicating the success or failure of the operation</returns>
        public async static Task<NftGetInfo_Response> NftAwaitMintComplete_Sync(
            NftMintNFT_Response nftMint, CancellationToken cancel, double timeOutInMinutes = 15.0)
        {
            Task<NftGetInfo_Response> data = Task.Run(() => NftAwaitMintComplete_Async(nftMint, cancel, timeOutInMinutes));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// This method asynchronously sends an "nft_mint_nft" message to mint an nft. 
        /// It then deserializes the response into an NftMintNFT_Response object and returns it. 
        /// It takes an NftMintNFT_RPC object as an argument.
        /// </summary>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async static Task<NftMintNFT_Response> NftMintNft_Async(NftMintNFT_RPC rpc)
        {
            string response = await SendCustomMessage_Async("nft_mint_nft", rpc.ToString());
            NftMintNFT_Response json = JsonSerializer.Deserialize<NftMintNFT_Response>(response);
            return json;
        }
        /// <summary>
        /// This method asynchronously sends an "nft_mint_nft" message to mint an nft. 
        /// It then deserializes the response into an NftMintNFT_Response object and returns it. 
        /// It takes an NftMintNFT_RPC object as an argument.
        /// </summary>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public static NftMintNFT_Response NftMintNft_Sync(NftMintNFT_RPC rpc)
        {
            Task<NftMintNFT_Response> data = Task.Run(() => NftMintNft_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Show all NFTs in a given wallet
        /// </summary>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async static Task<NftGetNfts_Response> NftGetNfts_Async(WalletID_RPC rpc)
        {
            string response = await SendCustomMessage_Async("nft_get_nfts", rpc.ToString());
            NftGetNfts_Response json = JsonSerializer.Deserialize<NftGetNfts_Response>(response);
            return json;
        }
        /// <summary>
        /// Show all NFTs in a given wallet
        /// </summary>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public static NftGetNfts_Response NftGetNfts_Sync(WalletID_RPC rpc)
        {
            Task<NftGetNfts_Response> data = Task.Run(() => NftGetNfts_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Set the DID for a specific NFT (the NFT must support DIDs)
        /// </summary>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async static Task<NftSetNftDID_Response> NftSetNftDID_Async(NftSetNftDID_RPC rpc)
        {
            string response = await SendCustomMessage_Async("nft_set_nft_did", rpc.ToString());
            NftSetNftDID_Response json = JsonSerializer.Deserialize<NftSetNftDID_Response>(response);
            return json;
        }
        /// <summary>
        /// Set the DID for a specific NFT (the NFT must support DIDs)
        /// </summary>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public static NftSetNftDID_Response NftSetNftDID_Sync(NftSetNftDID_RPC rpc)
        {
            Task<NftSetNftDID_Response> data = Task.Run(() => NftSetNftDID_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Show the NFT wallet associated with a DID
        /// </summary>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async static Task<WalletID_Response> NftGetByDID_Async(DidID_RPC rpc)
        {
            string response = await SendCustomMessage_Async("nft_get_by_did", rpc.ToString());
            WalletID_Response json = JsonSerializer.Deserialize<WalletID_Response>(response);
            return json;
        }
        /// <summary>
        /// Show the NFT wallet associated with a DID
        /// </summary>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public static WalletID_Response NftGetByDID_Sync(DidID_RPC rpc)
        {
            Task<WalletID_Response> data = Task.Run(() => NftGetByDID_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Get the DID associated with an NFT wallet
        /// </summary>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async static Task<DidID_Response> NftGetWalletDID_Async(WalletID_RPC rpc)
        {
            string response = await SendCustomMessage_Async("nft_get_wallet_did", rpc.ToString());
            DidID_Response json = JsonSerializer.Deserialize<DidID_Response>(response);
            return json;
        }
        /// <summary>
        /// Get the DID associated with an NFT wallet
        /// </summary>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public static DidID_Response NftGetWalletDID_Sync(WalletID_RPC rpc)
        {
            Task<DidID_Response> data = Task.Run(() => NftGetWalletDID_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Show all NFT wallets that are associated with DIDs
        /// </summary>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async static Task<NftGetWalletsWithDIDs_Response> NftGetWalletsWithDIDs_Async()
        {
            string response = await SendCustomMessage_Async("nft_get_wallets_with_dids");
            NftGetWalletsWithDIDs_Response json = JsonSerializer.Deserialize<NftGetWalletsWithDIDs_Response>(response);
            return json;
        }
        /// <summary>
        /// Show all NFT wallets that are associated with DIDs
        /// </summary>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public static NftGetWalletsWithDIDs_Response NftGetWalletsWithDIDs_Sync()
        {
            Task<NftGetWalletsWithDIDs_Response> data = Task.Run(() => NftGetWalletsWithDIDs_Async());
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Set the transaction status of an NFT
        /// </summary>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async static Task<Success_Response> NftSetNftStatus_Async(NftSetNftStatus_RPC rpc)
        {
            string response = await SendCustomMessage_Async("nft_set_nft_status", rpc.ToString());
            Success_Response json = JsonSerializer.Deserialize<Success_Response>(response);
            return json;
        }
        /// <summary>
        /// Set the transaction status of an NFT
        /// </summary>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public static Success_Response NftSetNftStatus_Sync(NftSetNftStatus_RPC rpc)
        {
            Task<Success_Response> data = Task.Run(() => NftSetNftStatus_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Transfer an NFT to a new wallet address
        /// </summary>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async static Task<NftMintNFT_Response> NftTransferNft_Async(NftTransferNft_RPC rpc)
        {
            string response = await SendCustomMessage_Async("nft_transfer_nft", rpc.ToString());
            NftMintNFT_Response json = JsonSerializer.Deserialize<NftMintNFT_Response>(response);
            return json;
        }
        /// <summary>
        /// Transfer an NFT to a new wallet address
        /// </summary>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public static NftMintNFT_Response NftTransferNft_Sync(NftTransferNft_RPC rpc)
        {
            Task<NftMintNFT_Response> data = Task.Run(() => NftTransferNft_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Get info about an NFT
        /// </summary>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async static Task<NftGetInfo_Response> NftGetInfo_Async(NftGetInfo_RPC rpc)
        {
            string response = await SendCustomMessage_Async("nft_get_info", rpc.ToString());
            NftGetInfo_Response json = JsonSerializer.Deserialize<NftGetInfo_Response>(response);
            return json;
        }
        /// <summary>
        /// Get info about an NFT
        /// </summary>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public static NftGetInfo_Response NftGetInfo_Sync(NftGetInfo_RPC rpc)
        {
            Task<NftGetInfo_Response> data = Task.Run(() => NftGetInfo_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Add a new URI to the location URI list
        /// </summary>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async static Task<NftMintNFT_Response> NftAddURI_Async(NftAddURI_RPC rpc)
        {
            string response = await SendCustomMessage_Async("nft_add_uri", rpc.ToString());
            NftMintNFT_Response json = JsonSerializer.Deserialize<NftMintNFT_Response>(response);
            return json;
        }
        /// <summary>
        /// Add a new URI to the location URI list
        /// </summary>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public static NftMintNFT_Response NftAddURI_Sync(NftAddURI_RPC rpc)
        {
            Task<NftMintNFT_Response> data = Task.Run(() => NftAddURI_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Given one or more NFTs to be exchanged for one or more fungible assets, calculate the correct royalty payments.
        /// </summary>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async static Task<NftCalculateRoyalties_Response> NftCalculateRoyalties_Async(NftCalculateRoyalties_RPC rpc)
        {
            string response = await SendCustomMessage_Async("nft_calculate_royalties", rpc.ToString());
            NftCalculateRoyalties_Response json = JsonSerializer.Deserialize<NftCalculateRoyalties_Response>(response);
            return json;
        }
        /// <summary>
        /// Given one or more NFTs to be exchanged for one or more fungible assets, calculate the correct royalty payments.
        /// </summary>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public static NftCalculateRoyalties_Response NftCalculateRoyalties_Sync(NftCalculateRoyalties_RPC rpc)
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
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async static Task<NftMintNFT_Response> NftMintBulk_Async(NftMintBulk_RPC rpc)
        {
            string response = await SendCustomMessage_Async("nft_mint_bulk", rpc.ToString());
            NftMintNFT_Response json = JsonSerializer.Deserialize<NftMintNFT_Response>(response);
            return json;
        }
        /// <summary>
        /// Create a spend bundle to mint multiple NFTs. (all the same)
        /// Note that this command does not push the spend bundle to the blockchain. 
        /// See our documentation for push_tx for info to accomplish this.
        /// </summary>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public static NftMintNFT_Response NftMintBulk_Sync(NftMintBulk_RPC rpc)
        {
            Task<NftMintNFT_Response> data = Task.Run(() => NftMintBulk_Async(rpc));
            data.Wait();
            return data.Result;
        }
    }
}
