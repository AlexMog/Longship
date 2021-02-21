using HarmonyLib;

namespace Longship.Patches
{
    [HarmonyPatch(typeof(ZNet), "Awake")]
    public class PatchServerInfo
    {
        static void Prefix(ref int ___m_serverPlayerLimit, ref string ___m_ServerName,
            ref string ___m_serverPassword, ref int ___m_hostPort)
        {
            ___m_hostPort = Longship.Instance.ConfigurationManager.Configuration.ServerPort.Value;
            ___m_serverPassword = Longship.Instance.ConfigurationManager.Configuration.ServerPassword.Value;
            ___m_ServerName = Longship.Instance.ConfigurationManager.Configuration.ServerName.Value;
            ___m_serverPlayerLimit = (int) Longship.Instance.ConfigurationManager.Configuration.MaxPlayers.Value;
        }
    }
}