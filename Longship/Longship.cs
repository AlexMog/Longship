using System;
using Longship.Events;
using Longship.Managers;
using Longship.Utils;
//using UnityEngine;

namespace Longship
{
    public class Longship
    {
        public const string BuildTag = "0.0.1";
        public static Longship Instance { get; private set; }
        public PluginManager PluginManager { get; }
        public ConfigurationManager ConfigurationManager { get; }
        private readonly EventManager _eventManager;
        
        public Longship(string configPath, string pluginsPath)
        {
            Instance = this;
            PluginManager = new PluginManager(pluginsPath);
            ConfigurationManager = new ConfigurationManager(configPath);
            _eventManager = new EventManager();
        }

        public void Init()
        {
            Log($"Starting v{BuildTag}...");
            Log("Checking for updates...");
            if (UpdatesChecker.CheckForUpdate(out var url))
            {
                Log("=== A NEW UPDATE IS AVAILABLE ===");
                Log($"You can download it here: {url}");
            }
            Log($"Loading server configuration...");
            ConfigurationManager.Init();
            Log($"Loading plugins...");
            PluginManager.Init();
            Log($"Ready.");
        }

        public void RegisterEventListener<T>(EventManager.EventListener<T> listener) where T : Event
        {
            _eventManager.RegisterListener(listener);
        }
        
        public static void Log(string message)
        {
            System.Console.WriteLine($"[Longship][INFO] {message}");
//            Debug.Log($"[Longship][INFO] {message}");
        }

        public static void LogError(string message)
        {
            System.Console.WriteLine($"[Longship][ERR] {message}");
//            Debug.LogError($"[Longship][ERR] {message}");
        }

        public static void LogException(Exception ex)
        {
            System.Console.WriteLine(ex.ToString());
//            Debug.LogException(ex);
        }

        public static void LogFormat(string format, params object[] args)
        {
//            Debug.LogFormat($"[Longship] {format}", args);
        }

        public static void LogWarning(string message)
        {
            System.Console.WriteLine($"[Longship][WARN] {message}");
//            Debug.Log($"[Longship][WARN] {message}");
        }
    }
}