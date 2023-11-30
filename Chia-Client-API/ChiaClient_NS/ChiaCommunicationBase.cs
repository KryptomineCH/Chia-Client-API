namespace Chia_Client_API.ChiaClient_NS;

public abstract class ChiaCommunicationBase
{
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
    public virtual string SendCustomMessageSync(string function, string json = " { } ")
    {
        // TODO: apparently, this can be improved
        Task<string> data = Task.Run(() => SendCustomMessageAsync(function, json));
        data.Wait();
        return data.Result;
    }
}