using RoR2;
using System;
using UnityEngine.Networking;

namespace NetLib
{
    public static partial class Extensions
    {
        public static void Write( this NetworkWriter writer, Run.FixedTimeStamp timeStamp )
        {
            NetworkExtensions.Write( writer, timeStamp );
        }
        public static Run.FixedTimeStamp ReadFixedTimeStamp( this NetworkReader reader )
        {
            return NetworkExtensions.ReadFixedTimeStamp( reader );
        }
    }
}
