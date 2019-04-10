using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Monitor.Server.Sockets.Interfaces
{
    public interface IMonitorsSocket
    {
        Task<bool> TryCloseSocket(string origin);
        Task<bool> TryOpenSocket(string origin, WebSocketManager webSocket);
    }
}
