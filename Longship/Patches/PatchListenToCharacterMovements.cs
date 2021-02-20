using HarmonyLib;

namespace Longship.Patches
{
    [HarmonyPatch(typeof(Character), "UpdateMotion")]
    public class PatchListenToCharacterMovements
    {
        static void Prefix() {}
        
        static void Postfix() {}
    }
}