using CHIA_RPC.General_NS;
using CHIA_RPC.Objects_NS;
using CHIA_RPC.Wallet_NS.CATsAndTrading_NS;
using CHIA_RPC.Wallet_NS.Offer_NS;
using CHIA_RPC.Wallet_NS.Wallet_NS;
using CHIA_RPC.Wallet_NS.WalletManagement_NS;

namespace Chia_Client_API.WalletAPI_NS
{
    public partial class WalletRpcClient
    {
        /// <summary>
        /// this function takes an array of coins and combines them within the same wallet
        /// </summary>
        /// <param name="walletId">the wallet where the coins are in</param>
        /// <param name="feeMojos">an optional blockchain fee in xch mojos</param>
        /// <param name="coinsToCombine">the coins which are to be combined into one large coin</param>
        /// <returns></returns>
        public async Task<Success_Response> CleanExpiredOffers()
        {
            Success_Response success = new Success_Response();
            success.success = false;
            success.error = "unknown error";

            GetAllOffers_RPC getOffers_RPC = new GetAllOffers_RPC(0, long.MaxValue, include_completed: false, file_contents: true);
            OfferFiles offerFiles = await GetAllOffers_Async(getOffers_RPC);
            for (int i = 0; i < offerFiles.offers.Length; i++)
            {
                GetOfferSummary_RPC offerSummaryRpc = new GetOfferSummary_RPC(offerFiles.offers[i]);
                GetOfferSummary_Response offerSummary = await GetOfferSummary_Async(offerSummaryRpc);

                if (offerSummary.summary.valid_times.max_time_DateTime < DateTime.UtcNow)
                {
                    // offer is expired
                    CancelOffer_RPC offerCancelRpc = new CancelOffer_RPC(trade_id: offerFiles.trade_records[i].trade_id, secure: false);
                    success = await CancelOffer_Async(offerCancelRpc);
                    if (!(bool)success.success || !string.IsNullOrEmpty(success.error)) return success;
                }
            }

            return success;
        }
    }
}
