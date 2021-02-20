using HarmonyLib;
using Longship.Events;

namespace Longship.Patches
{
    [HarmonyPatch(typeof(Character), "Damage")]
    public class PatchListenToCharacterDamaged
    {
        static void Prefix(Character __instance, HitData hit)
        {
            var evt = new CharacterDamagedEvent(__instance, hit);
            Longship.Instance.EventManager.DispatchEvent(evt);
        }
    }
}