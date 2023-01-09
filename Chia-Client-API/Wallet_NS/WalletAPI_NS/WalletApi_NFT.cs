using CHIA_RPC.General;
using CHIA_RPC.Wallet_RPC_NS.NFT;
using System.Text.Json;

namespace Chia_Client_API.Wallet_NS.WalletAPI_NS
{
    public static partial class WalletApi
    {
        /// <summary>
        /// This method asynchronously sends an "nft_mint_nft" message to mint an nft. 
        /// It then deserializes the response into an NFTMintNFT_Response object and returns it. 
        /// It takes an NFTMintNFT_RPC object as an argument.
        /// </summary>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async static Task<NftMintNFT_Response> NftMintNft(NftMintNFT_RPC rpc)
        {
            string response = await SendCustomMessage("nft_mint_nft", rpc.ToString());
            CHIA_RPC.Wallet_RPC_NS.NFT.NftMintNFT_Response json = JsonSerializer.Deserialize<NftMintNFT_Response>(response);
            return json;
        }
        /// <summary>
        /// Show all NFTs in a given wallet
        /// </summary>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async static Task<NftGetNfts_Response> NftGetNfts(WalletID_RPC rpc)
        {
            string response = await SendCustomMessage("nft_get_nfts", rpc.ToString());
            NftGetNfts_Response json = JsonSerializer.Deserialize<NftGetNfts_Response>(response);
            return json;
        }
        /// <summary>
        /// Set the DID for a specific NFT (the NFT must support DIDs)
        /// </summary>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async static Task<NftSetNftDID_Response> NftSetNftDID(NftSetNftDID_RPC rpc)
        {
            string response = await SendCustomMessage("nft_set_nft_did", rpc.ToString());
            NftSetNftDID_Response json = JsonSerializer.Deserialize<NftSetNftDID_Response>(response);
            return json;
        }
        /// <summary>
        /// Show the NFT wallet associated with a DID
        /// </summary>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async static Task<NftGetByDID_Response> NftGetByDID(NftGetByDID_RPC rpc)
        {
            string response = await SendCustomMessage("nft_get_by_did", rpc.ToString());
            NftGetByDID_Response json = JsonSerializer.Deserialize<NftGetByDID_Response>(response);
            return json;
        }
        /// <summary>
        /// Get the DID associated with an NFT wallet
        /// </summary>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async static Task<NftGetWalletDID_Response> NftGetWalletDID(NftGetWalletDID_RPC rpc)
        {
            string response = await SendCustomMessage("nft_get_wallet_did", rpc.ToString());
            NftGetWalletDID_Response json = JsonSerializer.Deserialize<NftGetWalletDID_Response>(response);
            return json;
        }
        /// <summary>
        /// Show all NFT wallets that are associated with DIDs
        /// </summary>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async static Task<NftGetWalletsWithDIDs_Response> NftGetWalletsWithDIDs(NftGetWalletsWithDIDs_RPC rpc)
        {
            string response = await SendCustomMessage("nft_get_wallets_with_dids", rpc.ToString());
            NftGetWalletsWithDIDs_Response json = JsonSerializer.Deserialize<NftGetWalletsWithDIDs_Response>(response);
            return json;
        }
        /// <summary>
        /// Set the transaction status of an NFT
        /// </summary>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async static Task<NftSetNftStatus_Response> NftSetNftStatus(NftSetNftStatus_RPC rpc)
        {
            string response = await SendCustomMessage("nft_set_nft_status", rpc.ToString());
            NftSetNftStatus_Response json = JsonSerializer.Deserialize<NftSetNftStatus_Response>(response);
            return json;
        }
        /// <summary>
        /// Transfer an NFT to a new wallet address
        /// </summary>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async static Task<NftTransferNft_Response> NftTransferNft(NftTransferNft_RPC rpc)
        {
            string response = await SendCustomMessage("nft_transfer_nft", rpc.ToString());
            NftTransferNft_Response json = JsonSerializer.Deserialize<NftTransferNft_Response>(response);
            return json;
        }
        /// <summary>
        /// Get info about an NFT
        /// </summary>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async static Task<NftGetInfo_Response> NftGetInfo(NftGetInfo_RPC rpc)
        {
            string response = await SendCustomMessage("nft_get_info", rpc.ToString());
            NftGetInfo_Response json = JsonSerializer.Deserialize<NftGetInfo_Response>(response);
            return json;
        }
        /// <summary>
        /// Add a new URI to the location URI list
        /// </summary>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async static Task<NftAddURI_Response> NftAddURI(NftAddURI_RPC rpc)
        {
            string response = await SendCustomMessage("nft_add_uri", rpc.ToString());
            NftAddURI_Response json = JsonSerializer.Deserialize<NftAddURI_Response>(response);
            return json;
        }
        /// <summary>
        /// Given one or more NFTs to be exchanged for one or more fungible assets, calculate the correct royalty payments.
        /// </summary>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async static Task<NftCalculateRoyalties_Response> NftCalculateRoyalties(NftCalculateRoyalties_RPC rpc)
        {
            string response = await SendCustomMessage("nft_calculate_royalties", rpc.ToString());
            NftCalculateRoyalties_Response json = JsonSerializer.Deserialize<NftCalculateRoyalties_Response>(response);
            return json;
        }
        /// <summary>
        /// Create a spend bundle to mint multiple NFTs. (all the same)
        /// Note that this command does not push the spend bundle to the blockchain. 
        /// See our documentation for push_tx for info to accomplish this.
        /// </summary>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async static Task<NftMintBulk_Response> NftMintBulk(NftMintBulk_RPC rpc)
        {
            string response = await SendCustomMessage("nft_mint_bulk", rpc.ToString());
            NftMintBulk_Response json = JsonSerializer.Deserialize<NftMintBulk_Response>(response);
            return json;
        }
    }
}
