using HarmonyLib;
using Longship.Events;
using UnityEngine;

namespace Longship.Patches
{
    [HarmonyPatch(typeof(Character), "UpdateMotion")]
    public class PatchListenToCharacterMovements
    {
        // Using a position cache to avoid over-using the Garbage collector by instancing too much vectors
        private static Vector3 _positionCache = new Vector3();

        static void Prefix(Character __instance)
        {
            var tmp = __instance.transform.position;
            _positionCache.Set(tmp.x, tmp.y, tmp.z);
        }

        static void Postfix(Character __instance)
        {
            if (_positionCache.Equals(__instance.transform.position))
            {
                return;
            }

            // Only fire the event if the position is different
            Longship.Instance.EventManager.DispatchEvent(
                new CharacterMoveEvent(__instance, _positionCache, __instance.transform.position));
        }
    }
}