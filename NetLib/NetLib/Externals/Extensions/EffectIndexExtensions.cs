using RoR2;
using System;
using UnityEngine.Networking;

namespace NetLib
{
    public static partial class Extensions
    {
        public static void Write( this NetworkWriter writer, EffectIndex effectIndex )
        {
            NetworkExtensions.WriteEffectIndex( writer, effectIndex );
        }

        public static EffectIndex ReadEffectIndex( this NetworkReader reader )
        {
            return NetworkExtensions.ReadEffectIndex( reader );
        }
    }
}
