﻿using Chia_Client_API.Helpers_NS;
using CHIA_RPC.General_NS;
using CHIA_RPC.Objects_NS;
using CHIA_RPC.Wallet_NS.CATsAndTrading_NS;
using System.Text.Json;

namespace Chia_Client_API.WalletAPI_NS
{
    public partial class Wallet_RPC_Client
    {
        /// <summary>
        /// Cancel all offers, with the option to cancel only offers for a specific asset class
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#cancel_offers"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<Success_Response?> CancelOffers_Async(CancelCatOffers_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("cancel_offers", rpc.ToString());
            Success_Response? deserializedObject = Success_Response.LoadResponseFromString(responseJson);
            if (ReportResponseErrors && !(bool)deserializedObject.success)
            {
                await ReportError.UploadFileAsync(new Error(deserializedObject, "WalletApi_CatsAndTrading", "cancel_offers"));
            }
            return deserializedObject;
        }
        /// <summary>
        /// Cancel all offers, with the option to cancel only offers for a specific asset class
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#cancel_offers"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public Success_Response? CancelOffers_Sync(CancelCatOffers_RPC rpc)
        {
            Task<Success_Response?> data = Task.Run(() => CancelOffers_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Retrieve a CAT's name from its ID
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#cat_asset_id_to_name"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<CatAssetIdToName_Response?> CatAssetIDToName_Async(CatAssetIdToName_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("cat_asset_id_to_name", rpc.ToString());
            CatAssetIdToName_Response? deserializedObject = CatAssetIdToName_Response.LoadResponseFromString(responseJson);
            if (ReportResponseErrors && !(bool)deserializedObject.success)
            {
                await ReportError.UploadFileAsync(new Error(deserializedObject, "WalletApi_CatsAndTrading", "cat_asset_id_to_name"));
            }
            return deserializedObject;
        }
        /// <summary>
        /// Retrieve a CAT's name from its ID
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#cat_asset_id_to_name"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public CatAssetIdToName_Response? CatAssetIDToName_Sync(CatAssetIdToName_RPC rpc)
        {
            Task<CatAssetIdToName_Response?> data = Task.Run(() => CatAssetIDToName_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Retrieve a the asset ID from a CAT wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#cat_get_asset_id"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<CatGetAssetId_Response?> CatGetAssetID_Async(WalletID_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("cat_get_asset_id", rpc.ToString());
            CatGetAssetId_Response? deserializedObject = CatGetAssetId_Response.LoadResponseFromString(responseJson);
            if (ReportResponseErrors && !(bool)deserializedObject.success)
            {
                await ReportError.UploadFileAsync(new Error(deserializedObject, "WalletApi_CatsAndTrading", "cat_get_asset_id"));
            }
            return deserializedObject;
        }
        /// <summary>
        /// Retrieve a the asset ID from a CAT wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#cat_get_asset_id"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public CatGetAssetId_Response? CatGetAssetID_Sync(WalletID_RPC rpc)
        {
            Task<CatGetAssetId_Response?> data = Task.Run(() => CatGetAssetID_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Get the name of a CAT associated with a wallet ID
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#cat_get_name"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<CatAssetIdToName_Response?> CatGetName_Async(WalletID_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("cat_get_name", rpc.ToString());
            CatAssetIdToName_Response? deserializedObject = CatAssetIdToName_Response.LoadResponseFromString(responseJson);
            if (ReportResponseErrors && !(bool)deserializedObject.success)
            {
                await ReportError.UploadFileAsync(new Error(deserializedObject, "WalletApi_CatsAndTrading", "cat_get_name"));
            }
            return deserializedObject;
        }
        /// <summary>
        /// Get the name of a CAT associated with a wallet ID
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#cat_get_name"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public CatAssetIdToName_Response? CatGetName_Sync(WalletID_RPC rpc)
        {
            Task<CatAssetIdToName_Response?> data = Task.Run(() => CatGetName_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Rename a CAT wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#cat_set_name"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<WalletID_Response?> CatSetName_Async(CatSetName_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("cat_set_name", rpc.ToString());
            WalletID_Response? deserializedObject = WalletID_Response.LoadResponseFromString(responseJson);
            if (ReportResponseErrors && !(bool)deserializedObject.success)
            {
                await ReportError.UploadFileAsync(new Error(deserializedObject, "WalletApi_CatsAndTrading", "cat_set_name"));
            }
            return deserializedObject;
        }
        /// <summary>
        /// Rename a CAT wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#cat_set_name"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public WalletID_Response? CatSetName_Sync(CatSetName_RPC rpc)
        {
            Task<WalletID_Response?> data = Task.Run(() => CatSetName_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Send CAT funds to another wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#cat_spend"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<CatSpend_Response?> CatSpend_Async(CatSpend_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("cat_spend", rpc.ToString());
            CatSpend_Response? deserializedObject = CatSpend_Response.LoadResponseFromString(responseJson);
            if (ReportResponseErrors && !(bool)deserializedObject.success)
            {
                await ReportError.UploadFileAsync(new Error(deserializedObject, "WalletApi_CatsAndTrading", "cat_spend"));
            }
            return deserializedObject;
        }
        /// <summary>
        /// Send CAT funds to another wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#cat_spend"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public CatSpend_Response? CatSpend_Sync(CatSpend_RPC rpc)
        {
            Task<CatSpend_Response?> data = Task.Run(() => CatSpend_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Create a new offer<br/><br/>
        /// WARNING: Due to missing documentation may be implemented incorrectly
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#create_offer_for_ids"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<OfferFile?> CreateOfferForIDs_Async(CreateOfferForIds_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("create_offer_for_ids", rpc.ToString());
            OfferFile? deserializedObject = OfferFile.LoadObjectFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Create a new offer<br/><br/>
        /// WARNING: Due to missing documentation may be implemented incorrectly
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#create_offer_for_ids"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public OfferFile? CreateOfferForIDs_Sync(CreateOfferForIds_RPC rpc)
        {
            Task<OfferFile?> data = Task.Run(() => CreateOfferForIDs_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Return the default CAT list
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_cat_list"/></remarks>
        /// <returns></returns>
        public async Task<GetCatList_Response?> GetCatList_Async()
        {
            string responseJson = await SendCustomMessage_Async("get_cat_list");
            GetCatList_Response? deserializedObject = GetCatList_Response.LoadResponseFromString(responseJson);
            if (ReportResponseErrors && !(bool)deserializedObject.success)
            {
                await ReportError.UploadFileAsync(new Error(deserializedObject, "WalletApi_CatsAndTrading", "get_cat_list"));
            }
            return deserializedObject;
        }
        /// <summary>
        /// Return the default CAT list
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_cat_list"/></remarks>
        /// <returns></returns>
        public GetCatList_Response? GetCatList_Sync()
        {
            Task<GetCatList_Response?> data = Task.Run(() => GetCatList_Async());
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Obtain the number of offers from the current wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_offers_count"/></remarks>
        /// <returns></returns>
        public async Task<GetOffersCount_Response?> GetOffersCount_Async()
        {
            string responseJson = await SendCustomMessage_Async("get_offers_count");
            GetOffersCount_Response? deserializedObject = GetOffersCount_Response.LoadResponseFromString(responseJson);
            if (ReportResponseErrors && !(bool)deserializedObject.success)
            {
                await ReportError.UploadFileAsync(new Error(deserializedObject, "WalletApi_CatsAndTrading", "get_offers_count"));
            }
            return deserializedObject;
        }
        /// <summary>
        /// Obtain the number of offers from the current wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_offers_count"/></remarks>
        /// <returns></returns>
        public GetOffersCount_Response? GetOffersCount_Sync()
        {
            Task<GetOffersCount_Response?> data = Task.Run(() => GetOffersCount_Async());
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Show a summary of an offer
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_offer_summary"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<GetCatOfferSummary_Response?> GetOfferSummary_Async(GetCatOfferSummary_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("get_offer_summary", rpc.ToString());
            GetCatOfferSummary_Response? deserializedObject = GetCatOfferSummary_Response.LoadResponseFromString(responseJson);
            if (ReportResponseErrors && !(bool)deserializedObject.success)
            {
                await ReportError.UploadFileAsync(new Error(deserializedObject, "WalletApi_CatsAndTrading", "get_offer_summary"));
            }
            return deserializedObject;
        }
        /// <summary>
        /// Show a summary of an offer
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_offer_summary"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public GetCatOfferSummary_Response? GetOfferSummary_Sync(GetCatOfferSummary_RPC rpc)
        {
            Task<GetCatOfferSummary_Response?> data = Task.Run(() => GetOfferSummary_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Get a list of all unacknowledged CATs
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_stray_cats"/></remarks>
        /// <returns></returns>
        public async Task<GetStrayCats_Response?> GetStrayCats_Async()
        {
            string responseJson = await SendCustomMessage_Async("get_stray_cats");
            GetStrayCats_Response? deserializedObject = GetStrayCats_Response.LoadResponseFromString(responseJson);
            if (ReportResponseErrors && !(bool)deserializedObject.success)
            {
                await ReportError.UploadFileAsync(new Error(deserializedObject, "WalletApi_CatsAndTrading", "get_stray_cats"));
            }
            return deserializedObject;
        }
        /// <summary>
        /// Get a list of all unacknowledged CATs
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_stray_cats"/></remarks>
        /// <returns></returns>
        public GetStrayCats_Response? GetStrayCats_Sync()
        {
            Task<GetStrayCats_Response?> data = Task.Run(() => GetStrayCats_Async());
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Take an offer
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#take_offer"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<TradeRecord_Response?> TakeOffer_Async(TakeCatOffer_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("take_offer", rpc.ToString());
            TradeRecord_Response? deserializedObject = TradeRecord_Response.LoadResponseFromString(responseJson);
            if (ReportResponseErrors && !(bool)deserializedObject.success)
            {
                await ReportError.UploadFileAsync(new Error(deserializedObject, "WalletApi_CatsAndTrading", "take_offer"));
            }
            return deserializedObject;
        }
        /// <summary>
        /// Take an offer
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#take_offer"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public TradeRecord_Response? TakeOffer_Sync(TakeCatOffer_RPC rpc)
        {
            Task<TradeRecord_Response?> data = Task.Run(() => TakeOffer_Async(rpc));
            data.Wait();
            return data.Result;
        }
    }
}
