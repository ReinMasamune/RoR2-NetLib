using RoR2;
using System;
using System.Runtime.CompilerServices;
using UnityEngine.Networking;

namespace NetLib
{
    public static partial class Extensions
    {
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static void Write( this NetworkWriter writer, UnlockableIndex index )
        {
            NetworkExtensions.Write( writer, index );
        }
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static UnlockableIndex ReadUnlockableIndex( this NetworkReader reader )
        {
            return NetworkExtensions.ReadUnlockableIndex( reader );
        }
    }
}
