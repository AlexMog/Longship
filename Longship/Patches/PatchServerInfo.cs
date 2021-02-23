using System;
using System.Reflection;
using HarmonyLib;

namespace Longship.Patches
{
    [HarmonyPatch]
    public class PatchServerInfo
    {
      private static readonly MethodInfo _getPublicPasswordError = typeof(FejdStartup).GetMethod("GetPublicPasswordError",
        BindingFlags.NonPublic | BindingFlags.Instance);
      private static readonly MethodInfo _isPublicPasswordValid = typeof(FejdStartup).GetMethod("IsPublicPasswordValid",
        BindingFlags.NonPublic | BindingFlags.Instance);
      
      [HarmonyPrefix]
      [HarmonyPatch(typeof(ZNet), "Awake")]
      static void PatchZNetAwake(ref int ___m_serverPlayerLimit)
      {
          ___m_serverPlayerLimit = (int) Longship.Instance.ConfigurationManager.Configuration.MaxPlayers.Value;
      }

      [HarmonyPrefix]
      [HarmonyPatch(typeof(FejdStartup), "ParseServerArguments")]
      static bool PatchFejdStartupParseServerArguments(FejdStartup __instance, ref bool __result)
      {
          var commandLineArgs = Environment.GetCommandLineArgs();
          var name = Longship.Instance.ConfigurationManager.Configuration.WorldName.Value;
          for (var index = 0; index < commandLineArgs.Length; ++index)
          {
            var str1 = commandLineArgs[index];
            if (str1 != "-savedir") continue;
            Utils.SetSaveDataPath(commandLineArgs[index + 1]);
            ++index;
          }
          var createWorld = World.GetCreateWorld(name);
          var password = Longship.Instance.ConfigurationManager.Configuration.ServerPassword.Value;
          if (!string.IsNullOrEmpty(password) && !(bool) _isPublicPasswordValid.Invoke(__instance,
            new object[]
            {
              password, createWorld
            }))
          {
            ZLog.LogError((object) ("Error bad password:" + _getPublicPasswordError.Invoke(__instance, new object[]
            {
              password, createWorld
            })));
            UnityEngine.Application.Quit();
            __result = false;
            return false;
          }

          ZNet.SetServer(true, true, true, 
            Longship.Instance.ConfigurationManager.Configuration.ServerName.Value,
            password,
            createWorld);
          ZNet.SetServerHost("", 0);
          SteamManager.SetServerPort(Longship.Instance.ConfigurationManager.Configuration.ServerPort.Value);
          __result = true;
          return false;
      }
    }
}