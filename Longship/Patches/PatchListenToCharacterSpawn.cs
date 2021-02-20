using HarmonyLib;
using Longship.Events;

namespace Longship.Patches
{
    [HarmonyPatch(typeof(Character), "Awake")]
    public class PatchListenToCharacterSpawn
    {
        static void Postfix(Character __instance)
        {
            Longship.Instance.EventManager.DispatchEvent(new CharacterSpawnEvent(__instance));
        }
    }
}