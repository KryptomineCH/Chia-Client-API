using CHIA_RPC.General_NS;
using CHIA_RPC.Wallet_NS.KeyManagement;
using System.Text.Json;

namespace Chia_Client_API.WalletAPI_NS
{
    public partial class Wallet_RPC_Client
    {
        /// <summary>
        /// Add a new key (wallet/fingerprint) from a given 24 word mnemonic seed phrase
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#add_key"/></remarks>
        /// <param name="mnemonic">24 word mnemonic passphrase</param>
        /// <returns></returns>
        public async Task<FingerPrint_Response?> AddKey_Async(AddKey_RPC mnemonic)
        {
            string responseJson = await SendCustomMessage_Async("add_key", mnemonic.ToString());
            FingerPrint_Response? deserializedObject = FingerPrint_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Add a new key (wallet/fingerprint) from a given 24 word mnemonic seed phrase
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#add_key"/></remarks>
        /// <param name="mnemonic">24 word mnemonic passphrase</param>
        /// <returns></returns>
        public FingerPrint_Response? AddKey_Sync(AddKey_RPC mnemonic)
        {
            Task<FingerPrint_Response?> data = Task.Run(() => AddKey_Async(mnemonic));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Display whether a fingerprint has a balance, and whether it is used for farming or pool rewards. 
        /// This is helpful when determining whether it is safe to delete a key without first backing it up
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#check_delete_key"/></remarks>
        /// <param name="checkDeleteKey_RPC"></param>
        /// <returns></returns>
        public async Task<CheckDeleteKey_Response?> CheckDeleteKey_Async(CheckDeleteKey_RPC checkDeleteKey_RPC)
        {
            string responseJson = await SendCustomMessage_Async("check_delete_key", checkDeleteKey_RPC.ToString());
            CheckDeleteKey_Response? deserializedObject = CheckDeleteKey_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Display whether a fingerprint has a balance, and whether it is used for farming or pool rewards. 
        /// This is helpful when determining whether it is safe to delete a key without first backing it up
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#check_delete_key"/></remarks>
        /// <param name="fingerprint"></param>
        /// <returns></returns>
        public CheckDeleteKey_Response? CheckDeleteKey_Sync(CheckDeleteKey_RPC fingerprint)
        {
            Task<CheckDeleteKey_Response?> data = Task.Run(() => CheckDeleteKey_Async(fingerprint));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Delete all keys from the wallet<br/><br/>
        /// WARNING: MAKE SURE YOU ARE SAFE!
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#delete_all_keys"/></remarks>
        /// <param name="I_AM_SURE">you need to supply yes, for security. Otherwise an exception is thrown</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">you are not sure if this is what you really want to do</exception>
        public async Task<Success_Response?> DeleteAllKeys_Async(bool I_AM_SURE = false)
        {
            if (!I_AM_SURE)
            {
                throw new ArgumentException("WARNING: This deletes ALL keys! If this is really what you want to do, pleas set I_AM_SURE to true!");
            }
            string responseJson = await SendCustomMessage_Async("delete_all_keys");
            Success_Response? deserializedObject = Success_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Delete all keys from the wallet<br/><br/>
        /// WARNING: MAKE SURE YOU ARE SAFE!
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#delete_all_keys"/></remarks>
        /// <param name="I_AM_SURE">you need to supply yes, for security. Otherwise an exception is thrown</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">you are not sure if this is what you really want to do</exception>
        public Success_Response? DeleteAllKeys_Sync(bool I_AM_SURE = false)
        {
            Task<Success_Response?> data = Task.Run(() => DeleteAllKeys_Async(I_AM_SURE));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Remove a key, based on its wallet fingerprint<br/><br/>
        /// NOTE: this does not delete a key from the blockchain. You can re-add it witn the 24 word mnemonic
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#delete_key"/></remarks>
        /// <param name="fingerprint">The wallet's fingerprint, obtainable by running chia wallet show</param>
        /// <returns></returns>
        public async Task<Success_Response?> DeleteKey_Async(FingerPrint_RPC fingerprint)
        {
            string responseJson = await SendCustomMessage_Async("delete_key", fingerprint.ToString());
            Success_Response? deserializedObject = Success_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Remove a key, based on its wallet fingerprint<br/><br/>
        /// NOTE: this does not delete a key from the blockchain. You can re-add it witn the 24 word mnemonic
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#delete_key"/></remarks>
        /// <param name="fingerprint">The wallet's fingerprint, obtainable by running chia wallet show</param>
        /// <returns></returns>
        public void DeleteKey_Sync(FingerPrint_RPC fingerprint)
        {
            Task data = Task.Run(() => DeleteKey_Async(fingerprint));
            data.Wait();
        }

        /// <summary>
        /// Generates a random 24-word mnemonic seed phrase / wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#generate_mnemonic"/></remarks>
        /// <returns>24 word mnemonic</returns>
        public async Task<GenerateMnemonic_Response?> GenerateMnemonic_Async()
        {
            string responseJson = await SendCustomMessage_Async("generate_mnemonic");
            GenerateMnemonic_Response? deserializedObject = GenerateMnemonic_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Generates a random 24-word mnemonic seed phrase / wallet
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#generate_mnemonic"/></remarks>
        /// <returns>24 word mnemonic</returns>
        public GenerateMnemonic_Response? GenerateMnemonic_Sync()
        {
            Task<GenerateMnemonic_Response?> data = Task.Run(() => GenerateMnemonic_Async());
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Obtain the fingerprint of the wallet that is currently logged in
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_logged_in_fingerprint"/></remarks>
        /// <returns></returns>
        public async Task<FingerPrint_Response?> GetLoggedInFingerprint_Async()
        {
            string responseJson = await SendCustomMessage_Async("get_logged_in_fingerprint");
            FingerPrint_Response? deserializedObject = FingerPrint_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Obtain the fingerprint of the wallet that is currently logged in
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_logged_in_fingerprint"/></remarks>
        /// <returns></returns>
        public FingerPrint_Response? GetLoggedInFingerprint_Sync()
        {
            Task<FingerPrint_Response?> data = Task.Run(() => GetLoggedInFingerprint_Async());
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Show public and private info about a key<br/><br/>
        /// WARNING: This RPC will show the private key and seed phrase for the given fingerprint. Use with caution.
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_private_key"/></remarks>
        /// <param name="fingerprint">The wallet's fingerprint, obtainable by running chia wallet show</param>
        /// <returns></returns>
        public async Task<GetPrivateKey_Response?> GetPrivateKey_Async(FingerPrint_RPC fingerprint)
        {
            string responseJson = await SendCustomMessage_Async("get_private_key", fingerprint.ToString());
            GetPrivateKey_Response? deserializedObject = GetPrivateKey_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Show public and private info about a key<br/><br/>
        /// WARNING: This RPC will show the private key and seed phrase for the given fingerprint. Use with caution.
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_private_key"/></remarks>
        /// <param name="fingerprint">The wallet's fingerprint, obtainable by running chia wallet show</param>
        /// <returns></returns>
        public GetPrivateKey_Response? GetPrivateKey_Sync(FingerPrint_RPC fingerprint)
        {
            Task<GetPrivateKey_Response?> data = Task.Run(() => GetPrivateKey_Async(fingerprint));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Show all public key fingerprints (main wallets) stored in the OS keyring. <br/><br/>
        /// NOTE: that the keyring must be unlocked in order to run this RPC
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_public_keys"/></remarks>
        /// <returns></returns>
        public async Task<GetPublicKeys_Response?> GetPublicKeys_Async()
        {
            string responseJson = await SendCustomMessage_Async("get_public_keys");
            GetPublicKeys_Response? deserializedObject = GetPublicKeys_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Show all public key fingerprints (main wallets) stored in the OS keyring.<br/><br/>
        /// NOTE: that the keyring must be unlocked in order to run this RPC
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_public_keys"/></remarks>
        /// <returns></returns>
        public GetPublicKeys_Response? GetPublicKeys_Sync()
        {
            Task<GetPublicKeys_Response?> data = Task.Run(() => GetPublicKeys_Async());
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Log into the wallet with the specified key
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#log_in"/></remarks>
        /// <param name="fingerprint">The wallet's fingerprint, obtainable by running chia wallet show</param>
        /// <returns>the logged in fingerprint</returns>
        public async Task<FingerPrint_Response?> LogIn_Async(FingerPrint_RPC fingerprint)
        {
            string responseJson = await SendCustomMessage_Async("log_in", fingerprint.ToString());
            FingerPrint_Response? deserializedObject = FingerPrint_Response.LoadResponseFromString(responseJson);
            return deserializedObject;
        }
        /// <summary>
        /// Log into the wallet with the specified key
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#log_in"/></remarks>
        /// <param name="fingerprint">The wallet's fingerprint, obtainable by running chia wallet show</param>
        /// <returns>the logged in fingerprint</returns>
        public FingerPrint_Response? LogIn_Sync(FingerPrint_RPC fingerprint)
        {
            Task<FingerPrint_Response?> data = Task.Run(() => LogIn_Async(fingerprint));
            data.Wait();
            return data.Result;
        }
    }
}
