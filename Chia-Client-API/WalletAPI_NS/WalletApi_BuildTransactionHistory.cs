using CHIA_RPC.General_NS;
using CHIA_RPC.Objects_NS;
using CHIA_RPC.Wallet_NS.CATsAndTrading_NS;
using CHIA_RPC.Wallet_NS.CustomTransactionHistoryObjects_NS;

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
                    CheckOfferValidity_RPC checkOfferValidity_RPC = new CheckOfferValidity_RPC(file);
                    CheckOfferValidity_Response validity = await CheckOfferValidity_Async(checkOfferValidity_RPC);
                    { }
                }
            }
            
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
