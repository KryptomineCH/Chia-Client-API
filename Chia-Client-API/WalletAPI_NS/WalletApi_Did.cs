using CHIA_RPC.General_NS;
using CHIA_RPC.HelperFunctions_NS;
using CHIA_RPC.Wallet_NS.DID_NS;
using CHIA_RPC.Wallet_NS.WalletManagement_NS;

namespace Chia_Client_API.WalletAPI_NS
{
    public partial class Wallet_RPC_Client
    {
        /// <summary>
        /// Create an attest for a DID, to be used for recovery. This command will output the attest data, which can then be added or redirected to a file
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_create_attest"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<DidCreateAttest_Response> DidCreateAttest_Async(DidCreateAttest_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("did_create_attest", rpc.ToString());
            ActionResult<DidCreateAttest_Response> deserializationResult = DidCreateAttest_Response.LoadResponseFromString(responseJson);
            DidCreateAttest_Response response = new DidCreateAttest_Response();

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
        /// Create an attest for a DID, to be used for recovery. This command will output the attest data, which can then be added or redirected to a file
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_create_attest"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public DidCreateAttest_Response DidCreateAttest_Sync(DidCreateAttest_RPC rpc)
        {
            Task<DidCreateAttest_Response> data = Task.Run(() => DidCreateAttest_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Output the backup data of a DID wallet's metadata. This output can then be saved or redirected to a file
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_create_backup_file"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<DidCreateBackupFile_Response> DidCreateBackupFile_Async(WalletID_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("did_create_backup_file", rpc.ToString());
            ActionResult<DidCreateBackupFile_Response> deserializationResult = DidCreateBackupFile_Response.LoadResponseFromString(responseJson);
            DidCreateBackupFile_Response response = new DidCreateBackupFile_Response();

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
        /// Output the backup data of a DID wallet's metadata. This output can then be saved or redirected to a file
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_create_backup_file"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public DidCreateBackupFile_Response DidCreateBackupFile_Sync(WalletID_RPC rpc)
        {
            Task<DidCreateBackupFile_Response> data = Task.Run(() => DidCreateBackupFile_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Recover a missing or unspendable DID wallet by submitting a coin id of the DID
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_find_lost_did"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<DidFindLostDid_Response> DidFindLostDid_Async(DidFindLostDid_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("did_find_lost_did", rpc.ToString());
            ActionResult<DidFindLostDid_Response> deserializationResult = DidFindLostDid_Response.LoadResponseFromString(responseJson);
            DidFindLostDid_Response response = new DidFindLostDid_Response();

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
        /// Recover a missing or unspendable DID wallet by submitting a coin id of the DID
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_find_lost_did"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public DidFindLostDid_Response DidFindLostDid_Sync(DidFindLostDid_RPC rpc)
        {
            Task<DidFindLostDid_Response> data = Task.Run(() => DidFindLostDid_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Get the current coin info (parent coin, puzzle hash, amount) for a DID wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_get_current_coin_info"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<DidGetCurrentCoinInfo_Response> DidGetCurrentCoinInfo_Async(WalletID_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("did_get_current_coin_info", rpc.ToString());
            ActionResult<DidGetCurrentCoinInfo_Response> deserializationResult = DidGetCurrentCoinInfo_Response.LoadResponseFromString(responseJson);
            DidGetCurrentCoinInfo_Response response = new DidGetCurrentCoinInfo_Response();

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
        /// Get the current coin info (parent coin, puzzle hash, amount) for a DID wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_get_current_coin_info"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public DidGetCurrentCoinInfo_Response DidGetCurrentCoinInfo_Sync(WalletID_RPC rpc)
        {
            Task<DidGetCurrentCoinInfo_Response> data = Task.Run(() => DidGetCurrentCoinInfo_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Fetch the my_did and coin_id (if applicable) settings for a given wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_get_did"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<DidGetDid_Response> DidGetDid_Async(WalletID_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("did_get_did", rpc.ToString());
            ActionResult<DidGetDid_Response> deserializationResult = DidGetDid_Response.LoadResponseFromString(responseJson);
            DidGetDid_Response response = new DidGetDid_Response();

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
        /// Fetch the my_did and coin_id (if applicable) settings for a given wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_get_did"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public DidGetDid_Response DidGetDid_Sync(WalletID_RPC rpc)
        {
            Task<DidGetDid_Response> data = Task.Run(() => DidGetDid_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Obtain info from a DID wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_get_info"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<DidGetInfo_Response> DidGetInfo_Async(DidGetInfo_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("did_get_info", rpc.ToString());
            ActionResult<DidGetInfo_Response> deserializationResult = DidGetInfo_Response.LoadResponseFromString(responseJson);
            DidGetInfo_Response response = new DidGetInfo_Response();

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
        /// Obtain info from a DID wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_get_info"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public DidGetInfo_Response DidGetInfo_Sync(DidGetInfo_RPC rpc)
        {
            Task<DidGetInfo_Response> data = Task.Run(() => DidGetInfo_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Display all relevant information needed to recover a given DID. This RPC must be called on a DID wallet that was created with "did_type":"recovery".
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_get_information_needed_for_recovery"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<DidGetInformationNeededForRecovery_Response> DidGetInformationNeededForRecovery_Async(WalletID_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("did_get_information_needed_for_recovery", rpc.ToString());
            ActionResult<DidGetInformationNeededForRecovery_Response> deserializationResult = DidGetInformationNeededForRecovery_Response.LoadResponseFromString(responseJson);
            DidGetInformationNeededForRecovery_Response response = new DidGetInformationNeededForRecovery_Response();

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
        /// Display all relevant information needed to recover a given DID. This RPC must be called on a DID wallet that was created with "did_type":"recovery".
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_get_information_needed_for_recovery"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public DidGetInformationNeededForRecovery_Response DidGetInformationNeededForRecovery_Sync(WalletID_RPC rpc)
        {
            Task<DidGetInformationNeededForRecovery_Response> data = Task.Run(() => DidGetInformationNeededForRecovery_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        ///  Fetch the metadata for a given wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_get_metadata"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<DidGetMetadata_Response> DidGetMetadata_Async(WalletID_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("did_get_metadata", rpc.ToString());
            ActionResult<DidGetMetadata_Response> deserializationResult = DidGetMetadata_Response.LoadResponseFromString(responseJson);
            DidGetMetadata_Response response = new DidGetMetadata_Response();

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
        ///  Fetch the metadata for a given wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_get_metadata"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public DidGetMetadata_Response DidGetMetadata_Sync(WalletID_RPC rpc)
        {
            Task<DidGetMetadata_Response> data = Task.Run(() => DidGetMetadata_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Get the public key for a DID
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_get_pubkey"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<DidGetPubkey_Response> DidGetPubkey_Async(WalletID_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("did_get_pubkey", rpc.ToString());
            ActionResult<DidGetPubkey_Response> deserializationResult = DidGetPubkey_Response.LoadResponseFromString(responseJson);
            DidGetPubkey_Response response = new DidGetPubkey_Response();

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
        /// Get the public key for a DID
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_get_pubkey"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public DidGetPubkey_Response DidGetPubkey_Sync(WalletID_RPC rpc)
        {
            Task<DidGetPubkey_Response> data = Task.Run(() => DidGetPubkey_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// For a given wallet, fetch the recovery list, as well as the number of IDs required for recovery
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_get_recovery_list"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<DidGetRecoveryList_Response> DidGetRecoveryList_Async(WalletID_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("did_get_recovery_list", rpc.ToString());
            ActionResult<DidGetRecoveryList_Response> deserializationResult = DidGetRecoveryList_Response.LoadResponseFromString(responseJson);
            DidGetRecoveryList_Response response = new DidGetRecoveryList_Response();

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
        /// For a given wallet, fetch the recovery list, as well as the number of IDs required for recovery
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_get_recovery_list"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public DidGetRecoveryList_Response DidGetRecoveryList_Sync(WalletID_RPC rpc)
        {
            Task<DidGetRecoveryList_Response> data = Task.Run(() => DidGetRecoveryList_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Given a DID wallet's ID, retrieve the name of that wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_get_wallet_name"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<DidGetWalletName_Response> DidGetWalletName_Async(WalletID_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("did_get_wallet_name", rpc.ToString());
            ActionResult<DidGetWalletName_Response> deserializationResult = DidGetWalletName_Response.LoadResponseFromString(responseJson);
            DidGetWalletName_Response response = new DidGetWalletName_Response();

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
        /// Given a DID wallet's ID, retrieve the name of that wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_get_wallet_name"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public DidGetWalletName_Response DidGetWalletName_Sync(WalletID_RPC rpc)
        {
            Task<DidGetWalletName_Response> data = Task.Run(() => DidGetWalletName_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Generate a spend bundle for a DID wallet to send a message (this RPC does not modify the blockchain)
        /// </summary>/
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_message_spend"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<DidMessageSpend_Response> DidMessageSpend_Async(DidMessageSpend_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("did_message_spend", rpc.ToString());
            ActionResult<DidMessageSpend_Response> deserializationResult = DidMessageSpend_Response.LoadResponseFromString(responseJson);
            DidMessageSpend_Response response = new DidMessageSpend_Response();

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
        /// Generate a spend bundle for a DID wallet to send a message (this RPC does not modify the blockchain)
        /// </summary>/
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_message_spend"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public DidMessageSpend_Response DidMessageSpend_Sync(DidMessageSpend_RPC rpc)
        {
            Task<DidMessageSpend_Response> data = Task.Run(() => DidMessageSpend_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Recover a DID to a new DID by using an attest file
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_recovery_spend"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<SpendBundle_Response> DidRecoverySpend_Async(DidRecoverySpend_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("did_recovery_spend", rpc.ToString());
            ActionResult<SpendBundle_Response> deserializationResult = SpendBundle_Response.LoadResponseFromString(responseJson);
            SpendBundle_Response response = new SpendBundle_Response();

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
        /// Recover a DID to a new DID by using an attest file
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_recovery_spend"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public SpendBundle_Response DidRecoverySpend_Sync(DidRecoverySpend_RPC rpc)
        {
            Task<SpendBundle_Response> data = Task.Run(() => DidRecoverySpend_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Set the name of a DID wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_set_wallet_name"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<WalletID_Response> DidSetWalletName_Async(DidSetWalletName_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("did_set_wallet_name", rpc.ToString());
            ActionResult<WalletID_Response> deserializationResult = WalletID_Response.LoadResponseFromString(responseJson);
            WalletID_Response response = new WalletID_Response();

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
        /// Set the name of a DID wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_set_wallet_name"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public WalletID_Response DidSetWalletName_Sync(DidSetWalletName_RPC rpc)
        {
            Task<WalletID_Response> data = Task.Run(() => DidSetWalletName_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Update the metadata for a DID wallet. The current metadata can be obtained with the did_get_metadata endpoint
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_update_metadata"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<Success_Response> DidUpdateMetadata_Async(DidUpdateMetadata_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("did_update_metadata", rpc.ToString());
            ActionResult<Success_Response> deserializationResult = Success_Response.LoadResponseFromString(responseJson);
            Success_Response response = new Success_Response();

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
        /// Update the metadata for a DID wallet. The current metadata can be obtained with the did_get_metadata endpoint
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_update_metadata"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public Success_Response DidUpdateMetadata_Sync(DidUpdateMetadata_RPC rpc)
        {
            Task<Success_Response> data = Task.Run(() => DidUpdateMetadata_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Append one or more IDs to be used for recovery of a DID wallet. The current list can be obtained with the did_get_recovery_list endpoint
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_update_recovery_ids"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<Success_Response> DidUpdateRecoveryIDs_Async(DidUpdateRecoveryIDs_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("did_update_recovery_ids", rpc.ToString());
            ActionResult<Success_Response> deserializationResult = Success_Response.LoadResponseFromString(responseJson);
            Success_Response response = new Success_Response();

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
        /// Append one or more IDs to be used for recovery of a DID wallet. The current list can be obtained with the did_get_recovery_list endpoint
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_update_recovery_ids"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public Success_Response DidUpdateRecoveryIDs_Sync(DidUpdateRecoveryIDs_RPC rpc)
        {
            Task<Success_Response> data = Task.Run(() => DidUpdateRecoveryIDs_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Create a new DID wallet (From the Chia wallet RPC endpoint)<br/>
        /// Note: This is part of the wallet RPC API. It is included here to document the only way in which to create a new DID with an RPC. 
        /// Because backup_dids is required, you must already have access to a DID in order to run this RPC. 
        /// If you do not already have a DID, then run the CLI command to create a DID wallet instead.
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#create_new_wallet"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<CreateNewDIDWallet_Response> CreateNewWallet_Async(CreateNewDIDWallet_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("create_new_wallet", rpc.ToString());
            ActionResult<CreateNewDIDWallet_Response> deserializationResult = CreateNewDIDWallet_Response.LoadResponseFromString(responseJson);
            CreateNewDIDWallet_Response response = new CreateNewDIDWallet_Response();

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
        /// Create a new DID wallet (From the Chia wallet RPC endpoint)<br/>
        /// Note: This is part of the wallet RPC API. It is included here to document the only way in which to create a new DID with an RPC. 
        /// Because backup_dids is required, you must already have access to a DID in order to run this RPC. 
        /// If you do not already have a DID, then run the CLI command to create a DID wallet instead.
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#create_new_wallet"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public CreateNewDIDWallet_Response CreateNewWallet_Sync(CreateNewDIDWallet_RPC rpc)
        {
            Task<CreateNewDIDWallet_Response> data = Task.Run(() => CreateNewWallet_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Transfer a DID
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_transfer_did"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<DidTransferDid_Response> DidTransferDid_Async(DidTransferDid_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("did_transfer_did", rpc.ToString());
            ActionResult<DidTransferDid_Response> deserializationResult = DidTransferDid_Response.LoadResponseFromString(responseJson);
            DidTransferDid_Response response = new DidTransferDid_Response();

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
        /// Transfer a DID
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_transfer_did"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public DidTransferDid_Response DidTransferDid_Sync(DidTransferDid_RPC rpc)
        {
            Task<DidTransferDid_Response> data = Task.Run(() => DidTransferDid_Async(rpc));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// returns an array with all did wallets associated to the currently logged in fingerprint
        /// </summary>
        /// <returns></returns>
        public async Task<Wallets_info[]> DidGetWallets_Async()
        {
            GetWallets_Response? walletsResponse = await GetWallets_Async();
            List<Wallets_info> foundDidWallets = new List<Wallets_info>();

            if (walletsResponse != null && walletsResponse.wallets != null)
            {
                foreach (Wallets_info wallet in walletsResponse.wallets)
                {
                    if (wallet.type == CHIA_RPC.Objects_NS.WalletType.DECENTRALIZED_ID)
                    {
                        foundDidWallets.Add(wallet);
                    }
                }
            }

            return foundDidWallets.ToArray();
        }

        /// <summary>
        /// returns an array with all did wallets associated to the currently logged in fingerprint
        /// </summary>
        /// <returns></returns>
        public Wallets_info[] DidGetWallets_Sync()
        {
            Task<Wallets_info[]> data = Task.Run(() => DidGetWallets_Async());
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// returns did ids and coin ids for all did wallets, matches the structure of DidGetWallets so both arrays can be matched
        /// </summary>
        /// <returns></returns>
        public async Task<DidGetDid_Response[]> DidGetAllDids_Async()
        {
            List<DidGetDid_Response> foundDidWallets = new List<DidGetDid_Response>();
            Wallets_info[] wallets = await DidGetWallets_Async();

            if (wallets != null)
            {
                foreach (Wallets_info wallet in wallets)
                {
                    foundDidWallets.Add(await DidGetDid_Async(wallet));
                }
            }

            return foundDidWallets.ToArray();
        }

        /// <summary>
        /// returns did ids and coin ids for all did wallets, matches the structure of DidGetWallets so both arrays can be matched
        /// </summary>
        /// <returns></returns>
        public DidGetDid_Response[] DidGetAllDids_Sync()
        {
            Task<DidGetDid_Response[]> data = Task.Run(() => DidGetAllDids_Async());
            data.Wait();
            return data.Result;
        }
    }
}
