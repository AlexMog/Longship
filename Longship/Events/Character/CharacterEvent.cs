namespace Longship.Events
{
    public abstract class CharacterEvent : Event
    {
        public Character Character { get; }

        public CharacterEvent(Character character)
        {
            Character = character;
        }
    }
}