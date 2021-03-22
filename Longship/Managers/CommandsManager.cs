using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Longship.Events;
using Longship.Plugins;

namespace Longship.Managers
{
    public class CommandsManager : Manager, IPlugin
    {
        private static readonly Regex CommandRegex;
        public delegate bool CommandListener(long playerId, string command, string argument);
        private readonly Dictionary<string, CommandListener> _commands = new Dictionary<string, CommandListener>();
        private readonly Dictionary<IPlugin, List<RegisteredListener>> _pluginListeners =
            new Dictionary<IPlugin, List<RegisteredListener>>();
        
        static CommandsManager()
        {
            CommandRegex = new Regex(@"\/(?<command>[A-Za-z]+) {0,1}(?<argument>.*)", RegexOptions.Compiled);
        }

        public override void Init()
        {
            Longship.Instance.EventManager.RegisterListener<ChatMessageEvent>(this, chatMessageEvent =>
            {
                ParseCommand(chatMessageEvent.PlayerId, chatMessageEvent.Text);
            });
        }

        public void RegisterCommand(IPlugin plugin, string command, CommandListener listener)
        {
            if (_commands.ContainsKey(command))
            {
                throw new Exception("Command already assigned");
            }

            if (!_pluginListeners.TryGetValue(plugin, out var listeners))
            {
                listeners = new List<RegisteredListener>();
                _pluginListeners[plugin] = listeners;
            }

            listeners.Add(new RegisteredListener()
            {
                Listener = listener,
                Command = command
            });
            _commands[command] = listener;
        }

        public void OnCommandExecuted(long sender, string command, string argument)
        {
            if (_commands.TryGetValue(command, out var listener))
            {
                listener.Invoke(sender, command, argument);
            }
            else
            {
                // TODO Send an error to the player
            }
        }

        public void ParseCommand(long sender, string message)
        {
            var match = CommandRegex.Match(message);
            if (!match.Success) return;
            var command = match.Groups["command"].Value;
            var argument = match.Groups["argument"].Success ? match.Groups["argument"].Value : null;
            OnCommandExecuted(sender, command, argument);
        }

        public void ClearListeners(IPlugin plugin)
        {
            if (!_pluginListeners.TryGetValue(plugin, out var listeners)) return;
            foreach (var listener in listeners)
            {
                _commands.Remove(listener.Command);
            }
        }

        private class RegisteredListener
        {
            public string Command;
            public CommandListener Listener;
        }

        public void OnEnable()
        {
        }

        public void OnDisable()
        {
        }
    }
}