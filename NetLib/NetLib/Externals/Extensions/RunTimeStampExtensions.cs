using RoR2;
using System;
using System.Runtime.CompilerServices;
using UnityEngine.Networking;

namespace NetLib
{
    public static partial class Extensions
    {
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static void Write( this NetworkWriter writer, Run.TimeStamp timeStamp )
        {
            NetworkExtensions.Write( writer, timeStamp );
        }
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static Run.TimeStamp ReadTimeStamp( this NetworkReader reader )
        {
            return NetworkExtensions.ReadTimeStamp( reader );
        }
    }
}
