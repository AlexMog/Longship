using HarmonyLib;

namespace Longship.Patches
{
    [HarmonyPatch]
    public class PatchServerInfo
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(ZNet), "Awake")]
        static void PatchZNetAwake(ref int ___m_serverPlayerLimit, ref string ___m_ServerName,
            ref string ___m_serverPassword, ref int ___m_hostPort)
        {
            ___m_hostPort = Longship.Instance.ConfigurationManager.Configuration.ServerPort;
            ___m_serverPassword = Longship.Instance.ConfigurationManager.Configuration.ServerPassword;
            ___m_ServerName = Longship.Instance.ConfigurationManager.Configuration.ServerName;
            ___m_serverPlayerLimit = (int) Longship.Instance.ConfigurationManager.Configuration.MaxPlayers;
        }
    }
}