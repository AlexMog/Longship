namespace Longship.Events
{
    public class HumanoidUnequipItemEvent : HumanoidEvent
    {
        public ItemDrop.ItemData Item { get; }
        
        public HumanoidUnequipItemEvent(Humanoid humanoid, ItemDrop.ItemData item) : base(humanoid)
        {
            Item = item;
        }
    }
}