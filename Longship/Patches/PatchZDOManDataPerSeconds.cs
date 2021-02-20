using System;
using HarmonyLib;

namespace Longship.Patches
{
    [HarmonyPatch]
    public class PatchZDOManDataPerSeconds
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(ZDOMan), MethodType.Constructor, new Type[] {typeof(int)})]
        static void PatchZDOManConstructor(ref int ___m_dataPerSec)
        {
            ___m_dataPerSec = (int) Longship.Instance.ConfigurationManager.Configuration.Network.DataPerSeconds;
            Longship.Log($"ZDOMan m_dataPerSec patched to value {___m_dataPerSec}");
        }
    }
}