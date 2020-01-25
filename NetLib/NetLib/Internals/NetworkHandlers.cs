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
    internal static class NetworkHandlers
    {
        private static readonly Int16[] indicies =
        {
            Const.MainMessageIndex
        };
        internal static readonly HashSet<Int16> usedIndicies = new HashSet<Int16>(indicies);



        [NetworkMessageHandler(client = true, server = true, msgType = Const.MainMessageIndex )]
        private static void HandleMessageMain( NetworkMessage message )
        {
            NetMessageHolder holder = message.ReadMessage<NetMessageHolder>();
            if( !holder.safe )
            {
                Plugin.LogError( "TEMP" );
                return;
            }

            holder.handler.HandleMessage( holder.message );
        }
    }
}
