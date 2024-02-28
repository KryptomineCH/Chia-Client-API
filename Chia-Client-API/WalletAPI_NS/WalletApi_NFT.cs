using CHIA_RPC.General_NS;
using CHIA_RPC.HelperFunctions_NS;
using CHIA_RPC.Wallet_NS.NFT_NS;
using Chia_Client_API.ChiaClient_NS;

namespace Chia_Client_API.WalletAPI_NS
{
    public abstract partial class WalletRpcBase : ChiaEndpointRouteBase
    {
        /// <summary>
        /// Add a new URI to the location URI list
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/nft-rpc#nft_add_uri"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<SpendBundle_Response> NftAddURI_Async(NftAddURI_RPC rpc)
        {
            string responseJson = await SendCustomMessageAsync("nft_add_uri", rpc.ToString());
            ActionResult<SpendBundle_Response> deserializationResult = SpendBundle_Response.LoadResponseFromString(responseJson);
            SpendBundle_Response response = new SpendBundle_Response();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
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
            string responseJson = await SendCustomMessageAsync("nft_calculate_royalties", rpc.ToString());
            ActionResult<NftCalculateRoyalties_Response> deserializationResult = NftCalculateRoyalties_Response.LoadResponseFromString(responseJson);
            NftCalculateRoyalties_Response response = new NftCalculateRoyalties_Response();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
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
        /// Count the number of NFTs in a wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/nft-rpc#nft_count_nfts"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<NftCountNfts_Response> NftCountNfts_Async(WalletID_RPC rpc)
        {
            string responseJson = await SendCustomMessageAsync("nft_count_nfts", rpc.ToString());
            ActionResult<NftCountNfts_Response> deserializationResult = NftCountNfts_Response.LoadResponseFromString(responseJson);
            NftCountNfts_Response response = new NftCountNfts_Response();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
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
        /// Show the NFT wallet associated with a DID
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/nft-rpc#nft_get_by_did"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<WalletID_Response> NftGetByDID_Async(DidID_RPC rpc)
        {
            string responseJson = await SendCustomMessageAsync("nft_get_by_did", rpc.ToString());
            ActionResult<WalletID_Response> deserializationResult = WalletID_Response.LoadResponseFromString(responseJson);
            WalletID_Response response = new WalletID_Response();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
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
        /// Get info about an NFT
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/nft-rpc#nft_get_info"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<NftGetInfo_Response> NftGetInfo_Async(NftGetInfo_RPC rpc)
        {
            string responseJson = await SendCustomMessageAsync("nft_get_info", rpc.ToString());
            ActionResult<NftGetInfo_Response> deserializationResult = NftGetInfo_Response.LoadResponseFromString(responseJson);
            NftGetInfo_Response response = new NftGetInfo_Response();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
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
        /// Show all NFTs in a given wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/nft-rpc#nft_get_nfts"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<NftGetNfts_Response> NftGetNfts_Async(WalletID_RPC rpc)
        {
            string responseJson = await SendCustomMessageAsync("nft_get_nfts", rpc.ToString());
            ActionResult<NftGetNfts_Response> deserializationResult = NftGetNfts_Response.LoadResponseFromString(responseJson);
            NftGetNfts_Response response = new NftGetNfts_Response();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
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
        /// Show all NFT wallets that are associated with DIDs
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/nft-rpc#nft_get_wallets_with_dids"/></remarks>
        /// <returns></returns>
        public async Task<NftGetWalletsWithDIDs_Response> NftGetWalletsWithDIDs_Async()
        {
            string responseJson = await SendCustomMessageAsync("nft_get_wallets_with_dids");
            ActionResult<NftGetWalletsWithDIDs_Response> deserializationResult = NftGetWalletsWithDIDs_Response.LoadResponseFromString(responseJson);
            NftGetWalletsWithDIDs_Response response = new NftGetWalletsWithDIDs_Response();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
        }

        /// <summary>
        /// Show all NFT wallets that are associated with DIDs
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/nft-rpc#nft_get_wallets_with_dids"/></remarks>
        /// <returns></returns>
        public NftGetWalletsWithDIDs_Response NftGetWalletsWithDIDs_Sync()
        {
            Task<NftGetWalletsWithDIDs_Response> data = Task.Run(() => NftGetWalletsWithDIDs_Async());
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
            string responseJson = await SendCustomMessageAsync("nft_get_wallet_did", rpc.ToString());
            ActionResult<DidID_Response> deserializationResult = DidID_Response.LoadResponseFromString(responseJson);
            DidID_Response response = new DidID_Response();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
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
        /// Create a spend bundle to mint multiple NFTs. (all the same)
        /// Note that this command does not push the spend bundle to the blockchain. 
        /// See our documentation for push_tx for info to accomplish this.
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/nft-rpc#nft_mint_bulk"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<SpendBundle_Response> NftMintBulk_Async(NftMintBulk_RPC rpc)
        {
            string responseJson = await SendCustomMessageAsync("nft_mint_bulk", rpc.ToString());
            ActionResult<SpendBundle_Response> deserializationResult = SpendBundle_Response.LoadResponseFromString(responseJson);
            SpendBundle_Response response = new SpendBundle_Response();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
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
        /// This method asynchronously sends an "nft_mint_nft" message to mint an nft. 
        /// It then deserializes the responseJson into an NftMintNFT_Response? object and returns it. 
        /// It takes an NftMintNFT_RPC object as an argument.
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/nft-rpc#nft_mint_nft"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<NftMintNFT_Response> NftMintNft_Async(NftMintNFT_RPC rpc)
        {
            string responseJson = await SendCustomMessageAsync("nft_mint_nft", rpc.ToString());
            ActionResult<NftMintNFT_Response> deserializationResult = NftMintNFT_Response.LoadResponseFromString(responseJson);
            NftMintNFT_Response response = new NftMintNFT_Response();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
        }

        /// <summary>
        /// This method asynchronously sends an "nft_mint_nft" message to mint an nft. 
        /// It then deserializes the responseJson into an NftMintNFT_Response? object and returns it. 
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
        /// Bulk set DID for NFTs across different wallets.
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/nft-rpc#nft_set_did_bulk"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<SpendBundle_MultiWallet_Response> NftSetDIDBulk_Async(NftSetDidBulk_RPC rpc)
        {
            string responseJson = await SendCustomMessageAsync("nft_set_did_bulk", rpc.ToString());
            ActionResult<SpendBundle_MultiWallet_Response> deserializationResult = SpendBundle_MultiWallet_Response.LoadResponseFromString(responseJson);
            SpendBundle_MultiWallet_Response response = new SpendBundle_MultiWallet_Response();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
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
        /// Set the DID for a specific NFT (the NFT must support DIDs)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/nft-rpc#nft_set_nft_did"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<NftSetNftDID_Response> NftSetNftDID_Async(NftSetNftDID_RPC rpc)
        {
            string responseJson = await SendCustomMessageAsync("nft_set_nft_did", rpc.ToString());
            ActionResult<NftSetNftDID_Response> deserializationResult = NftSetNftDID_Response.LoadResponseFromString(responseJson);
            NftSetNftDID_Response response = new NftSetNftDID_Response();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
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
            string responseJson = await SendCustomMessageAsync("nft_set_nft_status", rpc.ToString());
            ActionResult<Success_Response> deserializationResult = Success_Response.LoadResponseFromString(responseJson);
            Success_Response response = new Success_Response();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
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
        /// Transfer an NFT to a new wallet address.<br/>
        /// This causes a blockchain transaction.<br/>
        /// In order to validate the transaction, fetch the NFT with [] and access its properties
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/nft-rpc#nft_transfer_nft"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<SpendBundle_Response> NftTransferNft_Async(NftTransferNft_RPC rpc)
        {
            string responseJson = await SendCustomMessageAsync("nft_transfer_nft", rpc.ToString());
            ActionResult<SpendBundle_Response> deserializationResult = SpendBundle_Response.LoadResponseFromString(responseJson);
            SpendBundle_Response response = new SpendBundle_Response();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
        }

        /// <summary>
        /// Transfer an NFT to a new wallet address.<br/>
        /// This causes a blockchain transaction.<br/>
        /// In order to validate the transaction, fetch the NFT with [] and access its properties
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
        /// Bulk transfer NFTs to an address.
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/nft-rpc#nft_transfer_bulk"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<SpendBundle_MultiWallet_Response> NftTransferBulk_Async(NftTransferBulk_RPC rpc)
        {
            string responseJson = await SendCustomMessageAsync("nft_transfer_bulk", rpc.ToString());
            ActionResult<SpendBundle_MultiWallet_Response> deserializationResult = SpendBundle_MultiWallet_Response.LoadResponseFromString(responseJson);
            SpendBundle_MultiWallet_Response response = new SpendBundle_MultiWallet_Response();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
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
