namespace Longship.Events
{
    public class CharacterDeathEvent : CharacterEvent
    {
        public CharacterDeathEvent(Character character) : base(character)
        {
        }
    }
}