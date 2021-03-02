using System.Collections.Generic;
using Longship.Events;
using Longship.Plugins;

namespace Longship.Managers
{
    public class RpcManager : Manager, IPlugin
    {
        public override void Init()
        {
            Longship.Instance.EventManager.RegisterListener(this,
                (EventManager.EventListener<RpcToExecuteEvent>) RpcToExecuteListener);
            Longship.Instance.EventManager.RegisterListener(this,
                (EventManager.EventListener<RpcToRouteEvent>) RpcToRouteListener);
        }

        private void RpcToExecuteListener(RpcToExecuteEvent rpcToExecuteEvent) {}
        
        private void RpcToRouteListener(RpcToRouteEvent rpcToRouteEvent) {}

        public void OnEnable() {}
        public void OnDisable() {}
        
        private static Dictionary<int, string> _registeredRpc = new Dictionary<int, string>() {
            {"SleepStart".GetStableHashCode(), "SleepStart"}, {"SleepStop".GetStableHashCode(), "SleepStop"},
            {"DiscoverLocationRespons".GetStableHashCode(), "DiscoverLocationRespons"},
            {"DestroyZDO".GetStableHashCode(), "DestroyZDO"}, {"RequestZDO".GetStableHashCode(), "RequestZDO"},
            {"SetGlobalKey".GetStableHashCode(), "SetGlobalKey"}, {"GlobalKeys".GetStableHashCode(), "GlobalKeys"},
            {"LocationIcons".GetStableHashCode(), "LocationIcons"}, {"ShowMessage".GetStableHashCode(), "ShowMessage"},
            {"SpawnObject".GetStableHashCode(), "SpawnObject"}, {"SetEvent".GetStableHashCode(), "SetEvent"},
            {"ChatMessage".GetStableHashCode(), "ChatMessage"},
            {"DiscoverClosestLocation".GetStableHashCode(), "DiscoverClosestLocation"},
        };
    }
}