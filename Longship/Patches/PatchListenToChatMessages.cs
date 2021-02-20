using System;
using HarmonyLib;
using UnityEngine;

namespace Longship.Patches
{
    [HarmonyPatch]
    public class PatchListenToChatMessages
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(Chat), "RPC_ChatMessage")]
        static void PatchOnNewChatMessage(long sender, Vector3 position, int type, string name, string text)
        {
            Longship.Log($"RPC_ChatMessage(SenderId: {sender}, Type: {type}, Name: {name}, Text: {text}");
        }
    }
}