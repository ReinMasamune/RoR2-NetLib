using RoR2;
using System;
using System.Runtime.CompilerServices;
using UnityEngine.Networking;

namespace NetLib
{
    public static partial class Extensions
    {
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static void Write( this NetworkWriter writer, NetworkPlayerName networkPlayerName )
        {
            NetworkExtensions.Write( writer, networkPlayerName );
        }
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static NetworkPlayerName ReadNetworkPlayerName( this NetworkReader reader )
        {
            return NetworkExtensions.ReadNetworkPlayerName( reader );
        }
    }
}
