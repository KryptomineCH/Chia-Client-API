namespace Chia_Client_API.Wallet_NS.WalletApiResponses_NS
{
    public class get_network_info
    {
        /// <summary>
        /// eg mainnet or testnet
        /// </summary>
        public string network_name { get; set; }
        /// <summary>
        /// eg xch or txch
        /// </summary>
        public string network_prefix { get; set; }
        public bool success { get; set; }
        public string error { get; set; }
    }
}
