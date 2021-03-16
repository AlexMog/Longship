using UnityEngine;

namespace Longship.Events
{
    public class DiscoverLocationResponseEvent : RpcEvent
    {
        public string PinName { get; set; }
        public Minimap.PinType PinType { get; set; }
        public Vector3 Position { get; set; }
        
        public override void Construct(object[] parameters)
        {
            PinName = (string) parameters[1];
            PinType = (Minimap.PinType) parameters[2];
            Position = (Vector3) parameters[3];
        }
    }
}