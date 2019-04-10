using Microsoft.AspNetCore.Http;
using Monitor.Server.Entities;
using Monitor.Server.Monitorings.Interfaces;
using Monitor.Server.Sockets.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Monitor.Server.Sockets
{
    public class MonitorsSocket : IMonitorsSocket
    {
        public static ConcurrentDictionary<string, WebSocket> SocketConnections = new ConcurrentDictionary<string, WebSocket>();

        private static ConcurrentDictionary<string, CancellationTokenSource> _socketConnectionTokens = new ConcurrentDictionary<string, CancellationTokenSource>();

        private static readonly ConcurrentDictionary<MonitorType, float[]> _monitorStates = new ConcurrentDictionary<MonitorType, float[]>(new Dictionary<MonitorType, float[]>()
        {
            { MonitorType.CPU,new []{0f } },
            { MonitorType.RAM,new []{0f }},
            { MonitorType.HDD,new []{0f }},
            { MonitorType.NET,new []{0f }}
        });

        private readonly IMonitoring _monitoring;

        public MonitorsSocket(IMonitoring monitoring)
        {
            _monitoring = monitoring;
        }

        public async Task<bool> TryOpenSocket(string origin, WebSocketManager webSocket)
        {
            if (SocketConnections.ContainsKey(origin))
            {
                await TryCloseSocket(origin);
            }

            if (SocketConnections.TryAdd(origin, await webSocket.AcceptWebSocketAsync()))
            {
                var tokenSource = new CancellationTokenSource();

                if (_socketConnectionTokens.TryAdd(origin, tokenSource))
                    await Echo(SocketConnections[origin], tokenSource.Token);

                return true;
            }

            return false;
        }

        public async Task<bool> TryCloseSocket(string origin)
        {
            if (SocketConnections.TryRemove(origin ?? string.Empty, out WebSocket webSocket))
            {
                if (_socketConnectionTokens.TryRemove(origin, out CancellationTokenSource tokenSource))
                    tokenSource.Cancel();

                return true;
            }

            return false;
        }

        public async Task Echo(WebSocket webSocket, CancellationToken cancellationToken)
        {
            await CheckSocketReceiver(webSocket);
            await CollectStateInfo(cancellationToken);
            
            var cpu = new[] { 0f };
            var ram = new[] { 0f };
            var hdd = new[] { 0f };
            var net = new[] { 0f };

            while (!webSocket.CloseStatus.HasValue)
            {
                cpu = await SendState(webSocket, MonitorType.CPU, cpu, _monitorStates[MonitorType.CPU]);
                ram = await SendState(webSocket, MonitorType.RAM, ram, _monitorStates[MonitorType.RAM]);
                hdd = await SendState(webSocket, MonitorType.HDD, hdd, _monitorStates[MonitorType.HDD]);
                net = await SendState(webSocket, MonitorType.NET, net, _monitorStates[MonitorType.NET]);
            }

            await webSocket.CloseAsync(webSocket.CloseStatus.Value, webSocket.CloseStatusDescription, CancellationToken.None);
        }

        private async Task<float[]> SendState(WebSocket webSocket, MonitorType monitorType, float[] oldValue, float[] newValue)
        {
            if (oldValue.Sum() == newValue.Sum())
                return oldValue;

            MonitorState state = null;

            switch (monitorType)
            {
                case MonitorType.CPU:
                    state = new MonitorStateCPU(newValue);
                    break;
                case MonitorType.RAM:
                    state = new MonitorStateRAM(newValue);
                    break;
                case MonitorType.HDD:
                    state = new MonitorStateHDD(newValue);
                    break;
                case MonitorType.NET:
                    state = new MonitorStateNET(newValue);
                    break;
            }
            
            var buffer = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(state));

            await webSocket.SendAsync(new ReadOnlyMemory<byte>(buffer, 0, buffer.Length), WebSocketMessageType.Text, true, CancellationToken.None);

            return newValue;
        }

        private async Task CollectStateInfo(CancellationToken cancellationToken)
        {
            Task.Run(async () => 
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    Thread.Sleep(500);

                    _monitorStates[MonitorType.CPU] = await _monitoring.Collect(MonitorType.CPU);
                    _monitorStates[MonitorType.RAM] = await _monitoring.Collect(MonitorType.RAM);
                    _monitorStates[MonitorType.HDD] = await _monitoring.Collect(MonitorType.HDD);
                    _monitorStates[MonitorType.NET] = await _monitoring.Collect(MonitorType.NET);
                }
            }, cancellationToken);
        }

        private async Task CheckSocketReceiver(WebSocket webSocket)
        {
            Task.Run(async () =>
            {
                await webSocket.ReceiveAsync(new ArraySegment<byte>(new byte[1024 * 4]), CancellationToken.None);
            });
        }
    }
}
