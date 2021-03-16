using UnityEngine;

namespace Longship.Events
{
    public class ChatMessageEvent : RpcEvent
    {
        public Vector3 Position { get; set; }
        public Talker.Type Type { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        
        public override void Construct(object[] parameters)
        {
            Position = (Vector3) parameters[1];
            Type = (Talker.Type) parameters[2];
            Name = (string) parameters[3];
            Text = (string) parameters[4];
        }
    }
}