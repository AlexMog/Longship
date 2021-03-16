namespace Longship.Events
{
    public class ShowMessageEvent : RpcEvent
    {
        public MessageHud.MessageType Type { get; set; }
        public string Message { get; set; }
        
        public override void Construct(object[] parameters)
        {
            Type = (MessageHud.MessageType) parameters[1];
            Message = (string) parameters[2];
        }
    }
}