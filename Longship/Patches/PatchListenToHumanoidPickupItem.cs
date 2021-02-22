using HarmonyLib;
using Longship.Events;
using UnityEngine;

namespace Longship.Patches
{
    [HarmonyPatch(typeof(Humanoid), "Pickup")]
    public class PatchListenToHumanoidPickupItem
    {
        // This is an exact copy of the ingame method "Pickup" to provide a reliable way of controlling it later
        static bool Prefix(GameObject go, Humanoid __instance, Inventory ___m_inventory, ZNetView ___m_nview,
            ItemDrop.ItemData ___m_rightItem, ItemDrop.ItemData ___m_hiddenRightItem, EffectList ___m_pickupEffects,
            ref bool __result)
        {
            var component = go.GetComponent<ItemDrop>();
            if (component == null)
            {
                __result = false;
                return false;
            }
            if (!component.CanPickup())
            {
                __result = false;
                return false;
            }
            if (___m_inventory.ContainsItem(component.m_itemData))
            {
                __result = false;
                return false;
            }
            if (component.m_itemData.m_shared.m_questItem &&
                __instance.HaveUniqueKey(component.m_itemData.m_shared.m_name))
            {
                __instance.Message(MessageHud.MessageType.Center, "$msg_cantpickup");
                __result = false;
                return false;
            }
            
            var flag = ___m_inventory.AddItem(component.m_itemData);
            if (___m_nview.GetZDO() == null)
            {
                UnityEngine.Object.Destroy(go);
                __result = true;
                return true;
            }
            if (!flag)
            {
                __instance.Message(MessageHud.MessageType.Center, "$msg_noroom");
                __result = false;
                return false;
            }
            Longship.Instance.EventManager.DispatchEvent(new HumanoidPickupItemEvent(__instance,
                component.m_itemData));
            if (component.m_itemData.m_shared.m_questItem)
            {
                __instance.AddUniqueKey(component.m_itemData.m_shared.m_name);
            }
            ZNetScene.instance.Destroy(go);
            if (flag && __instance.IsPlayer() && ___m_rightItem == null && ___m_hiddenRightItem == null &&
                component.m_itemData.IsWeapon())
            {
                __instance.EquipItem(component.m_itemData);
            }
            ___m_pickupEffects.Create(__instance.transform.position, Quaternion.identity);
            if (__instance.IsPlayer())
            {
                __instance.ShowPickupMessage(component.m_itemData, component.m_itemData.m_stack);
            }
            __result = flag;
            return false;
        }
    }
}