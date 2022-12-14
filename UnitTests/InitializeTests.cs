using System;
using System.IO;
using Xunit;

namespace UnitTests
{
    public class InitializeTests : IDisposable
    {
        public InitializeTests()
        {
            // ... initialize ...
            Chia_Client_API.GlobalVar.API_TargetIP = "192.168.1.132";
            Chia_Client_API.GlobalVar.API_CertificateFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            @".testnet\ssl\");

        }

        public void Dispose()
        {
            // ... clean up ...
        }
    }
    [CollectionDefinition("Test collection")]
    public class TestCollection : ICollectionFixture<InitializeTests>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
