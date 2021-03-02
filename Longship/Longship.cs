using System;
using System.IO;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Longship.Events;
using Longship.Managers;
using Longship.Plugins;
using Longship.Utilities;

namespace Longship
{
    [BepInPlugin("gg.mog.valheim.longship", "Longship", "0.1.8")]
    public class Longship : BaseUnityPlugin
    {
        public const string BuildTag = "0.1.8";
        public static Longship Instance { get; private set; }
        public PluginManager PluginManager { get; }
        public ConfigurationManager ConfigurationManager { get; }
        public CommandsManager CommandsManager { get; }
        public EventManager EventManager { get; }
        public ManualLogSource Log => Logger;
        
        public Longship()
        {
            var harmony = new Harmony(this.Info.Metadata.GUID);
            harmony.PatchAll();
            
            var basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Longship");
            var baseDir = new DirectoryInfo(basePath);
            if (!baseDir.Exists)
            {
                baseDir.Create();
            }
            
            Instance = this;
            PluginManager = new PluginManager(Path.Combine(basePath, "Plugins"));
            ConfigurationManager = new ConfigurationManager(Config);
            EventManager = new EventManager();
            CommandsManager = new CommandsManager();
        }

        public void Awake()
        {
            Logger.LogInfo($"Starting v{BuildTag}...");
            Logger.LogInfo("Checking for updates...");
            if (UpdatesChecker.CheckForUpdate(out var url))
            {
                Logger.LogInfo("=== A NEW UPDATE IS AVAILABLE ===");
                Logger.LogInfo($"You can download it here: {url}");
            }
            Logger.LogInfo($"Loading server configuration...");
            ConfigurationManager.Init();
            Logger.LogInfo($"Loading plugins...");
            PluginManager.Init();
            Logger.LogInfo($"Ready.");
        }
    }
}