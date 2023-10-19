using System.Reflection;

namespace Chia_Client_API
{
    /// <summary>
    /// Provides Variables used for the Client
    /// </summary>
    public static class GlobalVar
    {
        /// <summary>
        /// The default certificate folder when accessing the local chia wallet
        /// </summary>
        public static string API_CertificateFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            @".chia\mainnet\config\ssl\");
        /// <summary>
        /// returns the nuget version
        /// </summary>
        public static Version PackageVersion
        {
            get
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                Version version = assembly.GetName().Version;
                return version;
            }
        }
    }
}
