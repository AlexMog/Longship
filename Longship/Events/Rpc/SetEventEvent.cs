using UnityEngine;

namespace Longship.Events
{
    public class SetEventEvent : RpcEvent
    {
        public string EventName { get; set; }
        public float Time { get; set; }
        public Vector3 Pos { get; set; }
        
        public override void Construct(object[] parameters)
        {
            EventName = (string) parameters[1];
            Time = (float) parameters[2];
            Pos = (Vector3) parameters[3];
        }
    }
}