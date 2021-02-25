namespace Longship.Events
{
    public class RpcToExecuteEvent : RpcEvent
    {
        public RpcToExecuteEvent(string name, object[] parameters) : base(name, parameters)
        {
        }
    }
}