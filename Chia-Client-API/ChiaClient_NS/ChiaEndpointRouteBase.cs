namespace Chia_Client_API.ChiaClient_NS;

public abstract class ChiaEndpointRouteBase : ChiaCommunicationBase
{
    /// <summary>
    /// specifies if errors in the response should be reported to kryptomine.ch for api improvements.
    /// </summary>
    /// <remarks>Reported data is anonymous</remarks>
    public bool ReportResponseErrors { get; set; }

    /// <summary>
    /// defines if the raw server response should be included or not
    /// </summary>
    public bool IncludeRawServerResponse { get; set; } = true;
}