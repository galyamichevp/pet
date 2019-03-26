using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using ZeroFormatter;

namespace exchanges.server.Entities
{
    [ZeroFormattable]
    [Union(typeof(CacheExchangeInfo),typeof(ProviderExchangeInfo))]
    public class ExchangeInfo
    {
        /// <summary>
        /// Продаваемая валюта
        /// </summary>
        [Index(0)]
        [BsonRepresentation(BsonType.String)]
        public virtual ExchangeType BaseType { get; set; }

        /// <summary>
        /// Дата и время публикации
        /// </summary>
        [Index(1)]
        public virtual DateTimeOffset Published { get; set; }

        /// <summary>
        /// Информация об обмене
        /// </summary>
        [Index(2)]
        public virtual List<ExchangeRateInfo> Rates { get; set; }
    }
}