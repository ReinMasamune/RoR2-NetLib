using RoR2;
using System;
using UnityEngine.Networking;

namespace NetLib
{
    public static partial class Extensions
    {
        public static void Write( this NetworkWriter writer, TeamIndex teamIndex )
        {
            NetworkExtensions.Write( writer, teamIndex );
        }
        public static TeamIndex ReadTeamIndex( this NetworkReader reader )
        {
            return NetworkExtensions.ReadTeamIndex( reader );
        }
    }
}
