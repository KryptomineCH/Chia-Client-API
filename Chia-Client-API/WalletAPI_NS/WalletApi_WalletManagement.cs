﻿using Chia_Client_API.ChiaClient_NS;
using Chia_Client_API.Helpers_NS;
using CHIA_RPC.HelperFunctions_NS;
using CHIA_RPC.Objects_NS;
using CHIA_RPC.Wallet_NS.CATsAndTrading_NS;
using CHIA_RPC.Wallet_NS.DID_NS;
using CHIA_RPC.Wallet_NS.NFT_NS;
using CHIA_RPC.Wallet_NS.WalletManagement_NS;

namespace Chia_Client_API.WalletAPI_NS
{
    public abstract partial class WalletRpcBase : ChiaEndpointRouteBase
    {
        /// <summary>
        /// create a new cat subwallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#create_new_wallet"/></remarks>
        /// <returns></returns>
        public async Task<CreateNewWallet_Response> CreateNewCatWallet_Async(CreateNewCatWallet_RPC createNewCatWallet_RPC)
        {
            string responseJson = await SendCustomMessageAsync("create_new_wallet", createNewCatWallet_RPC.ToString());
            ActionResult<CreateNewWallet_Response> deserializationResult = CreateNewWallet_Response.LoadResponseFromString(responseJson,IncludeRawServerResponse);
            CreateNewWallet_Response response = new ();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
        }

        /// <summary>
        /// create a new cat subwallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#create_new_wallet"/></remarks>
        /// <returns></returns>
        public CreateNewWallet_Response CreateNewCatWallet_Sync(CreateNewCatWallet_RPC createNewCatWallet_RPC)
        {
            Task<CreateNewWallet_Response> data = Task.Run(() => CreateNewCatWallet_Async(createNewCatWallet_RPC));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// create a new digital identity<br/><br/>
        /// NOTE: Because backup_dids is required, you must already have access to a DID in order to run this RPC for a did_wallet.
        /// If you do not already have a DID, then run the CLI command to create a DID wallet instead.
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#create_new_wallet"/></remarks>
        /// <param name="createNewDIDWallet_RPC"></param>
        /// <returns></returns>
        public async Task<CreateNewWallet_Response> CreateNewDidWallet_Async(CreateNewDIDWallet_RPC createNewDIDWallet_RPC)
        {
            string responseJson = await SendCustomMessageAsync("create_new_wallet", createNewDIDWallet_RPC.ToString());
            ActionResult<CreateNewWallet_Response> deserializationResult = CreateNewWallet_Response.LoadResponseFromString(responseJson,IncludeRawServerResponse);
            CreateNewWallet_Response response = new ();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
        }

        /// <summary>
        /// create a new digital identity<br/><br/>
        /// NOTE: Because backup_dids is required, you must already have access to a DID in order to run this RPC for a did_wallet.
        /// If you do not already have a DID, then run the CLI command to create a DID wallet instead.
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#create_new_wallet"/></remarks>
        /// <param name="createNewDIDWallet_RPC"></param>
        /// <returns></returns>
        public CreateNewWallet_Response CreateNewDidWallet_Sync(CreateNewDIDWallet_RPC createNewDIDWallet_RPC)
        {
            Task<CreateNewWallet_Response> data = Task.Run(() => CreateNewDidWallet_Async(createNewDIDWallet_RPC));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// create a new nft wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#create_new_wallet"/></remarks>
        /// <param name="createNewNFTWallet_RPC">eg My NFT WAllet</param>
        /// <returns></returns>
        public async Task<CreateNewWallet_Response> CreateNewNftWallet_Async(CreateNewNFTWallet_RPC createNewNFTWallet_RPC)
        {
            string responseJson = await SendCustomMessageAsync("create_new_wallet", createNewNFTWallet_RPC.ToString());
            ActionResult<CreateNewWallet_Response> deserializationResult = CreateNewWallet_Response.LoadResponseFromString(responseJson,IncludeRawServerResponse);
            CreateNewWallet_Response response = new ();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
        }

        /// <summary>
        /// create a new nft wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#create_new_wallet"/></remarks>
        /// <param name="createNewNFTWallet_RPC">eg My NFT WAllet</param>
        /// <returns></returns>
        public CreateNewWallet_Response CreateNewNftWallet_Sync(CreateNewNFTWallet_RPC createNewNFTWallet_RPC)
        {
            Task<CreateNewWallet_Response> data = Task.Run(() => CreateNewNftWallet_Async(createNewNFTWallet_RPC));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// returns all subwallets of the currently logged in wallet<br/><br/>
        /// Note:  if you want to obtain all main wallets, use <see cref="GetPublicKeys_Async"/> instead
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_wallets"/></remarks>
        /// <param name="rpc">Set to true to include all coin info for this wallet</param>
        /// <returns></returns>
        public async Task<GetWallets_Response> GetWallets_Async(GetWallets_RPC rpc)
        {
            string responseJson = await SendCustomMessageAsync("get_wallets", rpc.ToString());
            ActionResult<GetWallets_Response> deserializationResult = GetWallets_Response.LoadResponseFromString(responseJson,IncludeRawServerResponse);
            GetWallets_Response response = new ();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
        }

        /// <summary>
        /// returns all subwallets of the currently logged in wallet<br/><br/>
        /// Note:  if you want to obtain all main wallets, use <see cref="GetPublicKeys_Sync"/> instead
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_wallets"/></remarks>
        /// <param name="rpc">Set to true to include all coin info for this wallet</param>
        /// <returns></returns>
        public GetWallets_Response GetWallets_Sync(GetWallets_RPC rpc)
        {
            Task<GetWallets_Response> data = Task.Run(() => GetWallets_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// returns all subwallets of the currently logged in wallet<br/><br/>
        /// Note:  if you want to obtain all main wallets, use <see cref="GetPublicKeys_Async"/> instead
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_wallets"/></remarks>
        /// <param name="includeData">Set to true to include all coin info for this wallet</param>
        /// <param name="wallet_type">The type of wallet to retrieve. If included, must be one of cat_wallet, did_wallet, nft_wallet, or pool_wallet</param>
        /// <returns></returns>
        public async Task<GetWallets_Response> GetWallets_Async(bool includeData = true, WalletType? wallet_type = null)
        {
            string? type;
            if (wallet_type == null)
            {
                type = null;
            }
            else
            {
                type = wallet_type.Value.ToString();
            }
            GetWallets_RPC rpc = new() { include_data = includeData, type = type };
            return await GetWallets_Async(rpc);
        }
        /// <summary>
        /// returns all subwallets of the currently logged in wallet<br/><br/>
        /// Note:  if you want to obtain all main wallets, use <see cref="GetPublicKeys_Sync"/> instead
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_wallets"/></remarks>
        /// <param name="includeData">Set to true to include all coin info for this wallet</param>
        /// <param name="wallet_type">The type of wallet to retrieve. If included, must be one of cat_wallet, did_wallet, nft_wallet, or pool_wallet</param>
        /// <returns></returns>
        public GetWallets_Response GetWallets_Sync(bool includeData = true, WalletType? wallet_type = null)
        {
            Task<GetWallets_Response> data = Task.Run(() => GetWallets_Async(includeData, wallet_type));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// modify a cat wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#create_new_wallet"/></remarks>
        /// <param name="modifyCatWallet_RPC"></param>
        /// <returns></returns>
        public async Task<CreateNewWallet_Response> ModifyCatWallet_Async(ModifyCatWallet_RPC modifyCatWallet_RPC)
        {
            string responseJson = await SendCustomMessageAsync("create_new_wallet ", modifyCatWallet_RPC.ToString());
            ActionResult<CreateNewWallet_Response> deserializationResult = CreateNewWallet_Response.LoadResponseFromString(responseJson,IncludeRawServerResponse);
            CreateNewWallet_Response response = new ();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
        }

        /// <summary>
        /// modify a cat wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#create_new_wallet"/></remarks>
        /// <param name="modifyCatWallet_RPC"></param>
        /// <returns></returns>
        public CreateNewWallet_Response ModifyCatWallet_Sync(ModifyCatWallet_RPC modifyCatWallet_RPC)
        {
            Task<CreateNewWallet_Response> data = Task.Run(() => ModifyCatWallet_Async(modifyCatWallet_RPC));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// returns the corresponding wallet
        /// </summary>
        /// <param name="name"></param>
        /// <param name="caseSensitive"></param>
        /// <param name="walletType"></param>
        /// <returns></returns>
        public Wallets_info? GetWalletByName(string name, bool caseSensitive = false, WalletType? walletType = null)
        {
            GetWallets_RPC rpc = new(true,walletType);
            GetWallets_Response? response = GetWallets_Sync(rpc);
            if (response == null || response.wallets == null)
            {
                return null;
            }
            string nameLookup = name;
            if (!caseSensitive) nameLookup = nameLookup.ToLower();
            foreach (Wallets_info wallet in response.wallets)
            {
                string? walletName = wallet.name;
                if (!string.IsNullOrEmpty(walletName))
                {
                    if (!caseSensitive) walletName = walletName.ToLower();
                }
                if (nameLookup == walletName) return wallet;
            }
            return null;
        }
    }
}
