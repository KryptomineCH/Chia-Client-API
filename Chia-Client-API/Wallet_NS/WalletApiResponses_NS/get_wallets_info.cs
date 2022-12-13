namespace Chia_Client_API.Wallet_NS.WalletApiResponses_NS
{
    public class get_wallets_info
    {
        public ulong fingerprint { get; set; }
        public bool success { get; set; }
        public wallets_info[] wallets { get; set; }
        public string error { get; set; }
    }
}
