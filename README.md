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
*Note:* Per default, chia rpc will only listen to requests from the local machine.  
If you have remote machines to manage, eg a full node or farmers, you need to enable public port listening.  
For that, edit the chia configuration (default at `~/.chia/mainnet/config/config.yaml`).  
Look for a line starting with self-hostname like so: `self_hostname: &self_hostname "localhost"`   
change it to: `self_hostname: 0.0.0.0` to listen on all interfaces or to the local interface ip to listen on a specific interface.  

To use the Chia Client API in your C# applications, you will need to create an instance of the ChiaClient class and pass in your API key:
```
using ChiaClientAPI;
```
```
var client = new ChiaClient("your_api_key");
```
You can then call any of the API endpoints by calling the corresponding method on the client object. For example, to get the balance of an account, you can use the GetBalance method:

```
var balance = client.GetBalance("your_account_id");
Console.WriteLine(balance);
```
Refer to the documentation for specific usage instructions for each API endpoint.

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
offer_rpc.offer_xch.Add("1", -0.005); // you want to receive 500000 mojos
```

## Contributing
We welcome contributions to this repository! If you have suggestions for improvements or new features, please open an issue or submit a pull request.

## License
This library is licensed under the MIT License. Please see the LICENSE file for more information.
