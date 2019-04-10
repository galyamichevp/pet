namespace Monitor.Server.Entities
{
    public class MonitorStateNET : MonitorState
    {
        public MonitorStateNET(float[] current)
            : base()
        {
            Type = MonitorType.NET;
            Min = 0;
            Max = 100;
            Warning = 90;
            Critical = 95;
            Current = current;
        }
    }
}
