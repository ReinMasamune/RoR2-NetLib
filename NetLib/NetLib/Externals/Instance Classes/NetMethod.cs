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

namespace NetLib
{
    public sealed class NetMethod<T> : Internals.BaseNetMethod where T : NetMessage, new()
    {
        public NetMethod( Action<T> networkedAction ) : base( typeof(T) )
        {
            this.netAction = networkedAction;
        }

        public void Register()
        {
            base.InternalRegister();
        }

        internal override void Serialize( NetMessage message, NetworkWriter writer )
        {
            (message as T)?.Serialize( writer );
        }

        internal override NetMessage Deserialize( NetworkReader reader )
        {
            T message = new T();
            message.Deserialize( reader );
            return message;
        }

        internal override void HandleMessage( NetMessage message )
        {
            var msg = message as T;
            if( msg == null )
            {
                Internals.Plugin.LogError( "Unable to handle message" );
                return;
            }

            this.netAction?.Invoke( msg );
        }

        internal Action<T> netAction;
    }
}
