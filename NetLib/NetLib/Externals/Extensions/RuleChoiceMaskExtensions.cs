using RoR2;
using System;
using UnityEngine.Networking;

namespace NetLib
{
    public static partial class Extensions
    {
        public static void Write( this NetworkWriter writer, RuleChoiceMask src )
        {
            NetworkExtensions.Write( writer, src );
        }
        public static void ReadRuleChoiceMask( this NetworkReader reader, ref RuleChoiceMask dest )
        {
            NetworkExtensions.ReadRuleChoiceMask( reader, dest );
        }
    }
}
