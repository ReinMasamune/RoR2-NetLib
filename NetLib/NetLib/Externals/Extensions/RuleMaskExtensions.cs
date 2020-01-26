using RoR2;
using System;
using UnityEngine.Networking;

namespace NetLib
{
    public static partial class Extensions
    {
        public static void Write( this NetworkWriter writer, ref RuleMask src )
        {
            NetworkExtensions.Write( writer, src );
        }
        public static void ReadRuleMask( this NetworkReader reader, ref RuleMask dest )
        {
            NetworkExtensions.ReadRuleMask( reader, dest );
        }
    }
}
