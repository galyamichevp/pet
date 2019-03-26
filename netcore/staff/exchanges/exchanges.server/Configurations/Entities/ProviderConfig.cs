namespace exchanges.server.Configurations.Entities
{
    public class ProviderConfig
    {
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public int ExpiredInMsec { get; set; }
        public string Host { get; set; }
    }
}