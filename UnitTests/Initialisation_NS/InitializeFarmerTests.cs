using Chia_Client_API.FarmerAPI_NS;
using System;
using System.IO;
using Xunit;

namespace CHIA_API_Tests.Initialisation_NS
{
    public class Testnet_Farmer : IDisposable
    {
        static Testnet_Farmer()
        {
            // ... initialize ...
            string certificatePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                ".testnet","ssl");
            Farmer_Client = new Farmer_RPC_Client(reportResponseErrors: false,targetApiAddress: "192.168.1.117", targetCertificateBaseFolder: certificatePath);
        }
        public static Farmer_RPC_Client Farmer_Client;
        public void Dispose()
        {
            // ... clean up ...
        }
    }
    [CollectionDefinition("Testnet_Farmer")]
    public class FarmerTestCollection : ICollectionFixture<Testnet_Farmer>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
