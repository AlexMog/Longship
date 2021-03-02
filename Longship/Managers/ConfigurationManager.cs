using BepInEx.Configuration;
using Longship.Configuration;

namespace Longship.Managers
{
    public class ConfigurationManager : Manager
    {
        public ServerConfiguration Configuration { get; private set; }
        private ConfigFile _pluginConfigFile;

        public ConfigurationManager(ConfigFile pluginConfigFile)
        {
            _pluginConfigFile = pluginConfigFile;
        }        
        
        public override void Init()
        {
            Configuration = new ServerConfiguration();
            Configuration.ServerName =
                _pluginConfigFile.Bind("Server", "ServerName", "Longship Server",
                    "Name of the server");
            Configuration.ServerPort =
                _pluginConfigFile.Bind("Server", "ServerPort",2456 ,
                    "Port of the server");
            Configuration.MaxPlayers =
                _pluginConfigFile.Bind("Server", "MaxPlayers", 10u,
                    "Max players that can connect to the server");
            Configuration.ServerPassword = _pluginConfigFile.Bind("Server", "ServerPassword", "",
                "Server password. Note: leave empty if you don't want any password");
            Configuration.NetworkDataPerSeconds =
                _pluginConfigFile.Bind(
                    "Network",
                    "NetworkDataPerSeconds",
                    245760u,
                    "Upload bandwith allowed for the server, it is an easy fix for common lag problems, " +
                        "if you are lagging, you can augment this value.\n" +
                        "WARNING: This value WILL allow the server to use more bandwith. So be careful.\n" +
                        "Info: The value is in bytes (in this configuration, that means that the server is limited " +
                        "to ~250 Ko/s)");
            Configuration.WorldName = _pluginConfigFile.Bind("Server", "WorldName", "Dedicated",
                "Name of the world to use");
            Configuration.ServerPublic = _pluginConfigFile.Bind("Server", "ServerPublic", true,
                "Server visable in server list");
        }
    }
}
