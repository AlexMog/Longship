using System.Collections.Generic;

namespace Longship.Events
{
    public class GlobalKeysEvent : RpcEvent
    {
        public List<string> Keys { get; set; }
        
        public override void Construct(object[] parameters)
        {
            Keys = (List<string>) parameters[1];
        }
    }
}