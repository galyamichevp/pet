using Microsoft.Extensions.Diagnostics.HealthChecks;
using Monitor.Server.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Monitor.Server.HealthChecks
{
    public class HealthCheckSocket : IHealthCheck
    {
        public string Name => "socket";

        public HealthCheckSocket()
        {
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            foreach (var socketConnection in MonitorsSocket.SocketConnections)
            {
                if (socketConnection.Value.State != System.Net.WebSockets.WebSocketState.Open)
                    return HealthCheckResult.Unhealthy($"Socket connection corrupted! Id: {socketConnection.Key}");
            }

            return HealthCheckResult.Healthy();
        }
    }
}
