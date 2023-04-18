using Chia_Client_API.DatalayerAPI_NS;
using CHIA_RPC.General_NS;
using CHIA_RPC.Wallet_NS.KeyManagement;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CHIA_API_Tests.Initialisation_NS
{
    public class Testnet_Datalayer : IDisposable
    {
        public Testnet_Datalayer()
        {
            // ... initialize ...
            string certificatePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                @".testnet\ssl\");
            Datalayer_Client = new Datalayer_RPC_Client(targetApiAddress: "192.168.1.117", targetCertificateBaseFolder: certificatePath);
        }
        public static Datalayer_RPC_Client Datalayer_Client;
        
        public void Dispose()
        {
            // ... clean up ...
        }
    }
    [CollectionDefinition("Testnet_Datalayer")]
    public class DatalayerTestCollection : ICollectionFixture<Testnet_Datalayer>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
