using BepInEx;
using RoR2;
using System;
using System.Linq.Expressions;
using System.Reflection;
using UnityEngine.Networking;

namespace NetLib
{
    public static partial class Extensions
    {
        private static Action<NetworkWriter,DamageInfo> int_WriteDamageInfo;
        private static Func<NetworkReader,DamageInfo> int_ReadDamageInfo;

        
        public static void Write( this NetworkWriter writer, DamageInfo damageInfo )
        {
            if( int_WriteDamageInfo == null )
            {
                var asm = Assembly.GetAssembly( typeof( RoR2Application ) );
                var type = asm.GetType( "DamageInfoNetworkWriterExtension", true, true );
                var method = type.GetMethod( "Write", Internals.Const.AllFlags );
                int_WriteDamageInfo = Expression.Lambda<Action<NetworkWriter, DamageInfo>>( Expression.Call( Expression.Constant( null ), method ) ).Compile();
            }

            int_WriteDamageInfo( writer, damageInfo );
        }

        public static DamageInfo ReadDamageInfo( this NetworkReader reader )
        {
            if( int_ReadDamageInfo == null )
            {
                var asm = Assembly.GetAssembly( typeof( RoR2Application ) );
                var type = asm.GetType( "DamageInfoNetworkReaderExtension", true, true );
                var method = type.GetMethod( "ReadDamageInfo", Internals.Const.AllFlags );
                int_ReadDamageInfo = Expression.Lambda<Func<NetworkReader, DamageInfo>>( Expression.Call( Expression.Constant( null ), method ) ).Compile();
            }

            return int_ReadDamageInfo( reader );
        }
    }
}
