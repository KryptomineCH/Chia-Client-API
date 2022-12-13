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
        public async static Task<LogIn_Response> LogIn(FingerPrint_RPC fingerprint)
        {
            string response = await SendCustomMessage("log_in", fingerprint.ToString());
            LogIn_Response json = JsonSerializer.Deserialize<LogIn_Response>(response);
            return json;
        }
        /// <summary>
        /// Obtain the fingerprint of the wallet that is currently logged in
        /// </summary>
        /// <returns></returns>
        public async static Task<LogIn_Response> GetLoggedInFingerprint()
        {
            string response = await SendCustomMessage("get_logged_in_fingerprint");
            LogIn_Response json = JsonSerializer.Deserialize<LogIn_Response>(response);
            return json;
        }
        /// <summary>
        /// Show all public key fingerprints (main wallets) stored in the OS keyring. Note that the keyring must be unlocked in order to run this RPC
        /// </summary>
        /// <returns></returns>
        public async static Task<GetPublicKeys_Response> GetPublicKeys()
        {
            string response = await SendCustomMessage("get_public_keys");
            GetPublicKeys_Response json = JsonSerializer.Deserialize<GetPublicKeys_Response>(response);
            return json;
        }
        /// <summary>
        /// Show public and private info about a key
        /// </summary>
        /// <param name="fingerprint">The wallet's fingerprint, obtainable by running chia wallet show</param>
        /// <returns></returns>
        /// <remarks>This RPC will show the private key and seed phrase for the given fingerprint. Use with caution.</remarks>
        public async static Task<GetPrivateKey_Response> GetPrivateKey(FingerPrint_RPC fingerprint)
        {
            string response = await SendCustomMessage("get_private_key", fingerprint.ToString());
            GetPrivateKey_Response json = JsonSerializer.Deserialize<GetPrivateKey_Response>(response);
            return json;
        }
        /// <summary>
        /// Generates a random 24-word mnemonic seed phrase / wallet
        /// </summary>
        /// <returns>24 word mnemonic</returns>
        public async static Task<GenerateMnemonic_Response> GenerateMnemonic()
        {
            string response = await SendCustomMessage("generate_mnemonic");
            GenerateMnemonic_Response json = JsonSerializer.Deserialize<GenerateMnemonic_Response>(response);
            return json;
        }
        /// <summary>
        /// Add a new key (wallet/fingerprint) from a given 24 word mnemonic seed phrase
        /// </summary>
        /// <param name="mnemonic">24 word mnemonic passphrase</param>
        /// <returns></returns>
        public async static Task<LogIn_Response> AddKey(AddKey_RPC mnemonic)
        {
            string response = await SendCustomMessage("add_key", mnemonic.ToString());
            LogIn_Response json = JsonSerializer.Deserialize<LogIn_Response>(response);
            return json;
        }
        /// <summary>
        /// Remove a key, based on its wallet fingerprint
        /// </summary>
        /// <param name="fingerprint">The wallet's fingerprint, obtainable by running chia wallet show</param>
        /// <returns></returns>
        /// <remarks>this does not delete a key from the blockchain. You can re-add it witn the 24 word mnemonic</remarks>
        public async static Task<Success_Response> DeleteKey(FingerPrint_RPC fingerprint)
        {
            string response = await SendCustomMessage("delete_key", fingerprint.ToString());
            Success_Response json = JsonSerializer.Deserialize<Success_Response>(response);
            return json;
        }
        /// <summary>
        /// isplay whether a fingerprint has a balance, and whether it is used for farming or pool rewards. This is helpful when determining whether it is safe to delete a key without first backing it up
        /// </summary>
        /// <param name="checkDeleteKey_RPC"></param>
        /// <returns></returns>
        public async static Task<CheckDeleteKey_Response> CheckDeleteKey(CheckDeleteKey_RPC checkDeleteKey_RPC)
        {
            string response = await SendCustomMessage("check_delete_key", checkDeleteKey_RPC.ToString());
            CheckDeleteKey_Response json = JsonSerializer.Deserialize<CheckDeleteKey_Response>(response);
            return json;
        }
        /// <summary>
        /// Delete all keys from the wallet
        /// </summary>
        /// <param name="I_AM_SURE">you need to supply yes, for security. Otherwise an exception is thrown</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">you are not sure if this is what you really want to do</exception>
        public async static Task<Success_Response> DeleteAllKeys(bool I_AM_SURE = false)
        {
            if (!I_AM_SURE)
            {
                throw new ArgumentException("WARNING: This deletes ALL keys! If this is really what you want to do, pleas set I_AM_SURE to true!");
            }
            string response = await SendCustomMessage("delete_all_keys");
            Success_Response json = JsonSerializer.Deserialize<Success_Response>(response);
            return json;
        }
    }
}
