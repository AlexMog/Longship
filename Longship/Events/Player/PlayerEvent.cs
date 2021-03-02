namespace Longship.Events
{
    public abstract class PlayerEvent : Event
    {
        public Player ValheimPlayer { get; }

        protected PlayerEvent(Player valheimPlayer)
        {
            ValheimPlayer = valheimPlayer;
        }
    }
}