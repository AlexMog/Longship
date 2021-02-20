namespace Longship.Events
{
    public class PlayerChatEvent : PlayerEvent
    {
        public string Message { get; }
        
        public PlayerChatEvent(Player player, string messsage) : base(player)
        {
            Message = messsage;
        }
    }
}