using HarmonyLib;
using Longship.Events;

namespace Longship.Patches
{
    [HarmonyPatch(typeof(Character), "UpdateMotion")]
    public class PatchListenToCharacterMovements
    {
        static void Prefix(Character __instance, out CharacterMoveEvent __state)
        {
            __state = new CharacterMoveEvent(__instance, __instance.transform.position);
        }

        static void Postfix(Character __instance, CharacterMoveEvent __state)
        {
            __state.NewPos = __instance.transform.position;
            Longship.Instance.EventManager.DispatchEvent(__state);
        }
    }
}