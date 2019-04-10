namespace Monitor.Server.Entities
{
    public class MonitorStateRAM : MonitorState
    {
        public MonitorStateRAM(float[] current)
            : base()
        {
            Type = MonitorType.RAM;
            Min = 0;
            Max = 100;
            Warning = 50;
            Critical = 70;
            Current = current;
        }
    }
}
