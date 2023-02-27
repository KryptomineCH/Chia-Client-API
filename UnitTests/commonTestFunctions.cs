using CHIA_RPC.Wallet_RPC_NS.Wallet_NS;
using CHIA_RPC.Wallet_RPC_NS.WalletManagement_NS;

namespace UnitTests
{
    internal static class CommonTestFunctions
    {
        public static string TestAdress
        {
            get
            {
                return GetAdress();
            }
        }

        private static string GetAdress(ulong walletId = 1)
        {
            GetNextAddress_RPC same_address = new GetNextAddress_RPC
            {
                new_address = false,
                wallet_id = walletId
            };
            GetNextAddress_Response adressResponse = Testnet.Wallet_Client.GetNextAddress_Async(same_address).Result;
            return adressResponse.address.ToString();
        }
        public static Wallets_info TestDidWallet
        {
            get
            {
                GetWallets_Response wallets = Testnet.Wallet_Client.GetWallets_Async().Result;
                foreach (var wallet in wallets.wallets)
                {
                    if (wallet.type == CHIA_RPC.Objects_NS.WalletType.did_wallet)
                    {
                        return wallet;
                    }
                        
                }
                return null;
            }
        }
        public static Wallets_info TestNftWallet
        {
            get
            {
                GetWallets_Response wallets = Testnet.Wallet_Client.GetWallets_Async().Result;
                foreach (var wallet in wallets.wallets)
                {
                    if (wallet.type == CHIA_RPC.Objects_NS.WalletType.nft_wallet)
                    {
                        return wallet;
                    }

                }
                return null;
            }
        }
    }
}
