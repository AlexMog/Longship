using System;
using System.IO;
using HarmonyLib;

namespace Longship
{
    public class Entrypoint
    {
        public static void Main()
        {
            Harmony.DEBUG = true;
            var harmony = new Harmony("gg.mog.valheim.longship");
            harmony.PatchAll();
            var basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Longship");
            var baseDir = new DirectoryInfo(basePath);
            if (!baseDir.Exists)
            {
                baseDir.Create();
            }
            new Longship(Path.Combine(basePath, "Configs"), Path.Combine(basePath, "Plugins")).Init();
        }
    }
}