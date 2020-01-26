using RoR2;
using System;
using System.Runtime.CompilerServices;
using UnityEngine.Networking;

namespace NetLib
{
    public static partial class Extensions
    {
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static void Write( this NetworkWriter writer, PickupIndex value )
        {
            NetworkExtensions.Write( writer, value );
        }
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static PickupIndex ReadPickupIndex( this NetworkReader reader )
        {
            return NetworkExtensions.ReadPickupIndex( reader );
        }
    }
}
