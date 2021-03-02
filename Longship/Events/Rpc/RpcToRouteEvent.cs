namespace Longship.Events
{
    public class RpcToRouteEvent : RpcEvent
    {
        public ZNetPeer Target { get; set; }
    }
}