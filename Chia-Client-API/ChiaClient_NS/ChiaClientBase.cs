namespace Chia_Client_API.ChiaClient_NS;

public abstract class ChiaClientBase : ChiaCommunicationBase
    {
        /// <summary>
        /// the node this endpoint should connect to
        /// </summary>
        protected Endpoint _EndpointNode { get; set; }
        /// <summary>
        /// the address under which the node can be reached. Defaults to localhost (127.0.0.1)
        /// </summary>
        public string TargetApiAddress { get; set; }
        /// <summary>
        /// the port which should be used. defaults to 8555
        /// </summary>
        public int TargetApiPort { get; set; }
        
        /// <summary>
        /// the base folder is the folder where all certificates are contained within subfolders according to chias default structure
        /// </summary>
        public string API_CertificateFolder
        {
            get { return _API_CertificateFolder; }
            set { _API_CertificateFolder = value; SetNewCertificates(); }
        }

        protected string _API_CertificateFolder;
        
        protected HttpClient _Client { get; set; }
        // Constructor
        protected ChiaClientBase(
            Endpoint endpoint,
            string targetApiAddress = "localhost", int targetApiPort = 8559, 
            string? targetCertificateBaseFolder = null, 
            TimeSpan? timeout = null)
        {
            TargetApiAddress = targetApiAddress;
            TargetApiPort = targetApiPort;
            _EndpointNode = endpoint;

            // Set certificate folder (initializes the Client)
            API_CertificateFolder = targetCertificateBaseFolder ?? Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                ".chia", "mainnet", "config", "ssl");
        }

        /// <summary>
        /// sets new certificates for the client
        /// </summary>
        protected abstract void SetNewCertificates();

        
    }