using RoR2;
using System;
using UnityEngine.Networking;

namespace NetLib
{
    public static partial class Extensions
    {
        public static void Write( this NetworkWriter writer, DamageColorIndex damageColorIndex )
        {
            NetworkExtensions.Write( writer, damageColorIndex );
        }

        public static DamageColorIndex ReadDamageColorIndex( this NetworkReader reader )
        {
            return NetworkExtensions.ReadDamageColorIndex( reader );
        }
    }
}
