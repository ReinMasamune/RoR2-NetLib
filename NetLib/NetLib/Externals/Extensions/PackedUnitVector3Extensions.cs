using RoR2;
using System;
using UnityEngine.Networking;

namespace NetLib
{
    public static partial class Extensions
    {
        public static void Write( this NetworkWriter writer, PackedUnitVector3 value )
        {
            NetworkExtensions.Write( writer, value );
        }
        public static PackedUnitVector3 ReadPackedUnitVector3( this NetworkReader reader )
        {
            return NetworkExtensions.ReadPackedUnitVector3( reader );
        }
    }
}
