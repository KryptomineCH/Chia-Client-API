using CHIA_RPC.Wallet_RPC_NS.WalletManagement_NS;
using System.Text.Json;

namespace Chia_Client_API.Wallet_NS.WalletAPI_NS
{
    public static partial class WalletApi
    {
        /// <summary>
        /// returns all subwallets of the currently logged in wallet
        /// </summary>
        /// <param name="includeData">Set to true to include all coin info for this wallet</param>
        /// <returns></returns>
        public async static Task<GetWallets_Response> GetWallets(bool includeData = true)
        {
            GetWallets_RPC rpc = new GetWallets_RPC { include_data= includeData };
            string response = await SendCustomMessage("get_wallets", rpc.ToString());
            GetWallets_Response json = JsonSerializer.Deserialize<GetWallets_Response>(response);
            return json;
        }
        /// <summary>
        /// create a new subwallet
        /// </summary>
        /// <returns></returns>
        public async static Task<CreateNewWallet_Response> CreateNewCatWallet(CreateNewCatWallet_RPC createNewCatWallet_RPC)
        {
            string response = await SendCustomMessage("create_new_wallet ", createNewCatWallet_RPC.ToString());
            CreateNewWallet_Response json = JsonSerializer.Deserialize<CreateNewWallet_Response>(response);
            return json;
        }
        public async static Task<CreateNewWallet_Response> ModifyCatWallet(ModifyCatWallet_RPC modifyCatWallet_RPC)
        {
            string response = await SendCustomMessage("create_new_wallet ", modifyCatWallet_RPC.ToString());
            CreateNewWallet_Response json = JsonSerializer.Deserialize<CreateNewWallet_Response>(response);
            return json;
        }
        public async static Task<CreateNewWallet_Response> CreateNewDidWallet(CreateNewDIDWallet_RPC createNewDIDWallet_RPC)
        {
            string response = await SendCustomMessage("create_new_wallet ", createNewDIDWallet_RPC.ToString());
            CreateNewWallet_Response json = JsonSerializer.Deserialize<CreateNewWallet_Response>(response);
            return json;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">eg My NFT WAllet</param>
        /// <param name="didID">eg did:chia:1ypvxg7t327m4hsmgzrlhnuk4448nqc20crnnmzzd52lk7dvdza9s8qp8q6</param>
        /// <param name="networkFeeMojos"> eg 1000</param>
        /// <returns></returns>
        public async static Task<CreateNewWallet_Response> CreateNewNftWallet(CreateNewNFTWallet_RPC createNewNFTWallet_RPC)
        {
            string response = await SendCustomMessage("create_new_wallet ", createNewNFTWallet_RPC.ToString());
            CreateNewWallet_Response json = JsonSerializer.Deserialize<CreateNewWallet_Response>(response);
            return json;
        }
    }
}
