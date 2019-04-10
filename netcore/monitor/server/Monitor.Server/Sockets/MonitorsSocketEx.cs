using Microsoft.AspNetCore.Builder;
using Monitor.Server.Sockets.Interfaces;
using System;
using System.Net.WebSockets;

namespace Monitor.Server.Sockets
{
    public static class MonitorsSocketEx
    {
        public static WebSocketOptions BuildOptions()
        {
            var webSocketOptions = new WebSocketOptions()
            {
                KeepAliveInterval = TimeSpan.FromSeconds(120),
                ReceiveBufferSize = 4 * 1024
            };

            webSocketOptions.AllowedOrigins.Add("http://localhost:5000");
            webSocketOptions.AllowedOrigins.Add("http://localhost:3000");

            return webSocketOptions;
        }

        public static void UseSocketRoutes(this IApplicationBuilder app, IMonitorsSocket monitorSocket)
        {
            app.Use(async (context, next) =>
            {
                await next();

                //if (context.Request.Path == "/ws")
                //{
                //    if (context.WebSockets.IsWebSocketRequest)
                //    {
                //        WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();

                //        await monitorSocket.Echo(/*context, */webSocket);
                //    }
                //    else
                //    {
                //        context.Response.StatusCode = 400;
                //    }
                //}
                //else
                //{
                //    await next();
                //}
            });
        }
    }
}
