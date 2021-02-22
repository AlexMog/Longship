using System.Reflection;
using HarmonyLib;
using Longship.Events;
using UnityEngine;

namespace Longship.Patches
{
    [HarmonyPatch(typeof(Humanoid), "DropItem")]
    public class PatchListenToHumanoidDropItem
    {
        private static readonly MethodInfo _setupVisEquipment = typeof(Humanoid).GetMethod("SetupVisEquipment",
            BindingFlags.NonPublic | BindingFlags.Instance);
        
        // This is an exact copy of the ingame method "DropItem" to provide a reliable way of controlling it later
        static bool Prefix(Humanoid __instance, ref bool __result, Inventory inventory, ItemDrop.ItemData item,
            int amount, ItemDrop.ItemData ___m_hiddenLeftItem, ItemDrop.ItemData ___m_hiddenRightItem,
            ZSyncAnimation ___m_zanim, EffectList ___m_dropEffects, VisEquipment ___m_visEquipment)
        {
            if (item.m_shared.m_questItem)
            {
                __instance.Message(MessageHud.MessageType.Center, "$msg_cantdrop");
                __result = false;
                return false;
            }
            __instance.RemoveFromEquipQueue(item);
            __instance.UnequipItem(item, triggerEquipEffects: false);
            if (___m_hiddenLeftItem == item)
            {
                ___m_hiddenLeftItem = null;
                _setupVisEquipment.Invoke(__instance, new []{ ___m_visEquipment, (object) false });
            }
            if (___m_hiddenRightItem == item)
            {
                ___m_hiddenRightItem = null;
                _setupVisEquipment.Invoke(__instance, new []{ ___m_visEquipment, (object) false });
            }
            if (amount == item.m_stack)
            {
//                ZLog.Log("drop all " + amount + "  " + item.m_stack);
                if (!inventory.RemoveItem(item))
                {
//                    ZLog.Log("Was not removed");
                    __result = false;
                    return false;
                }
            }
            else
            {
//                ZLog.Log("drop some " + amount + "  " + item.m_stack);
                inventory.RemoveItem(item, amount);
            }
            ItemDrop itemDrop = ItemDrop.DropItem(item, amount, __instance.transform.position +
                                                                __instance.transform.forward + __instance.transform.up,
                __instance.transform.rotation);
            Longship.Instance.EventManager.DispatchEvent(new HumanoidDropItemEvent(__instance, item, amount));
            if (__instance.IsPlayer())
            {
                itemDrop.OnPlayerDrop();
            }
            itemDrop.GetComponent<Rigidbody>().velocity = (__instance.transform.forward + Vector3.up) * 5f;
            ___m_zanim.SetTrigger("interact");
            ___m_dropEffects.Create(__instance.transform.position, Quaternion.identity);
            __instance.Message(MessageHud.MessageType.TopLeft, "$msg_dropped " +
                                                               itemDrop.m_itemData.m_shared.m_name,
                itemDrop.m_itemData.m_stack, itemDrop.m_itemData.GetIcon());
            __result = true;
            return false;
        }
    }
}