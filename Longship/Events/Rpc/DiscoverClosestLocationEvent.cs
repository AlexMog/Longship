using UnityEngine;

namespace Longship.Events
{
    public class DiscoverClosestLocationEvent : RpcEvent
    {
        public string Name { get; set; }
        public Vector3 Point { get; set; }
        public string PinName { get; set; }
        public Minimap.PinType PinType { get; set; }
        
        public override void Construct(object[] parameters)
        {
            Name = (string) parameters[1];
            Point = (Vector3) parameters[2];
            PinName = (string) parameters[3];
            PinType = (Minimap.PinType) parameters[4];
        }
    }
}