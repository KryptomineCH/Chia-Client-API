namespace Chia_Client_API.ChiaClient_NS;

public abstract class ChiaClientBase
    {
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
        private string _API_CertificateFolder;
        /// <summary>
        /// specifies if errors in the response should be reported to kryptomine.ch for api improvements.
        /// </summary>
        /// <remarks>Reported data is anonymous</remarks>
        public bool ReportResponseErrors { get; set; }
        protected HttpClient _Client { get; set; }
        // Constructor
        protected ChiaClientBase(bool reportResponseErrors, string targetApiAddress = "localhost", int targetApiPort = 8559, string? targetCertificateBaseFolder = null, TimeSpan? timeout = null)
        {
            TargetApiAddress = targetApiAddress;
            TargetApiPort = targetApiPort;
            ReportResponseErrors = reportResponseErrors;

            // Set certificate folder
            _API_CertificateFolder = targetCertificateBaseFolder ?? Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                ".chia", "mainnet", "config", "ssl");

            // Initialize the Client
            InitializeClient();
        }

        /// <summary>
        /// initializes the client
        /// </summary>
        public abstract void InitializeClient();

        /// <summary>
        /// sets new certificates for the client
        /// </summary>
        public abstract void SetNewCertificates();

        /// <summary>
        /// with this function you can execute any RPC against the corresponding api. it is internally used by the library
        /// </summary>
        /// <param name="function"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public abstract Task<string> SendCustomMessageAsync(string function, string json = " { } ");

        /// <summary>
        /// with this function you can execute any RPC against the wallet api. it is internally used by the library
        /// </summary>
        /// <param name="function"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public string SendCustomMessageSync(string function, string json = " { } ")
        {
            // TODO: apparently, this can be improved
            Task<string> data = Task.Run(() => SendCustomMessageAsync(function, json));
            data.Wait();
            return data.Result;
        }
    }