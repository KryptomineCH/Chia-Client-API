
using CHIA_RPC.Objects_NS;

namespace Chia_Client_API.Wallet_NS.WalletApiResponses_NS
{
    public class get_wallet_balance_info
    {
        public bool success { get; set; }
        public wallet_balance_info wallet_balance { get; set; }
        public string error { get; set; }

    }
    public class wallet_balance_info
    {
        public ulong confirmed_wallet_balance { get; set; }
        public ulong fingerprint { get; set; }
        public ulong max_send_amount { get; set; }
        public ulong pending_change { get; set; }
        public ulong pending_coin_removal_count { get; set; }
        public ulong spendable_balance { get; set; }
        public ulong unconfirmed_wallet_balance { get; set; }
        public ulong unspent_coin_count { get; set; }
        public ulong wallet_id { get; set; }
        public WalletType wallet_type { get; set; }
    }
}
