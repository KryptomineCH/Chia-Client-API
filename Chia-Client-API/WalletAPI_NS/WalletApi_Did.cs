using CHIA_RPC.General_NS;
using CHIA_RPC.Wallet_NS.DID_NS;
using CHIA_RPC.Wallet_NS.WalletManagement_NS;
using System.Text.Json;

namespace Chia_Client_API.WalletAPI_NS
{
    public partial class Wallet_RPC_Client
    {
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
            CreateNewDIDWallet_Response deserializedObject = JsonSerializer.Deserialize<CreateNewDIDWallet_Response>(responseJson);
            return deserializedObject;
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
        /// Set the name of a DID wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_set_wallet_name"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<WalletID_Response> DidSetWalletName_Async(DidSetWalletName_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("did_set_wallet_name", rpc.ToString());
            WalletID_Response deserializedObject = JsonSerializer.Deserialize<WalletID_Response>(responseJson);
            return deserializedObject;
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
        /// Given a DID wallet's ID, retrieve the name of that wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_get_wallet_name"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<DidGetWalletName_Response> DidGetWalletName_Async(WalletID_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("did_get_wallet_name", rpc.ToString());
            DidGetWalletName_Response deserializedObject = JsonSerializer.Deserialize<DidGetWalletName_Response>(responseJson);
            return deserializedObject;
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
        /// Append one or more IDs to be used for recovery of a DID wallet. The current list can be obtained with the did_get_recovery_list endpoint
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_update_recovery_ids"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<Success_Response> DidUpdateRecoveryIDs_Async(DidUpdateRecoveryIDs_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("did_update_recovery_ids", rpc.ToString());
            Success_Response deserializedObject = JsonSerializer.Deserialize<Success_Response>(responseJson);
            return deserializedObject;
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
        /// Update the metadata for a DID wallet. The current metadata can be obtained with the did_get_metadata endpoint
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_update_metadata"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<Success_Response> DidUpdateMetadata_Async(DidUpdateMetadata_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("did_update_metadata", rpc.ToString());
            Success_Response deserializedObject = JsonSerializer.Deserialize<Success_Response>(responseJson);
            return deserializedObject;
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
        /// Get the public key for a DID
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_get_pubkey"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<DidGetPubkey_Response> DidGetPubkey_Async(WalletID_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("did_get_pubkey", rpc.ToString());
            DidGetPubkey_Response deserializedObject = JsonSerializer.Deserialize<DidGetPubkey_Response>(responseJson);
            return deserializedObject;
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
        /// Fetch the my_did and coin_id (if applicable) settings for a given wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_get_did"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<DidGetDid_Response> DidGetDid_Async(WalletID_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("did_get_did", rpc.ToString());
            DidGetDid_Response deserializedObject = JsonSerializer.Deserialize<DidGetDid_Response>(responseJson);
            return deserializedObject;
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
        /// Recover a DID to a new DID by using an attest file
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_recovery_spend"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<SpendBundle_Response> DidRecoverySpend_Async(DidRecoverySpend_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("did_recovery_spend", rpc.ToString());
            SpendBundle_Response deserializedObject = JsonSerializer.Deserialize<SpendBundle_Response>(responseJson);
            return deserializedObject;
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
        /// For a given wallet, fetch the recovery list, as well as the number of IDs required for recovery
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_get_recovery_list"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<DidGetRecoveryList_Response> DidGetRecoveryList_Async(WalletID_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("did_get_recovery_list", rpc.ToString());
            DidGetRecoveryList_Response deserializedObject = JsonSerializer.Deserialize<DidGetRecoveryList_Response>(responseJson);
            return deserializedObject;
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
        ///  Fetch the metadata for a given wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_get_metadata"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<DidGetMetadata_Response> DidGetMetadata_Async(WalletID_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("did_get_metadata", rpc.ToString());
            DidGetMetadata_Response deserializedObject = JsonSerializer.Deserialize<DidGetMetadata_Response>(responseJson);
            return deserializedObject;
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
        /// Create an attest for a DID, to be used for recovery. This command will output the attest data, which can then be added or redirected to a file
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_create_attest"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<DidCreateAttest_Response> DidCreateAttest_Async(DidCreateAttest_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("did_create_attest", rpc.ToString());
            DidCreateAttest_Response deserializedObject = JsonSerializer.Deserialize<DidCreateAttest_Response>(responseJson);
            return deserializedObject;
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
        /// Display all relevant information needed to recover a given DID. This RPC must be called on a DID wallet that was created with "did_type":"recovery".
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_get_information_needed_for_recovery"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<DidGetInformationNeededForRecovery_Response> DidGetInformationNeededForRecovery_Async(WalletID_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("did_get_information_needed_for_recovery", rpc.ToString());
            DidGetInformationNeededForRecovery_Response deserializedObject = JsonSerializer.Deserialize<DidGetInformationNeededForRecovery_Response>(responseJson);
            return deserializedObject;
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
        /// Get the current coin info (parent coin, puzzle hash, amount) for a DID wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_get_current_coin_info"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<DidGetCurrentCoinInfo_Response> DidGetCurrentCoinInfo_Async(WalletID_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("did_get_current_coin_info", rpc.ToString());
            DidGetCurrentCoinInfo_Response deserializedObject = JsonSerializer.Deserialize<DidGetCurrentCoinInfo_Response>(responseJson);
            return deserializedObject;
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
        /// Output the backup data of a DID wallet's metadata. This output can then be saved or redirected to a file
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_create_backup_file"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<DidCreateBackupFile_Response> DidCreateBackupFile_Async(WalletID_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("did_create_backup_file", rpc.ToString());
            DidCreateBackupFile_Response deserializedObject = JsonSerializer.Deserialize<DidCreateBackupFile_Response>(responseJson);
            return deserializedObject;
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
        /// Transfer a DID
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_transfer_did"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<DidTransferDid_Response> DidTransferDid_Async(DidTransferDid_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("did_transfer_did", rpc.ToString());
            DidTransferDid_Response deserializedObject = JsonSerializer.Deserialize<DidTransferDid_Response>(responseJson);
            return deserializedObject;
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
        /// Generate a spend bundle for a DID wallet to send a message (this RPC does not modify the blockchain)
        /// </summary>/
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_message_spend"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<DidMessageSpend_Response> DidMessageSpend_Async(DidMessageSpend_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("did_message_spend", rpc.ToString());
            DidMessageSpend_Response deserializedObject = JsonSerializer.Deserialize<DidMessageSpend_Response>(responseJson);
            return deserializedObject;
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
        /// Obtain info from a DID wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_get_info"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<DidGetInfo_Response> DidGetInfo_Async(DidGetInfo_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("did_get_info", rpc.ToString());
            DidGetInfo_Response deserializedObject = JsonSerializer.Deserialize<DidGetInfo_Response>(responseJson);
            return deserializedObject;
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
        /// Recover a missing or unspendable DID wallet by submitting a coin id of the DID
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/did-rpc/#did_find_lost_did"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<DidFindLostDid_Response> DidFindLostDid_Async(DidFindLostDid_RPC rpc)
        {
            string responseJson = await SendCustomMessage_Async("did_find_lost_did", rpc.ToString());
            DidFindLostDid_Response deserializedObject = JsonSerializer.Deserialize<DidFindLostDid_Response>(responseJson);
            return deserializedObject;
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
        /// returns an array with all did wallets associated to the currently logged in fingerprint
        /// </summary>
        /// <returns></returns>
        public async Task<Wallets_info[]> DidGetWallets_Async()
        {
            List<Wallets_info> foundDidWallets = new List<Wallets_info>();
            GetWallets_Response wallets = await GetWallets_Async();
            foreach (Wallets_info x in wallets.wallets)
            {
                if (x.type == CHIA_RPC.Objects_NS.WalletType.did_wallet) foundDidWallets.Add(x);
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
            GetWallets_Response wallets = await GetWallets_Async();
            foreach (Wallets_info x in wallets.wallets)
            {
                foundDidWallets.Add(await DidGetDid_Async(x));
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
