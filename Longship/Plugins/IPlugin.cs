namespace Longship.Plugins
{
    public abstract class IPlugin
    {
        public virtual void OnEnable() {}
        public virtual void OnDisable() {}
    }
}