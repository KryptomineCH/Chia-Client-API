using CHIA_API_Tests.Initialisation_NS;
using CHIA_RPC.Wallet_NS.Wallet_NS;
using CHIA_RPC.Wallet_NS.WalletManagement_NS;
using System;

namespace CHIA_API_Tests
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
            GetNextAddress_Response? adressResponse = Testnet_Wallet.Wallet_Client.GetNextAddress_Async(same_address).Result;
            if (adressResponse == null)
            {
                throw new InvalidOperationException("adressResponse is null!");
            }
            return adressResponse.address.ToString();
        }
        public static Wallets_info? TestDidWallet
        {
            get
            {
                GetWallets_Response? wallets = Testnet_Wallet.Wallet_Client.GetWallets_Async().Result;
                if (wallets == null)
                {
                    throw new InvalidOperationException("wallets is null!");
                }
                if(wallets.wallets == null)
                {
                    return null;
                }
                foreach (var wallet in wallets.wallets)
                {
                    if (wallet.type == CHIA_RPC.Objects_NS.WalletType.DECENTRALIZED_ID)
                    {
                        return wallet;
                    }
                        
                }
                return null;
            }
        }
        public static Wallets_info? TestNftWallet
        {
            get
            {
                GetWallets_Response? wallets = Testnet_Wallet.Wallet_Client.GetWallets_Async().Result;
                if (wallets == null)
                {
                    throw new InvalidOperationException("wallets is null!");
                }
                if (wallets.wallets == null)
                {
                    return null;
                }
                foreach (var wallet in wallets.wallets)
                {
                    if (wallet.type == CHIA_RPC.Objects_NS.WalletType.NFT)
                    {
                        return wallet;
                    }

                }
                return null;
            }
        }
    }
}
