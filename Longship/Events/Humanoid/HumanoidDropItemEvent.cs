namespace Longship.Events
{
    public class HumanoidDropItemEvent : HumanoidEvent
    {
        public ItemDrop.ItemData ItemData { get; }
        public int Amount { get; }
        
        public HumanoidDropItemEvent(Humanoid humanoid, ItemDrop.ItemData itemData, int amount) : base(humanoid)
        {
            ItemData = itemData;
            Amount = amount;
        }
    }
}