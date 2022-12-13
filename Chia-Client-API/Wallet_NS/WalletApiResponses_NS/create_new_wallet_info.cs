using Chia_Client_API.Objects_NS;

namespace Chia_Client_API.Wallet_NS.WalletApiResponses_NS
{
    public class create_new_wallet_info
    {
        public string asset_id { get; set; }
        public bool success { get; set; }
        public WalletType type { get; set; }
        public ulong wallet_id { get; set; }
        public string error { get; set; }
    }
}
