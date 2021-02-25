namespace Longship.Events
{
    public class RpcToRouteEvent : RpcEvent
    {
        public RpcToRouteEvent(ZNetPeer target, string name, object[] parameters) : base(name, parameters)
        {
        }
    }
}