using Chia_Client_API.FullNodeAPI_NS;
using System;
using System.IO;
using Xunit;

namespace CHIA_API_Tests.Initialisation_NS
{
    public class Testnet_FullNode : IDisposable
    {
        public Testnet_FullNode()
        {
            // ... initialize ...
            string certificatePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                @".testnet\ssl\");
            Fullnode_Client = new FullNode_RPC_Client(targetApiAddress: "192.168.1.117", targetCertificateBaseFolder: certificatePath);
        }
        public static FullNode_RPC_Client Fullnode_Client;
        public void Dispose()
        {
            // ... clean up ...
        }
    }
    [CollectionDefinition("Testnet_FullNode")]
    public class FullNodeTestCollection : ICollectionFixture<Testnet_FullNode>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
