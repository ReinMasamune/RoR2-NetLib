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

        internal static void RegisterType( BaseNetMethod netMethod )
        {

        }
    }
}
