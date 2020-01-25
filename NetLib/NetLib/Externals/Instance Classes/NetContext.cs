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
    public sealed class NetContext
    {
        public Boolean fromClient { get; private set; }
        public Boolean fromAuthority { get; private set; }
        public Boolean fromServer { get; private set; }

        public NetContext( Boolean authority = false )
        {
            this.fromClient = NetworkClient.active;
            this.fromServer = NetworkServer.active;
            this.fromAuthority = authority;

            this.pack = Packed.AllFalse;
            if( this.fromClient ) this.pack |= Packed.FromClient;
            if( this.fromServer ) this.pack |= Packed.FromServer;
            if( this.fromAuthority ) this.pack |= Packed.FromAuthority;
        }


        #region Internals
        internal Packed pack;

        [Flags]
        internal enum Packed : byte
        {
            AllFalse = 0,
            FromClient = 1,
            FromServer = 2,
            FromAuthority = 4,
            AllTrue = 7
        }

        internal void Serialize( NetworkWriter writer )
        {
            writer.Write( (Byte)this.pack );
        }

        internal NetContext( NetworkReader reader )
        {
            var pack = (Packed)reader.ReadByte();
            this.fromClient = pack.HasFlag( Packed.FromClient );
            this.fromServer = pack.HasFlag( Packed.FromServer );
            this.fromAuthority = pack.HasFlag( Packed.FromAuthority );

            this.pack = pack;
        }
        #endregion
    }
}
