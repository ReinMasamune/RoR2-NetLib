using RoR2;
using System;
using UnityEngine.Networking;

namespace NetLib
{
    public static partial class Extensions
    {
        public static void Write( this NetworkWriter writer, Run.TimeStamp timeStamp )
        {
            NetworkExtensions.Write( writer, timeStamp );
        }
        public static Run.TimeStamp ReadTimeStamp( this NetworkReader reader )
        {
            return NetworkExtensions.ReadTimeStamp( reader );
        }
    }
}
