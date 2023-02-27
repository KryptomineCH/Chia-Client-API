using CHIA_RPC.Wallet_RPC_NS.WalletManagement_NS;
using System.Text.Json;

namespace Chia_Client_API.WalletAPI_NS
{
    public partial class Wallet_RPC_Client
    {
        /// <summary>
        /// returns all subwallets of the currently logged in wallet
        /// </summary>
        /// <remarks>
        /// if you want to obtain all main wallets, use `GetPublicKeys` instead
        /// </remarks>
        /// <param name="includeData">Set to true to include all coin info for this wallet</param>
        /// <returns></returns>
        public async Task<GetWallets_Response> GetWallets_Async(bool includeData = true)
        {
            GetWallets_RPC rpc = new GetWallets_RPC { include_data= includeData };
            string response = await SendCustomMessage_Async("get_wallets", rpc.ToString());
            GetWallets_Response json = JsonSerializer.Deserialize<GetWallets_Response>(response);
            return json;
        }
        /// <summary>
        /// returns all subwallets of the currently logged in wallet
        /// </summary>
        /// <remarks>
        /// if you want to obtain all main wallets, use `GetPublicKeys` instead
        /// </remarks>
        /// <param name="includeData">Set to true to include all coin info for this wallet</param>
        /// <returns></returns>
        public GetWallets_Response GetWallets_Sync(bool includeData = true)
        {
            Task<GetWallets_Response> data = Task.Run(() => GetWallets_Async(includeData));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// create a new subwallet
        /// </summary>
        /// <returns></returns>
        public async Task<CreateNewWallet_Response> CreateNewCatWallet_Async(CreateNewCatWallet_RPC createNewCatWallet_RPC)
        {
            string response = await SendCustomMessage_Async("create_new_wallet ", createNewCatWallet_RPC.ToString());
            CreateNewWallet_Response json = JsonSerializer.Deserialize<CreateNewWallet_Response>(response);
            return json;
        }
        /// <summary>
        /// create a new subwallet
        /// </summary>
        /// <returns></returns>
        public CreateNewWallet_Response CreateNewCatWallet_Sync(CreateNewCatWallet_RPC createNewCatWallet_RPC)
        {
            Task<CreateNewWallet_Response> data = Task.Run(() => CreateNewCatWallet_Async(createNewCatWallet_RPC));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// modify a cat wallet
        /// </summary>
        /// <param name="modifyCatWallet_RPC"></param>
        /// <returns></returns>
        public async Task<CreateNewWallet_Response> ModifyCatWallet_Async(ModifyCatWallet_RPC modifyCatWallet_RPC)
        {
            string response = await SendCustomMessage_Async("create_new_wallet ", modifyCatWallet_RPC.ToString());
            CreateNewWallet_Response json = JsonSerializer.Deserialize<CreateNewWallet_Response>(response);
            return json;
        }
        /// <summary>
        /// modify a cat wallet
        /// </summary>
        /// <param name="modifyCatWallet_RPC"></param>
        /// <returns></returns>
        public CreateNewWallet_Response ModifyCatWallet_Sync(ModifyCatWallet_RPC modifyCatWallet_RPC)
        {
            Task<CreateNewWallet_Response> data = Task.Run(() => ModifyCatWallet_Async(modifyCatWallet_RPC));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// create a new digital identity
        /// </summary>
        /// <param name="createNewDIDWallet_RPC"></param>
        /// <returns></returns>
        public async Task<CreateNewWallet_Response> CreateNewDidWallet_Async(CreateNewDIDWallet_RPC createNewDIDWallet_RPC)
        {
            string response = await SendCustomMessage_Async("create_new_wallet ", createNewDIDWallet_RPC.ToString());
            CreateNewWallet_Response json = JsonSerializer.Deserialize<CreateNewWallet_Response>(response);
            return json;
        }
        /// <summary>
        /// create a new digital identity
        /// </summary>
        /// <param name="createNewDIDWallet_RPC"></param>
        /// <returns></returns>
        public CreateNewWallet_Response CreateNewDidWallet_Sync(CreateNewDIDWallet_RPC createNewDIDWallet_RPC)
        {
            Task<CreateNewWallet_Response> data = Task.Run(() => CreateNewDidWallet_Async(createNewDIDWallet_RPC));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">eg My NFT WAllet</param>
        /// <param name="didID">eg did:chia:1ypvxg7t327m4hsmgzrlhnuk4448nqc20crnnmzzd52lk7dvdza9s8qp8q6</param>
        /// <param name="networkFeeMojos"> eg 1000</param>
        /// <returns></returns>
        public async Task<CreateNewWallet_Response> CreateNewNftWallet_Async(CreateNewNFTWallet_RPC createNewNFTWallet_RPC)
        {
            string response = await SendCustomMessage_Async("create_new_wallet ", createNewNFTWallet_RPC.ToString());
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
        public CreateNewWallet_Response CreateNewNftWallet_Sync(CreateNewNFTWallet_RPC createNewNFTWallet_RPC)
        {
            Task<CreateNewWallet_Response> data = Task.Run(() => CreateNewNftWallet_Async(createNewNFTWallet_RPC));
            data.Wait();
            return data.Result;
        }
    }
}
