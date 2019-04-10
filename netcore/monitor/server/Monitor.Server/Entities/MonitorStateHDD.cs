namespace Monitor.Server.Entities
{
    public class MonitorStateHDD : MonitorState
    {
        public MonitorStateHDD(float[] current)
            : base()
        {
            Type = MonitorType.HDD;
            Min = 0;
            Max = 100;
            Warning = 90;
            Critical = 95;
            Current = current;
        }
    }
}
