using System.Collections.Generic;

namespace exchanges.server.Entities.OER
{
    internal class OERResponse
    {
        public long Timestamp { get; set; }
        public ExchangeType Base { get; set; }
        public Dictionary<string, decimal> Rates { get; set; }
    }
}
