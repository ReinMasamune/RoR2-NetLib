using RoR2;
using System;
using System.Runtime.CompilerServices;
using UnityEngine.Networking;

namespace NetLib
{
    public static partial class Extensions
    {
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static void Write( this NetworkWriter writer, BuffMask buffMask )
        {
            NetworkExtensions.WriteBuffMask( writer, buffMask );
        }
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static BuffMask ReadBuffMask( this NetworkReader reader )
        {
            return NetworkExtensions.ReadBuffMask( reader );
        }
    }
}
