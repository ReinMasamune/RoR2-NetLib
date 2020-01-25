using BepInEx;
using BepInEx.Logging;
using System;
using System.Runtime.CompilerServices;

namespace NetLib.Internals
{
    [BepInPlugin( Plugin.NetLibGUID, Plugin.NetLibName, Plugin.NetLibVersion )]
    internal class Plugin : BaseUnityPlugin
    {
        internal const String NetLibGUID = Const.GUID;
        internal const String NetLibName = Const.Name;
        internal const String NetLibVersion = Const.ver;

        private static Plugin instance;

        private Plugin()
        {
            instance = this;
        }

        private void OnEnable()
        {
            HookHandlerAttribute.InitHooks();
        }

        private void OnDisable()
        {
            HookHandlerAttribute.RemoveHooks();
        }


        private static void Log( LogLevel level, System.Object data, String member, Int32 line )
        {
            if( level == LogLevel.Fatal || level == LogLevel.Error || level == LogLevel.Warning || level == LogLevel.Message )
            {
#if EXTLOGGING
                Plugin.instance.Logger.Log( level, data );
#endif
            } else
            {
#if INTLOGGING
                Plugin.instance.Logger.Log( level, data );
#endif
            }
#if LOGLOCATION
            Plugin.instance.Logger.LogWarning( "Log: " + level.ToString() + " called by: " + member + " : " + line );
#endif
        }

        private static UInt64 logCounter = 0u;
        internal static void LogCounter( [CallerMemberName] String member = "", [CallerLineNumber] Int32 line = 0 ) => Plugin.Log( LogLevel.Info, member + ": " + line + ":: " + logCounter++, member, line );

        internal static void LogInternal( System.Object data, [CallerMemberName] String member = "", [CallerLineNumber] Int32 line = 0 ) => Plugin.Log( LogLevel.Info, data, member, line );
        internal static void LogMessage( System.Object data, [CallerMemberName] String member = "", [CallerLineNumber] Int32 line = 0 ) => Plugin.Log( LogLevel.Message, data, member, line );
        internal static void LogWarning( System.Object data, [CallerMemberName] String member = "", [CallerLineNumber] Int32 line = 0 ) => Plugin.Log( LogLevel.Warning, data, member, line );
        internal static void LogError( System.Object data, [CallerMemberName] String member = "", [CallerLineNumber] Int32 line = 0 ) => Plugin.Log( LogLevel.Error, data, member, line );
        internal static void LogFatal( System.Object data, [CallerMemberName] String member = "", [CallerLineNumber] Int32 line = 0 ) => Plugin.Log( LogLevel.Fatal, data, member, line );
    }
}
