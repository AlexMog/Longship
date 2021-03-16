namespace Longship.Events
{
    public class RpcReceptionToRouteEvent : RpcReceptionEvent
    {
        public ZNetPeer Target { get; set; }
    }
}