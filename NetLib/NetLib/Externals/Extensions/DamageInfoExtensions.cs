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
        private static Action<NetworkWriter,DamageInfo> int_WriteDamageInfo;
        private static Func<NetworkReader,DamageInfo> int_ReadDamageInfo;

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static void Write( this NetworkWriter writer, DamageInfo damageInfo )
        {
            int_WriteDamageInfo( writer, damageInfo );
        }
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static DamageInfo ReadDamageInfo( this NetworkReader reader )
        {
            return int_ReadDamageInfo( reader );
        }

        [ConstructorModule]
        private static class DamageInfoExtensionsConstructor
        {
            private static void Construct()
            {
                Internals.Plugin.LogInternal( "Building damageinfo writer" );
                var writeName = typeof(RoR2Application).AssemblyQualifiedName.Replace(nameof(RoR2Application), "DamageInfoNetworkWriterExtension" );
                Type writeType = Type.GetType( writeName, true );
                var writeMethod = writeType.GetMethod( "Write", Internals.Const.AllFlags );
                var writeParam1 = Expression.Parameter( typeof(NetworkWriter), "writer" );
                var writeParam2 = Expression.Parameter( typeof(DamageInfo), "damageInfo" );
                int_WriteDamageInfo = Expression.Lambda<Action<NetworkWriter, DamageInfo>>(Expression.Call( null, writeMethod, writeParam1, writeParam2 ), writeParam1, writeParam2).Compile();

                Internals.Plugin.LogInternal( "Building damageinfo reader" );
                var readName = typeof(RoR2Application).AssemblyQualifiedName.Replace(nameof(RoR2Application), "DamageInfoNetworkReaderExtension" );
                var readType = Type.GetType( readName, true );
                var readMethod = readType.GetMethod( "ReadDamageInfo", Internals.Const.AllFlags );
                var readParam1 = Expression.Parameter( typeof( NetworkReader ), "reader" );
                int_ReadDamageInfo = Expression.Lambda<Func<NetworkReader, DamageInfo>>( Expression.Call( null, readMethod, readParam1 ), readParam1 ).Compile();
            }
        }
    }
}
