using RoR2;
using System;
using UnityEngine.Networking;

namespace NetLib
{
    public static partial class Extensions
    {
        public static void Write( this NetworkWriter writer, PitchYawPair pitchYawPair )
        {
            NetworkExtensions.Write( writer, pitchYawPair );
        }
        public static PitchYawPair ReadPitchYawPair( this NetworkReader reader )
        {
            return NetworkExtensions.ReadPitchYawPair( reader );
        }
    }
}
