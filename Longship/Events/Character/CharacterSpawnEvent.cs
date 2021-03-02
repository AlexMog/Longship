namespace Longship.Events
{
    public class CharacterSpawnEvent : CharacterEvent
    {
        public CharacterSpawnEvent(Character character) : base(character)
        {
        }
    }
}