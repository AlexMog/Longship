namespace Longship.Events
{
    public abstract class RpcEvent : Event, ICancellable
    {
        public bool Cancelled { get; set; } = false;
        public string RpcName { get; }
        public object[] Params { get; }

        public RpcEvent(string name, object[] parameters)
        {
            RpcName = name;
            Params = parameters;
        }
    }
}