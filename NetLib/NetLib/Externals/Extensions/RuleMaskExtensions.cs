using RoR2;
using System;
using System.Runtime.CompilerServices;
using UnityEngine.Networking;

namespace NetLib
{
    public static partial class Extensions
    {
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static void Write( this NetworkWriter writer, ref RuleMask src )
        {
            NetworkExtensions.Write( writer, src );
        }
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static void ReadRuleMask( this NetworkReader reader, ref RuleMask dest )
        {
            NetworkExtensions.ReadRuleMask( reader, dest );
        }
    }
}
