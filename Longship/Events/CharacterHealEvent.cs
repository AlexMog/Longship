namespace Longship.Events
{
    public class CharacterHealEvent : CharacterEvent
    {
        public float HealValue { get; set; }
        
        public CharacterHealEvent(Character character, float healValue) : base(character)
        {
            HealValue = healValue;
        }
    }
}