using CHIA_RPC.General_NS;
using CHIA_RPC.Objects_NS;
using CHIA_RPC.Wallet_NS.CATsAndTrading_NS;
using CHIA_RPC.Wallet_NS.CustomTransactionHistoryObjects_NS;
using CHIA_RPC.Wallet_NS.Wallet_NS;
using CHIA_RPC.Wallet_NS.WalletManagement_NS;

namespace Chia_Client_API.WalletAPI_NS
{
    public partial class Wallet_RPC_Client
    {
        public async Task<CustomChiaTransactionBundle[]> BuildtransactionHistory_Async(DirectoryInfo transactionHistoryDirectory)
        {
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

            // fetch orders
            GetAllOffers_RPC getAllOffers_RPC = new GetAllOffers_RPC(
                start: heightInfo.ImportedOfferHeight.SequenceHeight,
                end: long.MaxValue,
                exclude_my_offers: false,
                exclude_taken_offers: false,
                include_completed: true,
                sort_key: null,//"created_at_time",
                reverse: true,
                file_contents: true) ;
            OfferFiles offerFiles = await GetAllOffers_Async(getAllOffers_RPC);
            if (!offerFiles.success)
            {
                throw new Exception(offerFiles.error);
            }
            List<OfferFile> allOffers = new List<OfferFile>();
            if (offerFiles.offers != null)
            {
                if (offerFiles.offers.Length != offerFiles.trade_records.Length)
                {
                    throw new InvalidDataException("the trade record length dons not match the offers length!");
                }
                
                for(int i = 0; i < offerFiles.offers.Length; i++)
                {
                    GetOffer_RPC getOffer_RPC = new GetOffer_RPC(offerFiles.trade_records[i], offerFiles.offers[i]);
                    OfferFile file = await GetOffer_Async(getOffer_RPC);
                    allOffers.Add(file);
                }
                allOffers = allOffers.OrderBy(t => t.trade_record.created_at_time).ToList();
            }

            // fetch all transactions
            List<Transaction_DictMemos> relevantTransactions = new List<Transaction_DictMemos>();
            GetWallets_Response walletsResponse = await GetWallets_Async();
            foreach (Wallets_info wallet in walletsResponse.wallets)
            {
                GetTransactions_RPC rpc = new GetTransactions_RPC(wallet.id,start: 0, end: long.MaxValue, reverse: true);
                GetTransactions_Response transactions = await GetTransactions_Async(rpc);
                foreach (Transaction_DictMemos transaction in transactions.transactions)
                {
                    if (transaction.confirmed == true)
                    {
                        relevantTransactions.Add(transaction);
                    }
                }
            }
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
                    // Create bundle
                    CustomChiaTransactionBundle bundle = new CustomChiaTransactionBundle();
                    bundle.BlockHeight = currentBlock;
                    bundle.Time = blockTransactions[0].created_at_time_dateTime.Value;
                    // work through block
                    /// transaction count == 1 -> individual transaction
                    if (blockTransactions.Count == 1)
                    {
                        Transaction_DictMemos transaction = blockTransactions[0];
                        // individual transaction
                        if (transaction.amount != 0)
                        {
                            { }
                            bundle.Type = transaction.type.Value;
                            CustomChiaTransaction customChiaTransaction = new CustomChiaTransaction();
                            if (transaction.amount > 0)
                            {
                                bundle.IncomingAssets = new[] { transaction }; 
                            }
                            else
                            {
                                bundle.OutgoingAssets = new[] { transaction };
                            }
                            if (transaction.fee_amount != 0)
                            {
                                { }
                            }

                        }
                        // fee transaction (cancellation)
                        else
                        {
                            // the transaction has 0 amount. It seems to be a cancellation/neutral transaction
                            { }
                        }
                    }
                    /// transaction count >= 1 -> possible trade / offer
                    else if (blockTransactions.Count > 1)
                    {
                        List<Transaction_DictMemos> outgoingTransactions = new List<Transaction_DictMemos> ();
                        List<Transaction_DictMemos> incomingTransactions = new List<Transaction_DictMemos> ();
                        foreach(Transaction_DictMemos transaction in blockTransactions)
                        {
                            if (transaction.type == TransactionType.INCOMING 
                                || transaction.type == TransactionType.INCOMING_TRADE
                                || transaction.type == TransactionType.COINBASE_REWARD
                                || transaction.type == TransactionType.FEE_REWARD
                                )
                            {
                                incomingTransactions.Add( transaction );
                            }
                            else if (transaction.type == TransactionType.OUTGOING
                                || transaction.type == TransactionType.OUTGOING_TRADE)
                            {
                                outgoingTransactions.Add( transaction );
                            }
                            else
                            {
                                throw new Exception("unknown transaction type!");
                            }
                        }
                        // perhaps an offer?
                        List<OfferFile> matchingOffers = new List<OfferFile> ();
                        foreach (OfferFile offerFile in allOffers)
                        {
                            List<string> offerStrings = new List<string>();
                            foreach (Coin coin in offerFile.trade_record.coins_of_interest)
                            {
                                offerStrings.Add(coin.puzzle_hash);
                            }
                            foreach (Transaction_DictMemos outgoingTransaction in outgoingTransactions)
                            {

                                foreach (Coin removal in outgoingTransaction.removals)
                                {
                                    if (offerStrings.Contains(removal.puzzle_hash))
                                    {
                                        //offerFile was found!
                                        { }
                                        if (blockTransactions.Count >= 3)
                                        {
                                            { }
                                        }
                                    }
                                }
                            }

                        }

                        
                        { }
                        // multiple individual transactions
                        // cat transaction with fee
                        // offer
                        
                    }
                    else
                    {
                        throw new Exception("blockTransactions.Count is null or ");
                    }
                    // update Info
                    blockTransactions.Clear();
                    currentBlock = relevantTransactions[counter].confirmed_at_height.Value;
                    blockTransactions.Add(relevantTransactions[counter]);
                }
                counter++;
            }
            

            // Finito
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
