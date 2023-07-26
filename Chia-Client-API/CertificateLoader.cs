using System.Security.Cryptography.X509Certificates;

namespace Chia_Client_API
{
    internal class CertificateLoader
    {
        internal static X509Certificate2 GetCertificate(Endpoint endpoint, string basePath)
        {
            X509Certificate2? certificate = null;
            if (Certificates.TryGetValue(endpoint, out certificate))
            {
                return certificate;
            }
            DirectoryInfo endpointCertificateDirectory = new DirectoryInfo(Path.Combine(basePath, endpoint.ToString()));
            FileInfo[] files = endpointCertificateDirectory.GetFiles();
            FileInfo? certFile = null;
            FileInfo? keyFile = null;
            foreach(FileInfo file in files)
            {
                if (!file.Name.StartsWith("private")) continue;
                if (file.Name.EndsWith(".crt")) certFile = file;
                else if (file.Name.EndsWith(".key")) keyFile = file;
            }
            if (certFile == null || keyFile == null)
            {
                throw new Exception($"certificate file for endpoint {endpoint} not found!");
            }
            certificate = X509Certificate2.CreateFromPemFile(certFile.FullName, keyFile.FullName);
            certificate = new X509Certificate2(certificate.Export(X509ContentType.Pfx));
            Certificates[endpoint] = certificate;
            return certificate;
        }
        private static Dictionary<Endpoint, X509Certificate2> Certificates = new Dictionary<Endpoint, X509Certificate2>();
        /// <summary>
        /// Constructs an ephemeral <see cref="X509Certificate2"/> from a crt and key stored as files
        /// </summary>
        /// <param name="ed">The the path of the endpoint</param>
        /// <param name="basePath">The full path to the RSA encoded private key (.key)</param>
        /// <returns>An ephemeral certificate that can be used for WebSocket authentication</returns>
        public static X509Certificate2Collection GetCerts(Endpoint ed, string basePath)
        {
            using X509Certificate2 cert = GetCertificate(ed, basePath);
            return new(cert);
        }
    }
}