using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chia_Client_API
{
    internal class WebsocketResponse
    {
        /// <summary>
        /// the comand which was requested against the server
        /// </summary>
         public string command { get; set; }
        /// <summary>
        /// presumed: Did the server acknowledge the request???
        /// </summary>
        public bool ack { get; set; }
        public object data { get; set; }
        /// <summary>
        /// used to identify which answer belongs to which request
        /// </summary>
        public int request_id { get; set; }
        /// <summary>
        /// to which interface did the daemon rout the request?
        /// </summary>
        public string destination { get; set; }
        /// <summary>
        /// where was the request comin from
        /// </summary>
        public string origin { get; set; }
}
}

