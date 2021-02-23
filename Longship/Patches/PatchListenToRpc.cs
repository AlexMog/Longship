using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;

interface RoutedMethodBase
{
    void Invoke(long rpc, ZPackage pkg);
}


namespace Longship.Patches
{
    [HarmonyPatch]
    public class PatchListenToRpc
    {
        private static readonly MethodInfo _getPeer = typeof(ZRoutedRpc).GetMethod("GetPeer",
            BindingFlags.NonPublic | BindingFlags.Instance);
        
        [HarmonyPrefix]
        [HarmonyPatch(typeof(ZRoutedRpc), "HandleRoutedRPC")]
        static void PatchZRoutedRpcHandleRoutedRPC(ZRoutedRpc.RoutedRPCData data,
            Dictionary<int, RoutedMethodBase> ___m_functions)
        {
            if (data.m_targetZDO.IsNone())
            {
                if (!___m_functions.TryGetValue(data.m_methodHash, out var routedMethodBase))
                    return;
                routedMethodBase.Invoke(data.m_senderPeerID, data.m_parameters);
            }
            else
            {
                var zdo = ZDOMan.instance.GetZDO(data.m_targetZDO);
                if (zdo == null)
                    return;
                var instance = ZNetScene.instance.FindInstance(zdo);
                if (!(instance != null))
                    return;
                instance.HandleRoutedRPC(data);
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(ZRoutedRpc), "RouteRPC")]
        static void PatchZRoutedRpcRouteRPC(ZRoutedRpc __instance, ZRoutedRpc.RoutedRPCData rpcData, bool ___m_server,
            List<ZNetPeer> ___m_peers)
        {
            var pkg = new ZPackage();
            rpcData.Serialize(pkg);
            if (___m_server)
            {
                if (rpcData.m_targetPeerID != 0L)
                {
                    var peer = (ZNetPeer) _getPeer.Invoke(__instance,
                        new [] { (object) rpcData.m_targetPeerID} );
                    if (peer == null || !peer.IsReady())
                        return;
                    peer.m_rpc.Invoke("RoutedRPC", (object) pkg);
                }
                else
                {
                    foreach (var peer in ___m_peers)
                    {
                        if (rpcData.m_senderPeerID != peer.m_uid && peer.IsReady())
                            peer.m_rpc.Invoke("RoutedRPC", (object) pkg);
                    }
                }
            }
            else
            {
                foreach (var peer in ___m_peers)
                {
                    if (peer.IsReady())
                        peer.m_rpc.Invoke("RoutedRPC", (object) pkg);
                }
            }
        }
    }
}