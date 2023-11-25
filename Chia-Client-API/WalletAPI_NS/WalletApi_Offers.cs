using Chia_Client_API.Helpers_NS;
using CHIA_RPC.General_NS;
using CHIA_RPC.HelperFunctions_NS;
using CHIA_RPC.Objects_NS;
using CHIA_RPC.Wallet_NS.CATsAndTrading_NS;
using CHIA_RPC.Wallet_NS.Offer_NS;

namespace Chia_Client_API.WalletAPI_NS
{
    public partial class Wallet_RPC_Client
    {
        /// <summary>
        /// Cancel an Offer with a specific identifier
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/offer-rpc#cancel_offer"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<Success_Response> CancelOffer_Async(CancelOffer_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("cancel_offer", rpc.ToString());
            ActionResult<Success_Response> deserializationResult = Success_Response.LoadResponseFromString(responseJson);
            Success_Response response = new ();

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
        /// Cancel an Offer with a specific identifier
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/offer-rpc#cancel_offer"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public Success_Response CancelOffer_Sync(CancelOffer_RPC rpc)
        {
            Task<Success_Response> data = Task.Run(() => CancelOffer_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Cancel multiple Offers
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/offer-rpc#cancel_offers"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<Success_Response> CancelOffers_Async(CancelOffers_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("cancel_offers", rpc.ToString());
            ActionResult<Success_Response> deserializationResult = Success_Response.LoadResponseFromString(responseJson);
            Success_Response response = new ();

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
        /// Cancel multiple Offers
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/offer-rpc#cancel_offers"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public Success_Response CancelOffers_Sync(CancelOffers_RPC rpc)
        {
            Task<Success_Response> data = Task.Run(() => CancelOffers_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Checks whether a specific Offer is valid (see below for definitions)<br/><br/>
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/offer-rpc#check_offer_validity"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<CheckOfferValidity_Response> CheckOfferValidity_Async(CheckOfferValidity_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("check_offer_validity", rpc.ToString());
            ActionResult<CheckOfferValidity_Response> deserializationResult = CheckOfferValidity_Response.LoadResponseFromString(responseJson);
            CheckOfferValidity_Response response = new ();

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
        /// Checks whether a specific Offer is valid (see below for definitions)
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/offer-rpc#check_offer_validity"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public CheckOfferValidity_Response CheckOfferValidity_Sync(CheckOfferValidity_RPC rpc)
        {
            Task<CheckOfferValidity_Response> data = Task.Run(() => CheckOfferValidity_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Creates a new Offer
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/offer-rpc#create_offer_for_ids"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<OfferFile> CreateOfferForIds_Async(CreateOfferForIds_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("create_offer_for_ids", rpc.ToString());
            ActionResult<OfferFile> deserializationResult = OfferFile.LoadObjectFromString(responseJson);
            OfferFile response = new ();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
        }

        /// <summary>
        /// Creates a new Offer
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/offer-rpc#create_offer_for_ids"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public OfferFile CreateOfferForIds_Sync(CreateOfferForIds_RPC rpc)
        {
            Task<OfferFile> data = Task.Run(() => CreateOfferForIds_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Gets multiple Offers for the current wallet, depending on the supplied parameters
        /// </summary>
        /// <remarks>
        /// Technical Limitations:<br/>
        /// - accepted_at_time can only be filled if you accepted the offer<br/>
        /// - accepted_at_index cannot be filled at all currently<br/>
        /// <see href="https://docs.chia.net/offer-rpc#get_all_offers"/>
        /// </remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<OfferFiles> GetAllOffers_Async(GetAllOffers_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_all_offers", rpc.ToString());
            ActionResult<OfferFiles> deserializationResult = OfferFiles.LoadObjectFromString(responseJson);
            OfferFiles response = new ();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
        }

        /// <summary>
        /// Gets multiple Offers for the current wallet, depending on the supplied parameters
        /// </summary>
        /// <remarks>
        /// Technical Limitations:<br/>
        /// - accepted_at_time can only be filled if you accepted the offer<br/>
        /// - accepted_at_index cannot be filled at all currently<br/>
        /// <see href="https://docs.chia.net/offer-rpc#get_all_offers"/>
        /// </remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public OfferFiles GetAllOffers_Sync(GetAllOffers_RPC rpc)
        {
            Task<OfferFiles> data = Task.Run(() => GetAllOffers_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Get the details of an Offer
        /// </summary>
        /// <remarks>
        /// Technical Limitations:<br/>
        /// - accepted_at_time can only be filled if you accepted the offer<br/>
        /// - accepted_at_index cannot be filled at all currently<br/>
        /// <see href="https://docs.chia.net/offer-rpc#get_offer"/>
        /// </remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<OfferFile> GetOffer_Async(GetOffer_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_offer", rpc.ToString());
            ActionResult<OfferFile> deserializationResult = OfferFile.LoadObjectFromString(responseJson);
            OfferFile response = new ();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
        }

        /// <summary>
        /// Get the details of an Offer
        /// </summary>
        /// <remarks>
        /// Technical Limitations:<br/>
        /// - accepted_at_time can only be filled if you accepted the offer<br/>
        /// - accepted_at_index cannot be filled at all currently<br/>
        /// <see href="https://docs.chia.net/offer-rpc#get_offer"/>
        /// </remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public OfferFile GetOffer_Sync(GetOffer_RPC rpc)
        {
            Task<OfferFile> data = Task.Run(() => GetOffer_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Returns the summary of a specific Offer. Works for Offers in any state
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/offer-rpc#get_offer_summary"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetOfferSummary_Response> GetOfferSummary_Async(GetOfferSummary_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_offer_summary", rpc.ToString());
            ActionResult<GetOfferSummary_Response> deserializationResult = GetOfferSummary_Response.LoadResponseFromString(responseJson);
            GetOfferSummary_Response response = new ();

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
        /// Returns the summary of a specific Offer. Works for Offers in any state
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/offer-rpc#get_offer_summary"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetOfferSummary_Response GetOfferSummary_Sync(GetOfferSummary_RPC rpc)
        {
            Task<GetOfferSummary_Response> data = Task.Run(() => GetOfferSummary_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Takes (accepts) a specific Offer, with a given fee
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/offer-rpc#take_offer"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<TradeRecord_Response> TakeOffer_Async(TakeOffer_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("take_offer", rpc.ToString());
            ActionResult<TradeRecord_Response> deserializationResult = TradeRecord_Response.LoadResponseFromString(responseJson);
            TradeRecord_Response response = new ();

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
        /// Takes (accepts) a specific Offer, with a given fee
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/offer-rpc#take_offer"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public TradeRecord_Response TakeOffer_Sync(TakeOffer_RPC rpc)
        {
            Task<TradeRecord_Response> data = Task.Run(() => TakeOffer_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// this function takes a spend bundle, which is returned from NftMintNFT.
        /// it converts the coin into a coin ID which can be used to draft an NftGetInfo_Requests.
        /// 
        /// </summary>
        /// <param name="offer">spend bundle, which is retiurned from NftMintNFT</param>
        /// <returns>It returns the NFT Info of the nft which was/is to be minted.</returns>
        public async Task<OfferFile> CreateOfferForIds_Async(Offer_RPC offer)
        {
            string response = await SendCustomMessage_Async("create_offer_for_ids", offer.ToString());
            ActionResult<OfferFile> deserializationResult = OfferFile.LoadObjectFromString(response);
            OfferFile offerFile = new ();

            if (deserializationResult.Data != null)
            {
                offerFile = deserializationResult.Data;
            }
            else
            {
                offerFile.RawContent = deserializationResult.RawJson;
            }
            return offerFile;
        }
        /// <summary>
        /// this function takes a spend bundle, which is returned from NftMintNFT.
        /// it converts the coin into a coin ID which can be used to draft an NftGetInfo_Requests.
        /// 
        /// </summary>
        /// <param name="offer">spend bundle, which is retiurned from NftMintNFT</param>
        /// <returns>It returns the NFT Info of the nft which was/is to be minted.</returns>
        public OfferFile CreateOfferForIds_Sync(Offer_RPC offer)
        {
            Task<OfferFile> data = Task.Run(() => CreateOfferForIds_Async(offer));
            data.Wait();
            return data.Result;
        }
    }
}
