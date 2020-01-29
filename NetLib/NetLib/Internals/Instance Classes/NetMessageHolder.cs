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
using NetLib.Internals;

namespace NetLib.Internals
{
    internal class NetMessageHolder : MessageBase
    {
        internal Boolean safe { get; private set; } = false;
        
        internal BaseNetMethod handler { get; private set; }
        internal NetContext context { get; private set; }
        internal NetMessage message { get; private set; }

        private String typekey = "Invalid";

        public override void Serialize( NetworkWriter writer )
        {
            writer.Write( this.safe );
            if( !this.safe )
            {
                Plugin.LogError( "Tries to serialize unsafe message" );
                return;
            }

            writer.Write( this.typekey );
            writer.Write( this.context );
            this.handler.Serialize( this.message, writer );
        }

        public override void Deserialize( NetworkReader reader )
        {
            this.safe = reader.ReadBoolean();
            if( !this.safe )
            {
                Plugin.LogError( "Recieved unsafe message" );
                return;
            }

            this.typekey = reader.ReadString();
            this.context = reader.ReadNetContext();

            this.handler = MessageLookup.GetNetMethod( this.typekey );
            if( this.handler == null )
            {
                Plugin.LogError( "Stopping deserialization due to unknown message type: " + this.typekey );
                return;
            }

            this.message = this.handler.Deserialize( reader );
        }

        internal NetMessageHolder( NetMessage message, NetContext context )
        {
            if( message == null )
            {
                Plugin.LogError( "Cannot create NetMessageHolder from null message" );
                return;
            }
            this.message = message;

            if( context == null )
            {
                Plugin.LogError( "Cannot create NetMessageHolder from null context" );
                return;
            }
            this.context = context;

            this.typekey = MessageLookup.GetTypeKey( this.message );
            if( this.typekey == Const.Invalidtypekey )
            {
                Plugin.LogError( "Cannot create NetMessageHolder from invalid type" );
                return;
            }

            this.handler = MessageLookup.GetNetMethod( this.typekey );
            if( this.handler == null )
            {
                Plugin.LogError( "Cannot create NetMessageHolder from null NetMethod" );
                return;
            }

            this.safe = true;
        }

        public NetMessageHolder() { }
    }
}
