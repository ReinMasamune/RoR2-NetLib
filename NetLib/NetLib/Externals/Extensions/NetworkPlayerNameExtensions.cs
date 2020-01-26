using RoR2;
using System;
using UnityEngine.Networking;

namespace NetLib
{
    public static partial class Extensions
    {
        public static void Write( this NetworkWriter writer, NetworkPlayerName networkPlayerName )
        {
            NetworkExtensions.Write( writer, networkPlayerName );
        }
        public static NetworkPlayerName ReadNetworkPlayerName( this NetworkReader reader )
        {
            return NetworkExtensions.ReadNetworkPlayerName( reader );
        }
    }
}
