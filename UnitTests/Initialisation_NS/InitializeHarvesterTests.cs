using Chia_Client_API.HarvesterAPI_NS;
using System;
using System.IO;
using Xunit;

namespace CHIA_API_Tests.Initialisation_NS
{
    public class Testnet_Harvester : IDisposable
    {
        static Testnet_Harvester()
        {
            // ... initialize ...
            string certificatePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                ".testnet","ssl");
            Harvester_Client = new Harvester_RPC_Client(reportResponseErrors: false,targetApiAddress: "192.168.1.117", targetCertificateBaseFolder: certificatePath);
        }
        public static Harvester_RPC_Client Harvester_Client;
        public void Dispose()
        {
            // ... clean up ...
        }
    }
    [CollectionDefinition("Testnet_Harvester")]
    public class HarvesterTestCollection : ICollectionFixture<Testnet_Harvester>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
