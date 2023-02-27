using Chia_Client_API.FullNodeAPI_NS;
using Chia_Client_API.WalletAPI_NS;
using CHIA_RPC.General;
using CHIA_RPC.Wallet_RPC_NS.KeyManagement;
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
            string certificatePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            @".testnet\ssl\");
            Wallet_Client = new Wallet_RPC_Client(targetApiAddress: "192.168.1.132", targetCertificateBaseFolder: certificatePath);
            Fullnode_Client = new FullNode_RPC_Client(targetApiAddress: "192.168.1.132", targetCertificateBaseFolder: certificatePath);
            
            // close all old wallets (if more than 1 are open)
            GetPublicKeys_Response pre_wallets = Wallet_Client.GetPublicKeys_Async().Result;
            if (pre_wallets.success)
            {
                if (pre_wallets.public_key_fingerprints.Length > 1)
                {
                    Wallet_Client.DeleteAllKeys_Async(I_AM_SURE: true);
                }
            }
            // create wallet or login with existing
            if (!File.Exists("testwallet.rpc"))
            {
                GenerateMnemonic_Response generatedWallet = Wallet_Client.GenerateMnemonic_Async().Result;
                AddKey_RPC addKey = new AddKey_RPC { mnemonic = generatedWallet.mnemonic };
                addKey.Save("testwallet.rpc");
            }
            if (!File.Exists("testfingerprint.rpc"))
            {
                AddKey_RPC addKey = AddKey_RPC.Load("testwallet.rpc");
                LogIn_Response response = Wallet_Client.AddKey_Async(addKey).Result;
                FingerPrint_RPC fingerprint = new FingerPrint_RPC { fingerprint = response.fingerprint };
                fingerprint.Save("testfingerprint.rpc");
            }
            FingerPrint_RPC login_rpc = FingerPrint_RPC.Load("testfingerprint.rpc");
            GetPublicKeys_Response wallets = Wallet_Client.GetPublicKeys_Async().Result;
            if (!wallets.public_key_fingerprints.Contains(login_rpc.fingerprint))
            {
                AddKey_RPC addKey = AddKey_RPC.Load("testwallet.rpc");
                LogIn_Response response = Wallet_Client.AddKey_Async(addKey).Result;
                { }
            }
            Task.Delay(5000).Wait();
            Wallet_Client.LogIn_Async(login_rpc).Wait();
            _ = Wallet_Client.AwaitWalletSync_Async(CancellationToken.None).Result;
        }
        public static Wallet_RPC_Client Wallet_Client;
        public static FullNode_RPC_Client Fullnode_Client;
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
