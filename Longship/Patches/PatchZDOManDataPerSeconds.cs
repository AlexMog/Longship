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
            ___m_dataPerSec = (int) Longship.Instance.ConfigurationManager.Configuration.NetworkDataPerSeconds.Value;
            Longship.Instance.Log.LogInfo($"ZDOMan m_dataPerSec patched to value {___m_dataPerSec}");
        }
    }
}