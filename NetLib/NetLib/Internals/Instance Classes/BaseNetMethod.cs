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
    internal class BaseNetMethod
    {
        internal Type type;

        internal BaseNetMethod( Type type )
        {
            this.type = type;
        }

        internal void InternalRegister()
        {
            MessageLookup.RegisterType( this );
        }

        internal virtual void HandleMessage( NetMessage message ) { }
        internal virtual NetMessage Deserialize( NetworkReader reader ) { return null; }
        internal virtual void Serialize( NetMessage message, NetworkWriter writer ) { }
    }
}
