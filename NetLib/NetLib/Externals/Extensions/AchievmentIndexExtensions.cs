using RoR2;
using System;
using System.Runtime.CompilerServices;
using UnityEngine.Networking;

namespace NetLib
{
    public static partial class Extensions
    {
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static void Write( this NetworkWriter writer, AchievementIndex value )
        {
            NetworkExtensions.WriteAchievementIndex( writer, value );
        }
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static AchievementIndex ReadAchievementIndex( this NetworkReader reader )
        {
            return NetworkExtensions.ReadAchievementIndex( reader );
        }
    }
}
