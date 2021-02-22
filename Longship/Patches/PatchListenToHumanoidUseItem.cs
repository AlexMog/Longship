using System.Reflection;
using HarmonyLib;
using Longship.Events;
using UnityEngine;

namespace Longship.Patches
{
    [HarmonyPatch(typeof(Humanoid), "UseItem")]
    public class PatchListenToHumanoidUseItem
    {
        private static readonly MethodInfo _toggleEquiped = typeof(Humanoid).GetMethod("ToggleEquiped",
            BindingFlags.NonPublic | BindingFlags.Instance);
        
        // This is an exact copy of the ingame method "UseItem" to provide a reliable way of controlling it later
        static bool Prefix(Humanoid __instance, Inventory inventory, ItemDrop.ItemData item, bool fromInventoryGui,
            Inventory ___m_inventory, EffectList ___m_consumeItemEffects, ZSyncAnimation ___m_zanim)
        {
            if (inventory == null)
            {
                inventory = ___m_inventory;
            }
            if (!inventory.ContainsItem(item))
            {
                return false;
            }
            var hoverObject = __instance.GetHoverObject();
            var hoverable = (hoverObject ? hoverObject.GetComponentInParent<Hoverable>() : null);
            if (hoverable != null && !fromInventoryGui)
            {
                var componentInParent = hoverObject.GetComponentInParent<Interactable>();
                if (componentInParent != null && componentInParent.UseItem(__instance, item))
                {
                    return false;
                }
            }
            if (item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Consumable)
            {
                if (__instance.ConsumeItem(inventory, item))
                {
                    ___m_consumeItemEffects.Create(Player.m_localPlayer.transform.position, Quaternion.identity);
                    ___m_zanim.SetTrigger("eat");
                }
            }
            else if ((inventory != ___m_inventory ||
                      !(bool) _toggleEquiped.Invoke(__instance, new []{ item })) && !fromInventoryGui)
            {
                if (hoverable != null)
                {
                    // FIXME Fix localization
                    __instance.Message(MessageHud.MessageType.Center, "$msg_cantuseon");
//                    __instance.Message(MessageHud.MessageType.Center, Localization.instance.Localize("$msg_cantuseon", item.m_shared.m_name, hoverable.GetHoverName()));
                }
                else
                {
                    // FIXME Fix localization
                    __instance.Message(MessageHud.MessageType.Center, "$msg_useonwhat");
//                    __instance.Message(MessageHud.MessageType.Center, Localization.instance.Localize("$msg_useonwhat", item.m_shared.m_name));
                }

                return false;
            }
            
            Longship.Instance.EventManager.DispatchEvent(new HumanoidUseItemEvent(__instance, inventory, item));
            return false;
        }
    }
}