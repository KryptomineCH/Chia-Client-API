using Chia_Client_API.FullNodeAPI_NS;
using Chia_Client_API.WalletAPI_NS;
using CHIA_RPC.General_NS;
using CHIA_RPC.Objects_NS;
using CHIA_RPC.Wallet_NS.CATsAndTrading_NS;
using CHIA_RPC.Wallet_NS.Offer_NS;
using CHIA_RPC.Wallet_NS.Wallet_NS;
using CHIA_RPC.Wallet_NS.WalletManagement_NS;
using CHIA_RPC.Wallet_NS.WalletNode_NS;

namespace TransactionTypeTests.Objects
{
    internal static class Clients
    {
        // clients
        internal static WalletRpcClient Wallet_Client;
        internal static FullNodeRpcClient Fullnode_Client;
        // fingerprints
        internal static FingerPrint_RPC Primary_Fingerprint = new FingerPrint_RPC(2647845394);
        internal static FingerPrint_RPC Secondary_Fingerprint = new FingerPrint_RPC(549897867);
        static Clients ()
        {
            string fullNodeCertificatePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), @".testnet\ssl\");
            string walletCertificatePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), @".testnet\testnetclientSsl\");
            Fullnode_Client = new FullNodeRpcClient(reportResponseErrors: true, targetApiAddress: "192.168.1.117", targetCertificateBaseFolder: fullNodeCertificatePath);
            Wallet_Client = new WalletRpcClient(reportResponseErrors: true, targetApiAddress: "kryp-chiatestclient", targetCertificateBaseFolder: walletCertificatePath);
        }
        internal static void SwitchWallet(FingerPrint_RPC rpc)
        {
            FingerPrint_Response loggedInWallet = Clients.Wallet_Client.GetLoggedInFingerprint_Sync();
            if ((bool)loggedInWallet.success && loggedInWallet.fingerprint == rpc.fingerprint)
            {
                return;
            }
            Console.WriteLine($"switching to wallet: {rpc.fingerprint}");
            FingerPrint_Response loginResponse = Clients.Wallet_Client.LogIn_Sync(rpc);
            if (!(bool)loginResponse.success)
            {
                throw new Exception(loginResponse.error);
            }
            Console.WriteLine($"awaiting sync: {rpc.fingerprint}");
            Clients.Wallet_Client.AwaitWalletSync_Sync(timeoutSeconds: 1000);
        }
        internal static ulong SendTransaction(FingerPrint_RPC sourceWallet, FingerPrint_RPC targetWallet, ulong fee)
        {
            Console.WriteLine($"sending default transaction from: {sourceWallet.fingerprint} to {targetWallet.fingerprint}");
            // switch wallet and obtain height info

            SendTransaction_RPC transaction = new SendTransaction_RPC(
                wallet_id: 1, GlobalVar.WalletAdresses[targetWallet.fingerprint], amount_mojos: 1000UL, fee_mojos: fee);
            SwitchWallet(sourceWallet);
            GetHeightInfo_Response heightInfo = Clients.Wallet_Client.GetHeightInfo_Sync();
            ulong height = heightInfo.height;
            // generate transaction and wait for it
            while (true)
            {
                GetTransaction_Response sendTransaction_Result = Clients.Wallet_Client.SendTransaction_Sync(transaction);
                if (!(bool)sendTransaction_Result.success)
                {
                    if (sendTransaction_Result.error.StartsWith("Can't send more than"))
                    {
                        throw new Exception(sendTransaction_Result.error);
                    }
                    else if (sendTransaction_Result.error == "Wallet needs to be fully synced before sending transactions")
                    {
                        continue;
                    }
                }
                Console.WriteLine($"awaiting transaction completion: {sendTransaction_Result.transaction_id}");
                Clients.Wallet_Client.AwaitTransactionToConfirm_Sync(sendTransaction_Result, CancellationToken.None, timeoutInMinutes: 120);
                return height;
            }
        }
        internal static ulong SendCatTransaction(FingerPrint_RPC sourceWallet, FingerPrint_RPC targetWallet, Asset asset , ulong fee, string? rpcPath = null)
        {
            Console.WriteLine($"sending default transaction from: {sourceWallet.fingerprint} to {targetWallet.fingerprint}");
            // switch wallet and obtain height info
            SendTransaction_RPC transaction = new SendTransaction_RPC(
                walletID_RPC: AssetWrapper.GetAssetWallet(asset, sourceWallet),
                address: GlobalVar.WalletAdresses[targetWallet.fingerprint],
                amount_mojos: 1000,
                fee_mojos: fee
                );
            SwitchWallet(sourceWallet);
            GetHeightInfo_Response heightInfo = Clients.Wallet_Client.GetHeightInfo_Sync();
            ulong height = heightInfo.height;
            // generate transaction and wait for it
            CatSpend_RPC rpc = new CatSpend_RPC();
            rpc.wallet_id = transaction.wallet_id;
            rpc.amount = transaction.amount;
            rpc.inner_address = transaction.address;
            rpc.memos = new string[] { "Transfer" };
            rpc.fee = transaction.fee;
            if (rpcPath != null)
            {
                FingerPrint_Response sendingWallet = Clients.Wallet_Client.GetLoggedInFingerprint_Sync();
                sendingWallet.SaveResponseToFile(Path.Combine(rpcPath, "sendingWallet"));
                rpc.SaveRpcToFile(Path.Combine(rpcPath, "cat_spend_rpc"));
            }

            while (true)
            {
                CatSpend_Response record = Clients.Wallet_Client.CatSpend_Sync(rpc);

                if (!record.success ?? false)
                {
                    throw new Exception(record.error);
                }
                Console.WriteLine($"awaiting transaction completion: {record.transaction.trade_id}");
                GetTransaction_Response? transactionDetails = Clients.Wallet_Client.AwaitTransactionToConfirm_Sync(record.transaction, CancellationToken.None, TimeSpan.FromMinutes(120));
                return height;
            }
        }
        internal static ulong CreateAndAcceptOffer(FingerPrint_RPC sourceWallet, FingerPrint_RPC targetWallet, Asset[] assetsToOffer, Asset[] assetsToRequest, ulong senderFee, ulong RecipientFee)
        {
            Console.WriteLine($"creating offer from: {sourceWallet.fingerprint} to {targetWallet.fingerprint}");

            // switch wallet and obtain height info
            SwitchWallet(sourceWallet);
            GetHeightInfo_Response heightInfo = Clients.Wallet_Client.GetHeightInfo_Sync();
            ulong height = heightInfo.height;

            // generate Offer
            CreateOfferForIds_RPC createRPC = new CreateOfferForIds_RPC();
            foreach (Asset asset in assetsToOffer)
            {
                if (asset.AssetType == AssetType.NFT)
                {
                    createRPC.offer.Add(asset.AssetID, -1);
                }
                else
                {
                    createRPC.offer.Add(asset.AssetID, -7);
                }
            }
            foreach (Asset asset in assetsToRequest)
            {
                if (asset.AssetType == AssetType.NFT)
                {
                    createRPC.offer.Add(asset.AssetID, 1);
                }
                else
                {
                    createRPC.offer.Add(asset.AssetID, 7);
                }
            }
            createRPC.fee = senderFee;
            createRPC.SaveRpcToFile("transactions\\createOffer.rpc");
            OfferFile offer = Clients.Wallet_Client.CreateOfferForIds_Sync(createRPC);
            Thread.Sleep(5000);

            // go to accepting wallet
            SwitchWallet(targetWallet);
            TakeOffer_RPC take = new TakeOffer_RPC(offer, fee: RecipientFee);

            // generate transaction and wait for it
            while (true)
            {
                TradeRecord_Response tradeRecord = Clients.Wallet_Client.TakeOffer_Sync(take);
                if (!(bool)tradeRecord.success &&
                    (
                        tradeRecord.error == "Wallet needs to be fully synced before sending transactions"
                        || tradeRecord.error == "No peer connected"
                    ))
                {
                    Thread.Sleep(10000);
                    Clients.Wallet_Client.AwaitWalletSync_Sync();
                    continue;

                }
                Console.WriteLine($"awaiting transaction completion: {tradeRecord.trade_record.trade_id}");
                Clients.Wallet_Client.AwaitTransactionToConfirm_Sync(new TransactionID_RPC(tradeRecord.trade_record.trade_id), CancellationToken.None, timeoutInMinutes: 120);

                return height;
            }
        }
        internal static ulong CreateOffer(FingerPrint_RPC sourceWallet, Asset[] assetsToOffer, Asset[] assetsToRequest, ulong senderFee)
        {
            Console.WriteLine($"creating offer from: {sourceWallet.fingerprint}");

            // switch wallet and obtain height info
            SwitchWallet(sourceWallet);
            GetHeightInfo_Response heightInfo = Clients.Wallet_Client.GetHeightInfo_Sync();
            ulong height = heightInfo.height;

            // generate Offer
            CreateOfferForIds_RPC createRPC = new CreateOfferForIds_RPC();
            foreach (Asset asset in assetsToOffer)
            {
                if (asset.AssetType == AssetType.NFT)
                {
                    createRPC.offer.Add(asset.AssetID, -1);
                }
                else
                {
                    createRPC.offer.Add(asset.AssetID, -7);
                }
            }
            foreach (Asset asset in assetsToRequest)
            {
                if (asset.AssetType == AssetType.NFT)
                {
                    createRPC.offer.Add(asset.AssetID, 1);
                }
                else
                {
                    createRPC.offer.Add(asset.AssetID, 7);
                }
            }
            createRPC.fee = senderFee;
            createRPC.SaveRpcToFile("transactions\\createOffer.rpc");
            OfferFile offer = Clients.Wallet_Client.CreateOfferForIds_Sync(createRPC);
            Thread.Sleep(5000);

            return height;
        }
        internal static ulong CreateAndCancelOffer(FingerPrint_RPC targetWallet, Asset[] assetsToOffer, Asset[] assetsToRequest, ulong senderFee, ulong cancellationFee)
        {
            Console.WriteLine($"creating offer from: {targetWallet.fingerprint}");

            // switch wallet and obtain height info
            SwitchWallet(targetWallet);
            GetHeightInfo_Response heightInfo = Clients.Wallet_Client.GetHeightInfo_Sync();
            ulong height = heightInfo.height;

            // generate Offer
            CreateOfferForIds_RPC createRPC = new CreateOfferForIds_RPC();
            foreach (Asset asset in assetsToOffer)
            {
                if (asset.AssetType == AssetType.NFT)
                {
                    createRPC.offer.Add(asset.AssetID, -1);
                }
                else
                {
                    createRPC.offer.Add(asset.AssetID, -7);
                }
            }
            foreach (Asset asset in assetsToRequest)
            {
                if (asset.AssetType == AssetType.NFT)
                {
                    createRPC.offer.Add(asset.AssetID, 1);
                }
                else
                {
                    createRPC.offer.Add(asset.AssetID, 7);
                }
            }
            createRPC.fee = senderFee;

            OfferFile offer = Clients.Wallet_Client.CreateOfferForIds_Sync(createRPC);
            Thread.Sleep(5000);
            if (!offer.success)
            {
                throw new Exception(offer.error);
            }
            // cancel offer
            CancelOffer_RPC cancelRPC = new CancelOffer_RPC(offer, true, cancellationFee);
            while (true)
            {
                Success_Response cancelResponse = Clients.Wallet_Client.CancelOffer_Sync(cancelRPC);
                if (!(bool)cancelResponse.success && cancelResponse.error == "Wallet needs to be fully synced before sending transactions")
                {
                    continue;
                }
                Clients.Wallet_Client.AwaitOfferToExecuteOrCancel_Sync(offer, CancellationToken.None, timeoutInMinutes: 120);
                return height;
            }
        }
        internal static Transaction_DictMemos[] FetchNewTransactions(ulong blockHeight, FingerPrint_RPC targetWallet)
        {

            List<Transaction_DictMemos> relevantTransactions = new List<Transaction_DictMemos>();
            SwitchWallet(targetWallet);
            Thread.Sleep(20000);
            Console.WriteLine($"fetching transactions from: {targetWallet.fingerprint}");
            GetWallets_Response walletsResponse = Clients.Wallet_Client.GetWallets_Sync();
            Clients.Wallet_Client.AwaitWalletSync_Sync(timeoutSeconds: 1000);
            foreach (Wallets_info wallet in walletsResponse.wallets)
            {
                GetTransactions_RPC rpc = new GetTransactions_RPC(wallet.id, reverse: true);
                GetTransactions_Response transactions = Clients.Wallet_Client.GetTransactions_Sync(rpc);
                foreach (Transaction_DictMemos transaction in transactions.transactions)
                {
                    if (transaction.confirmed_at_height >= blockHeight)
                    {
                        relevantTransactions.Add(transaction);
                    }
                }
            }
            Console.WriteLine($"found transactions: {relevantTransactions.Count}");
            return relevantTransactions.ToArray();
        }
    }
}
