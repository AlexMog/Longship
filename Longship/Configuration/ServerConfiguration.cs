using BepInEx.Configuration;

namespace Longship.Configuration
{
    public class ServerConfiguration
    {
        public ConfigEntry<string> ServerName { get; set; }
        public ConfigEntry<string> WorldName { get; set; }
        public ConfigEntry<string> ServerPassword { get; set; }
        public ConfigEntry<uint> MaxPlayers { get; set; }
        public ConfigEntry<int> ServerPort { get; set; }
        public ConfigEntry<uint> NetworkDataPerSeconds { get; set; }
        public ConfigEntry<bool> ServerPublic { get; set; }
    }
}