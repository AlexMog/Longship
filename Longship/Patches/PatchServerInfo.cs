using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;

namespace Longship.Patches
{
    [HarmonyPatch]
    public class PatchServerInfo
    {
        public static void ServerArgumentsModificator(ref string worldName, ref string password, ref string serverName, ref int port, ref bool publicServer)
        {
            worldName = Longship.Instance.ConfigurationManager.Configuration.WorldName.Value;
            password = Longship.Instance.ConfigurationManager.Configuration.ServerPassword.Value;
            serverName = Longship.Instance.ConfigurationManager.Configuration.ServerName.Value;
            port = Longship.Instance.ConfigurationManager.Configuration.ServerPort.Value;
            publicServer = Longship.Instance.ConfigurationManager.Configuration.ServerPublic.Value;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(ZNet), "Awake")]
        static void PatchZNetAwake(ref int ___m_serverPlayerLimit)
        {
            ___m_serverPlayerLimit = (int)Longship.Instance.ConfigurationManager.Configuration.MaxPlayers.Value;
        }

        [HarmonyTranspiler]
        [HarmonyPatch(typeof(FejdStartup), "ParseServerArguments")]
        static IEnumerable<CodeInstruction> TranspilerFejdStartupParseServerArguments(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            /*
             *  World createWorld = World.GetCreateWorld(name);
             *  
             *  ldloc.1      // name
             *  call         class World World::GetCreateWorld(string)
             *  stloc.s      world(V_6)
            */
            return new CodeMatcher(instructions, generator)
                .MatchForward(false,
                    new CodeMatch(i => CodeInstructionExtensions.IsLdloc(i)),
                    new CodeMatch(OpCodes.Call, AccessTools.Method(typeof(World), "GetCreateWorld")),
                    new CodeMatch(OpCodes.Stloc_S)
                )
                .InsertAndAdvance(
                            new CodeInstruction(OpCodes.Ldloca_S, 1),
                            new CodeInstruction(OpCodes.Ldloca_S, 2),
                            new CodeInstruction(OpCodes.Ldloca_S, 3),
                            new CodeInstruction(OpCodes.Ldloca_S, 4),
                            new CodeInstruction(OpCodes.Ldloca_S, 5),
                            new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(PatchServerInfo), "ServerArgumentsModificator"))
                        )
                .InstructionEnumeration();
        }
    }
}