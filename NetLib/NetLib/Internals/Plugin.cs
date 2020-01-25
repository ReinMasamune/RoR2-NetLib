using BepInEx;
using System;

namespace NetLib.Internals
{
    [BepInPlugin( Plugin.guid, Plugin.name, Plugin.version )]
    internal class Plugin
    {
        internal const String guid = Const.guid;
        internal const String name = Const.name;
        internal const String version = Const.ver;



        internal void OnEnable()
        {
            HookHandlerAttribute.InitHooks();
        }

        internal void OnDisable()
        {
            HookHandlerAttribute.RemoveHooks();
        }
    }
}
