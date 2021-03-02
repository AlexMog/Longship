namespace Longship.Events
{
    public class CharacterDeathEvent : CharacterEvent
    {
        public CharacterDeathEvent(global::Character character) : base(character)
        {
        }
    }
}