using BepInEx;
using RoR2;
using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine.Networking;

namespace NetLib
{
    public static partial class Extensions
    {
        private static Action<NetworkWriter,ProcChainMask> int_WriteProcChainMask;
        private static Func<NetworkReader,ProcChainMask> int_ReadProcChainMask;

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static void Write( this NetworkWriter writer, ProcChainMask procChainMask )
        {
            int_WriteProcChainMask( writer, procChainMask );
        }
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static ProcChainMask ReadProcChainMask( this NetworkReader reader )
        {
            return int_ReadProcChainMask( reader );
        }

        [ConstructorModule]
        private static class ProcChainMaskExtensionsConstructor
        {
            private static void Construct()
            {
                Internals.Plugin.LogInternal( "Building procmask writer" );
                var writeName = typeof(RoR2Application).AssemblyQualifiedName.Replace(nameof(RoR2Application), "ProcChainMaskNetworkWriterExtension" );
                Type writeType = Type.GetType( writeName, true );
                var writeMethod = writeType.GetMethod( "Write", Internals.Const.AllFlags );
                var writeParam1 = Expression.Parameter( typeof(NetworkWriter), "writer" );
                var writeParam2 = Expression.Parameter( typeof(ProcChainMask), "mask" );
                int_WriteProcChainMask = Expression.Lambda<Action<NetworkWriter, ProcChainMask>>( Expression.Call( null, writeMethod, writeParam1, writeParam2 ), writeParam1, writeParam2 ).Compile();

                Internals.Plugin.LogInternal( "Building procmask reader" );
                var readName = typeof(RoR2Application).AssemblyQualifiedName.Replace(nameof(RoR2Application), "ProcChainMaskNetworkReaderExtension" );
                var readType = Type.GetType( readName, true );
                var readMethod = readType.GetMethod( "ReadProcChainMask", Internals.Const.AllFlags );
                var readParam1 = Expression.Parameter( typeof( NetworkReader ), "reader" );
                int_ReadProcChainMask = Expression.Lambda<Func<NetworkReader, ProcChainMask>>( Expression.Call( null, readMethod, readParam1 ), readParam1 ).Compile();
            }
        }
    }
}
