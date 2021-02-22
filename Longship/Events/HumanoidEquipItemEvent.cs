namespace Longship.Events
{
    public class HumanoidEquipItemEvent : HumanoidEvent
    {
        public ItemDrop.ItemData Item { get; }
        
        public HumanoidEquipItemEvent(Humanoid humanoid, ItemDrop.ItemData item) : base(humanoid)
        {
            Item = item;
        }
    }
}