using RoR2;
using System;
using System.Runtime.CompilerServices;
using UnityEngine.Networking;

namespace NetLib
{
    public static partial class Extensions
    {
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static void Write( this NetworkWriter writer, RuleBook src )
        {
            NetworkExtensions.Write( writer, src );
        }
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static void ReadRuleBook( this NetworkReader reader, ref RuleBook dest )
        {
            NetworkExtensions.ReadRuleBook( reader, dest );
        }
    }
}
