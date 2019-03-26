using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace exchanges.server.Configurations.Entities
{
    public class MongoConfig
    {
        public string Host { get; set; }
        public string DBName { get; set; }
        public string DBCollection { get; set; }
    }
}
