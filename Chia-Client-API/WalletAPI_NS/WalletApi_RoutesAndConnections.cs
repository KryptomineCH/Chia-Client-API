﻿using Chia_Client_API.Helpers_NS;
using CHIA_RPC.Datalayer_NS;
using CHIA_RPC.General_NS;
using CHIA_RPC.HelperFunctions_NS;
using CHIA_RPC.Wallet_NS.RoutesAndConnections_NS;
using System.Text.Json;
using Chia_Client_API.ChiaClient_NS;

namespace Chia_Client_API.WalletAPI_NS
{
    public abstract partial class WalletRpcBase : ChiaEndpointRouteBase
    {
        /// <summary>
        /// Close an active connection
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#close_connection"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<Success_Response> CloseConnection_Async(CloseConnection_RPC rpc)
        {
            string responseJson = await SendCustomMessageAsync("close_connection", rpc.ToString());
            ActionResult<Success_Response> deserializationResult = Success_Response.LoadResponseFromString(responseJson,IncludeRawServerResponse);
            Success_Response response = new ();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
        }

        /// <summary>
        /// Close an active connection
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#close_connection"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public Success_Response CloseConnection_Sync(CloseConnection_RPC rpc)
        {
            Task<Success_Response> data = Task.Run(() => CloseConnection_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Get a list of active connections
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_connections"/></remarks>
        /// <returns></returns>
        public async Task<GetConnections_Response> GetConnections_Async()
        {
            string responseJson = await SendCustomMessageAsync("get_connections");
            ActionResult<GetConnections_Response> deserializationResult = GetConnections_Response.LoadResponseFromString(responseJson,IncludeRawServerResponse);
            GetConnections_Response response = new ();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
        }

        /// <summary>
        /// Get a list of active connections
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_connections"/></remarks>
        /// <returns></returns>
        public GetConnections_Response GetConnections_Sync()
        {
            Task<GetConnections_Response> data = Task.Run(() => GetConnections_Async());
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// List all available RPC routes
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_routes"/></remarks>
        /// <returns></returns>
        public async Task<GetRoutes_Response> GetRoutes_Async()
        {
            string responseJson = await SendCustomMessageAsync("get_routes");
            ActionResult<GetRoutes_Response> deserializationResult = GetRoutes_Response.LoadResponseFromString(responseJson,IncludeRawServerResponse);
            GetRoutes_Response response = new ();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
        }

        /// <summary>
        /// List all available RPC routes
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#get_routes"/></remarks>
        /// <returns></returns>
        public GetRoutes_Response GetRoutes_Sync()
        {
            Task<GetRoutes_Response> data = Task.Run(() => GetRoutes_Async());
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Open a connection to another node
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#open_connection"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public async Task<Success_Response> OpenConnection_Async(OpenConnection_RPC rpc)
        {
            string responseJson = await SendCustomMessageAsync("open_connection", rpc.ToString());
            ActionResult<Success_Response> deserializationResult = Success_Response.LoadResponseFromString(responseJson,IncludeRawServerResponse);
            Success_Response response = new ();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
        }

        /// <summary>
        /// Open a connection to another node
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#open_connection"/></remarks>
        /// <param name="rpc"></param>
        /// <returns></returns>
        public Success_Response OpenConnection_Sync(OpenConnection_RPC rpc)
        {
            Task<Success_Response> data = Task.Run(() => OpenConnection_Async(rpc));
            data.Wait();
            return data.Result;
        }

        /// <summary>
        /// Stop your local node
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#stop_node"/></remarks>
        /// <returns></returns>
        public async Task<Success_Response> StopNode_Async()
        {
            string responseJson = await SendCustomMessageAsync("stop_node");
            ActionResult<Success_Response> deserializationResult = Success_Response.LoadResponseFromString(responseJson,IncludeRawServerResponse);
            Success_Response response = new ();

            if (deserializationResult.Data != null)
            {
                response = deserializationResult.Data;
            }
            else
            {
                response.success = deserializationResult.Success;
                response.error = deserializationResult.Error;
                response.RawContent = deserializationResult.RawJson;
            }
            return response;
        }

        /// <summary>
        /// Stop your local node
        /// </summary>
        /// <remarks><see href="https://docs.chia.net/wallet-rpc#stop_node"/></remarks>
        /// <returns></returns>
        public Success_Response StopNode_Sync()
        {
            Task<Success_Response> data = Task.Run(() => StopNode_Async());
            data.Wait();
            return data.Result;
        }
    }
}
