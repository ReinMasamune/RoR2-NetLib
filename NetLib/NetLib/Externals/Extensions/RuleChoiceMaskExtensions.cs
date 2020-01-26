using RoR2;
using System;
using System.Runtime.CompilerServices;
using UnityEngine.Networking;

namespace NetLib
{
    public static partial class Extensions
    {
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static void Write( this NetworkWriter writer, RuleChoiceMask src )
        {
            NetworkExtensions.Write( writer, src );
        }
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static void ReadRuleChoiceMask( this NetworkReader reader, ref RuleChoiceMask dest )
        {
            NetworkExtensions.ReadRuleChoiceMask( reader, dest );
        }
    }
}
