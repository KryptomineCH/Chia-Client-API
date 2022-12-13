namespace Chia_Client_API.Wallet_NS.WalletApiResponses_NS
{
    public class get_sync_status
    {
        public bool genesis_initialized { get; set; }
        public bool success { get; set; }
        public bool synced { get; set; }
        public bool syncing { get; set; }
        public string error { get; set; }
    }
}
