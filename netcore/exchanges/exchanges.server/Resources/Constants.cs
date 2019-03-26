namespace exchanges.server.Resources
{
    internal static class Constants
    {
        internal static class Providers
        {
            public const string ProvidersSection = "Providers";
            public const string OERProvider = "oer";
            public const string LocalProvider = "local";
        }

        internal static class Redis
        {
            public const string RedisSection = "Redis";
            public const string Host = "host";
            public const string Instance = "instance";
        }

        internal static class Mongo
        {
            public const string MongoSection = "Mongo";
            public const string Host = "host";
            public const string Instance = "instance";
        }
    }
}
