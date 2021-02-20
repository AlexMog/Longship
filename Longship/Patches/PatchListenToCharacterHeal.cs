using HarmonyLib;
using Longship.Events;

namespace Longship.Patches
{
    [HarmonyPatch(typeof(Character), "Heal")]
    public class PatchListenToCharacterHeal
    {
        static void Prefix(Character __instance, ref float hp)
        {
            var evt = new CharacterHealEvent(__instance, hp);
            Longship.Instance.EventManager.DispatchEvent(evt);
            hp = evt.HealValue;
        }
    }
}