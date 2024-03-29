﻿using CHIA_API_Tests.Initialisation_NS;
using CHIA_RPC.Datalayer_NS;
using CHIA_RPC.General_NS;
using CHIA_RPC.Wallet_NS.DID_NS;
using CHIA_RPC.Wallet_NS.WalletManagement_NS;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CHIA_API_Tests.Wallet_NS.DID_NS
{
    [Collection("Testnet_Wallet")]
    public class Datalayer_Connections_Tests
    {
        [Fact]
        [Trait("Category", "Manual")]
        public void CreateNewDidWallet_Test()
        {
            Wallets_info[]? didWallets = Testnet_Wallet.Wallet_Client.DidGetWallets_Sync();
            Assert.NotNull(didWallets);
            DidGetDid_Response? firstDidInfo = Testnet_Wallet.Wallet_Client.DidGetDid_Sync(didWallets![0]);
            Assert.NotNull(firstDidInfo);
            Assert.NotNull(firstDidInfo!.my_did);
            CreateNewDIDWallet_RPC rpc = new CreateNewDIDWallet_RPC(new string[] { firstDidInfo!.my_did! }, amount_mojos: 1, numOfBackupIdsNeeded:1);
            CreateNewWallet_Response? response = Testnet_Wallet.Wallet_Client.CreateNewDidWallet_Sync(rpc);
            Assert.NotNull(response);
            Assert.True(response!.success, response.error);
            Assert.Null(response.error);
            { }
        }
        [Fact]
        [Trait("Category", "Automatic")]
        public void DidGetSetWalletName_Test()
        {
            Wallets_info[]? didWallets = Testnet_Wallet.Wallet_Client.DidGetWallets_Sync();
            Assert.NotNull(didWallets);
            string newName = Testhelper_NS.RandomString.GenerateRandomString(10);
            DidSetWalletName_RPC rpc = new DidSetWalletName_RPC(didWallets!.Last().id, newName);
            WalletID_Response? response = Testnet_Wallet.Wallet_Client.DidSetWalletName_Sync(rpc);
            Assert.NotNull(response);
            Assert.True(response!.success, response.error);
            Assert.Null(response.error);
            DidGetWalletName_Response? newNameResponse = Testnet_Wallet.Wallet_Client.DidGetWalletName_Sync(response);
            Assert.NotNull(newNameResponse);
            Assert.Equal(newName, newNameResponse!.name);
            { }
        }
        [Fact]
        [Trait("Category", "Manual")]
        public void DidUpdateRecoveryIDs_Test()
        {
            DidGetDid_Response?[]? didWallets = Testnet_Wallet.Wallet_Client.DidGetAllDids_Sync();
            Assert.NotNull(didWallets);
            List<string> recoveryIDsList = new List<string>();
            for(int i = 1; i < didWallets!.Length -1; i++)
            {
                Assert.NotNull(didWallets[i]!.my_did);
                recoveryIDsList.Add(didWallets[i]!.my_did!);
            }
            string[] recoveryIDs = recoveryIDsList.ToArray();
            Assert.NotEmpty(didWallets);
            Assert.NotNull(didWallets.Last());
            Assert.NotNull(didWallets.Last()!.wallet_id);
            DidUpdateRecoveryIDs_RPC rpc = new DidUpdateRecoveryIDs_RPC((ulong)didWallets.Last()!.wallet_id!, recoveryIDs,fee: 1000);
            Success_Response? response = Testnet_Wallet.Wallet_Client.DidUpdateRecoveryIDs_Sync(rpc);
            Assert.NotNull(response);
            Assert.True(response!.success, response.error);
            Assert.Null(response.error);
            DidGetRecoveryList_Response? newNameResponse = Testnet_Wallet.Wallet_Client.DidGetRecoveryList_Sync(new WalletID_RPC((ulong)didWallets.Last()!.wallet_id!));
            Assert.NotNull(newNameResponse);
            Assert.Equal(newNameResponse!.recovery_list, recoveryIDs);
            { }
        }
        [Fact]
        [Trait("Category", "Automatic")]
        public void DidGetMetadata_Test()
        {
            Wallets_info[]? didWallets = Testnet_Wallet.Wallet_Client.DidGetWallets_Sync();
            Assert.NotNull(didWallets);
            DidGetMetadata_Response? response = Testnet_Wallet.Wallet_Client.DidGetMetadata_Sync(didWallets![0]);
            Assert.NotNull(response);
            Assert.True(response!.success, response.error);
            Assert.Null(response.error);
            { }
        }
        [Fact]
        [Trait("Category", "Automatic")]
        public void DidGetCurrentCoinInfo_Test()
        {
            Wallets_info[]? didWallets = Testnet_Wallet.Wallet_Client.DidGetWallets_Sync();
            Assert.NotNull(didWallets);
            DidGetCurrentCoinInfo_Response? response = Testnet_Wallet.Wallet_Client.DidGetCurrentCoinInfo_Sync(didWallets![0]);
            Assert.NotNull(response);
            Assert.True(response!.success, response.error);
            Assert.Null(response.error);
            { }
        }
        [Fact]
        [Trait("Category", "Automatic")]
        public void DidGetInfo_Test()
        {
            DidGetDid_Response?[]? didWallets = Testnet_Wallet.Wallet_Client.DidGetAllDids_Sync();
            Assert.NotNull(didWallets);
            Assert.NotEmpty(didWallets);
            DidGetInfo_Response? response = Testnet_Wallet.Wallet_Client.DidGetInfo_Sync(didWallets![0]!);
            Assert.NotNull(response);
            Assert.True(response!.success, response.error);
            Assert.Null(response.error);
            { }
        }
    }
}
