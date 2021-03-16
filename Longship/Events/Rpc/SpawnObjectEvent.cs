using UnityEngine;

namespace Longship.Events
{
    public class SpawnObjectEvent : RpcEvent
    {
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
        public int PrefabHash { get; set; }
        
        public override void Construct(object[] parameters)
        {
            Position = (Vector3) parameters[1];
            Rotation = (Quaternion) parameters[2];
            PrefabHash = (int) parameters[3];
        }
    }
}