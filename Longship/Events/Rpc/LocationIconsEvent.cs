namespace Longship.Events
{
    public class LocationIconsEvent : RpcEvent
    {
        public ZPackage Package { get; set; }
        
        public override void Construct(object[] parameters)
        {
            Package = (ZPackage) parameters[1];
        }
    }
}