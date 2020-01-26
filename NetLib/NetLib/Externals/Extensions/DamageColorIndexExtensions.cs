using RoR2;
using System;
using System.Runtime.CompilerServices;
using UnityEngine.Networking;

namespace NetLib
{
    public static partial class Extensions
    {
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static void Write( this NetworkWriter writer, DamageColorIndex damageColorIndex )
        {
            NetworkExtensions.Write( writer, damageColorIndex );
        }
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static DamageColorIndex ReadDamageColorIndex( this NetworkReader reader )
        {
            return NetworkExtensions.ReadDamageColorIndex( reader );
        }
    }
}
