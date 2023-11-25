using CHIA_RPC.General_NS;
using CHIA_RPC.Wallet_NS.Wallet_NS;
using System;
using Xunit;
using System.Threading;
using CHIA_API_Tests.Initialisation_NS;
using CHIA_RPC.Wallet_NS.WalletManagement_NS;
using System.Net;
using CHIA_RPC.Wallet_NS.CATsAndTrading_NS;
using NFT.Storage.Net.ClientResponse;

namespace CHIA_API_Tests.Wallet_NS
{
    [Collection("Testnet_Wallet")]
    public class WalletAPI_Wallet
    {
        [Fact]
        public void GetWalletBalance_Test()
        {
            // wallets start with 1
            WalletID_RPC id = new WalletID_RPC { wallet_id = 1};
            bool success = Testnet_Wallet.Wallet_Client.AwaitWalletSync_Async(CancellationToken.None).Result;
            if (!success)
            {
                throw new Exception("wallet could not be synced!");
            }
            GetWalletBalance_Response? response = Testnet_Wallet.Wallet_Client.GetWalletBalance_Async(id).Result;
            Assert.NotNull(response);
            if (!(response!.success ?? false))
            {
                throw new Exception(response.error);
            }
            Assert.NotNull(response.wallet_balance);
            if (response.wallet_balance!.confirmed_wallet_balance == 0)
            {
                throw new Exception("test wallet does not seem to have any balance!");
            }
            { }
        }
        /// <summary>
        /// Get a transaction's details from its ID
        /// </summary>
        [Fact]
        public void GetTransaction()
        {
            bool success = Testnet_Wallet.Wallet_Client.AwaitWalletSync_Async(CancellationToken.None).Result;
            if (!success)
            {
                throw new Exception("wallet could not be synced!");
            }
            WalletID_RPC id = new WalletID_RPC { wallet_id = 1 };
            GetTransactions_Response? all_transactions = Testnet_Wallet.Wallet_Client.GetTransactions_Async(id).Result;
            Assert.NotNull(all_transactions);
            Assert.NotNull(all_transactions!.transactions);
            TransactionID_RPC? transID = new TransactionID_RPC { transaction_id = all_transactions.transactions![0].name };
            GetTransaction_Response? response = Testnet_Wallet.Wallet_Client.GetTransaction_Async(transID).Result;
            Assert.NotNull(response);
            if (!(response!.success ?? false))
            {
                throw new Exception(response.error);
            }
            if (response.transaction == null)
            {
                throw new Exception("Transaction could not be loaded!");
            }
        }
        /// <summary>
        /// Get all transactions for a given wallet
        /// </summary>
        [Fact]
        public void GetTransactions()
        {
            WalletID_RPC id = new WalletID_RPC { wallet_id = 1 };
            bool success = Testnet_Wallet.Wallet_Client.AwaitWalletSync_Async(CancellationToken.None).Result;
            if (!success)
            {
                throw new Exception("wallet could not be synced!");
            }
            GetTransactions_Response? response = Testnet_Wallet.Wallet_Client.GetTransactions_Async(id).Result;
            Assert.NotNull(response);
            if (!(response!.success ?? false))
            {
                throw new Exception(response.error);
            }
            Assert.NotNull(response.transactions);
            if (response.transactions!.Length <= 0)
            {
                throw new Exception("no transactions were reported!");
            }
        }
        /// <summary>
        /// Obtain the number of transactions for a wallet
        /// </summary>
        [Fact]
        public void GetTransactionCount()
        {
            WalletID_RPC id = new WalletID_RPC { wallet_id = 1 };
            bool success = Testnet_Wallet.Wallet_Client.AwaitWalletSync_Async(CancellationToken.None).Result;
            if (!success)
            {
                throw new Exception("wallet could not be synced!");
            }
            GetTransactionCount_Response? response = Testnet_Wallet.Wallet_Client.GetTransactionCount_Async(id).Result;
            Assert.NotNull(response);
            if (!(response!.success ?? false))
            {
                throw new Exception(response.error);
            }
            if (response.count <= 0)
            {
                throw new Exception("no transactions were found!");
            }
        }
        /// <summary>
        /// Get the next address in the HD tree, with the option to show the latest address
        /// </summary>
        [Fact]
        public void GetNextAddress()
        {
            GetNextAddress_RPC same_address = new GetNextAddress_RPC
            {
                new_address = false,
                wallet_id = 1
            };
            GetNextAddress_Response? response1 = Testnet_Wallet.Wallet_Client.GetNextAddress_Async(same_address).Result;
            Assert.NotNull(response1);
            if (!(response1!.success ?? false))
            {
                throw new Exception(response1.error);
            }
            GetNextAddress_Response? response2 = Testnet_Wallet.Wallet_Client.GetNextAddress_Async(same_address).Result;
            Assert.NotNull(response2);
            if (!(response2!.success ?? false))
            {
                throw new Exception(response2.error);
            }
            if (response1.address != response2.address)
            {
                throw new Exception("obtaining the last known adress failed! Adresses are not equal!");
            }
            GetNextAddress_RPC new_address = new GetNextAddress_RPC
            {
                new_address = true,
                wallet_id = 1
            };
            GetNextAddress_Response? response3 = Testnet_Wallet.Wallet_Client.GetNextAddress_Async(new_address).Result;
            Assert.NotNull(response3);
            if (!(response3!.success ?? false))
            {
                throw new Exception(response3.error);
            }
            if (response1.address == response3.address)
            {
                throw new Exception("obtaining a new adress failed! Adresses are equal!");
            }
        }/*
        [Fact]
        public void GetWalletAddresses()
        {
            GetWalletAddresses_RPC same_address = new GetWalletAddresses_RPC
            {
                wallet_id = 1,
                index = 0,
                count = 5
            };
            GetRoutes_Response response = Testnet_Wallet.Wallet_Client.GetRoutes_Sync();
            GetNextAddress_Response? response1 = Testnet_Wallet.Wallet_Client.GetWalletAddresses_Async(same_address).Data;
            Assert.NotNull(response1);
            if (!(response1!.success ?? false))
            {
                throw new Exception(response1.error);
            }
        }*/
        /// <summary>
        /// Send a (chia) transaction
        /// </summary>
        [Fact]
        public void SendTransaction()
        {
            SendTransaction_RPC rpc = new SendTransaction_RPC
            {
                address = CommonTestFunctions.TestAdress,
                amount = 1000,
                fee = 0,
                memos = new[] { "this is a testmemo1", "this is a testmemo2" },
                wallet_id = 1
            };
            GetTransaction_Response? response = Testnet_Wallet.Wallet_Client.SendTransaction_Async(rpc).Result;
            Assert.NotNull(response);
            if (!(response!.success ?? false))
            {
                throw new Exception(response.error);
            }
            Assert.NotNull(response.transaction);
            GetTransaction_Response? result = Testnet_Wallet.Wallet_Client.AwaitTransactionToConfirm_Async(response.transaction!,CancellationToken.None,10.0).Result;
            { }
            Assert.NotNull(result);
            if (!(result!.success ?? false))
            {
                throw new Exception(result.error);
            }
            if ((result.transaction == null) || !(result.transaction.confirmed ?? false))
            {
                throw new Exception("transaction has not be confirmed within 10 minutes!");
            }
        }
        /// <summary>
        /// Send a (chia) transaction
        /// </summary>
        [Fact]
        public void SendCATTransaction()
        {
            Wallets_info? info = Testnet_Wallet.Wallet_Client.GetWalletByName("btf-test");
            Assert.NotNull(info);
            CatSpend_RPC rpc = new CatSpend_RPC();
            rpc.wallet_id = info!.id;
            rpc.amount = 1000;
            rpc.inner_address = CommonTestFunctions.TestAdress;
            rpc.memos = new string[] { "BTF commission" };
            rpc.fee = 0;
            CatSpend_Response? record = Testnet_Wallet.Wallet_Client.CatSpend_Sync(rpc);
            Assert.NotNull(record);
            Assert.NotNull(record!.transaction);
            if (!(record!.success ?? false))
            {
                throw new Exception(record.error);
            }
            GetTransaction_Response? result = Testnet_Wallet.Wallet_Client.AwaitTransactionToConfirm_Async(record.transaction!, CancellationToken.None, 10.0).Result;
            { }
            Assert.NotNull(result);
            if (!(result!.success ?? false))
            {
                throw new Exception(result.error);
            }
            if (result.transaction == null || !(result.transaction.confirmed ?? false))
            {
                throw new Exception("transaction has not be confirmed within 10 minutes!");
            }
        }
        /// <summary>
        /// not well documented. please use custom rpc
        /// </summary>
        [Fact]
        public void SendTransactionMulti()
        {
            throw new NotImplementedException();
            //SendTransaction_RPC wallet_Send_XCH_RPC;
        }
        /// <summary>
        /// Show the total amount that has been farmed
        /// </summary>
        [Fact]
        public void GetFarmedAmount()
        {
            GetFarmedAmount_Response? response = Testnet_Wallet.Wallet_Client.GetFarmedAmount_Async().Result;
            Assert.NotNull(response);
            if (!(response!.success ?? false))
            {
                throw new Exception(response.error);
            }
            { }
        }
        /// <summary>
        /// not yet implemented
        /// </summary>
        [Fact]
        public void CreateSignedTransaction()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Delete all transactions that have yet to be confirmed on the blockchain from the given wallet
        /// </summary>
        [Fact]
        public void DeleteUnconfirmedTransactions()
        {
            GetNextAddress_RPC same_address = new GetNextAddress_RPC
            {
                new_address = false,
                wallet_id = 1
            };
            GetNextAddress_Response? adressResponse = Testnet_Wallet.Wallet_Client.GetNextAddress_Async(same_address).Result;
            Assert.NotNull(adressResponse);
            Assert.NotNull(adressResponse!.address);
            SendTransaction_RPC rpc = new SendTransaction_RPC
            {
                address = adressResponse.address,
                amount = 1000,
                fee = 0,
                memos = new[] { "this is a testmemo1", "this is a testmemo2" },
                wallet_id = 1
            };
            GetTransaction_Response? response = Testnet_Wallet.Wallet_Client.SendTransaction_Async(rpc).Result;
            Assert.NotNull(response);
            Success_Response? success = Testnet_Wallet.Wallet_Client.DeleteUnconfirmedTransactions_Async(1).Result;
            Assert.NotNull(success);
            if (!(success!.success ?? false))
            {
                throw new Exception(success.error);
            }
        }
        /// <summary>
        /// Select coins from a given wallet that add up to at least the specified amount
        /// </summary>
        [Fact]
        public void SelectCoins()
        {
            SelectCoins_RPC select = new SelectCoins_RPC
            {
                amount = 100,
                wallet_id = 1
            };
            SelectCoins_Response? response = Testnet_Wallet.Wallet_Client.SelectCoins_Async(select).Result;
            Assert.NotNull(response);
            if (!(response!.success ?? false))
            {
                throw new Exception(response.error);
            }
        }
        /// <summary>
        /// Get all spendable coins, with various possible filters
        /// </summary>
        [Fact]
        public void GetSpendableCoins()
        {
            GetSpendableCoins_RPC rpc = new GetSpendableCoins_RPC
            {
                wallet_id = 1
            };
            GetSpendableCoins_Response? response = Testnet_Wallet.Wallet_Client.GetSpendableCoins_Async(rpc).Result;
            Assert.NotNull(response);
            if (!(response!.success ?? false))
            {
                throw new Exception(response.error);
            }
            { }
        }
        /// <summary>
        /// Obtain coin records from a list of coin names
        /// </summary>
        [Fact]
        public void GetCoinRecordsByNames()
        {
            GetCoinRecordsByNames_RPC rpc = new GetCoinRecordsByNames_RPC
            {
                names = new[] { "0xeb17e80fcb72f15bfb28924f0bcd684df626646dca282bc88098cb0d59ffe1bb" }
            };
            GetCoinRecords_Response? response = Testnet_Wallet.Wallet_Client.GetCoinRecordsByNames_Async(rpc).Result;
            Assert.NotNull(response);
            if (!(response!.success ?? false))
            {
                throw new Exception(response.error);
            }
            { }
        }
        /// <summary>
        /// Obtain the current derivation index for the current wallet
        /// </summary>
        /// <returns></returns>
        [Fact]
        public void GetCurrentDerivationIndex()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Increase the derivation index
        /// </summary>
        [Fact]
        public void ExtendDerivationIndex()
        {
            throw new NotImplementedException();
            //Index_RPC extendDerivationIndex_RPC;
        }
        /// <summary>
        /// Obtain current notifications
        /// </summary>
        [Fact]
        public void GetNotifications()
        {
            throw new NotImplementedException();
            //GetNotifications_RPC getNotifications_RPC = null;
        }
        /// <summary>
        /// Delete notifications, with the option to specify IDs from which to delete
        /// </summary>
        [Fact]
        public void DeleteNotifications()
        {
            throw new NotImplementedException();
            //DeleteNotifications_RPC deleteNotifications_RPC = null;
        }
        /// <summary>
        /// Send a notification to a specified puzzle hash
        /// </summary>
        [Fact]
        public void SendNotification()
        {
            throw new NotImplementedException();
            //SendNotification_RPC sendNotification_RPC;
        }
        /// <summary>
        /// Sign a message using an XCH address without incurring an on-chain transaction
        /// </summary>
        [Fact]
        public void SignMessageByAddress()
        {
            string adress = CommonTestFunctions.TestAdress;
            SignMessageByAddress_RPC rpc = new SignMessageByAddress_RPC
            {
                address = adress,
                message = "this is a test"
            };
            SignMessage_Response? response = Testnet_Wallet.Wallet_Client.SignMessageByAddress_Async(rpc).Result;
            Assert.NotNull(response);
            if (!(response!.success ?? false))
            {
                throw new Exception(response.error);
            }
            { }
            VerifySignature_RPC verifySignature_RPC = new VerifySignature_RPC
            {
                address= adress,
                message = "this is a test",
                pubkey = response.pubkey,
                signature = response.signature
            };
            //WalletApi.VerifySignature(verifySignature_RPC);
        }
        /// <summary>
        /// Sign a message using a DID or NFT ID without incurring an on-chain transaction
        /// </summary>
        [Fact]
        public void SignMessageByID()
        {
            throw new NotImplementedException();
            //SignMessageByID_RPC signMessageByID_RPC;
        }
    }
}
