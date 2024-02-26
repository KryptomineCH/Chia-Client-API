using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Collections.Concurrent;
using CHIA_RPC.General_NS;

namespace Chia_Client_API
{
    internal class CertificateLoader
    {
        private static ConcurrentDictionary<Endpoint, X509Certificate2Collection> Certificates = new ConcurrentDictionary<Endpoint, X509Certificate2Collection>();

        /// <summary>
        /// Retrieves a certificate collection for a given endpoint, loading it from files if necessary.
        /// </summary>
        /// <param name="endpoint">The endpoint for which to retrieve the certificate.</param>
        /// <param name="basePath">The base file path for certificate files.</param>
        /// <returns>A collection of X509 certificates.</returns>
        /// <exception cref="FileNotFoundException">Thrown when the certificate file or key file is not found.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the certificate or key file is not correctly formatted.</exception>
        internal static X509Certificate2Collection GetCertificate(Endpoint endpoint, string basePath)
        {
            if (Certificates.TryGetValue(endpoint, out var certificateCollection))
            {
                return certificateCollection;
            }

            var endpointCertificateDirectory = new DirectoryInfo(Path.Combine(basePath, endpoint.ToString()));
            var files = endpointCertificateDirectory.GetFiles();
            FileInfo? certFile = files.FirstOrDefault(f => f.Name.EndsWith(".crt"));
            FileInfo? keyFile = files.FirstOrDefault(f => f.Name.EndsWith(".key"));

            if (certFile == null || keyFile == null)
            {
                throw new FileNotFoundException($"Certificate or key file for endpoint {endpoint} not found!");
            }

            var newCertCollection = LoadCertificateFromFiles(certFile.FullName, keyFile.FullName);
            Certificates[endpoint] = newCertCollection; // This ensures the dictionary doesn't grow indefinitely
            return newCertCollection;
        }

        /// <summary>
        /// Loads the certificate from the specified certificate and key file paths.
        /// </summary>
        /// <param name="certPath">The path to the certificate file.</param>
        /// <param name="keyPath">The path to the key file.</param>
        /// <returns>A collection of X509 certificates.</returns>
        public static X509Certificate2Collection LoadCertificateFromFiles(string certPath, string keyPath)
        {
            if (!File.Exists(certPath))
            {
                throw new FileNotFoundException($"CRT file {certPath} not found.");
            }

            if (!File.Exists(keyPath))
            {
                throw new FileNotFoundException($"Key file {keyPath} not found.");
            }

            using StreamReader certStreamReader = new(certPath);
            using StreamReader keyStreamReader = new(keyPath);

            return DeserializeCert(certStreamReader.ReadToEnd(), keyStreamReader.ReadToEnd());
        }

        /// <summary>
        /// Deserializes a certificate from its string representations.
        /// </summary>
        /// <param name="certBlob">The certificate data as a string.</param>
        /// <param name="keyBlob">The key data as a string.</param>
        /// <returns>A collection containing the deserialized certificate.</returns>
        public static X509Certificate2Collection DeserializeCert(string certBlob, string keyBlob)
        {
            if (string.IsNullOrEmpty(certBlob))
            {
                throw new ArgumentNullException(nameof(certBlob), "Certificate data is empty.");
            }

            if (string.IsNullOrEmpty(keyBlob))
            {
                throw new ArgumentNullException(nameof(keyBlob), "Key data is empty.");
            }

            using X509Certificate2 cert = new(Encoding.UTF8.GetBytes(certBlob));
            using var rsa = DeserializePrivateKey(keyBlob);
            using var certWithKey = cert.CopyWithPrivateKey(rsa);

            var keyBytes = certWithKey.Export(X509ContentType.Pkcs12);
            var ephemeralX509Cert = new X509Certificate2(keyBytes); // do not dispose in this method!

            return new X509Certificate2Collection(ephemeralX509Cert);
        }
        private static RSA DeserializePrivateKey(string serializedKey)
        {
            const string BeginRsaPrivateKey = "-----BEGIN RSA PRIVATE KEY-----";
            const string EndRsaPrivateKey = "-----END RSA PRIVATE KEY-----";
            const string BeginPrivateKey = "-----BEGIN PRIVATE KEY-----";
            const string EndPrivateKey = "-----END PRIVATE KEY-----";

            var rsa = RSA.Create();
            if (serializedKey.StartsWith(BeginRsaPrivateKey, StringComparison.Ordinal))
            {
                var base64 = serializedKey.Replace(BeginRsaPrivateKey, string.Empty)
                                           .Replace(EndRsaPrivateKey, string.Empty);
                var keyBytes = Convert.FromBase64String(base64);
                rsa.ImportRSAPrivateKey(keyBytes, out _);
            }
            else
            {
                var base64 = serializedKey.Replace(BeginPrivateKey, string.Empty)
                                           .Replace(EndPrivateKey, string.Empty);
                var keyBytes = Convert.FromBase64String(base64);
                rsa.ImportPkcs8PrivateKey(keyBytes, out _);
            }

            return rsa;
        }
    }
}
