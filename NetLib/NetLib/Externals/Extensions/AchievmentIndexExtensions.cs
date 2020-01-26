using RoR2;
using System;
using UnityEngine.Networking;

namespace NetLib
{
    public static partial class Extensions
    {
        public static void Write( this NetworkWriter writer, AchievementIndex value )
        {
            NetworkExtensions.WriteAchievementIndex( writer, value );
        }
        public static AchievementIndex ReadAchievementIndex( this NetworkReader reader )
        {
            return NetworkExtensions.ReadAchievementIndex( reader );
        }
    }
}
