using HarmonyLib;
using Steamworks;

namespace Longship.Patches
{
    [HarmonyPatch]
    public class PatchPalyerCountLimit
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(ZNet), "Awake")]
        static void PatchZNetMaxPlayers(ref int ___m_serverPlayerLimit)
        {
            ___m_serverPlayerLimit = (int) Longship.Instance.ConfigurationManager.Configuration.MaxPlayers;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(SteamGameServer), "SetMaxPlayerCount")]
        static void PatchSteamGameServerMaxPlayers(ref int cPlayersMax)
        {
            cPlayersMax = (int) Longship.Instance.ConfigurationManager.Configuration.MaxPlayers;
        }
    }
}