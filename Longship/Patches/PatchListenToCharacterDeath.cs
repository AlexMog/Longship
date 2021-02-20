using HarmonyLib;

namespace Longship.Events
{
    [HarmonyPatch(typeof(Character), "OnDeath")]
    public class PatchListenToCharacterDeath
    {
        static void Prefix(Character __instance)
        {
            var evt = new CharacterDeathEvent(__instance);
            Longship.Instance.EventManager.DispatchEvent(evt);
        }
    }
}