﻿using Chia_Client_API.WalletAPI_NS;
using CHIA_RPC.General_NS;
using CHIA_RPC.Wallet_NS.KeyManagement;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CHIA_API_Tests.Initialisation_NS
{
    public static class Testnet_Wallet
    {
        static Testnet_Wallet()
        {
            lock (Wallet_Client_Lock)
            {
                if (Wallet_Client != null) return;
                // ... initialize ...
                string certificatePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                ".testnet","ssl");
                Wallet_Client = new WalletRpcClient(reportResponseErrors: false, targetApiAddress: "192.168.1.117", targetCertificateBaseFolder: certificatePath);
                //Wallet_WebSocket_Client = new WalletWebSocketClient(reportResponseErrors: false, targetApiAddress: "192.168.1.117", targetCertificateBaseFolder: certificatePath);
                // close all old wallets (if more than 1 are open)
                GetPublicKeys_Response? pre_wallets = Wallet_Client.GetPublicKeys_Async().Result;
                Assert.NotNull(pre_wallets);
                if ((pre_wallets!.success ?? false))
                {
                    if (pre_wallets.public_key_fingerprints.Length > 1)
                    {
                        _ = Wallet_Client.DeleteAllKeys_Async(I_AM_SURE: true);
                    }
                }
                // create wallet or login with existing
                if (!File.Exists("testwallet.rpc"))
                {
                    GenerateMnemonic_Response? generatedWallet = Wallet_Client.GenerateMnemonic_Async().Result;
                    Assert.NotNull(generatedWallet);
                    AddKey_RPC addKey = new AddKey_RPC { mnemonic = generatedWallet!.mnemonic };
                    addKey.SaveRpcToFile("testwallet.rpc");
                }
                if (!File.Exists("testfingerprint.rpc"))
                {
                    AddKey_RPC? addKey = AddKey_RPC.LoadRpcFromFile("testwallet.rpc");
                    Assert.NotNull(addKey);
                    FingerPrint_Response? response = Wallet_Client.AddKey_Async(addKey!).Result;
                    Assert.NotNull(response);
                    FingerPrint_RPC fingerprint = response!;
                    fingerprint.SaveRpcToFile("testfingerprint.rpc");
                }
                FingerPrint_RPC? login_rpc = FingerPrint_RPC.LoadRpcFromFile("testfingerprint.rpc");
                GetPublicKeys_Response? wallets = Wallet_Client.GetPublicKeys_Async().Result;
                Assert.NotNull(login_rpc);
                Assert.NotNull(wallets);
                Assert.NotNull(wallets!.public_key_fingerprints);
                if (!wallets.public_key_fingerprints!.Contains(login_rpc!.fingerprint))
                {
                    AddKey_RPC? addKey = AddKey_RPC.LoadRpcFromFile("testwallet.rpc");
                    Assert.NotNull(addKey);
                    FingerPrint_Response? response = Wallet_Client.AddKey_Async(addKey!).Result;
                    //FingerPrint_RPC fingerprint = response.Get_RPC();
                    //fingerprint.SaveRpcToFile("testfingerprint.rpc");
                }
                Task.Delay(5000).Wait();
                Wallet_Client.LogIn_Async(login_rpc).Wait();
                _ = Wallet_Client.AwaitWalletSync_Async(CancellationToken.None).Result;
            }
            
        }
        private static object Wallet_Client_Lock = new object();
        public static WalletRpcClient Wallet_Client;
        public static WalletWebSocketClient Wallet_WebSocket_Client;
        
        //public void Dispose()
        //{
        //    // ... clean up ...
        //}
    }
    //[CollectionDefinition("Testnet_Wallet")]
    //public class WalletTestCollection : ICollectionFixture<Testnet_Wallet>
    //{
    //    // This class has no code, and is never created. Its purpose is simply
    //    // to be the place to apply [CollectionDefinition] and all the
    //    // ICollectionFixture<> interfaces.
    //}
}
