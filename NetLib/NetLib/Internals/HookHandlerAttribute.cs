using BepInEx;
using MonoMod;
using MonoMod.RuntimeDetour;
using MonoMod.RuntimeDetour.HookGen;
using MonoMod.Utils;
using System;
using System.Reflection;
using System.ComponentModel;
using RoR2.Networking;
using UnityEngine.Networking;
using Mono.Cecil;
using Mono.Cecil.Rocks;
using System.Collections.Generic;

namespace NetLib.Internals
{
    internal static class HookHandlerAttribute
    {
        internal delegate void orig_CollectHandlers();
        internal delegate void on_CollectHandlers( orig_CollectHandlers orig );
        private static MethodBase base_CollectHandlers;
        internal static void InitHooks()
        {
            if( base_CollectHandlers == null )
            {
                base_CollectHandlers = MethodBase.GetMethodFromHandle( typeof( NetworkMessageHandlerAttribute ).GetMethod( "CollectHandlers", Const.AllFlags ).MethodHandle );
            }

            HookEndpointManager.Add( base_CollectHandlers, (on_CollectHandlers)hook_CollectHandlers );
        }
        internal static void RemoveHooks()
        {
            if( base_CollectHandlers == null )
            {
                base_CollectHandlers = MethodBase.GetMethodFromHandle( typeof( NetworkMessageHandlerAttribute ).GetMethod( "CollectHandlers", Const.AllFlags ).MethodHandle );
            }

            HookEndpointManager.Remove( base_CollectHandlers, (on_CollectHandlers)hook_CollectHandlers );
        }

        private static void hook_CollectHandlers( orig_CollectHandlers orig )
        {
            orig();
            var clientListInfo = typeof(NetworkMessageHandlerAttribute).GetField("clientMessageHandlers", Const.AllFlags);
            var clientList = (List<NetworkMessageHandlerAttribute>)clientListInfo?.GetValue(typeof(NetworkMessageHandlerAttribute));

            var serverListInfo = typeof(NetworkMessageHandlerAttribute).GetField("serverMessageHandlers", Const.AllFlags);
            var serverList = (List<NetworkMessageHandlerAttribute>)serverListInfo?.GetValue(typeof(NetworkMessageHandlerAttribute));

            FieldInfo netAttribHandler = typeof(NetworkMessageHandlerAttribute).GetField("messageHandler", Const.AllFlags);

            foreach( MemberInfo member in typeof( NetworkHandlers ).GetMembers( Const.AllFlags ) )
            {
                var handlerAttribute = member.GetCustomAttribute<NetworkMessageHandlerAttribute>();
                if( handlerAttribute == null ) continue;

                var method = member as MethodInfo;
                if( method == null ) continue;

                var networkDelegate = Delegate.CreateDelegate( typeof(NetworkMessageDelegate), method ) as NetworkMessageDelegate;
                if( networkDelegate == null ) continue;

                netAttribHandler?.SetValue( handlerAttribute, networkDelegate );

                if( handlerAttribute.client ) clientList?.Add( handlerAttribute );
                if( handlerAttribute.server ) serverList?.Add( handlerAttribute );
            }
        }
    }
}
