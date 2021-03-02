namespace Longship.Events
{
    public class HumanoidUseItemEvent : HumanoidEvent
    {
        public Inventory FromInventory { get; }
        public ItemDrop.ItemData ItemData { get; }
        
        public HumanoidUseItemEvent(Humanoid humanoid, Inventory fromInventory, ItemDrop.ItemData itemData)
            : base(humanoid)
        {
            FromInventory = fromInventory;
            ItemData = itemData;
        }
    }
}