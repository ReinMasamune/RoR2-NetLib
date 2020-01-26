using RoR2;
using System;
using UnityEngine.Networking;

namespace NetLib
{
    public static partial class Extensions
    {
        public static void Write( this NetworkWriter writer, PickupIndex value )
        {
            NetworkExtensions.Write( writer, value );
        }
        public static PickupIndex ReadPickupIndex( this NetworkReader reader )
        {
            return NetworkExtensions.ReadPickupIndex( reader );
        }
    }
}
