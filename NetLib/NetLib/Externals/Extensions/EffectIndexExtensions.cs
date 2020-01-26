using RoR2;
using System;
using System.Runtime.CompilerServices;
using UnityEngine.Networking;

namespace NetLib
{
    public static partial class Extensions
    {
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static void Write( this NetworkWriter writer, EffectIndex effectIndex )
        {
            NetworkExtensions.WriteEffectIndex( writer, effectIndex );
        }
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static EffectIndex ReadEffectIndex( this NetworkReader reader )
        {
            return NetworkExtensions.ReadEffectIndex( reader );
        }
    }
}
