namespace Chia_Client_API.ChiaClient_NS;

using CHIA_RPC.General_NS;
using CHIA_RPC.HelperFunctions_NS;
using Multithreading_Library;
using System;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using Multithreading_Library.ThreadControl;

/// <summary>
/// NOTE: set daemon_allow_tls_1_2: True  in chia config!
/// </summary>
public class WebsocketClient : ChiaClientBase
{
    // TODO: Certificate error. Check with chia dotnet if config correct
    /// <summary>
    /// the client that is being used for communication
    /// </summary>
    private ClientWebSocket? _Client { get; set; }
    public DateTime ConnectionAlive;
    private bool Reconnecting = false;
    /// <summary>
    /// NOTE: set daemon_allow_tls_1_2: True  in chia config!
    /// </summary>
    /// <param name="endpoint"></param>
    /// <param name="targetApiAddress"></param>
    /// <param name="targetApiPort"></param>
    /// <param name="targetCertificateBaseFolder"></param>
    /// <param name="timeout"></param>
    public WebsocketClient(
        Endpoint endpoint,
        string targetApiAddress = "localhost", int targetApiPort = 58444,
        string? targetCertificateBaseFolder = null, 
        TimeSpan? timeout = null)
        : base(endpoint, targetApiAddress, targetApiPort, targetCertificateBaseFolder, timeout)
    {
        SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
        var mainContext = SynchronizationContext.Current;
        Task receiveLoop = Task.Run(async () =>
        {
            Thread.CurrentThread.Name = $"{_EndpointNode}-WebsocketReceiveLoop";
            try
            {
                await ReceiveLoop();
            }
            catch (Exception ex) when (ex.Message != "invalid method!")
            {
                { }
            }
        });
        receiveLoop.ContinueWith(t =>
        {
            // Force a debugger break on the UI thread when task ended unexpectedly
            if (t.IsFaulted)
            {
                mainContext.Post(_ =>
                {
                    // Handle the exception
                    // This code now runs on the main thread
                }, null);
            }
        });
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
            
        _Client = new ClientWebSocket();
        _Client.Options.ClientCertificates = CertificateLoader.GetCertificate(Endpoint.daemon, _API_CertificateFolder);
        AsyncHelper.RunSync(() => _Client.ConnectAsync(new Uri("wss://" + TargetApiAddress+":"+ TargetApiPort), CancellationToken.None));
    }
    private RequestIDGenerator _RequestID = new();

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
        int requestID = _RequestID.GetNextRequestId();
        WebSocket_RPC rpc = new WebSocket_RPC(function, _EndpointNode, json, requestID, $"chia-Client-Api_{_EndpointNode}");
        await Send(rpc, true);
        WebSocket_Response response = await ReceiveStatusUpdate(requestID);
        return "not implemented";
    }
    private ConcurrentDictionary<int, WebSocket_Response> StatusResults = new ConcurrentDictionary<int, WebSocket_Response>();
    private byte[] BufferArray = new byte[8192];
    private List<DateTime> FailureCounter = new List<DateTime>();
    private int MaxFailures = 20;
    private TimeSpan FailureTime = TimeSpan.FromHours(1);
    private TimeSpan FailureWaitTime = TimeSpan.FromMinutes(0.5);
    /// <summary>
    /// this function loops indefinitely and retrieves Messages from the server
    /// </summary>
    private async Task ReceiveLoop()
    {
        DateTime messageReceived;
        while (true)
        {
            if (FailureCounter.Count > 0 && FailureCounter[0] < DateTime.Now - FailureTime)
            {
                FailureCounter.RemoveAt(0);
            }
            try
            {
                if (_Client.State == WebSocketState.Aborted)
                {
                    await FailureReconnect();
                    continue;
                }
                Array.Clear(BufferArray, 0, BufferArray.Length);
                await _Client.ReceiveAsync(BufferArray, CancellationToken.None);
                messageReceived = DateTime.Now;
                ConnectionAlive = messageReceived; // server is still alive(yehi)
                string result = Encoding.UTF8.GetString(BufferArray);
                ActionResult< WebSocket_Response> convertedResponse = WebSocket_Response.LoadResponseFromString(result);
           
                WebSocket_Response resultObject = convertedResponse.Data;
                if (resultObject == null)
                {
                    throw new NullReferenceException($"resultObject was null! {result}");
                }
                
            }
            catch (Exception e)
            {
                if (e.Message == "The remote party closed the WebSocket connection without completing the close handshake.")
                {
                    await FailureReconnect();
                    continue;
                }
                // message: "unknown failure"
                throw;
            }
        }
    }
    /// <summary>
    /// sends a message to the server
    /// </summary>
    /// <remarks>
    /// id 1 & 2 are reserved for pingpong and time request
    /// </remarks>
    /// <param name="context"></param>
    /// <param name="connectionvalidate">checks if the websocket is open</param>
    private async Task Send(WebSocket_RPC rpc, bool connectionvalidate)
    {
        var encoded = Encoding.UTF8.GetBytes(rpc.ToString());
        var buffer = new ArraySegment<byte>(encoded, 0, encoded.Length);
        try
        {
            if (!connectionvalidate || (_Client.State == WebSocketState.Open && !Reconnecting))
            {
                await _Client.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }
        catch (System.AggregateException ex)
        {
            var _ = ex.Message;
            { }
        }
        catch (Exception ex)
        {
            var _ = ex.Message;
            { }
        }
    }
    /// <summary>
    /// waits until there is a status update with a specific id in queue and returns it
    /// </summary>
    /// <param name="requestID"></param>
    /// <returns></returns>
    private async Task<WebSocket_Response?> ReceiveStatusUpdate(int requestID, TimeSpan? timeOut = null)
    {
        DateTime endTime = DateTime.Now + (timeOut ?? TimeSpan.FromMinutes(5));
        WebSocket_Response result = null;
        bool success = false;
        while (!success)
        {
            if (endTime < DateTime.Now)
            {
                throw new HttpRequestException($"Timeout has been reached without receiving a message with id {requestID}!");
            }
            success = StatusResults.Remove(requestID, out result);
            if (success) break;
            await Task.Delay(10);
        }
        return result;
    }
    private async Task FailureReconnect()
    {
        FailureCounter.Add(DateTime.Now);
        if (FailureCounter.Count < MaxFailures)
        {
            Reconnecting = true;
            if (_Client.State != WebSocketState.Aborted && _Client.State != WebSocketState.Closed)
            {
                await _Client.CloseAsync(WebSocketCloseStatus.EndpointUnavailable, "endpoint unavailable", CancellationToken.None);
            }
            await Task.Delay(FailureWaitTime * ((FailureCounter.Count + 1) * 2 * FailureCounter.Count));
            _Client = new ClientWebSocket();
            _Client.Options.KeepAliveInterval = TimeSpan.FromDays(48);
            SetNewCertificates();
            Reconnecting = false;
        }
        else
        {
            throw new Exception($"Websocket failed {FailureCounter.Count} times within {FailureTime.ToString()}");
        }
    }
    /// <summary>
    /// unsubscribes from depht update and closes connection
    /// </summary>
    /// <returns></returns>
    public async Task CloseConnection()
    {
        try
        {
            await _Client.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
        }
        catch (System.AggregateException ex)
        {
            var _ = ex.Message;
            { }
        }
    }
}
