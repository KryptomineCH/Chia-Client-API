using Chia_Client_API.ChiaClient_NS;
using CHIA_RPC.General_NS;
using CHIA_RPC.Objects_NS;

namespace Chia_Client_API.WalletAPI_NS
{
    public abstract partial class WalletRpcBase : ChiaEndpointRouteBase
    {
        /// <summary>
        /// fetches all offers and builds two dictionaries from it:<br/>
        /// - Offers organized by confirmation / execution height<br/>
        /// - offers organized by involved asset ids (in offered and requested)
        /// </summary>
        /// <param name="startHeight"></param>
        /// <returns></returns>
        /// <exception cref="Exception">unable to fetch offer files</exception>
        /// <exception cref="InvalidDataException">
        /// trade records supplied by chia client are not the same lenght than offers (should not happen)
        /// </exception>
        public async Task<
            (Dictionary<ulong?, List<OfferFile>> OffersOrganizedByHeight,  Dictionary<string, List<OfferFile>> OffersOrganizedByAssetID)>
            FetchAndOrganizeOffers(ulong startHeight, Dictionary<string, List<Transaction_DictMemos>>? transactionsByAssetID = null )
        {
            // Fetch all offers
            GetAllOffers_RPC getAllOffers_RPC = new GetAllOffers_RPC(
                start: startHeight,
                end: long.MaxValue,
                exclude_my_offers: false,
                exclude_taken_offers: false,
                include_completed: true,
                sort_key: null, // "created_at_time",
                reverse: true,
                file_contents: true);

            OfferFiles offerFiles = await GetAllOffers_Async(getAllOffers_RPC);
            if (!offerFiles.success)
            {
                throw new Exception(offerFiles.error);
            }

            // Initialize dictionaries
#nullable disable
            var allOffersByBlock = new Dictionary<ulong?, List<OfferFile>>();
#nullable restore
            var allOffersByAssetIDValue = new Dictionary<string, List<OfferFile>>();

            if (offerFiles.offers is not null && offerFiles.trade_records is not null)
            {
                if (offerFiles.offers.Length != offerFiles.trade_records.Length)
                {
                    throw new InvalidDataException("The trade record length does not match the offers length!");
                }

                for (int i = 0; i < offerFiles.offers.Length; i++)
                {

                    OfferFile file = new OfferFile
                    {
                        offer = offerFiles.offers[i],
                        trade_record = offerFiles.trade_records[i]
                    };
                    // confirmed at is null (hopefully legacy) try to fix
                    if (transactionsByAssetID is not null && (file.trade_record.confirmed_at_index is null || file.trade_record.confirmed_at_index == 0)
                        && (file.trade_record.status == TradeStatus.CONFIRMED || file.trade_record.status == TradeStatus.CANCELLED))
                    {
                        // TODO: MERGE INFO
                        // get involved asset IDs
                        List<string> assetIDs = new List<string>();
                        foreach(var id in file.trade_record.summary.offered)
                            assetIDs.Add(id.Key);
                        foreach (var id in file.trade_record.summary.requested)
                            assetIDs.Add(id.Key);
                        // get involved Coins
                        HashSet<Coin> coinsOfInterest = new HashSet<Coin>(file.trade_record.coins_of_interest);
                        // Check if coin is contained
                        List<Transaction_DictMemos> foundtransactions = new List<Transaction_DictMemos>();
                        ulong confirmedAtHeight = 0;
                        foreach(string assetId in assetIDs)
                        {
                            foreach(Transaction_DictMemos transaction in transactionsByAssetID[assetId])
                            {
                                HashSet<Coin> removals = new HashSet<Coin>(transaction.removals);
                                HashSet<Coin> additions = new HashSet<Coin>(transaction.additions);
                                if (removals.Overlaps(coinsOfInterest) || additions.Overlaps(coinsOfInterest))
                                {
                                    // REMOVE OVERLAPPING COINS from coinsOfInterest
                                    // Remove overlapping coins from coinsOfInterest
                                    coinsOfInterest.ExceptWith(removals);
                                    coinsOfInterest.ExceptWith(additions);
                                    foundtransactions.Add(transaction);
                                    if (confirmedAtHeight == 0)
                                    {
                                        confirmedAtHeight = transaction.confirmed_at_height.Value;
                                    }
                                    else if (confirmedAtHeight != transaction.confirmed_at_height.Value)
                                    {
                                        // this can happen, if the coin was added first and then removed
                                        throw new InvalidOperationException("uh oh, the height is different!");
                                    }
                                }
                            }
                        }
                    }

                    // Add to allOffersByBlock (transaction height)
                    if (allOffersByBlock.TryGetValue(file.trade_record.confirmed_at_index.Value, out var offerListBlock))
                        offerListBlock.Add(file);
                    else
                        allOffersByBlock[file.trade_record.confirmed_at_index.Value] = new List<OfferFile> { file };

                    // Generate identifiers and add to allOffersByAssetIDValue
                    var identifiers = new List<string>();
                    foreach (var offered in file.trade_record.summary.offered)
                        identifiers.Add(offered.Value + offered.Key);
                    foreach (var requested in file.trade_record.summary.requested)
                        identifiers.Add(requested.Value + requested.Key);

                    foreach (var identifier in identifiers)
                    {
                        if (allOffersByAssetIDValue.TryGetValue(identifier, out var offerListAssetID))
                            offerListAssetID.Add(file);
                        else
                            allOffersByAssetIDValue[identifier] = new List<OfferFile> { file };
                    }
                }
            }

            return (allOffersByBlock, allOffersByAssetIDValue);
        }

    }
}
