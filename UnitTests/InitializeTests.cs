using Chia_Client_API.Wallet_NS.WalletAPI_NS;
using CHIA_RPC.General;
using CHIA_RPC.Wallet_RPC_NS.KeyManagement;
using CHIA_RPC.Wallet_RPC_NS.WalletManagement_NS;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class Testnet : IDisposable
    {
        public Testnet()
        {
            // ... initialize ...
            Chia_Client_API.GlobalVar.API_TargetIP = "192.168.1.132";
            Chia_Client_API.GlobalVar.API_CertificateFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            @".testnet\ssl\");
            // close all old wallets (if more than 1 are open)
            GetWallets_Response pre_wallets = WalletApi.GetWallets().Result;
            if (pre_wallets.success)
            {
                int walletAmount = 0;
                foreach (Wallets_info wallet in pre_wallets.wallets)
                {
                    if (wallet.type == CHIA_RPC.Objects_NS.WalletType.ChiaWallet)
                    {
                        walletAmount++;
                    }
                }
                if (walletAmount > 1)
                {
                    WalletApi.DeleteAllKeys(I_AM_SURE: true);
                }
            }
            // create wallet or login with existing
            if (!File.Exists("testwallet.rpc"))
            {
                GenerateMnemonic_Response generatedWallet = WalletApi.GenerateMnemonic().Result;
                AddKey_RPC addKey = new AddKey_RPC { mnemonic = generatedWallet.mnemonic };
                addKey.Save("testwallet.rpc");
            }
            if (!File.Exists("testfingerprint.rpc"))
            {
                AddKey_RPC addKey = AddKey_RPC.Load("testwallet.rpc");
                LogIn_Response response = WalletApi.AddKey(addKey).Result;
                FingerPrint_RPC fingerprint = new FingerPrint_RPC { fingerprint = response.fingerprint };
                fingerprint.Save("testfingerprint.rpc");
            }
            FingerPrint_RPC login_rpc = FingerPrint_RPC.Load("testfingerprint.rpc");
            GetPublicKeys_Response wallets = WalletApi.GetPublicKeys().Result;
            if (!wallets.public_key_fingerprints.Contains(login_rpc.fingerprint))
            {
                AddKey_RPC addKey = AddKey_RPC.Load("testwallet.rpc");
                LogIn_Response response = WalletApi.AddKey(addKey).Result;
                { }
            }
            Task.Delay(5000).Wait();
            WalletApi.LogIn(login_rpc).Wait();
            _ = WalletApi.AwaitWalletSync_Async(CancellationToken.None).Result;
        }

        public void Dispose()
        {
            // ... clean up ...
        }
    }
    [CollectionDefinition("Testnet")]
    public class TestCollection : ICollectionFixture<Testnet>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
