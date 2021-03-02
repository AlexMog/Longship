namespace Longship.Events
{
    public class HumanoidEvent : Event
    {
        public Humanoid Humanoid { get; }

        public HumanoidEvent(Humanoid humanoid)
        {
            Humanoid = humanoid;
        }
    }
}