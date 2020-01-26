using RoR2;
using System;
using System.Runtime.CompilerServices;
using UnityEngine.Networking;

namespace NetLib
{
    public static partial class Extensions
    {
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static void Write( this NetworkWriter writer, HurtBoxReference hurtBoxReference )
        {
            NetworkExtensions.Write( writer, hurtBoxReference );
        }
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static HurtBoxReference ReadHurtBoxReference( this NetworkReader reader )
        {
            return NetworkExtensions.ReadHurtBoxReference( reader );
        }
    }
}
