using System;
using System.Text.RegularExpressions;
using HarmonyLib;
using UnityEngine;

namespace Longship.Patches
{
    [HarmonyPatch]
    public class PatchListenToChatMessages
    {
        private static Regex CommandRegex;

        static PatchListenToChatMessages()
        {
            CommandRegex = new Regex(@"\/(?<command>[A-Za-z]+) {0,1}(?<argument>.*)", RegexOptions.Compiled);
        }
        
        [HarmonyPrefix]
        [HarmonyPatch(typeof(Chat), "RPC_ChatMessage")]
        static void PatchOnNewChatMessage(long sender, Vector3 position, int type, string name, string text)
        {
            var match = CommandRegex.Match(text);
            if (match.Success)
            {
                var command = match.Groups["command"].Value;
                var argument = match.Groups["argument"].Success ? match.Groups["argument"].Value : null;
                Longship.Instance.CommandsManager.OnCommandExecuted(command, argument);
            }
        }
    }
}