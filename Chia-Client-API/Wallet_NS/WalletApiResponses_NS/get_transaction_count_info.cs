namespace Chia_Client_API.Wallet_NS.WalletApiResponses_NS
{
    public class get_transaction_count_info
    {
        public ulong count { get; set; }
        public bool success { get; set; }
        public ulong wallet_id { get; set; }
        public string error { get; set; }
    }
}
