using CHIA_RPC.General;
using CHIA_RPC.Wallet_RPC_NS.KeyManagement;
using System.Text.Json;

namespace Chia_Client_API.Wallet_NS.WalletAPI_NS
{
    public static partial class WalletApi
    {
        /// <summary>
        /// Log into the wallet with the specified key
        /// </summary>
        /// <param name="fingerprint">The wallet's fingerprint, obtainable by running chia wallet show</param>
        /// <returns>the logged in fingerprint</returns>
        public async static Task<LogIn_Response> LogIn_Async(FingerPrint_RPC fingerprint)
        {
            string response = await SendCustomMessage_Async("log_in", fingerprint.ToString());
            LogIn_Response json = JsonSerializer.Deserialize<LogIn_Response>(response);
            return json;
        }
        /// <summary>
        /// Log into the wallet with the specified key
        /// </summary>
        /// <param name="fingerprint">The wallet's fingerprint, obtainable by running chia wallet show</param>
        /// <returns>the logged in fingerprint</returns>
        public static LogIn_Response LogIn_Sync(FingerPrint_RPC fingerprint)
        {
            Task<LogIn_Response> data = Task.Run(() => LogIn_Async(fingerprint));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Obtain the fingerprint of the wallet that is currently logged in
        /// </summary>
        /// <returns></returns>
        public async static Task<LogIn_Response> GetLoggedInFingerprint_Async()
        {
            string response = await SendCustomMessage_Async("get_logged_in_fingerprint");
            LogIn_Response json = JsonSerializer.Deserialize<LogIn_Response>(response);
            return json;
        }
        /// <summary>
        /// Obtain the fingerprint of the wallet that is currently logged in
        /// </summary>
        /// <returns></returns>
        public static LogIn_Response GetLoggedInFingerprint_Sync()
        {
            Task<LogIn_Response> data = Task.Run(() => GetLoggedInFingerprint_Async());
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Show all public key fingerprints (main wallets) stored in the OS keyring. Note that the keyring must be unlocked in order to run this RPC
        /// </summary>
        /// <returns></returns>
        public async static Task<GetPublicKeys_Response> GetPublicKeys_Async()
        {
            string response = await SendCustomMessage_Async("get_public_keys");
            GetPublicKeys_Response json = JsonSerializer.Deserialize<GetPublicKeys_Response>(response);
            return json;
        }
        /// <summary>
        /// Show all public key fingerprints (main wallets) stored in the OS keyring. Note that the keyring must be unlocked in order to run this RPC
        /// </summary>
        /// <returns></returns>
        public static GetPublicKeys_Response GetPublicKeys_Sync()
        {
            Task<GetPublicKeys_Response> data = Task.Run(() => GetPublicKeys_Async());
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Show public and private info about a key
        /// </summary>
        /// <param name="fingerprint">The wallet's fingerprint, obtainable by running chia wallet show</param>
        /// <returns></returns>
        /// <remarks>This RPC will show the private key and seed phrase for the given fingerprint. Use with caution.</remarks>
        public async static Task<GetPrivateKey_Response> GetPrivateKey_Async(FingerPrint_RPC fingerprint)
        {
            string response = await SendCustomMessage_Async("get_private_key", fingerprint.ToString());
            GetPrivateKey_Response json = JsonSerializer.Deserialize<GetPrivateKey_Response>(response);
            return json;
        }
        /// <summary>
        /// Show public and private info about a key
        /// </summary>
        /// <param name="fingerprint">The wallet's fingerprint, obtainable by running chia wallet show</param>
        /// <returns></returns>
        /// <remarks>This RPC will show the private key and seed phrase for the given fingerprint. Use with caution.</remarks>
        public static GetPrivateKey_Response GetPrivateKey_Sync(FingerPrint_RPC fingerprint)
        {
            Task<GetPrivateKey_Response> data = Task.Run(() => GetPrivateKey_Async(fingerprint));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Generates a random 24-word mnemonic seed phrase / wallet
        /// </summary>
        /// <returns>24 word mnemonic</returns>
        public async static Task<GenerateMnemonic_Response> GenerateMnemonic_Async()
        {
            string response = await SendCustomMessage_Async("generate_mnemonic");
            GenerateMnemonic_Response json = JsonSerializer.Deserialize<GenerateMnemonic_Response>(response);
            return json;
        }
        /// <summary>
        /// Generates a random 24-word mnemonic seed phrase / wallet
        /// </summary>
        /// <returns>24 word mnemonic</returns>
        public static GenerateMnemonic_Response GenerateMnemonic_Sync()
        {
            Task<GenerateMnemonic_Response> data = Task.Run(() => GenerateMnemonic_Async());
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Add a new key (wallet/fingerprint) from a given 24 word mnemonic seed phrase
        /// </summary>
        /// <param name="mnemonic">24 word mnemonic passphrase</param>
        /// <returns></returns>
        public async static Task<LogIn_Response> AddKey_Async(AddKey_RPC mnemonic)
        {
            string response = await SendCustomMessage_Async("add_key", mnemonic.ToString());
            LogIn_Response json = JsonSerializer.Deserialize<LogIn_Response>(response);
            return json;
        }
        /// <summary>
        /// Add a new key (wallet/fingerprint) from a given 24 word mnemonic seed phrase
        /// </summary>
        /// <param name="mnemonic">24 word mnemonic passphrase</param>
        /// <returns></returns>
        public static LogIn_Response AddKey_Sync(AddKey_RPC mnemonic)
        {
            Task<LogIn_Response> data = Task.Run(() => AddKey_Async(mnemonic));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Remove a key, based on its wallet fingerprint
        /// </summary>
        /// <param name="fingerprint">The wallet's fingerprint, obtainable by running chia wallet show</param>
        /// <returns></returns>
        /// <remarks>this does not delete a key from the blockchain. You can re-add it witn the 24 word mnemonic</remarks>
        public async static Task<Success_Response> DeleteKey_Async(FingerPrint_RPC fingerprint)
        {
            string response = await SendCustomMessage_Async("delete_key", fingerprint.ToString());
            Success_Response json = JsonSerializer.Deserialize<Success_Response>(response);
            return json;
        }
        /// <summary>
        /// Remove a key, based on its wallet fingerprint
        /// </summary>
        /// <param name="fingerprint">The wallet's fingerprint, obtainable by running chia wallet show</param>
        /// <returns></returns>
        /// <remarks>this does not delete a key from the blockchain. You can re-add it witn the 24 word mnemonic</remarks>
        public static void DeleteKey_Sync(FingerPrint_RPC fingerprint)
        {
            Task data = Task.Run(() => DeleteKey_Async(fingerprint));
            data.Wait();
        }
        /// <summary>
        /// isplay whether a fingerprint has a balance, and whether it is used for farming or pool rewards. This is helpful when determining whether it is safe to delete a key without first backing it up
        /// </summary>
        /// <param name="checkDeleteKey_RPC"></param>
        /// <returns></returns>
        public async static Task<CheckDeleteKey_Response> CheckDeleteKey_Async(CheckDeleteKey_RPC checkDeleteKey_RPC)
        {
            string response = await SendCustomMessage_Async("check_delete_key", checkDeleteKey_RPC.ToString());
            CheckDeleteKey_Response json = JsonSerializer.Deserialize<CheckDeleteKey_Response>(response);
            return json;
        }
        /// <summary>
        /// isplay whether a fingerprint has a balance, and whether it is used for farming or pool rewards. This is helpful when determining whether it is safe to delete a key without first backing it up
        /// </summary>
        /// <param name="checkDeleteKey_RPC"></param>
        /// <returns></returns>
        public static CheckDeleteKey_Response CheckDeleteKey_Sync(CheckDeleteKey_RPC fingerprint)
        {
            Task<CheckDeleteKey_Response> data = Task.Run(() => CheckDeleteKey_Async(fingerprint));
            data.Wait();
            return data.Result;
        }
        /// <summary>
        /// Delete all keys from the wallet
        /// </summary>
        /// <param name="I_AM_SURE">you need to supply yes, for security. Otherwise an exception is thrown</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">you are not sure if this is what you really want to do</exception>
        public async static Task<Success_Response> DeleteAllKeys_Async(bool I_AM_SURE = false)
        {
            if (!I_AM_SURE)
            {
                throw new ArgumentException("WARNING: This deletes ALL keys! If this is really what you want to do, pleas set I_AM_SURE to true!");
            }
            string response = await SendCustomMessage_Async("delete_all_keys");
            Success_Response json = JsonSerializer.Deserialize<Success_Response>(response);
            return json;
        }
        /// <summary>
        /// Delete all keys from the wallet
        /// </summary>
        /// <param name="I_AM_SURE">you need to supply yes, for security. Otherwise an exception is thrown</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">you are not sure if this is what you really want to do</exception>
        public static Success_Response DeleteAllKeys_Sync(bool I_AM_SURE = false)
        {
            Task<Success_Response> data = Task.Run(() => DeleteAllKeys_Async(I_AM_SURE));
            data.Wait();
            return data.Result;
        }
    }
}
