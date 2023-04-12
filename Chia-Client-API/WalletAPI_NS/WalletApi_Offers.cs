using CHIA_RPC.Objects_NS;
using CHIA_RPC.Wallet_NS.Offer_NS;
using CHIA_RPC.Wallet_NS.Wallet_NS;
using System.Text.Json;

namespace Chia_Client_API.WalletAPI_NS
{
    public partial class Wallet_RPC_Client
    {
        /// <summary>
        /// this function takes a spend bundle, which is retiurned from NftMintNFT.
        /// it converts the coin into a coin ID which can be used to draft an NftGetInfo_Requests.
        /// 
        /// </summary>
        /// <param name="nftMint">spend bundle, which is retiurned from NftMintNFT</param>
        /// <returns>It returns the NFT Info of the nft which was/is to be minted.</returns>
        public async Task<OfferFile> CreateOfferForIds(Offer_RPC offer)
        {
            string response = await SendCustomMessage_Async("create_offer_for_ids", offer.ToString());
            try
            {
                File.WriteAllText("testoffer.offer", response);
                OfferFile json = JsonSerializer.Deserialize<OfferFile>(response);
                return json;
            }
            catch(Exception ex)
            {
                if (response == null || response == "")
                {
                    throw new ArgumentNullException("offer response is null or empty!");
                }
                else
                {
                    throw new InvalidDataException($"offer response could not be deserialized, {ex.Message}{Environment.NewLine}{response}");
                }
            }
        }
    }
}
