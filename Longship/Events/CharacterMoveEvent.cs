using UnityEngine;

namespace Longship.Events
{
    public class CharacterMoveEvent : CharacterEvent
    {
        public Vector3 OldPos { get; }
        public Vector3 NewPos { get; set; }
        
        public CharacterMoveEvent(Character character, Vector3 oldPos) : base(character)
        {
            OldPos = oldPos;
        }
    }
}