namespace Longship.Events
{
    public class RequestZDOEvent : RpcEvent
    {
        public ZDOID ZdoId { get; set; }
        
        public override void Construct(object[] parameters)
        {
            ZdoId = (ZDOID) parameters[1];
        }
    }
}