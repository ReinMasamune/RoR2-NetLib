using RoR2;
using System;
using System.Runtime.CompilerServices;
using UnityEngine.Networking;

namespace NetLib
{
    public static partial class Extensions
    {
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static void Write( this NetworkWriter writer, Run.FixedTimeStamp timeStamp )
        {
            NetworkExtensions.Write( writer, timeStamp );
        }
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static Run.FixedTimeStamp ReadFixedTimeStamp( this NetworkReader reader )
        {
            return NetworkExtensions.ReadFixedTimeStamp( reader );
        }
    }
}
