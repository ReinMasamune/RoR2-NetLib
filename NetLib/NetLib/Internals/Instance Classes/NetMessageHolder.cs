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
    internal class NetMessageHolder : MessageBase
    {
        internal Boolean safe { get; private set; } = false;
        
        internal BaseNetMethod handler { get; private set; }
        internal NetContext context { get; private set; }
        internal NetMessage message { get; private set; }
    }
}
