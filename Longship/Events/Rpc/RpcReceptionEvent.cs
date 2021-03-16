namespace Longship.Events
{
    public abstract class RpcReceptionEvent : Event, ICancellable
    {
        public bool Cancelled { get; set; } = false;
        public string RpcName { get; set; }
        public object[] Params { get; set; }
    }
}