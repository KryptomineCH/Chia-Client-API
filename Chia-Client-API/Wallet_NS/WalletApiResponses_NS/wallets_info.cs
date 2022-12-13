using Chia_Client_API.Objects_NS;

namespace Chia_Client_API.Wallet_NS.WalletApiResponses_NS
{
    public class wallets_info
    {
        public string data { get; set; }
        public ulong id { get; set; }
        public string name { get; set; }
        public WalletType type { get; set; }
    }
}
