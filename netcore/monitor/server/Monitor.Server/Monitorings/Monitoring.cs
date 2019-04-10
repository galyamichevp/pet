using Monitor.Server.Entities;
using Monitor.Server.Monitorings.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Threading.Tasks;

namespace Monitor.Server.Monitorings
{
    public class Monitoring : IMonitoring
    {
        public async Task<float[]> Collect(MonitorType monitorType)
        {
            switch (monitorType)
            {
                case MonitorType.CPU:
                    return CalculateCPU();
                case MonitorType.RAM:
                    return CalculateRAM();
                case MonitorType.HDD:
                    return CalculateHDD();
                case MonitorType.NET:
                    return new[] { 0f };
            }

            return new float[] { };
        }

        private float[] CalculateCPU()
        {
            var cores = new float[Environment.ProcessorCount];
            var cpu = new PerformanceCounter[Environment.ProcessorCount];

            for (int i = 0; i < cores.Length; i++)
            {
                cpu[i] = new PerformanceCounter("Processor", "% Processor Time", $"{i}");
                cpu[i].NextValue();
            }

            System.Threading.Thread.Sleep(1000); // wait a second to get a valid reading

            for (int i = 0; i < cores.Length; i++)
            {
                cores[i] = cpu[i].NextValue();
            }

            return cores;
        }

        private float[] CalculateRAM()
        {
            ObjectQuery wql = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(wql);
            ManagementObjectCollection results = searcher.Get();

            double free = 1;
            double total =1;

            foreach (ManagementObject result in results)
            {
                total = Convert.ToDouble(result["TotalVisibleMemorySize"]);
                free = Convert.ToDouble(result["FreePhysicalMemory"]);
            }

            return new float[] { (float)free / (1024), (float)total / (1024) };
        }

        private float[] CalculateHDD()
        {
            var src = new List<float[]>();

            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                    src.Add((new[] { (float)drive.AvailableFreeSpace / (1024 * 1024), (float)drive.TotalSize / (1024 * 1024) }));
            }

            return src.SelectMany(x=>x).ToArray();
        }
    }
}
