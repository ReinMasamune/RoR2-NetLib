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
            Const.mainMessageIndex
        };
        internal static readonly HashSet<Int16> usedIndicies = new HashSet<Int16>(indicies);


    }
}
