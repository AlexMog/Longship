using HarmonyLib;
using Longship.Events;

namespace Longship.Patches
{
    [HarmonyPatch(typeof(Humanoid), "EquipItem")]
    public class PatchListenToHumanoidEquipItem
    {
        static void Postfix(Humanoid __instance, ItemDrop.ItemData item, bool __result)
        {
            if (__result)
            {
                Longship.Instance.EventManager.DispatchEvent(new HumanoidEquipItemEvent(__instance, item));
            }
        }
    }
}