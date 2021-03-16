namespace Longship.Events
{
    public class SetGlobalKeyEvent : RpcEvent
    {
        public string Key { get; set; }
        
        public override void Construct(object[] parameters)
        {
            Key = (string) parameters[1];
        }
    }
}