namespace Longship.Configuration
{
    public class ServerConfiguration
    {
        public string ServerName { get; set; } = "Default Server";
        public uint MaxPlayers { get; set; } = 10;
        public NetworkConfiguration Network { get; set; } = new NetworkConfiguration();
        
        public class NetworkConfiguration
        {
            public uint DataPerSeconds { get; set; } = 245760;
        } 
    }
}