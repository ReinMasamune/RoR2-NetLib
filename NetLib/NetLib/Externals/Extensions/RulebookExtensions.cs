using RoR2;
using System;
using UnityEngine.Networking;

namespace NetLib
{
    public static partial class Extensions
    {
        public static void Write( this NetworkWriter writer, ref RuleBook src )
        {
            NetworkExtensions.Write( writer, src );
        }
        public static void ReadRuleBook( this NetworkReader reader, ref RuleBook dest )
        {
            NetworkExtensions.ReadRuleBook( reader, dest );
        }
    }
}
