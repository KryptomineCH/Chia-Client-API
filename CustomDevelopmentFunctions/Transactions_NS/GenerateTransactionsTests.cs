using CHIA_RPC.Objects_NS;
using TransactionTypeTests.Objects;

namespace TransactionTypeTests.Transactions_NS
{
    
    public class GenerateTransactionsTests
    {
        [Fact]
        [Trait("Category", "Manual")]
        public async void GenerateTransactions()
        {
            ulong CurrencyAmount = 7;
            ulong FeeAmount = 1000;

            ulong height = 0;

            // ####################
            // std chia transaction
            // ####################
            /// send std chia transaction without fee
            height = Clients.SendTransaction(Clients.Primary_Fingerprint, Clients.Secondary_Fingerprint, 0);
            Transaction_DictMemos[] transactions0 = Clients.FetchNewTransactions(height, Clients.Primary_Fingerprint);
            TransactionStorage.StoreTransactions(transactions0, "OutgoingXchWithoutFee");

            /// send std chia transaction with fee
            height = Clients.SendTransaction(Clients.Primary_Fingerprint, Clients.Secondary_Fingerprint, FeeAmount);
            Transaction_DictMemos[] transactions1 = Clients.FetchNewTransactions(height, Clients.Primary_Fingerprint);
            TransactionStorage.StoreTransactions(transactions1, "OutgoingXchWithFee");

            /// receive standard chia transaction without fee
            height = Clients.SendTransaction(Clients.Secondary_Fingerprint, Clients.Primary_Fingerprint, 0UL);
            Transaction_DictMemos[] transactions2 = Clients.FetchNewTransactions(height, Clients.Primary_Fingerprint);
            TransactionStorage.StoreTransactions(transactions2, "IncomingXchWithoutFee");

            /// receive standard chia transaction with fee
            height = Clients.SendTransaction(Clients.Secondary_Fingerprint, Clients.Primary_Fingerprint, FeeAmount);
            Transaction_DictMemos[] transactions3 = Clients.FetchNewTransactions(height, Clients.Primary_Fingerprint);
            TransactionStorage.StoreTransactions(transactions3, "IncomingXchWithFee");

            // ###################
            // std cat transaction
            // ###################
            /// send std cat transaction without fee
            height = Clients.SendCatTransaction(Clients.Primary_Fingerprint, Clients.Secondary_Fingerprint, AssetWrapper.Assets["btf"], 0UL);
            Transaction_DictMemos[] transactions4 = Clients.FetchNewTransactions(height, Clients.Primary_Fingerprint);
            TransactionStorage.StoreTransactions(transactions4, "OutgoingCatWithoutFee");

            /// send std cat transaction with fee
            height = Clients.SendCatTransaction(Clients.Primary_Fingerprint, Clients.Secondary_Fingerprint, AssetWrapper.Assets["btf"], FeeAmount);
            Transaction_DictMemos[] transactions5 = Clients.FetchNewTransactions(height, Clients.Primary_Fingerprint);
            TransactionStorage.StoreTransactions(transactions5, "OutgoingCatWithFee");

            /// receive std cat transaction without fee
            height = Clients.SendCatTransaction(Clients.Secondary_Fingerprint, Clients.Primary_Fingerprint, AssetWrapper.Assets["btf"], 0UL);
            Transaction_DictMemos[] transactions6 = Clients.FetchNewTransactions(height, Clients.Primary_Fingerprint);
            TransactionStorage.StoreTransactions(transactions6, "IncomingCatWithoutFee");

            /// receive std cat transaction with fee 
            height = Clients.SendCatTransaction(Clients.Secondary_Fingerprint, Clients.Primary_Fingerprint, AssetWrapper.Assets["btf"], FeeAmount);
            Transaction_DictMemos[] transactions7 = Clients.FetchNewTransactions(height, Clients.Primary_Fingerprint);
            TransactionStorage.StoreTransactions(transactions7, "IncomingCatWitFee");

            // #################################
            // accept foreign offers without fee
            // #################################
            /// accept foreign offer xch to cat without fee
            height = Clients.CreateAndAcceptOffer(Clients.Secondary_Fingerprint, Clients.Primary_Fingerprint, new[] { AssetWrapper.Assets["xch"] }, new[] { AssetWrapper.Assets["btf"] }, 0,0);
            Transaction_DictMemos[] transactions8 = Clients.FetchNewTransactions(height, Clients.Primary_Fingerprint);
            TransactionStorage.StoreTransactions(transactions8, "AcceptedCatToXchOfferWithoutFee");

            /// accept foreign offer cat to xch without fee
            height = Clients.CreateAndAcceptOffer(Clients.Secondary_Fingerprint, Clients.Primary_Fingerprint, new[] { AssetWrapper.Assets["btf"] }, new[] { AssetWrapper.Assets["xch"] }, 0, 0);
            Transaction_DictMemos[] transactions9 = Clients.FetchNewTransactions(height, Clients.Primary_Fingerprint);
            TransactionStorage.StoreTransactions(transactions9, "AcceptedXchToCatOfferWithoutFee");

            /// accept foreign offer cat to cat without fee
            height = Clients.CreateAndAcceptOffer(Clients.Secondary_Fingerprint, Clients.Primary_Fingerprint, new[] { AssetWrapper.Assets["btf"] }, new[] { AssetWrapper.Assets["tdbx"] }, 0, 0);
            Transaction_DictMemos[] transactions12 = Clients.FetchNewTransactions(height, Clients.Primary_Fingerprint);
            TransactionStorage.StoreTransactions(transactions12, "AcceptedCatToCatOfferWithoutFee_1");
            height = Clients.CreateAndAcceptOffer(Clients.Secondary_Fingerprint, Clients.Primary_Fingerprint, new[] { AssetWrapper.Assets["tdbx"] }, new[] { AssetWrapper.Assets["btf"] }, 0, 0);
            Transaction_DictMemos[] transactions13 = Clients.FetchNewTransactions(height, Clients.Primary_Fingerprint);
            TransactionStorage.StoreTransactions(transactions13, "AcceptedCatToCatOfferWithoutFee_2");


            /// accept foreign offer xch to cat + cat without fee
            height = Clients.CreateAndAcceptOffer(Clients.Secondary_Fingerprint, Clients.Primary_Fingerprint, new[] { AssetWrapper.Assets["xch"] }, 
                new[] { AssetWrapper.Assets["tdbx"], AssetWrapper.Assets["btf"] }, 0, 0);
            Transaction_DictMemos[] transactions16 = Clients.FetchNewTransactions(height, Clients.Primary_Fingerprint);
            TransactionStorage.StoreTransactions(transactions16, "AcceptedCatAndCatToXchOfferWithoutFee");

            /// accept foreign offer cat + cat to xch without fee
            height = Clients.CreateAndAcceptOffer(Clients.Secondary_Fingerprint, Clients.Primary_Fingerprint, new[] { AssetWrapper.Assets["tdbx"], AssetWrapper.Assets["btf"] },
                new[] { AssetWrapper.Assets["xch"] }, 0, 0);
            Transaction_DictMemos[] transactions17 = Clients.FetchNewTransactions(height, Clients.Primary_Fingerprint);
            TransactionStorage.StoreTransactions(transactions17, "AcceptedXchToCatAndCatOfferWithoutFee");

            // ##############################
            // accept foreign offers with fee
            // ##############################
            /// accept foreign offer xch to cat with fee
            //height = DefaultFunctions.CreateAndAcceptOffer(TestWallet2_Fingerprint, TestWallet3_Fingerprint, new[] { AssetWrapper.Assets["xch"] }, new[] { AssetWrapper.Assets["btf"] }, 0, FeeAmount);
            //Transaction_DictMemos[] transactions18 = DefaultFunctions.FetchNewTransactions(height, TestWallet3_Fingerprint);
            //TransactionStorage.StoreTransactions(transactions18, "AcceptedCatToXchOfferWithFee");

            /// accept foreign offer cat to xch with fee
            //height = DefaultFunctions.CreateAndAcceptOffer(TestWallet2_Fingerprint, TestWallet3_Fingerprint, new[] { AssetWrapper.Assets["btf"] }, new[] { AssetWrapper.Assets["xch"] }, 0, FeeAmount);
            //Transaction_DictMemos[] transactions19 = DefaultFunctions.FetchNewTransactions(height, TestWallet3_Fingerprint);
            //TransactionStorage.StoreTransactions(transactions19, "AcceptedXchToCatOfferWithFee");

            /// accept foreign offer cat to cat with fee
            //height = DefaultFunctions.CreateAndAcceptOffer(TestWallet2_Fingerprint, TestWallet3_Fingerprint, new[] { AssetWrapper.Assets["btf"] }, new[] { AssetWrapper.Assets["tdbx"] }, 0, FeeAmount);
            //Transaction_DictMemos[] transactions22 = DefaultFunctions.FetchNewTransactions(height, TestWallet3_Fingerprint);
            //TransactionStorage.StoreTransactions(transactions22, "AcceptedCatToCatOfferWithFee");

            //height = DefaultFunctions.CreateAndAcceptOffer(TestWallet2_Fingerprint, TestWallet3_Fingerprint, new[] { AssetWrapper.Assets["tdbx"] }, new[] { AssetWrapper.Assets["btf"] }, 0, FeeAmount);
            //Transaction_DictMemos[] transactions23 = DefaultFunctions.FetchNewTransactions(height, TestWallet3_Fingerprint);
            //TransactionStorage.StoreTransactions(transactions23, "AcceptedCatToCatOfferWithFee");

            /// accept foreign offer xch to cat + cat with fee
            //height = DefaultFunctions.CreateAndAcceptOffer(TestWallet2_Fingerprint, TestWallet3_Fingerprint, new[] { AssetWrapper.Assets["xch"] },
            //    new[] { AssetWrapper.Assets["tdbx"], AssetWrapper.Assets["btf"] }, 0, FeeAmount);
            //Transaction_DictMemos[] transactions26 = DefaultFunctions.FetchNewTransactions(height, TestWallet3_Fingerprint);
            //TransactionStorage.StoreTransactions(transactions26, "AcceptedCatAndCatToXchOfferWithFee");


            /// accept foreign offer cat + cat to xch with fee
            //height = DefaultFunctions.CreateAndAcceptOffer(TestWallet2_Fingerprint, TestWallet3_Fingerprint, new[] { AssetWrapper.Assets["tdbx"], AssetWrapper.Assets["btf"] },
            //    new[] { AssetWrapper.Assets["xch"] }, 0, FeeAmount);
            //Transaction_DictMemos[] transactions27 = DefaultFunctions.FetchNewTransactions(height, TestWallet3_Fingerprint);
            //TransactionStorage.StoreTransactions(transactions27, "AcceptedXchToCatAndCatOfferWithFee");

            // ##################################
            // own offer without fee got accepted
            // ##################################
            /// offer xch to cat without fee got accepted
            //height = DefaultFunctions.CreateAndAcceptOffer(TestWallet3_Fingerprint, TestWallet2_Fingerprint, new[] { AssetWrapper.Assets["xch"] }, new[] { AssetWrapper.Assets["btf"] }, 0, 0);
            //Transaction_DictMemos[] transactions28 = DefaultFunctions.FetchNewTransactions(height, TestWallet3_Fingerprint);
            //TransactionStorage.StoreTransactions(transactions28, "XchToCatOfferWithoutFeeGotAccepted");

            /// offer cat to xch without fee got accepted
            //height = DefaultFunctions.CreateAndAcceptOffer(TestWallet3_Fingerprint, TestWallet2_Fingerprint, new[] { AssetWrapper.Assets["btf"] }, new[] { AssetWrapper.Assets["xch"] }, 0, 0);
            //Transaction_DictMemos[] transactions29 = DefaultFunctions.FetchNewTransactions(height, TestWallet3_Fingerprint);
            //TransactionStorage.StoreTransactions(transactions29, "CatToXchOfferWithoutFeeGotAccepted");

            /// offer cat to cat without fee got accepted
            //height = DefaultFunctions.CreateAndAcceptOffer(TestWallet3_Fingerprint, TestWallet2_Fingerprint, new[] { AssetWrapper.Assets["btf"] }, new[] { AssetWrapper.Assets["tdbx"] }, 0, 0);
            //Transaction_DictMemos[] transactions32 = DefaultFunctions.FetchNewTransactions(height, TestWallet3_Fingerprint);
            //TransactionStorage.StoreTransactions(transactions32, "CatToCatOfferWithoutFeeGotAccepted");
            // (1?)
            //height = DefaultFunctions.CreateAndAcceptOffer(TestWallet3_Fingerprint, TestWallet2_Fingerprint, new[] { AssetWrapper.Assets["tdbx"] }, new[] { AssetWrapper.Assets["btf"] }, 0, 0);
            //Transaction_DictMemos[] transactions33 = DefaultFunctions.FetchNewTransactions(height, TestWallet3_Fingerprint);
            //TransactionStorage.StoreTransactions(transactions33, "CatToCatOfferWithoutFeeGotAccepted");

            /// offer xch to cat + cat without fee got accepted
            //height = DefaultFunctions.CreateAndAcceptOffer(TestWallet3_Fingerprint, TestWallet2_Fingerprint, new[] { AssetWrapper.Assets["xch"] },
            //    new[] { AssetWrapper.Assets["tdbx"], AssetWrapper.Assets["btf"] }, 0, 0);
            //Transaction_DictMemos[] transactions36 = DefaultFunctions.FetchNewTransactions(height, TestWallet3_Fingerprint);
            //TransactionStorage.StoreTransactions(transactions36, "XchToCatAndCatOfferWithoutFeeGotAccepted");

            /// offer cat + cat to xch without fee got accepted
            //height = DefaultFunctions.CreateAndAcceptOffer(TestWallet3_Fingerprint, TestWallet2_Fingerprint, new[] { AssetWrapper.Assets["tdbx"], AssetWrapper.Assets["btf"] },
            //    new[] { AssetWrapper.Assets["xch"] }, 0, 0);
            //Transaction_DictMemos[] transactions37 = DefaultFunctions.FetchNewTransactions(height, TestWallet3_Fingerprint);
            //TransactionStorage.StoreTransactions(transactions37, "CatAndCatToXchOfferWithoutFeeGotAccepted");

            // ###############################
            // own offer with fee got accepted
            // ###############################
            /// offer xch to cat with fee got accepted (2?)
            //height = DefaultFunctions.CreateAndAcceptOffer(TestWallet3_Fingerprint, TestWallet2_Fingerprint, new[] { AssetWrapper.Assets["xch"] }, new[] { AssetWrapper.Assets["btf"] }, FeeAmount, 0);
            //Transaction_DictMemos[] transactions38 = DefaultFunctions.FetchNewTransactions(height, TestWallet3_Fingerprint);
            //TransactionStorage.StoreTransactions(transactions38, "XchToCatOfferWithFeeGotAccepted");

            /// offer cat to xch with fee got accepted (5?)
            //height = DefaultFunctions.CreateAndAcceptOffer(TestWallet3_Fingerprint, TestWallet2_Fingerprint, new[] { AssetWrapper.Assets["btf"] }, new[] { AssetWrapper.Assets["xch"] }, FeeAmount, 0);
            //Transaction_DictMemos[] transactions39 = DefaultFunctions.FetchNewTransactions(height, TestWallet3_Fingerprint);
            //TransactionStorage.StoreTransactions(transactions39, "CatToXchOfferWithFeeGotAccepted");

            /// offer cat to cat with fee got accepted
            //height = DefaultFunctions.CreateAndAcceptOffer(TestWallet3_Fingerprint, TestWallet2_Fingerprint, new[] { AssetWrapper.Assets["btf"] }, new[] { AssetWrapper.Assets["tdbx"] }, FeeAmount, 0);
            //Transaction_DictMemos[] transactions42 = DefaultFunctions.FetchNewTransactions(height, TestWallet3_Fingerprint);
            //TransactionStorage.StoreTransactions(transactions42, "CatToCatOfferWithFeeGotAccepted");
            //height = DefaultFunctions.CreateAndAcceptOffer(TestWallet3_Fingerprint, TestWallet2_Fingerprint, new[] { AssetWrapper.Assets["tdbx"] }, new[] { AssetWrapper.Assets["btf"] }, FeeAmount, 0);
            //Transaction_DictMemos[] transactions43 = DefaultFunctions.FetchNewTransactions(height, TestWallet3_Fingerprint);
            //TransactionStorage.StoreTransactions(transactions43, "CatToCatOfferWithFeeGotAccepted");

            /// offer xch to cat + cat with fee got accepted
            //height = DefaultFunctions.CreateAndAcceptOffer(TestWallet3_Fingerprint, TestWallet2_Fingerprint, new[] { AssetWrapper.Assets["xch"] },
            //    new[] { AssetWrapper.Assets["tdbx"], AssetWrapper.Assets["btf"] }, FeeAmount, 0);
            //Transaction_DictMemos[] transactions46 = DefaultFunctions.FetchNewTransactions(height, TestWallet3_Fingerprint);
            //TransactionStorage.StoreTransactions(transactions46, "XchToCatAndCatOfferWithFeeGotAccepted");

            /// offer cat + cat to xch with fee got accepted
            //height = DefaultFunctions.CreateAndAcceptOffer(TestWallet3_Fingerprint, TestWallet2_Fingerprint, new[] { AssetWrapper.Assets["tdbx"], AssetWrapper.Assets["btf"] },
            //    new[] { AssetWrapper.Assets["xch"] }, FeeAmount, 0);
            //Transaction_DictMemos[] transactions47 = DefaultFunctions.FetchNewTransactions(height, TestWallet3_Fingerprint);
            //TransactionStorage.StoreTransactions(transactions47, "CatAndCatToXchOfferWithFeeGotAccepted");

            // ########################################
            // offer without fee got cancelled with fee
            // ########################################
            /// offer xch to cat with fee got accepted
            //height = DefaultFunctions.CreateAndCancelOffer(TestWallet3_Fingerprint, new[] { AssetWrapper.Assets["xch"] }, new[] { AssetWrapper.Assets["btf"] }, 0, FeeAmount);
            //Transaction_DictMemos[] transactions58 = DefaultFunctions.FetchNewTransactions(height, TestWallet3_Fingerprint);
            //TransactionStorage.StoreTransactions(transactions58, "XchToCatOfferWithoutFeeCancelledWithFee");

            /// offer cat to xch with fee got accepted
            //height = DefaultFunctions.CreateAndCancelOffer(TestWallet3_Fingerprint, new[] { AssetWrapper.Assets["btf"] }, new[] { AssetWrapper.Assets["xch"] }, 0, FeeAmount);
            //Transaction_DictMemos[] transactions59 = DefaultFunctions.FetchNewTransactions(height, TestWallet3_Fingerprint);
            //TransactionStorage.StoreTransactions(transactions59, "CatToXchOfferWithoutFeeCancelledWithFee");


            /// offer cat to cat with fee got accepted
            //height = DefaultFunctions.CreateAndCancelOffer(TestWallet3_Fingerprint, new[] { AssetWrapper.Assets["btf"] }, new[] { AssetWrapper.Assets["tdbx"] }, 0, FeeAmount);
            //Transaction_DictMemos[] transactions62 = DefaultFunctions.FetchNewTransactions(height, TestWallet3_Fingerprint);
            //TransactionStorage.StoreTransactions(transactions62, "CatToCatOfferWithoutFeeCancelledWithFee");

            /// offer xch to cat + cat with fee got accepted
            //height = DefaultFunctions.CreateAndCancelOffer(TestWallet3_Fingerprint, new[] { AssetWrapper.Assets["xch"] },
            //    new[] { AssetWrapper.Assets["tdbx"], AssetWrapper.Assets["btf"] }, 0, FeeAmount);
            //Transaction_DictMemos[] transactions66 = DefaultFunctions.FetchNewTransactions(height, TestWallet3_Fingerprint);
            //TransactionStorage.StoreTransactions(transactions66, "XchToCatAndCatOfferWithoutFeeCancelledWithFee");

            /// offer cat + cat to xch with fee got accepted
            //height = DefaultFunctions.CreateAndCancelOffer(TestWallet3_Fingerprint, new[] { AssetWrapper.Assets["tdbx"], AssetWrapper.Assets["btf"] },
            //    new[] { AssetWrapper.Assets["xch"] }, 0, FeeAmount);
            //Transaction_DictMemos[] transactions67 = DefaultFunctions.FetchNewTransactions(height, TestWallet3_Fingerprint);
            //TransactionStorage.StoreTransactions(transactions67, "CatAndCatToXchOfferWithoutFeeCancelledWithFee");

            // #####################################
            // offer with fee got cancelled with fee
            // #####################################
            /// offer xch to cat with fee got accepted
            // TODO: Why no result here??
            //height = DefaultFunctions.CreateAndCancelOffer(TestWallet3_Fingerprint, new[] { AssetWrapper.Assets["xch"] }, new[] { AssetWrapper.Assets["btf"] }, FeeAmount, FeeAmount);
            //Thread.Sleep(10000);
            //Transaction_DictMemos[] transactions78 = DefaultFunctions.FetchNewTransactions(height, TestWallet3_Fingerprint);
            //TransactionStorage.StoreTransactions(transactions78, "XchToCatOfferWithFeeCancelledWithFee");
            //Thread.Sleep(10000);

            /// offer cat to xch with fee got accepted
            //height = DefaultFunctions.CreateAndCancelOffer(TestWallet3_Fingerprint, new[] { AssetWrapper.Assets["btf"] }, new[] { AssetWrapper.Assets["xch"] }, FeeAmount, FeeAmount);
            //Thread.Sleep(10000);
            //Transaction_DictMemos[] transactions79 = DefaultFunctions.FetchNewTransactions(height, TestWallet3_Fingerprint);
            //TransactionStorage.StoreTransactions(transactions79, "CatToXchOfferWithFeeCancelledWithFee");
            //Thread.Sleep(10000);

            /// offer cat to cat with fee got accepted
            //height = DefaultFunctions.CreateAndCancelOffer(TestWallet3_Fingerprint, new[] { AssetWrapper.Assets["btf"] }, new[] { AssetWrapper.Assets["tdbx"] }, FeeAmount, FeeAmount);
            //Thread.Sleep(10000);
            //Transaction_DictMemos[] transactions82 = DefaultFunctions.FetchNewTransactions(height, TestWallet3_Fingerprint);
            //TransactionStorage.StoreTransactions(transactions82, "CatToCatOfferWithFeeCancelledWithFee");
            //Thread.Sleep(10000);

            /// offer xch to cat + cat with fee got accepted
            //height = DefaultFunctions.CreateAndCancelOffer(TestWallet3_Fingerprint, new[] { AssetWrapper.Assets["xch"] },
            //    new[] { AssetWrapper.Assets["tdbx"], AssetWrapper.Assets["btf"] }, FeeAmount, FeeAmount);
            //Thread.Sleep(10000);
            //Transaction_DictMemos[] transactions86 = DefaultFunctions.FetchNewTransactions(height, TestWallet3_Fingerprint);
            //TransactionStorage.StoreTransactions(transactions86, "XchToCatAndCatOfferWithFeeCancelledWithFee");
            //Thread.Sleep(10000);

            /// offer cat + cat to xch with fee got accepted
            //height = DefaultFunctions.CreateAndCancelOffer(TestWallet3_Fingerprint, new[] { AssetWrapper.Assets["tdbx"], AssetWrapper.Assets["btf"] },
            //    new[] { AssetWrapper.Assets["xch"] }, FeeAmount, FeeAmount);
            //Thread.Sleep(10000);
            //Transaction_DictMemos[] transactions87 = DefaultFunctions.FetchNewTransactions(height, TestWallet3_Fingerprint);
            //TransactionStorage.StoreTransactions(transactions87, "CatAndCatToXchOfferWithFeeCancelledWithFee");
            //Thread.Sleep(10000);
        }
    }
}
