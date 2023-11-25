using Chia_Client_API.Helpers_NS;
using CHIA_RPC.FullNode_NS;
using CHIA_RPC.General_NS;
using CHIA_RPC.Objects_NS;
using CHIA_RPC.Wallet_NS.NFT_NS;
using CHIA_RPC.Wallet_NS.WalletManagement_NS;
using System.Text.Json;
using System.Threading;

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
        /// <param name="refreshInterwallSeconds">the time in between each check if the Mint was complete</param>
        /// <returns>A boolean indicating the success or failure of the operation</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<NftGetInfo_Response?> NftAwaitMintComplete_Async(
            NftMintNFT_Response nftMint, CancellationToken cancel, double timeOutInMinutes = 15.0, int refreshInterwallSeconds = 60)
        {
            if (nftMint.spend_bundle == null)
            {
                throw new ArgumentNullException(nameof(nftMint.spend_bundle), "nftMint.spend_bundle == null");
            }
            if (nftMint.spend_bundle.coin_solutions == null)
            {
                throw new ArgumentNullException(nameof(nftMint.spend_bundle.coin_solutions), "nftMint.spend_bundle.coin_solutions == null");
            }
            if (nftMint.spend_bundle.coin_solutions.Length == 0)
            {
                throw new InvalidOperationException("nftMint.spend_bundle.coin_solutions.Length == 0");
            }
            if (nftMint.spend_bundle.coin_solutions[0].coin == null)
            {
                throw new ArgumentNullException(nameof(nftMint), "nftMint.spend_bundle.coin_solutions[0].coin == null");
            }
            // set timeout
            DateTime timeOut = DateTime.Now + TimeSpan.FromMinutes(timeOutInMinutes);
            // obtain request data to validate transaction
            string? coinID = nftMint.spend_bundle.coin_solutions[0].coin!.CoinName;
            if (string.IsNullOrEmpty(coinID))
            {
                throw new InvalidOperationException("coin id could not be generated!");
            }
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
            NftGetInfo_Response? nftInfo = new NftGetInfo_Response();
            while (!cancel.IsCancellationRequested && DateTime.Now < timeOut)
            {
                nftInfo = await VerifyMint(nftMint).ConfigureAwait(false);
                
                if (nftInfo != null && (nftInfo.success ?? false))
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
        public NftGetInfo_Response? NftAwaitMintComplete_Sync(
            NftMintNFT_Response nftMint, CancellationToken cancel, double timeOutInMinutes = 15.0)
        {
            Task<NftGetInfo_Response?> data = Task.Run(() => NftAwaitMintComplete_Async(nftMint, cancel, timeOutInMinutes));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// this function takes a spend bundle, which is returned from NftMintNFT.
        /// it converts the coin into a coin ID which can be used to draft an NftGetInfo_Requests.
        /// </summary>
        /// <param name="nftMint">spend bundle, which is returned from NftMintNFT</param>
        /// <returns>It returns the NFT Info of the nft which was/is to be minted.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<NftGetInfo_Response?> VerifyMint(NftMintNFT_Response nftMint)
        {
            if (nftMint.spend_bundle == null)
            {
                throw new ArgumentNullException(nameof(nftMint.spend_bundle), "nftMint.spend_bundle == null");
            }
            if (nftMint.spend_bundle.coin_solutions == null)
            {
                throw new ArgumentNullException(nameof(nftMint.spend_bundle.coin_solutions), "nftMint.spend_bundle.coin_solutions == null");
            }
            if (nftMint.spend_bundle.coin_solutions.Length == 0)
            {
                throw new InvalidOperationException("nftMint.spend_bundle.coin_solutions.Length == 0");
            }
            if (nftMint.spend_bundle.coin_solutions[0].coin == null)
            {
                throw new ArgumentNullException(nameof(nftMint), "nftMint.spend_bundle.coin_solutions[0].coin == null");
            }
            string? coinID = nftMint.spend_bundle.coin_solutions[0].coin!.CoinName;
            if (string.IsNullOrEmpty(coinID))
            {
                throw new InvalidOperationException("coin id could not be generated!");
            }
            NftGetInfo_RPC nftRequest = new NftGetInfo_RPC
            {
                coin_id = coinID,
                wallet_id = nftMint.wallet_id
            };
            NftGetInfo_Response response =  await NftGetInfo_Async(nftRequest).ConfigureAwait(false);
            
            return response;
        }
        /// <summary>
        /// searches for the nft in all wallets and returns the wallet id which the nft is located in.
        /// </summary>
        /// <remarks>
        /// for performance reasons, better avoid this function
        /// </remarks>
        /// <param name="nft"></param>
        /// <returns></returns>
        public WalletID_Response NftGetwallet_Sync(Nft nft)
        {
            Task<WalletID_Response> data = Task.Run(() => NftGetwallet_Async(nft));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// searches for the nft in all wallets and returns the wallet id which the nft is located in.
        /// </summary>
        /// <remarks>
        /// for performance reasons, better avoid this function
        /// </remarks>
        /// <param name="nft"></param>
        /// <returns></returns>
        public async Task<WalletID_Response> NftGetwallet_Async(Nft nft)
        {
            GetWallets_Response wallets = await GetWallets_Async();
            {
                foreach (Wallets_info info in wallets.wallets)
                {
                    if (info.type == WalletType.NFT)
                    {
                        NftGetNfts_Response nfts = await NftGetNfts_Async(info.GetWalletID_RPC());
                        foreach (Nft walletNft in nfts.nft_list)
                        {
                            if (walletNft.launcher_id == nft.launcher_id)
                            {
                                return new WalletID_Response()
                                {
                                    success = true,
                                    wallet_id = info.id
                                };
                            }
                        }
                    }
                }
            }
            return new WalletID_Response()
            {
                success = false,
                error = "nft could not be found in any wallet!"
            };
        }
        /// <summary>
        /// Awaits the completion of an NFT transfer operation for a given wallet and NFT.
        /// </summary>
        /// <param name="walletId">The unique identifier of the wallet involved in the NFT transfer.</param>
        /// <param name="nft">An object representing the NFT to be transferred.</param>
        /// <param name="timeOut">The maximum time duration to wait for the transfer to complete.</param>
        /// <param name="requestTimerMS">The time interval, in milliseconds, between successive checks for transfer completion.</param>
        /// <returns>
        /// <c>true</c> if the transfer is complete within the specified <see cref="timeOut"/>, otherwise <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This function continuously checks the transfer status of an NFT by invoking <see cref="NftGetInfo_Async"/>.<br/>
        /// The function exits and returns <c>true</c> as soon as the pending transaction is complete.<br/>
        /// If the <see cref="timeOut"/> is reached before the transaction is complete, the function exits and returns <c>false</c>.
        /// </remarks>
        public bool NftAwaitTransferComplete_Sync(ulong walletId, Nft nft, TimeSpan timeOut, int requestTimerMS)
        {
            Task<bool> data = Task.Run(() => NftAwaitTransferComplete_Async(walletId, nft, timeOut, requestTimerMS));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Awaits the completion of an NFT transfer operation for a given wallet and NFT.
        /// </summary>
        /// <param name="walletId">The unique identifier of the wallet involved in the NFT transfer.</param>
        /// <param name="nft">An object representing the NFT to be transferred.</param>
        /// <param name="timeOut">The maximum time duration to wait for the transfer to complete.</param>
        /// <param name="requestTimerMS">The time interval, in milliseconds, between successive checks for transfer completion.</param>
        /// <returns>
        /// <c>true</c> if the transfer is complete within the specified <see cref="timeOut"/>, otherwise <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This function continuously checks the transfer status of an NFT by invoking <see cref="NftGetInfo_Async"/>.<br/>
        /// The function exits and returns <c>true</c> as soon as the pending transaction is complete.<br/>
        /// If the <see cref="timeOut"/> is reached before the transaction is complete, the function exits and returns <c>false</c>.
        /// </remarks>
        public async Task<bool> NftAwaitTransferComplete_Async(ulong walletId, Nft nft, TimeSpan timeOut, int requestTimerMS)
        {
            NftGetInfo_RPC infoRpc = new NftGetInfo_RPC(nft.nft_coin_id, walletId);
            DateTime end = DateTime.Now + timeOut;
            while (true)
            {
                NftGetInfo_Response info = await NftGetInfo_Async(infoRpc);
                if (!info.nft_info.pending_transaction)
                {
                    return true;
                }
                if (DateTime.Now > end)
                {
                    return false;
                }
                await Task.Delay(requestTimerMS);
            }
        }
    }
}
