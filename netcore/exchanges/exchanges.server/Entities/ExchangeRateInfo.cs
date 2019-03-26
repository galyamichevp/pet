using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using ZeroFormatter;

namespace exchanges.server.Entities
{
    [ZeroFormattable]
    public class ExchangeRateInfo
    {
        /// <summary>
        /// Покупаемая валюта
        /// </summary>
        [Index(0)]
        [BsonRepresentation(BsonType.String)]
        public virtual ExchangeType Type { get; set; }

        /// <summary>
        /// Курс обмена
        /// </summary>
        [Index(1)]
        public virtual decimal Rate { get; set; }
    }
}