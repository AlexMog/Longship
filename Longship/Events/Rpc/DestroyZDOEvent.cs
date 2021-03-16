namespace Longship.Events
{
    public class DestroyZDOEvent : RpcEvent
    {
        public ZPackage Package { get; set; }
        
        public override void Construct(object[] parameters)
        {
            Package = (ZPackage) parameters[1];
        }
    }
}