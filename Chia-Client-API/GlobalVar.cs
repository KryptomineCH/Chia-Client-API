namespace Chia_Client_API
{
    public static class GlobalVar
    {
        public static string API_TargetIP = "localhost";
        public static string API_CertificateFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            @".chia\mainnet\config\ssl\");
    }
}
