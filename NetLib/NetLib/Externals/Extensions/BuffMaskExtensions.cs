using RoR2;
using System;
using UnityEngine.Networking;

namespace NetLib
{
    public static partial class Extensions
    {
        public static void Write( this NetworkWriter writer, BuffMask buffMask )
        {
            NetworkExtensions.WriteBuffMask( writer, buffMask );
        }
        public static BuffMask ReadBuffMask( this NetworkReader reader )
        {
            return NetworkExtensions.ReadBuffMask( reader );
        }
    }
}
