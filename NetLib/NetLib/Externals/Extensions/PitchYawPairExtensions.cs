using RoR2;
using System;
using System.Runtime.CompilerServices;
using UnityEngine.Networking;

namespace NetLib
{
    public static partial class Extensions
    {
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static void Write( this NetworkWriter writer, PitchYawPair pitchYawPair )
        {
            NetworkExtensions.Write( writer, pitchYawPair );
        }
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static PitchYawPair ReadPitchYawPair( this NetworkReader reader )
        {
            return NetworkExtensions.ReadPitchYawPair( reader );
        }
    }
}
