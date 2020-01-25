using BepInEx;
using System;

namespace NetLib
{
    public static class NetLib
    {
        public const String guid = Internals.Plugin.guid;
        public const String name = Internals.Plugin.name;
        public const String version = Internals.Plugin.version;
        
        public static Boolean IsMessageIndexUsed( Int16 index )
        {
            return Internals.NetworkHandlers.usedIndicies.Contains( index );
        }
    }
}
