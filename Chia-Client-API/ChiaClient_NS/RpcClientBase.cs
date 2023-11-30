namespace Chia_Client_API.ChiaClient_NS;

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

public class RpcClientBase : ChiaClientBase
{
    
    /// <summary>
    /// the client that is being used for communication
    /// </summary>
    private HttpClient? _Client { get; set; }
    // Constructor
    public RpcClientBase(
        Endpoint endpoint,
        string targetApiAddress = "localhost", int targetApiPort = 8559,
        string? targetCertificateBaseFolder = null, 
        TimeSpan? timeout = null)
        : base(endpoint, targetApiAddress, targetApiPort, targetCertificateBaseFolder, timeout)
    {
        _Client.Timeout = timeout ?? TimeSpan.FromMinutes(5);
    }

    // Override the SetNewCertificates method
    /// <summary>
    /// this function creates a new http client with the set certificates
    /// </summary>
    protected override void SetNewCertificates()
    {
        if (_Client != null) 
            _Client.Dispose();

        // Initialize http client with proper certificate
            
        if (_API_CertificateFolder == null)
        {
            throw new ArgumentNullException(nameof(_API_CertificateFolder));
        }
            
        var handler = new SocketsHttpHandler();
        handler.SslOptions.ClientCertificates = CertificateLoader.GetCertificate(_EndpointNode, _API_CertificateFolder);
        // TODO: Actually validate certificate
        handler.SslOptions.RemoteCertificateValidationCallback += (sender, cert, chain, errors) =>
        {
            Console.WriteLine($"SSL Policy Errors: {errors}");
            return true; // For testing purposes
        };

        _Client = new HttpClient(handler);
    }

    /// <summary>
    /// with this function you can execute any RPC against the wallet api. it is internally used by the library
    /// </summary>
    /// <param name="function"></param>
    /// <param name="json"></param>
    /// <returns></returns>
    public override async Task<string> SendCustomMessageAsync(string function, string json = " { } ")
    {
        if (_Client == null)
        {
            throw new NullReferenceException(nameof(_Client));
        }

        using (var request = new HttpRequestMessage(new HttpMethod("POST"),
                   "https://" + TargetApiAddress + ":" + TargetApiPort.ToString() + "/" + function))
        {
            request.Content = new StringContent(json);
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            var response = await _Client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
