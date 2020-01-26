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
    public abstract class NetMessage
    {
        public NetContext context { get; private set; }
        
        public NetMessage messageBase
        {
            get
            {
                return this as NetMessage;
            }
        }

        public abstract void Serialize( NetworkWriter writer );
        public abstract void Deserialize( NetworkReader reader );


        [Flags]
        public enum Destination
        {
            None = 0,
            Client = 1,
            Server = 2,
            All = 3
        }

        protected void Send( Destination dest = Destination.All, Boolean authority = false, QosType sendType = Internals.Const.DefaultQos )
        {
            this.context = new NetContext( authority );
            var holder = new Internals.NetMessageHolder(this, this.context );

            if( holder == null || !holder.safe )
            {
                Internals.Plugin.LogError( "NetMessageHolder is not safe and will not be sent." );
                return;
            }

            if( this.context.fromServer )
            {
                if( dest.HasFlag( Destination.Client ) )
                {
                    foreach( NetworkConnection connection in NetworkServer.connections )
                    {
                        connection.SendByChannel( Internals.Const.MainMessageIndex, holder, (Int32)sendType );
                    }
                }
            }

            if( this.context.fromClient )
            {
                if( dest.HasFlag( Destination.Server ) )
                {
                    ClientScene.readyConnection.SendByChannel( Internals.Const.MainMessageIndex, holder, (Int32)sendType );
                }
            }
        }
    }

}
