using System;
using System.Collections.Generic;

namespace Longship.Managers
{
    public class CommandsManager : Manager
    {
        public delegate bool CommandListener(string command, string argument);
        private readonly Dictionary<string, CommandListener> _commands = new Dictionary<string, CommandListener>();
        
        public void Init() {}

        public void RegisterCommand(string command, CommandListener listener)
        {
            if (_commands.ContainsKey(command))
            {
                throw new Exception("Command already assigned");
            }

            _commands[command] = listener;
        }

        public void OnCommandExecuted(string command, string argument)
        {
            if (_commands.TryGetValue(command, out var listener))
            {
                listener.Invoke(command, argument);
            }
            else
            {
                // TODO Send an error to the player
            }
        }
    }
}