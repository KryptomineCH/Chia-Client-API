using System;
using Xunit;
using Chia_Client_API.WalletAPI_NS;
using CHIA_RPC.Wallet_NS.KeyManagement;
using CHIA_RPC.General_NS;

namespace UnitTests.Wallet_RPC_NS
{
    [Collection("Testnet")]
    public class WalletAPI_KeyManagement
    {
        /// <summary>
        /// Log into the wallet with the specified key
        /// </summary>
        /// <param name="fingerprint">The wallet's fingerprint, obtainable by running chia wallet show</param>
        /// <returns>the logged in fingerprint</returns>
        [Fact]
        public void Keymanagement_IntegrationTest()
        {
            // generate test wallets
            GenerateMnemonic_Response generatedWallet1 = Testnet.Wallet_Client.GenerateMnemonic_Async().Result;
            if (!generatedWallet1.success) throw new Exception("wallet1 mnemonics couldnt be created!");
            FingerPrint_Response response1 = Testnet.Wallet_Client.AddKey_Async(new AddKey_RPC { mnemonic = generatedWallet1.mnemonic }).Result;
            if (!response1.success) throw new Exception("wallet1 couldnt be added!");
            GenerateMnemonic_Response generatedWallet2 = Testnet.Wallet_Client.GenerateMnemonic_Async().Result;
            if (!generatedWallet2.success) throw new Exception("wallet2 mnemonics couldnt be created!");
            FingerPrint_Response response2 = Testnet.Wallet_Client.AddKey_Async(new AddKey_RPC { mnemonic = generatedWallet2.mnemonic }).Result;
            if (!response2.success) throw new Exception("wallet2 couldnt be added!");
            // wallets have been added, create fingerprint rpcs
            FingerPrint_RPC fingerprint1 = new FingerPrint_RPC { fingerprint = response1.fingerprint };
            FingerPrint_RPC fingerprint2 = new FingerPrint_RPC { fingerprint = response2.fingerprint };

            try
            {
                // test adding wallet 1
                FingerPrint_Response test1 = Testnet.Wallet_Client.LogIn_Async(fingerprint1).Result;
                FingerPrint_Response login1 = Testnet.Wallet_Client.GetLoggedInFingerprint_Async().Result;
                if (login1.fingerprint != response1.fingerprint)
                {
                    throw new Exception("Login1 Failed!");
                }
                // test getting private key for 1
                GetPrivateKey_Response privateKey1 = Testnet.Wallet_Client.GetPrivateKey_Async(fingerprint1).Result;
                // test adding wallet 2
                FingerPrint_Response test2 = Testnet.Wallet_Client.LogIn_Async(fingerprint2).Result;
                FingerPrint_Response login2 = Testnet.Wallet_Client.GetLoggedInFingerprint_Async().Result;
                if (login2.fingerprint != response2.fingerprint)
                {
                    throw new Exception("Login1 Failed!");
                }
                // test getting private key for 2
                GetPrivateKey_Response privateKey2 = Testnet.Wallet_Client.GetPrivateKey_Async(fingerprint2).Result;
            }
            finally
            {
                CheckDeleteKey_Response checkDelete1 =
                    Testnet.Wallet_Client.CheckDeleteKey_Async(new CheckDeleteKey_RPC { fingerprint = response1.fingerprint }).Result;
                if (!checkDelete1.success || checkDelete1.wallet_balance || checkDelete1.used_for_pool_rewards || checkDelete1.used_for_farmer_rewards)
                {
                    throw new Exception("couldnt check if address1 can be deleted!");
                }
                CheckDeleteKey_Response checkDelete2 =
                    Testnet.Wallet_Client.CheckDeleteKey_Async (new CheckDeleteKey_RPC { fingerprint = response1.fingerprint }).Result;
                if (!checkDelete2.success || checkDelete2.wallet_balance || checkDelete2.used_for_pool_rewards || checkDelete2.used_for_farmer_rewards)
                {
                    throw new Exception("couldnt check if address2 can be deleted!");
                }
                // ensure wallets get removed
                Success_Response deleteSuccess1 = Testnet.Wallet_Client.DeleteKey_Async(fingerprint1).Result;
                Success_Response deleteSuccess2 = Testnet.Wallet_Client.DeleteKey_Async(fingerprint2).Result;
                if (!deleteSuccess1.success) throw new Exception("wallet1 could not be removed!");
                if (!deleteSuccess2.success) throw new Exception("wallet2 could not be removed!");
            }
        }
        /// <summary>
        /// Delete all keys from the wallet
        /// </summary>
        /// <param name="I_AM_SURE">you need to supply yes, for security. Otherwise an exception is thrown</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">you are not sure if this is what you really want to do</exception>
        [Fact]
        public void DeleteAllKeys()
        {
            bool areYouSure = false;
            if (areYouSure)
            {
                Testnet.Wallet_Client.DeleteAllKeys_Async(I_AM_SURE: areYouSure).Wait();
                GetPublicKeys_Response keys = Testnet.Wallet_Client.GetPublicKeys_Async().Result;
                if (!keys.success)
                {
                    throw new Exception("failed to check if keys got deleted!");
                }
                if (keys.public_key_fingerprints.Length > 0)
                {
                    throw new Exception("not all Keys have been deleted!");
                }
            }
            else
            {
                try
                {
                    Testnet.Wallet_Client.DeleteAllKeys_Async(I_AM_SURE: areYouSure).Wait();
                    throw new Exception("Oh Oh!");
                }
                catch (Exception ex)
                {

                }
                GetPublicKeys_Response keys = Testnet.Wallet_Client.GetPublicKeys_Async().Result;
                if (!keys.success)
                {
                    throw new Exception("failed to check if keys got deleted!");
                }
                if (keys.public_key_fingerprints.Length <= 0)
                {
                    throw new Exception("all Keys have been deleted!");
                }
            }  
        }
    }
}
