using CHIA_RPC.General;
using CHIA_RPC.Wallet_RPC_NS.Wallet_NS;
using System;
using Xunit;
using Chia_Client_API.WalletAPI_NS;
using System.Threading;
using System.Reflection.Metadata;

namespace UnitTests.Wallet_RPC_NS
{
    [Collection("Testnet")]
    public class WalletAPI_Wallet
    {
        [Fact]
        public void GetWalletBalance_Test()
        {
            // wallets start with 1
            WalletID_RPC id = new WalletID_RPC { wallet_id = 1};
            bool success = Testnet.Wallet_Client.AwaitWalletSync_Async(CancellationToken.None).Result;
            if (!success)
            {
                throw new Exception("wallet could not be synced!");
            }
            GetWalletBalance_Response response = Testnet.Wallet_Client.GetWalletBalance_Async(id).Result;
            if (!response.success)
            {
                throw new Exception(response.error);
            }
            if (response.wallet_balance.confirmed_wallet_balance == 0)
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
            bool success = Testnet.Wallet_Client.AwaitWalletSync_Async(CancellationToken.None).Result;
            if (!success)
            {
                throw new Exception("wallet could not be synced!");
            }
            WalletID_RPC id = new WalletID_RPC { wallet_id = 1 };
            GetTransactions_Response all_transactions = Testnet.Wallet_Client.GetTransactions_Async(id).Result;
            TransactionID_RPC transID = new TransactionID_RPC { transaction_id = all_transactions.transactions[0].name };
            GetTransaction_Response response = Testnet.Wallet_Client.GetTransaction_Async(transID).Result;
            if (!response.success)
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
            bool success = Testnet.Wallet_Client.AwaitWalletSync_Async(CancellationToken.None).Result;
            if (!success)
            {
                throw new Exception("wallet could not be synced!");
            }
            GetTransactions_Response response = Testnet.Wallet_Client.GetTransactions_Async(id).Result;
            if (!response.success)
            {
                throw new Exception(response.error);
            }
            if (response.transactions.Length <= 0)
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
            bool success = Testnet.Wallet_Client.AwaitWalletSync_Async(CancellationToken.None).Result;
            if (!success)
            {
                throw new Exception("wallet could not be synced!");
            }
            GetTransactionCount_Response response = Testnet.Wallet_Client.GetTransactionCount_Async(id).Result;
            if (!response.success)
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
            GetNextAddress_Response response1 = Testnet.Wallet_Client.GetNextAddress_Async(same_address).Result;
            if (!response1.success)
            {
                throw new Exception(response1.error);
            }
            GetNextAddress_Response response2 = Testnet.Wallet_Client.GetNextAddress_Async(same_address).Result;
            if (!response2.success)
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
            GetNextAddress_Response response3 = Testnet.Wallet_Client.GetNextAddress_Async(new_address).Result;
            if (!response3.success)
            {
                throw new Exception(response3.error);
            }
            if (response1.address == response3.address)
            {
                throw new Exception("obtaining a new adress failed! Adresses are equal!");
            }
        }
        /// <summary>
        /// Send a (chia) transaction
        /// </summary>
        [Fact]
        public void SendTransaction()
        {
            SendXCH_RPC rpc = new SendXCH_RPC
            {
                address = CommonTestFunctions.TestAdress,
                amount = 1000,
                fee = 0,
                memos = new[] { "this is a testmemo1", "this is a testmemo2" },
                wallet_id = 1
            };
            GetTransaction_Response response = Testnet.Wallet_Client.SendTransaction_Async(rpc).Result;
            if (!response.success)
            {
                throw new Exception(response.error);
            }
            GetTransaction_Response result = Testnet.Wallet_Client.AwaitTransactionToComplete_Async(response.transaction,CancellationToken.None,10.0).Result;
            { }
            if (!result.success)
            {
                throw new Exception(result.error);
            }
            if (!result.transaction.confirmed)
            {
                throw new Exception("transaction has not be confirmed within 10 minutes!");
            }
        }
        /// <summary>
        /// not well documented. pleas use custom rpc
        /// </summary>
        [Fact]
        public void SendTransactionMulti()
        {
            throw new NotImplementedException();
            SendXCH_RPC wallet_Send_XCH_RPC;
        }
        /// <summary>
        /// Show the total amount that has been farmed
        /// </summary>
        [Fact]
        public void GetFarmedAmount()
        {
            GetFarmedAmount_Response response = Testnet.Wallet_Client.GetFarmedAmount_Async().Result;
            if (!response.success)
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
            GetNextAddress_Response adressResponse = Testnet.Wallet_Client.GetNextAddress_Async(same_address).Result;
            SendXCH_RPC rpc = new SendXCH_RPC
            {
                address = adressResponse.address,
                amount = 1000,
                fee = 0,
                memos = new[] { "this is a testmemo1", "this is a testmemo2" },
                wallet_id = 1
            };
            GetTransaction_Response response = Testnet.Wallet_Client.SendTransaction_Async(rpc).Result;
            Success_Response success = Testnet.Wallet_Client.DeleteUnconfirmedTransactions_Async(1).Result;
            if (!success.success)
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
            SelectCoins_Response response = Testnet.Wallet_Client.SelectCoins_Async(select).Result;
            if (!response.success)
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
            GetSpendableCoins_Response response = Testnet.Wallet_Client.GetSpendableCoins_Async(rpc).Result;
            if (!response.success)
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
            GetCoinRecordsByNames_Response response = Testnet.Wallet_Client.GetCoinRecordsByNames_Async(rpc).Result;
            if (!response.success)
            {
                throw new Exception(response.error);
            }
            { }
        }
        /// <summary>
        /// Obtain the current derivation index for the current wallet
        /// </summary>
        /// <returns></returns>
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
            ExtendDerivationIndex_RPC extendDerivationIndex_RPC;
        }
        /// <summary>
        /// Obtain current notifications
        /// </summary>
        [Fact]
        public void GetNotifications()
        {
            throw new NotImplementedException();
            GetNotifications_RPC getNotifications_RPC = null;
        }
        /// <summary>
        /// Delete notifications, with the option to specify IDs from which to delete
        /// </summary>
        [Fact]
        public void DeleteNotifications()
        {
            throw new NotImplementedException();
            DeleteNotifications_RPC deleteNotifications_RPC = null;
        }
        /// <summary>
        /// Send a notification to a specified puzzle hash
        /// </summary>
        [Fact]
        public void SendNotification()
        {
            throw new NotImplementedException();
            SendNotification_RPC sendNotification_RPC;
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
            SignMessage_Response response = Testnet.Wallet_Client.SignMessageByAddress_Async(rpc).Result;
            if(!response.success)
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
            SignMessageByID_RPC signMessageByID_RPC;
        }
    }
}
