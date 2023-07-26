using CHIA_API_Tests.Initialisation_NS;
using Chia_Client_API.WalletAPI_NS;
using CHIA_RPC.General_NS;
using CHIA_RPC.Wallet_NS.DID_NS;
using CHIA_RPC.Wallet_NS.KeyManagement;
using CHIA_RPC.Wallet_NS.WalletManagement_NS;
using System;
using System.Threading;
using Xunit;

namespace CHIA_API_Tests.Wallet_NS
{
    [Collection("Testnet_Wallet")]
    public class WalletAPI_WalletManagement
    {
        [Fact]
        public void GetWallets_NoData_Test()
        {
            GetWallets_Response? response = Testnet_Wallet.Wallet_Client.GetWallets_Async(includeData: false).Result;
            Assert.NotNull(response);
            Assert.NotNull(response!.wallets);
            if (!(response!.success ?? false)) throw new Exception($"an error occurred: {response.error}");
            else if (response.wallets!.Length <= 0) throw new Exception("Wallets count seems suspicious!");
        }
        [Fact]
        public void GetWallets_IncludeData_Test()
        {
            GetWallets_Response? response = Testnet_Wallet.Wallet_Client.GetWallets_Async(includeData: true).Result;
            Assert.NotNull(response);
            Assert.NotNull(response!.wallets);
            if (!(response.success ?? false)) throw new Exception($"an error occurred: {response.error}");
            else if (response.wallets!.Length <= 0) throw new Exception("Wallets count seems suspicious!");
            { }
        }
        [Fact]
        public void CreateWallets_IntegrationTest()
        {
            // create empty main wallet and log in
            GenerateMnemonic_Response? generatedMainWallet = Testnet_Wallet.Wallet_Client.GenerateMnemonic_Async().Result;
            Assert.NotNull(generatedMainWallet);
            FingerPrint_Response? mainWalletFinger = Testnet_Wallet.Wallet_Client.AddKey_Async(new AddKey_RPC { mnemonic = generatedMainWallet!.mnemonic }).Result;
            Assert.NotNull(mainWalletFinger);
            FingerPrint_RPC fingerprint1 = new FingerPrint_RPC { fingerprint = mainWalletFinger!.fingerprint };
            FingerPrint_Response? test1 = Testnet_Wallet.Wallet_Client.LogIn_Async(fingerprint1).Result;
            Testnet_Wallet.Wallet_Client.AwaitWalletSync_Async(CancellationToken.None).Wait();
            // generate DID recovery Wallet
            try
            {
                // create recovery did
                CreateNewDIDWallet_RPC createRecoveryDIDWallet_RPC = new CreateNewDIDWallet_RPC
                {
                    did_type = "recovery",
                    wallet_name = "MyRecoveryDid"
                };
                CreateNewWallet_Response? recoveryDid = Testnet_Wallet.Wallet_Client.CreateNewDidWallet_Async(createRecoveryDIDWallet_RPC).Result;
                { }
            }
            finally
            {
                // clean up
                Success_Response? deleteSuccess1 = Testnet_Wallet.Wallet_Client.DeleteKey_Async(fingerprint1).Result;
            }
        }
    }
}
