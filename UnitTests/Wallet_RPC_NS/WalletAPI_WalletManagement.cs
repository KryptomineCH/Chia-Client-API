using Chia_Client_API.WalletAPI_NS;
using CHIA_RPC.General;
using CHIA_RPC.Wallet_RPC_NS.KeyManagement;
using CHIA_RPC.Wallet_RPC_NS.WalletManagement_NS;
using System;
using System.Threading;
using Xunit;

namespace UnitTests.Wallet_RPC_NS
{
    [Collection("Testnet")]
    public class WalletAPI_WalletManagement
    {
        [Fact]
        public void GetWallets_NoData_Test()
        {
            GetWallets_Response response = Testnet.Wallet_Client.GetWallets_Async(includeData: false).Result;
            if (!response.success) throw new Exception("an error occurred!");
            else if (response.wallets.Length <= 0) throw new Exception("Wallets count seems suspicious!");
        }
        [Fact]
        public void GetWallets_IncludeData_Test()
        {
            GetWallets_Response response = Testnet.Wallet_Client.GetWallets_Async(includeData: true).Result;
            if (!response.success) throw new Exception("an error occurred!");
            else if (response.wallets.Length <= 0) throw new Exception("Wallets count seems suspicious!");
            { }
        }
        [Fact]
        public void CreateWallets_IntegrationTest()
        {
            // create empty main wallet and log in
            GenerateMnemonic_Response generatedMainWallet = Testnet.Wallet_Client.GenerateMnemonic_Async().Result;
            LogIn_Response mainWalletFinger = Testnet.Wallet_Client.AddKey_Async(new AddKey_RPC { mnemonic = generatedMainWallet.mnemonic }).Result;
            FingerPrint_RPC fingerprint1 = new FingerPrint_RPC { fingerprint = mainWalletFinger.fingerprint };
            LogIn_Response test1 = Testnet.Wallet_Client.LogIn_Async(fingerprint1).Result;
            Testnet.Wallet_Client.AwaitWalletSync_Async(CancellationToken.None).Wait();
            // generate DID recovery Wallet
            try
            {
                // create recovery did
                CreateNewDIDWallet_RPC createRecoveryDIDWallet_RPC = new CreateNewDIDWallet_RPC
                {
                    did_type = "recovery",
                    wallet_name = "MyRecoveryDid"
                };
                CreateNewWallet_Response recoveryDid = Testnet.Wallet_Client.CreateNewDidWallet_Async(createRecoveryDIDWallet_RPC).Result;
                { }
            }
            finally
            {
                // clean up
                Success_Response deleteSuccess1 = Testnet.Wallet_Client.DeleteKey_Async(fingerprint1).Result;
            }
        }
    }
}
