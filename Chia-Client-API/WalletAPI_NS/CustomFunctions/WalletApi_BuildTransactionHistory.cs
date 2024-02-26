using Chia_Client_API.ChiaClient_NS;
using Chia_Client_API.Helpers_NS;
using CHIA_RPC.General_NS;
using CHIA_RPC.Objects_NS;
using CHIA_RPC.Wallet_NS.CATsAndTrading_NS;
using CHIA_RPC.Wallet_NS.CustomTransactionHistoryObjects_NS;
using CHIA_RPC.Wallet_NS.Wallet_NS;
using CHIA_RPC.Wallet_NS.WalletManagement_NS;

namespace Chia_Client_API.WalletAPI_NS
{
    public partial class WalletRpcClient : WalletRpcBase
    {
        public async Task<CustomChiaTransactionBundle[]> BuildtransactionHistory_Async(DirectoryInfo transactionHistoryDirectory)
        {
            
            // Nessesary Helpers
            AssetIDCache assetIDCache = new AssetIDCache(this);
            // create directories
            _ = EnsureDirectoryExists(transactionHistoryDirectory);
            DirectoryInfo completedOffersDirectory = EnsureDirectoryExists(transactionHistoryDirectory, "completed_offers");
            DirectoryInfo incompleteOffersDirectory = EnsureDirectoryExists(transactionHistoryDirectory, "incomplete_offers");
            DirectoryInfo transactionsDirectory = EnsureDirectoryExists(transactionHistoryDirectory, "chiaTransactions");

            // get config files
            FileInfo heightInfoFile = new FileInfo(Path.Combine(transactionHistoryDirectory.FullName, "heightInfo"));
            CustomTransactionHistorySaveState heightInfo = new CustomTransactionHistorySaveState();
            if (heightInfoFile.Exists) heightInfo = CustomTransactionHistorySaveState.LoadObjectFromFile(heightInfoFile);

            // check old incomplete offers
            foreach (FileInfo incompleteOfferFile in incompleteOffersDirectory.GetFiles())
            {
                // TODO: Implement old offers checking
                throw new NotImplementedException("");
            }

            // fetch all transactions
            Dictionary<ulong, List<Transaction_DictMemos>> transactionsByBlock = new Dictionary<ulong, List<Transaction_DictMemos>>();
            Dictionary<string, List<Transaction_DictMemos>> transactionsByAssetID = new Dictionary<string, List<Transaction_DictMemos>>();
            (transactionsByBlock, transactionsByAssetID) = await FetchAndOrganizeTransactions(heightInfo.ImportedOfferHeight.SequenceHeight);

            // fetch all offers
            Dictionary<ulong?, List<OfferFile>> allOffersByBlock;
            Dictionary<string, List<OfferFile>> allOffersByAssetID;
            (allOffersByBlock, allOffersByAssetID) = await FetchAndOrganizeOffers(heightInfo.ImportedOfferHeight.SequenceHeight, transactionsByAssetID);


            List<Transaction_DictMemos> relevantTransactions = new List<Transaction_DictMemos>();
            relevantTransactions = relevantTransactions.OrderBy(t => t.confirmed_at_height).ToList();
            { }
            // Build history
            int counter = 0;
            ulong currentBlock = relevantTransactions[0].confirmed_at_height.Value;
            List<Transaction_DictMemos> blockTransactions = new List<Transaction_DictMemos>();
            List<CustomChiaTransactionBundle> transactionHistory = new List<CustomChiaTransactionBundle>();
            while (counter < relevantTransactions.Count)
            {
                if (relevantTransactions[counter].confirmed_at_height == currentBlock)
                {
                    blockTransactions.Add(relevantTransactions[counter]);
                }
                else
                {

                    // work through block
                    // possible trade / offer
                    bool IsOutgoingTransaction(Transaction_DictMemos transaction)
                    {
                        return transaction.type == TransactionType.OUTGOING ||
                               transaction.type == TransactionType.OUTGOING_TRADE;
                    }
                    if (blockTransactions.Count > 1)
                    {
                        List<Transaction_DictMemos> outgoingTransactions = new List<Transaction_DictMemos> ();
                        List<Transaction_DictMemos> incomingTransactions = new List<Transaction_DictMemos> ();
                        foreach(Transaction_DictMemos transaction in blockTransactions)
                        {
                            if (IsOutgoingTransaction(transaction))
                            {
                                outgoingTransactions.Add( transaction );
                            }
                            else
                            {
                                incomingTransactions.Add(transaction);
                            }
                        }
                        // perhaps an offer?
                        List<OfferFile> matchingOffers = new List<OfferFile> ();
                        bool foundOffer = false;
                        foreach (OfferFile offerFile in new OfferFile[] { })
                        {
                            foundOffer = false;
                            if (offerFile.trade_record.is_my_offer == null)
                            {
                                throw new MissingFieldException("could not identify if offer is own offer or foreign offer!");
                            }
                            List<string> offerStrings = new List<string>();
                            foreach (Coin coin in offerFile.trade_record.coins_of_interest)
                            {
                                offerStrings.Add(coin.parent_coin_info);
                            }
                            foreach (Transaction_DictMemos outgoingTransaction in outgoingTransactions)
                            {
                                foreach (Coin removal in outgoingTransaction.removals)
                                {
                                    if (offerStrings.Contains(removal.parent_coin_info))
                                    {
                                        // offer matches!
                                        matchingOffers.Add(offerFile);
                                        foundOffer = true;
                                        break;
                                    }
                                }
                                if (foundOffer)
                                {
                                    break;
                                }
                            }
                        }
                        if (matchingOffers.Count > outgoingTransactions.Count)
                        {
                            throw new InvalidOperationException("offer count cannot be bigger than the amount of outgoing or incoming transactions");
                        }
                        
                        // Bundle transactions with offers
                        if (matchingOffers.Count > 0)
                        {
                            foreach (OfferFile offerFile in matchingOffers)
                            {
                                if (offerFile.trade_record.status != TradeStatus.CONFIRMED)
                                {
                                    // TODO: only fetch cancellation transactions upon a cancelled offer
                                    throw new NotImplementedException("cancellations and so on are not implemented yet");
                                }
                                if (blockTransactions.Count == 0)
                                {
                                    throw new InvalidOperationException("transactionsblock is empty!");
                                }
                                CustomChiaTransactionBundle offerBundle = new CustomChiaTransactionBundle();
                                offerBundle.BlockHeight = currentBlock;
                                offerBundle.Time = blockTransactions[0].created_at_time_dateTime.Value;
                                List<Transaction_DictMemos> incomingAssets = new List<Transaction_DictMemos>();
                                List<Transaction_DictMemos> outgoingAssets = new List<Transaction_DictMemos>();
                                offerBundle.OfferFile = offerFile;
                                offerBundle.Type = CustomChiaTransactionType.Trade;
                                void BundleAssetsWithOffer(string offerAssetID, ulong offerAssetAmount, bool offerDirection_Offered)
                                {
                                    for (int i = 0; i < blockTransactions.Count; i++)
                                    {
                                        // get asset ID
                                        string assetID;
                                        if (!assetIDCache.GetOrObtainAssetID(blockTransactions[i].wallet_id.Value, out assetID))
                                        {
                                            throw new Exception(assetID);
                                        }
                                        // determine if transaction is incoming or outgoing                   
                                        if (assetID == offerAssetID
                                            && blockTransactions[i].amount == offerAssetAmount)
                                        {
                                            
                                            if (IsOutgoingTransaction(blockTransactions[i])
                                                && (bool)offerFile.trade_record.is_my_offer == offerDirection_Offered)
                                            {
                                                if (blockTransactions[i].fee_amount != offerFile.trade_record.summary.fees && assetID == "xch")
                                                {
                                                    // do not include transaction if fee is incorrect
                                                    // this also prevents duplicate fee transactions automatically
                                                    continue;
                                                }
                                                offerBundle.OutgoingAssets.Add(blockTransactions[i]);
                                            }
                                            else
                                            {
                                                offerBundle.IncomingAssets.Add(blockTransactions[i]);   
                                            }
                                            blockTransactions.RemoveAt(i);
                                            break;
                                        }
                                    }
                                }
                                foreach (var requestedAsset in offerFile.trade_record.summary.requested)
                                {
                                    BundleAssetsWithOffer(offerAssetID: requestedAsset.Key, offerAssetAmount: requestedAsset.Value, 
                                        offerDirection_Offered: false);
                                }
                                foreach (var offeredAsset in offerFile.trade_record.summary.offered)
                                {
                                    BundleAssetsWithOffer(offerAssetID: offeredAsset.Key, offerAssetAmount: offeredAsset.Value,
                                        offerDirection_Offered: true);
                                }
                                // bundle in fee transaction
                                if (offerFile.trade_record.contains_additional_fee_transaction)
                                {
                                    for (int i = 0; i < blockTransactions.Count; i++)
                                    {
                                        // determine if transaction is incoming or outgoing
                                        bool transactionIsOutgoing = IsOutgoingTransaction(blockTransactions[i]);
                                        if (transactionIsOutgoing
                                            && blockTransactions[i].wallet_id == 1
                                            && blockTransactions[i].fee_amount == offerFile.trade_record.summary.fees
                                            && blockTransactions[i].amount == 0)
                                        {
                                            offerBundle.OutgoingAssets.Add(blockTransactions[i]);
                                            blockTransactions.RemoveAt(i);
                                            break;
                                        }
                                    }
                                }
                                if (offerBundle.IncomingAssets.Count == 0 || offerBundle.OutgoingAssets.Count == 0)
                                {
                                    // todo: saerch for transacions in other Blocks if offer transactions are missing
                                    throw new InvalidOperationException("trade offer must have at least one incoming and outgoing transaction!");
                                }
                                transactionHistory.Add(offerBundle);
                            }
                        }
                        
                        { }
                                             
                    }
                    // individual transactions
                    if (blockTransactions.Count > 0)
                    {
                        List<CustomChiaTransactionBundle> feeTransactions = new List<CustomChiaTransactionBundle>();
                        // generate normal transactions and sort out the ones which have a separate fee
                        for(int i = blockTransactions.Count-1; i >= 0; i--)
                        {
                            if (blockTransactions[i].amount == 0)
                            {
                                // neutral (cancellation transactions)
                                continue;
                            }
                            // Create bundle
                            CustomChiaTransactionBundle bundle = new CustomChiaTransactionBundle();
                            bundle.BlockHeight = currentBlock;
                            bundle.Time = blockTransactions[0].created_at_time_dateTime.Value;
                            bundle.Type = CustomChiaTransactionType.Transfer;

                            if (IsOutgoingTransaction(blockTransactions[i]) && blockTransactions[i].amount != 0)
                            {
                                bundle.OutgoingAssets.Add(blockTransactions[i]);
                                // cat transaction fee
                                if (blockTransactions[i].wallet_id != 1 && blockTransactions[i].fee_amount > 0)
                                {
                                    feeTransactions.Add(bundle);
                                }
                                else
                                {
                                    transactionHistory.Add(bundle);
                                }
                            }
                            else
                            {
                                bundle.IncomingAssets.Add(blockTransactions[i]);
                                transactionHistory.Add(bundle);
                            }
                            blockTransactions.RemoveAt(i);
                        }
                        // validate result
                        for (int i = 0; i < blockTransactions.Count; i++)
                        {
                            if (blockTransactions[i].amount != 0)
                            {
                                throw new Exception("there seems to be unprocessed transactions in the transactions queue after generate normal transactions");
                            }
                        }
                        // match fee transactions
                        foreach (CustomChiaTransactionBundle bundle in feeTransactions)
                        {
                            for (int i = 0; i < blockTransactions.Count; i++)
                            {
                                // determine if transaction is incoming or outgoing
                                if (blockTransactions[i].fee_amount == bundle.OutgoingAssets[0].fee_amount)
                                {
                                    bundle.OutgoingAssets.Add(blockTransactions[i]);
                                    blockTransactions.RemoveAt(i);
                                    break;
                                }
                            }
                            transactionHistory.Add(bundle);
                        }
                        // cancellation transactions
                        for (int i = blockTransactions.Count-1; i >= 0; i--)
                        {
                            
                            CustomChiaTransactionBundle bundle = new CustomChiaTransactionBundle();
                            bundle.BlockHeight = currentBlock;
                            bundle.Time = blockTransactions[0].created_at_time_dateTime.Value;
                            bundle.Type = CustomChiaTransactionType.Neutral;
                            bundle.OutgoingAssets.Add(blockTransactions[i]);
                            transactionHistory.Add(bundle);
                            blockTransactions.RemoveAt(i);
                        }
                        { }
                    }
                    // move to next block
                    if (blockTransactions.Count > 0)
                    {
                        throw new InvalidOperationException("there are some unprocessed block transactions!");
                    }
                    currentBlock = relevantTransactions[counter].confirmed_at_height.Value;
                    blockTransactions.Add(relevantTransactions[counter]);
                }
                counter++;
            }
            

            // Finito
            return transactionHistory.ToArray();
            return null;
        }
        
        private static DirectoryInfo EnsureDirectoryExists(DirectoryInfo parentDir, string? childDir = null)
        {
            DirectoryInfo newDir = parentDir;
            if (!string.IsNullOrEmpty(childDir))
            {
                newDir = new DirectoryInfo(Path.Combine(parentDir.FullName, childDir));
            }
            if (!newDir.Exists)
            {
                newDir.Create();
            }
            return newDir;
        }
    }
}
