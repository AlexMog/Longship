using System.Collections.Generic;
using Longship.Events;
using Longship.Plugins;

namespace Longship.Managers
{
    public class RpcManager : Manager, IPlugin
    {
        public override void Init()
        {
            Longship.Instance.EventManager.RegisterListener<RpcReceptionToExecuteEvent>(this, RpcToExecuteListener);
            Longship.Instance.EventManager.RegisterListener<RpcReceptionToRouteEvent>(this, RpcToRouteListener);
        }

        private void RpcToExecuteListener(RpcReceptionToExecuteEvent rpcReceptionToExecuteEvent)
        {
            HandleRpc(rpcReceptionToExecuteEvent);
        }

        private void RpcToRouteListener(RpcReceptionToRouteEvent rpcReceptionToRouteEvent)
        {
            HandleRpc(rpcReceptionToRouteEvent);
        }

        private void HandleRpc(RpcReceptionEvent rpcReceptionEvent)
        {
            if (_registeredRpc.TryGetValue(rpcReceptionEvent.RpcName, out var evt))
            {
                evt.PlayerId = (long) rpcReceptionEvent.Params[0];
                evt.Construct(rpcReceptionEvent.Params);
                Longship.Instance.EventManager.DispatchEvent(evt);
                rpcReceptionEvent.Cancelled = evt.Cancelled;
            }
            else
            {
                Longship.Instance.Log.LogWarning($"[RpcManager] Rpc not registered: {rpcReceptionEvent.RpcName}");
            }
        }

        public void OnEnable() {}
        public void OnDisable() {}
        
        // Cached events
        private static Dictionary<string, RpcEvent> _registeredRpc = new Dictionary<string, RpcEvent>() {
            {"SleepStart", new SleepStartEvent()}, {"SleepStop", new SleepStopEvent()},
            {"DiscoverLocationRespons", new DiscoverLocationResponseEvent()},
            {"DestroyZDO", new DestroyZDOEvent()}, {"RequestZDO", new RequestZDOEvent()},
            {"SetGlobalKey", new SetGlobalKeyEvent()}, {"GlobalKeys", new GlobalKeysEvent()},
            {"LocationIcons", new LocationIconsEvent()}, {"ShowMessage", new ShowMessageEvent()},
            {"SpawnObject", new SpawnObjectEvent()}, {"SetEvent", new SetEventEvent()},
            {"ChatMessage", new ChatMessageEvent()},
            {"DiscoverClosestLocation", new DiscoverClosestLocationEvent()},
        };
    }
}