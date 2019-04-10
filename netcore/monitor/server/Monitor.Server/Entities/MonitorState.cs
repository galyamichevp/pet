using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Monitor.Server.Entities
{
    public class MonitorState
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public MonitorType Type { get; protected set; }

        public float Min { get; protected set; }
        public float Max { get; protected set; }
        public float Warning { get; protected set; }
        public float Critical { get; protected set; }

        public float[] Current { get; protected set; }
    }
}
