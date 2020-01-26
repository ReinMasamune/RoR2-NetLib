using RoR2;
using System;
using UnityEngine.Networking;

namespace NetLib
{
    public static partial class Extensions
    {
        public static void Write( this NetworkWriter writer, UnlockableIndex index )
        {
            NetworkExtensions.Write( writer, index );
        }
        public static UnlockableIndex ReadUnlockableIndex( this NetworkReader reader )
        {
            return NetworkExtensions.ReadUnlockableIndex( reader );
        }
    }
}
