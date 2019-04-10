using System;

namespace Monitor.Server.Entities
{
    public class MonitorStateCPU : MonitorState
    {
        public MonitorStateCPU(float[] current)
            : base()
        {
            Type = MonitorType.CPU;
            Min = 0;
            Max = 100;
            Warning = 80;
            Critical = 90;
            Current = current;
        }
    }
}
