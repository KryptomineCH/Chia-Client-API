# Chia-Client-API
Thank you for considering using the Chia Client API for C#! Here is a brief overview of what you can expect to find in this repository:

## Description
This repository contains a C# wrapper for the Chia blockchain API. It allows you to easily interact with the Chia blockchain from your C# applications.

This library is also available on NuGet. You can find it by searching for "ChiaClientAPI" in the NuGet Package Manager or by using the following command in the Package Manager Console:
```
Install-Package Chia-Client-API
```
## Features
A simple, easy-to-use interface for accessing the Chia API
Support for all API endpoints, including those for managing accounts, sending and receiving transactions, and querying the blockchain
Detailed documentation for each API endpoint, including descriptions of input and output parameters
- viewing, creating and managing wallets
- sending chia, cats, nfts
- creating offers for chia, cats and nfts
- exploring the blockchain
- managing datalayer
- managing simulator
- Minting NFTs
- Creating CATs

## Requirements
.NET 6 or higher
## Installation
To install this library from the repository, simply clone the repository and open the solution file in Visual Studio. You can then build the solution and reference the compiled library in your own C# projects.

Alternatively, you can install the library from NuGet as described above.

## Usage
*Note:* Per default, chia rpc will only listen to requests from the local machine and also load the certificates of the local machine automatically.  
If you have remote machines to manage, eg a full node or farmers, you need to enable public port listening.  
For that, edit the chia configuration (default at `~/.chia/mainnet/config/config.yaml`).  
Look for a line starting with self-hostname like so: `self_hostname: &self_hostname "localhost"`   
change it to: `self_hostname: 0.0.0.0` to listen on all interfaces or to the local interface ip to listen on a specific interface.  

You will need to create an instance of the ChiaClient class and pass in your API certificates:
```
// ... available endpoints ...
using Chia_Client_API.FullNodeAPI_NS;
using Chia_Client_API.WalletAPI_NS;
using Chia_Client_API.FarmerAPI_NS;
using Chia_Client_API.HarvesterAPI_NS;
using Chia_Client_API.DatalayerAPI_NS;

// ... initialize local host ...
Wallet_RPC_Client client = new Wallet_RPC_Client();

// ... initialize remote host ...
string certificatePath = Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
    @".testnet\ssl\");
Fullnode_Client = new FullNode_RPC_Client(targetApiAddress: "192.168.1.117", targetCertificateBaseFolder: certificatePath);
```

You can then call any of the API endpoints by calling the corresponding method on the client object. For example, to get the balance of an account, you can use the GetWalletBalance_Sync method:
```
using Chia_Client_API.WalletAPI_NS;
using CHIA_RPC.General_NS;
using CHIA_RPC.Wallet_NS.Wallet_NS;

namespace ChiaTransactionExaminator
{
    internal class Clients
    {
        private static Wallet_RPC_Client Wallet = new Wallet_RPC_Client();
        public decimal GetBalance()
        {
            WalletID_RPC walletID_RPC = new WalletID_RPC(1);
            GetWalletBalance_Response response = Wallet.GetWalletBalance_Sync(walletID_RPC);
            return response.wallet_balance.confirmed_wallet_balance_in_xch;
        }
    }
}
```
Note that a request usually consists of 3 Steps:
1. Compile the RPC document (with the included chia rpc library)
This defines the data which you want to pull prom the chia node
2. Making the request (with the chia client library)
this actually connects to the node and makes the request
3. Loading the data into a response

Refer to the documentation for specific usage instructions for each API endpoint.

### Sending a transaction
Sending a standard transaction is straight forward, you can use the following procedure:
```
// login
FingerPrint_RPC myFingerprint = new FingerPrint_RPC(1234567890);
FingerPrint_Response loginResponse = Clients.Wallet_Client.LogIn_Sync(rpc);

// wait for the wallet to fully sync
Clients.Wallet_Client.AwaitWalletSync_Sync(timeoutSeconds: 1000);

// send transaction
SendTransaction_RPC XchTransactionRPC = new SendTransaction_RPC(1, "TargetAdress", amount_mojos: 1000, fee_mojos: 0);
GetTransaction_Response sendTransaction_Result = Clients.Wallet_Client.SendTransaction_Sync(transaction);

// wait to complete transaction (recommended)
Clients.Wallet_Client.AwaitTransactionToConfirm_Sync(sendTransaction_Result, CancellationToken.None, timeoutInMinutes: 60);
```

### Creating a cat offer
this is how you offer Cat vs chia:
```
CatGetAssetId_Response assetId = Testnet_Wallet.Wallet_Client.CatGetAssetID_Sync(new WalletID_RPC(wallet.id));
CreateOfferForIds_RPC offer_rpc = new CreateOfferForIds_RPC();
offer_rpc.offer.Add("1", -50000); // you want to give 500000 mojos
offer_rpc.offer.Add(assetId.asset_id, 500); // you want to receive 0.5 of asset x
OfferFile offer = Testnet_Wallet.Wallet_Client.CreateOfferForIds_Sync(offer_rpc);
offer.Export("btftestoffer");
```

note you can also give amounts in decimal chia (note that 1 cat is normally 1000 mojos so mind the conversion there)
```
offer_rpc.AddOfferPosition("1", -0.005); // you want to give 500000 mojos
```

## Contributing
We welcome contributions to this repository! If you have suggestions for improvements or new features, please open an issue or submit a pull request.

## License
This library is licensed under the MIT License. Please see the LICENSE file for more information.
