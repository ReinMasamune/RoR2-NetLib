using RoR2;
using System;
using System.Runtime.CompilerServices;
using UnityEngine.Networking;

namespace NetLib
{
    public static partial class Extensions
    {
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static void Write( this NetworkWriter writer, TeamIndex teamIndex )
        {
            NetworkExtensions.Write( writer, teamIndex );
        }
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static TeamIndex ReadTeamIndex( this NetworkReader reader )
        {
            return NetworkExtensions.ReadTeamIndex( reader );
        }
    }
}
