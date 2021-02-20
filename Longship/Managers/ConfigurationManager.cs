using System.IO;
using Longship.Configuration;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Longship.Managers
{
    public class ConfigurationManager
    {
        private readonly string _configurationDirectory;
        public ServerConfiguration Configuration { get; private set; }

        public ConfigurationManager(string configurationDirectory)
        {
            _configurationDirectory = configurationDirectory;
        }
        
        public void Init()
        {
            var directory = new DirectoryInfo(_configurationDirectory);
            if (!directory.Exists)
            {
                directory.Create();
            }

            var configFilePath = Path.Combine(_configurationDirectory, "Server.yml");
            var configFileInfo = new FileInfo(configFilePath);
            if (!configFileInfo.Exists)
            {
                configFileInfo.Create().Close();
                var serializer = new SerializerBuilder()
                    .WithNamingConvention(CamelCaseNamingConvention.Instance)
                    .Build();
               File.WriteAllText(configFilePath, serializer.Serialize(new ServerConfiguration()));
            }
            
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
            Configuration = deserializer.Deserialize<ServerConfiguration>(
                File.ReadAllText(configFilePath));
        }
    }
}