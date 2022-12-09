using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chia_Client_API
{
    /// <summary>
    /// those are the communicationports use by the client
    /// </summary>
    public enum Endpoint
    {
        daemon = 55400,
        full_node = 8555,
        farmer = 8559,
        harvester = 8560,
        wallet = 9256,
        data_layer = 8562,
    }
}
