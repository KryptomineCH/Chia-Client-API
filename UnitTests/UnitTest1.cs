using chia.dotnet;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Chia_Client_API.WebsocketAPI.Send("{\"command\": \"get_blockchain_state\",\"ack\": false,\"data\": { },\"request_id\": \"123456\",\"destination\": \"wallet\",\"origin\": \"ui\"}");
            var test = Chia_Client_API.WebsocketAPI.ReceiveStatusUpdate("123456");
            { }
        }
        [Fact]
        public void TestTraditionalApi()
        {
            Chia_Client_API.httpclient.Test();
            { }
        }
        [Fact]
        public void TestWebSocketSharp()
        {
            Chia_Client_API.websocketsharp.Connect("{\"command\": \"get_blockchain_state\",\"ack\": false,\"data\": { },\"request_id\": \"123456\",\"destination\": \"wallet\",\"origin\": \"ui\"}");
            while (true)
            {
                Task.Delay(100).Wait();
            }
            { }
        }
        [Fact]
        public void TestChiaDotNet()
        {
            var endpoint = Config.Open().GetEndpoint("daemon");
            using var rpcClient = new WebSocketRpcClient(endpoint);
            rpcClient.Connect().Wait();

            var daemon = new DaemonProxy(rpcClient, "unit_tests");
            daemon.RegisterService().Wait();

            var fullNode = new FullNodeProxy(rpcClient, "unit_tests");
            var state = fullNode.GetBlockchainState().Result;
        }
    }
}