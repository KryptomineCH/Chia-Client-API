using CHIA_API_Tests.Initialisation_NS;
using CHIA_RPC.FullNode_NS;
using CHIA_RPC.General_NS;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CHIA_API_Tests.FullNode_NS
{
    [Collection("Testnet_FullNode")]
    public class Fullnode_Tests
    {
        [Fact]
        public void GetAllMempoolItems_Test()
        {
            Task<GetAllMempoolItems_Response?> response = Testnet_FullNode.Fullnode_Client.GetAllMempoolItems_Async();
            GetAllMempoolItems_Response? results = response.Result;

            { }
        }
        [Fact]
        public void CheckHealthz_Test()
        {
            Success_Response? test1 = Testnet_FullNode.Fullnode_Client.HealthZ_Sync();
            Task<Success_Response?> response = Testnet_FullNode.Fullnode_Client.HealthZ_Async();
            Success_Response? results = response.Result;

            { }
        }
        [Fact]
        public void GetCoinRecordByPuzzhashes_Test()
        {
            GetCoinRecordsByPuzzleHashes_RPC rpc = new GetCoinRecordsByPuzzleHashes_RPC(
                puzzle_hashes: new string[] {
                "0x3d55fb3762b6db45add3942a3f8ce35be981092e385c7a0f4fdcdbbd5c26d273",
                "0x3440784feb92f41a0b0d85f2a60b2319cb323372775bdf223b7042479f41c5cd",
                "0x2832b7d8e2aefd9d49592c134b50fb8d83607122ac5c9219b404b99596ea311d"
                }, include_spent_coins: true);

            GetCoinRecords_Response? test1 = Testnet_FullNode.Fullnode_Client.GetCoinRecordsByPuzzleHashes_Sync(rpc);
            { }
        }
        [Fact]
        public void GetCoinRecordsByNames_Test()
        {
            GetCoinRecordsByNames_RPC rpc = new GetCoinRecordsByNames_RPC()
            {
                names= new string[] {
                    "0x2832b7d8e2aefd9d49592c134b50fb8d83607122ac5c9219b404b99596ea311d",
                    "0x2bc1f5e8240e6bcbf4ffb3d4d590761612fe98986fb4db1fd1948de2cc65a7e5"
                },
                include_spent_coins=true
            };

            GetCoinRecords_Response? test1 = Testnet_FullNode.Fullnode_Client.GetCoinRecordsByNames_Sync(rpc);
            { }
        }
        [Fact]
        public void GetStandardTransactionFee_Test()
        {
            GetFeeEstimate_Response? result = Testnet_FullNode.Fullnode_Client.GetFeeEstimate_Sync(new GetFeeEstimate_RPC(
                new TimeSpan[] { TimeSpan.FromSeconds(20), TimeSpan.FromSeconds(60), TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(50) })
            );
            { }
        }
    }
}
