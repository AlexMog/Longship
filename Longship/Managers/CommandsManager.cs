using System;
using System.Collections.Generic;
using Longship.Plugins;

namespace Longship.Managers
{
    public class CommandsManager : Manager
    {
        public delegate bool CommandListener(Character sender, string command, string argument);
        private readonly Dictionary<string, CommandListener> _commands = new Dictionary<string, CommandListener>();
        private readonly Dictionary<IPlugin, List<RegisteredListener>> _pluginListeners =
            new Dictionary<IPlugin, List<RegisteredListener>>();
        
        public override void Init() {}

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

        public void OnCommandExecuted(string command, string argument)
        {
            if (_commands.TryGetValue(command, out var listener))
            {
                listener.Invoke(null /* TODO */, command, argument);
            }
            else
            {
                // TODO Send an error to the player
            }
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
    }
}