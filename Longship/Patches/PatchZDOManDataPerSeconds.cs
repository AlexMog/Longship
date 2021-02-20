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
            Longship.Log("ZDOMan constructor patched !");
            ___m_dataPerSec = (int) Longship.Instance.ConfigurationManager.Configuration.Network.DataPerSeconds;
        }
    }
}