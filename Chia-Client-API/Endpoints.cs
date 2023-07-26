using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chia_Client_API
{
    /// <summary>
    /// An enumeration that defines the different communication ports used by the Chia client.
    /// </summary>
    /// <remarks>
    /// Each value in this enumeration represents a specific service in the Chia network and the associated port number 
    /// that the service listens on. These services include the daemon, full node, farmer, harvester, wallet, and data layer 
    /// services. Each of these services plays a specific role in the functioning of the Chia blockchain and network.
    /// </remarks>
    public enum Endpoint
    {
        /// <summary>
        /// The daemon service, which controls and manages other Chia services. It runs in the background and 
        /// facilitates communication between the user interface and other services.
        /// </summary>
        daemon = 55400,

        /// <summary>
        /// The full node service, responsible for validating transactions and blocks in the Chia network. 
        /// It maintains a complete copy of the blockchain and shares this information with other nodes.
        /// </summary>
        full_node = 8555,

        /// <summary>
        /// The farmer service, which plays a crucial role in the creation of new blocks. It collects transactions, 
        /// creates block candidates and presents them to the network. It also listens for challenges and manages proofs 
        /// from the harvester service.
        /// </summary>
        farmer = 8559,

        /// <summary>
        /// The harvester service, which is responsible for finding proofs of space on your machine. Once a proof is 
        /// found, it is sent to the farmer.
        /// </summary>
        harvester = 8560,

        /// <summary>
        /// The wallet service, which manages the user's Chia coins. It keeps track of all transactions, generates new 
        /// addresses, and allows users to send and receive coins.
        /// </summary>
        wallet = 9256,

        /// <summary>
        /// The data layer service, which provides an abstraction of the blockchain's state. It supports querying of 
        /// various blockchain entities and their relationships.
        /// </summary>
        data_layer = 8562,
    }

}
