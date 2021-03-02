using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using Longship.Events;

namespace Longship.Patches
{
    [HarmonyPatch]
    public class PatchListenToRpc
    {
        private static readonly FieldInfo _action = typeof(RoutedMethod<object, object, object, object>)
            .GetField("m_action");
        private static readonly MethodInfo _getPeer = typeof(ZRoutedRpc).GetMethod("GetPeer",
            BindingFlags.NonPublic | BindingFlags.Instance);
        // Caches
        private static readonly RpcToExecuteEvent _rpcToExecuteEvent = new RpcToExecuteEvent();
        private static readonly RpcToRouteEvent _rpcToRouteEvent = new RpcToRouteEvent();
        
        [HarmonyPrefix]
        [HarmonyPatch(typeof(ZRoutedRpc), "HandleRoutedRPC")]
        static bool PatchZRoutedRpcHandleRoutedRPC(ZRoutedRpc.RoutedRPCData data,
            Dictionary<int, RoutedMethod<object, object, object, object>> ___m_functions)
        {
            Longship.Instance.Log.LogDebug($"HandleRoutedRPC {_registeredRpc[data.m_methodHash]}");
            if (data.m_targetZDO.IsNone())
            {
                if (!___m_functions.TryGetValue(data.m_methodHash, out var routedMethodBase))
                    return false;
                Longship.Instance.Log.LogDebug($"Routed method found: {routedMethodBase} with hash: {data.m_methodHash}");
                _executeRpc(data.m_methodHash, routedMethodBase, data.m_senderPeerID, data.m_parameters);
            }
            else
            {
                var zdo = ZDOMan.instance.GetZDO(data.m_targetZDO);
                if (zdo == null)
                    return false;
                var instance = ZNetScene.instance.FindInstance(zdo);
                if (!(instance != null))
                    return false;
                Longship.Instance.Log.LogDebug($"HandleRpc method found: {instance} with hash: {data.m_methodHash}");
                instance.HandleRoutedRPC(data);
            }

            return false;
        }

        private static void _executeRpc(int rpcHash, RoutedMethod<object, object, object, object> method, long rpc,
            ZPackage package)
        {
            var action = _action.GetValue(method);
            if (method.GetType().IsAssignableFrom(typeof(RoutedMethod<object, object, object, object>)))
            {
                var parameters = ZNetView.Deserialize(rpc,
                    ((RoutedMethod<object, object, object, object>.Method) action).Method.GetParameters(), package);
                _rpcToExecuteEvent.Cancelled = false;
                _rpcToExecuteEvent.RpcName = _registeredRpc[rpcHash];
                _rpcToExecuteEvent.Params = parameters;
                Longship.Instance.EventManager.DispatchEvent(_rpcToExecuteEvent);
                if (_rpcToExecuteEvent.Cancelled)
                {
                    return;
                }
                ((RoutedMethod<object, object, object, object>.Method) action).DynamicInvoke(parameters);
            }
            else
            {
                var parameters = ZNetView.Deserialize(rpc, ((Action) action).Method.GetParameters(), package);
                _rpcToExecuteEvent.Cancelled = false;
                _rpcToExecuteEvent.RpcName = _registeredRpc[rpcHash];
                _rpcToExecuteEvent.Params = parameters;
                Longship.Instance.EventManager.DispatchEvent(_rpcToExecuteEvent);
                if (_rpcToExecuteEvent.Cancelled)
                {
                    return;
                }
                ((Action) action).DynamicInvoke(parameters);
            }
        }

        private static void _sendRoutedRpc(IDictionary<int, RoutedMethod<object, object, object, object>> functions,
            ZRoutedRpc.RoutedRPCData rpcData, ZNetPeer peer, ZPackage pkg)
        {
            if (!functions.TryGetValue(rpcData.m_methodHash, out var routedMethodBase))
                return;
            Longship.Instance.Log.LogDebug($"Routing RPC: {routedMethodBase} with hash: {rpcData.m_methodHash}");
            var rpc = peer.m_rpc;
            var action = _action.GetValue(routedMethodBase);
            var parameters = routedMethodBase.GetType()
                .IsAssignableFrom(typeof(RoutedMethod<object, object, object, object>)) ?
                ZNetView.Deserialize(peer.m_uid,((RoutedMethod<object, object, object, object>.Method) action)
                    .Method.GetParameters(), rpcData.m_parameters) :
                ZNetView.Deserialize(peer.m_uid,
                    ((Action) action).Method.GetParameters(), rpcData.m_parameters);
            // FIXME Use cached object or pool to avoid GBC usage
            _rpcToRouteEvent.Cancelled = false;
            _rpcToRouteEvent.Target = peer;
            _rpcToRouteEvent.RpcName = _registeredRpc[rpcData.m_methodHash];
            _rpcToRouteEvent.Params = parameters;
            Longship.Instance.EventManager.DispatchEvent(_rpcToRouteEvent);
            if (_rpcToRouteEvent.Cancelled)
            {
                return;
            }
            rpc.Invoke("RoutedRPC", (object) pkg);
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(ZRoutedRpc), "RouteRPC")]
        static bool PatchZRoutedRpcRouteRPC(ZRoutedRpc __instance, ZRoutedRpc.RoutedRPCData rpcData, bool ___m_server,
            List<ZNetPeer> ___m_peers, Dictionary<int, RoutedMethod<object, object, object, object>> ___m_functions)
        {
            Longship.Instance.Log.LogDebug($"RouteRPC {rpcData.m_methodHash}");
            var pkg = new ZPackage();
            rpcData.Serialize(pkg);
            if (___m_server)
            {
                if (rpcData.m_targetPeerID != 0L)
                {
                    var peer = (ZNetPeer) _getPeer.Invoke(__instance,
                        new [] { (object) rpcData.m_targetPeerID} );
                    if (peer == null || !peer.IsReady())
                        return false;
                    Longship.Instance.Log.LogDebug($"Routed to target {rpcData.m_targetPeerID}: {rpcData.m_methodHash}");
                    _sendRoutedRpc(___m_functions, rpcData, peer, pkg);
                }
                else
                {
                    foreach (var peer in ___m_peers)
                    {
                        if (rpcData.m_senderPeerID == peer.m_uid || !peer.IsReady()) continue;
                        Longship.Instance.Log.LogDebug($"Routed to target {peer.m_uid}: {rpcData.m_methodHash}");
                        _sendRoutedRpc(___m_functions, rpcData, peer, pkg);
                    }
                }
            }
            else
            {
                foreach (var peer in ___m_peers)
                {
                    if (!peer.IsReady()) continue;
                    Longship.Instance.Log.LogDebug($"Routed to target {peer.m_uid}: {rpcData.m_methodHash}");
                    _sendRoutedRpc(___m_functions, rpcData, peer, pkg);
                }
            }

            return false;
        }
        
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