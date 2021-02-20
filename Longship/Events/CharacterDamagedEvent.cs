namespace Longship.Events
{
    public class CharacterDamagedEvent : CharacterEvent
    {
        public HitData HitData { get; }
        
        public CharacterDamagedEvent(Character character, HitData hitData) : base(character)
        {
            HitData = hitData;
        }
    }
}