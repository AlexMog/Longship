namespace Longship.Events
{
    public class HumanoidPickupItemEvent : HumanoidEvent
    {
        public ItemDrop.ItemData ItemData { get; }
        
        public HumanoidPickupItemEvent(Humanoid humanoid, ItemDrop.ItemData itemData) : base(humanoid)
        {
            ItemData = itemData;
        }
    }
}