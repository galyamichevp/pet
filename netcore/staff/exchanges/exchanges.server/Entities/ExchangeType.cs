using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace exchanges.server.Entities
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ExchangeType
    {
        NONE = 0,
        USD,
        EUR,
        RUB
    }
}