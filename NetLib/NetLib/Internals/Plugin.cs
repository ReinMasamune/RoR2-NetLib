using BepInEx;
using System;

namespace NetLib.Internals
{
    [BepInPlugin( Plugin.guid, Plugin.name, Plugin.version )]
    internal class Plugin
    {
        internal const String guid = "com.Rein.NetLib";
        internal const String name = "NetLib";
        internal const String version = Version.ver;



        internal void OnEnable()
        {

        }

        internal void OnDisable()
        {

        }
    }
}
