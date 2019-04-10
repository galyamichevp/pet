using Monitor.Server.Entities;
using System.Threading.Tasks;

namespace Monitor.Server.Monitorings.Interfaces
{
    public interface IMonitoring
    {
        Task<float[]> Collect(MonitorType monitorType);
    }
}
