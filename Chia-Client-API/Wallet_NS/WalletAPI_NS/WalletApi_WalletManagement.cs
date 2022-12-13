using Chia_Client_API.Wallet_NS.WalletApiResponses_NS;
using System.Text;
using System.Text.Json;

namespace Chia_Client_API.Wallet_NS.WalletAPI_NS
{
    public static partial class WalletApi
    {
        /// <summary>
        /// returns all subwallets of the currently logged in wallet
        /// </summary>
        /// <returns></returns>
        public async static Task<get_wallets_info> GetWallets(bool includeData = false)
        {
            string response = await SendCustomMessage("get_wallets", "{\"include_data\":"+includeData+"}");
            get_wallets_info json = JsonSerializer.Deserialize<get_wallets_info>(response);
            return json;
        }
        /// <summary>
        /// create a new subwallet
        /// </summary>
        /// <returns></returns>
        public async static Task<create_new_wallet_info> CreateNewCatWallet(
            ulong amount = 1, string name = "", ulong networkFeeMojos = 1000)
        {
            StringBuilder rpc = new StringBuilder();
            rpc.Append('{');
            rpc.Append("{\"wallet_type\": \"cat_wallet\", \"mode\": \"new\"");
            if (name != "") rpc.Append(", \"name\": " + name);
            rpc.Append(", \"amount\": " + amount);
            rpc.Append(", \"fee\": " + networkFeeMojos);
            rpc.Append("}");
            string response = await SendCustomMessage("create_new_wallet ", rpc.ToString());
            create_new_wallet_info json = JsonSerializer.Deserialize<create_new_wallet_info>(response);
            return json;
        }
        public async static Task<create_new_wallet_info> ModifyCatWallet(
            string assetID, string name = "", ulong networkFeeMojos = 1000)
        {
            StringBuilder rpc = new StringBuilder();
            rpc.Append('{');
            rpc.Append("{\"wallet_type\": \"cat_wallet\", \"mode\": \"existing\"");
            rpc.Append(", \"asset_id\": " + assetID);
            if (name != "") rpc.Append(", \"name\": "+name);
            rpc.Append(", \"fee\": " + networkFeeMojos);
            rpc.Append("}");
            string response = await SendCustomMessage("create_new_wallet ", rpc.ToString());
            create_new_wallet_info json = JsonSerializer.Deserialize<create_new_wallet_info>(response);
            return json;
        }
        public async static Task<create_new_wallet_info> CreateNewDidWallet(
            bool isRecoverydid = false,
            ulong amount = 1, string name = "", ulong networkFeeMojos = 1000)
        {
            throw new NotImplementedException("not yet implemented due to uncertainties in documentation");
            StringBuilder rpc = new StringBuilder();
            rpc.Append('{');
            rpc.Append("{\"wallet_type\": \"did_wallet\"");
            if (isRecoverydid)
            {
                rpc.Append(", \"mode\": \"recovery\"\"");
            }
            else
            {
                rpc.Append(", \"mode\": \"new\"\"");
            }
            if (name != "") rpc.Append(", \"name\": ");
            rpc.Append(", \"amount\": " + amount);
            rpc.Append(", \"fee\": " + networkFeeMojos);
            rpc.Append("}");
            string response = await SendCustomMessage("create_new_wallet ", rpc.ToString());
            create_new_wallet_info json = JsonSerializer.Deserialize<create_new_wallet_info>(response);
            return json;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">eg My NFT WAllet</param>
        /// <param name="didID">eg did:chia:1ypvxg7t327m4hsmgzrlhnuk4448nqc20crnnmzzd52lk7dvdza9s8qp8q6</param>
        /// <param name="networkFeeMojos"> eg 1000</param>
        /// <returns></returns>
        public async static Task<create_new_wallet_info> CreateNewNftWallet(
            string name = "", string didID = "", ulong networkFeeMojos = 1000)
        {
            StringBuilder rpc = new StringBuilder();
            rpc.Append('{');
            rpc.Append("{\"wallet_type\": \"nft_wallet\"");
            if (name != "") rpc.Append(", \"name\": "+name);
            if (didID != "") rpc.Append(", \"did_id\": "+didID);
            rpc.Append(", \"fee\": " + networkFeeMojos);
            rpc.Append("}");
            string response = await SendCustomMessage("create_new_wallet ", rpc.ToString());
            create_new_wallet_info json = JsonSerializer.Deserialize<create_new_wallet_info>(response);
            return json;
        }
    }
}
