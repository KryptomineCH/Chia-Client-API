namespace Chia_Client_API.Wallet_NS.WalletApiResponses_NS
{
    public class get_farmed_amount_info
    {
        public ulong farmed_amount { get; set; }
        public ulong farmer_reward_amount { get; set; }
        public ulong fee_amount { get; set; }
        public ulong last_height_farmed { get; set; }
        public ulong pool_reward_amount { get; set; }
        public bool success { get; set; }
        public string error { get; set; }
    }
}
