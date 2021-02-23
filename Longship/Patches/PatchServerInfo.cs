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
      static void PatchZNetAwake(ref int ___m_serverPlayerLimit, ref int ___m_hostPort)
      {
          ___m_hostPort = Longship.Instance.ConfigurationManager.Configuration.ServerPort.Value;
          ___m_serverPlayerLimit = (int) Longship.Instance.ConfigurationManager.Configuration.MaxPlayers.Value;
      }

      [HarmonyPrefix]
      [HarmonyPatch(typeof(FejdStartup), "ParseServerArguments")]
      static bool PatchFejdStartupParseServerArguments(FejdStartup __instance, ref bool __result)
      {
          string[] commandLineArgs = Environment.GetCommandLineArgs();
          string name = "Dedicated";
          int port = 2456;
          for (int index = 0; index < commandLineArgs.Length; ++index)
          {
            string str1 = commandLineArgs[index];
            if (str1 == "-world")
            {
              string str2 = commandLineArgs[index + 1];
              if (str2 != "")
                name = str2;
              ++index;
            }
            else if (str1 == "-port")
            {
              string s = commandLineArgs[index + 1];
              if (s != "")
                port = int.Parse(s);
              ++index;
            }
            else if (str1 == "-savedir")
            {
              Utils.SetSaveDataPath(commandLineArgs[index + 1]);
              ++index;
            }
          }
          World createWorld = World.GetCreateWorld(name);
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
          SteamManager.SetServerPort(port);
          __result = true;
          return false;
      }
    }
}