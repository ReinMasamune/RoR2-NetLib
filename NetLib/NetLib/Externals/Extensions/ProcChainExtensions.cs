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
            if( int_WriteProcChainMask == null )
            {
                var asm = Assembly.GetAssembly( typeof( RoR2Application ) );
                var type = asm.GetType( "ProcChainMaskNetworkWriterExtension", true, true );
                var method = type.GetMethod( "Write", Internals.Const.AllFlags );
                int_WriteProcChainMask = Expression.Lambda<Action<NetworkWriter, ProcChainMask>>( Expression.Call( Expression.Constant( null ), method ) ).Compile();
            }

            int_WriteProcChainMask( writer, procChainMask );
        }
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static ProcChainMask ReadProcChainMask( this NetworkReader reader )
        {
            if( int_ReadProcChainMask == null )
            {
                var asm = Assembly.GetAssembly( typeof( RoR2Application ) );
                var type = asm.GetType( "ProcChainMaskNetworkReaderExtension", true, true );
                var method = type.GetMethod( "ReadProcChainMask", Internals.Const.AllFlags );
                int_ReadProcChainMask = Expression.Lambda<Func<NetworkReader, ProcChainMask>>( Expression.Call( Expression.Constant( null ), method ) ).Compile();
            }

            return int_ReadProcChainMask( reader );
        }
    }
}
