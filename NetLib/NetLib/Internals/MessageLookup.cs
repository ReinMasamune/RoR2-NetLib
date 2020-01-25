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
    internal static class MessageLookup
    {
        private static Dictionary<String, BaseNetMethod> messageTypes = new Dictionary<String, BaseNetMethod>();

        internal static String GetTypeKey( BaseNetMethod netMethod )
        {
            if( netMethod == null )
            {
                Plugin.LogError( "Cannot get key for null netmethod" );
                return "Invalid";
            }

            if( netMethod.GetType() == typeof( BaseNetMethod ) )
            {
                Plugin.LogError( "Cannot get key for a BaseNetMethod. Use NetMethod<T>" );
                return "Invalid";
            }

            return netMethod.type.AssemblyQualifiedName;
        }

        internal static void RegisterType( BaseNetMethod netMethod )
        {
            String typeKey = GetTypeKey( netMethod );
            if( typeKey == "Invalid" )
            {
                Plugin.LogError( "Error with key generation" );
                return;
            }

            if( messageTypes.ContainsKey( typeKey ) )
            {
                Plugin.LogError( "Tried to register with an already-used key" );
                return;
            }

            messageTypes[typeKey] = netMethod;
        }

        internal static BaseNetMethod GetNetMethod( String typekey )
        {
            if( !messageTypes.ContainsKey( typekey ) )
            {
                Plugin.LogError( "Key: " + typekey + " does not have a registered NetMethod. You may be missing mods or a NetMethod was not registered properly." );
            }

            return messageTypes[typekey];
        }


    }
}
