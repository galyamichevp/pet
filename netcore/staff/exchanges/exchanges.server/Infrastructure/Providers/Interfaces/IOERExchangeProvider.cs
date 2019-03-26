using System.Collections.Generic;
using exchanges.server.Entities;

namespace exchanges.server.Infrastructure.Providers.Interfaces
{
    internal interface IOERExchangeProvider : IExchangeProvider
    {
        string Host { get; set; }
    }
}