using UnityEngine;

namespace Longship.Events
{
    public class CharacterMoveEvent : CharacterEvent
    {
        public Vector3 OldPos { get; }
        public Vector3 NewPos { get; }
        
        public CharacterMoveEvent(Character character, Vector3 oldPos, Vector3 newPos) : base(character)
        {
            OldPos = oldPos;
        }
    }
}