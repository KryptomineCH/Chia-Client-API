namespace Chia_Client_API.Wallet_NS.WalletApiResponses_NS
{
    public class check_delete_key
    {
        public ulong fingerprint { get; set; }
        public bool success { get; set; }
        public bool used_for_farmer_rewards { get; set; }
        public bool used_for_pool_rewards { get; set; }
        public bool wallet_balance { get; set; }
        public string error { get; set; }
    }
}
