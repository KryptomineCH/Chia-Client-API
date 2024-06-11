using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Collections.Concurrent;
using CHIA_RPC.General_NS;

namespace Chia_Client_API
{
    internal class CompositeKey : IEquatable<CompositeKey>
    {
        public string Host { get; }
        public int Port { get; }
        public Endpoint Endpoint { get; }

        public CompositeKey(string host, int port, Endpoint endpoint)
        {
            Host = host;
            Port = port;
            Endpoint = endpoint;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as CompositeKey);
        }

        public bool Equals(CompositeKey other)
        {
            return other != null &&
                   Host == other.Host &&
                   Port == other.Port &&
                   EqualityComparer<Endpoint>.Default.Equals(Endpoint, other.Endpoint);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Host, Port, Endpoint);
        }
    }
    internal static class CertificateLoader
    {
        private static ConcurrentDictionary<CompositeKey, X509Certificate2Collection> Certificates = new ConcurrentDictionary<CompositeKey, X509Certificate2Collection>();

        /// <summary>
        /// Retrieves a certificate collection for a given endpoint, loading it from files if necessary.
        /// </summary>
        /// <param name="host">The target host.</param>
        /// <param name="port">The target port.</param>
        /// <param name="endpoint">The endpoint for which to retrieve the certificate.</param>
        /// <param name="basePath">The base file path for certificate files.</param>
        /// <returns>A collection of X509 certificates.</returns>
        /// <exception cref="FileNotFoundException">Thrown when the certificate file or key file is not found.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the certificate or key file is not correctly formatted.</exception>
        internal static X509Certificate2Collection GetCertificate(string host, int port, Endpoint endpoint, string basePath)
        {
            var key = new CompositeKey(host, port, endpoint);
            if (Certificates.TryGetValue(key, out var certificateCollection))
            {
                return certificateCollection;
            }

            var endpointCertificateDirectory = new DirectoryInfo(Path.Combine(basePath, endpoint.ToString()));
            var files = endpointCertificateDirectory.GetFiles();
            FileInfo? certFile = files.FirstOrDefault(f => f.Name.EndsWith(".crt") && f.Name.Contains("private"));
            FileInfo? keyFile = files.FirstOrDefault(f => f.Name.EndsWith(".key") && f.Name.Contains("private"));

            if (certFile == null || keyFile == null)
            {
                throw new FileNotFoundException($"Certificate or key file for endpoint {endpoint} not found!");
            }

            var newCertCollection = LoadCertificateFromFiles(certFile.FullName, keyFile.FullName);
            Certificates[key] = newCertCollection; // This ensures the dictionary doesn't grow indefinitely
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

        /// <summary>
        /// Clears the certificate cache for the specified composite key.
        /// </summary>
        /// <param name="host">The target host.</param>
        /// <param name="port">The target port.</param>
        /// <param name="endpoint">The endpoint for which to clear the cache.</param>
        public static void ClearCacheForEndpoint(string host, int port, Endpoint endpoint)
        {
            var key = new CompositeKey(host, port, endpoint);
            Certificates.TryRemove(key, out _);
        }
    }
}