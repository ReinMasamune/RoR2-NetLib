using BepInEx;
using System;

namespace NetLib
{
    public static class NetLib
    {
        public const String guid = Internals.Plugin.NetLibGUID;
        public const String name = Internals.Plugin.NetLibName;
        public const String version = Internals.Plugin.NetLibVersion;
        
        public static Boolean IsMessageIndexUsed( Int16 index )
        {
            return Internals.NetworkHandlers.usedIndicies.Contains( index );
        }
    }
}
