using Chia_Client_API.Wallet_NS.WalletAPI_NS;
using CHIA_RPC.Wallet_RPC_NS.Wallet_NS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            GetNextAddress_Response adressResponse = WalletApi.GetNextAddress(same_address).Result;
            return adressResponse.address.ToString();
        }
    }
}
