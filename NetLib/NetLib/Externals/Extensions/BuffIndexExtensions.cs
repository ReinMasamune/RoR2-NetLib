using System;
using UnityEngine.Networking;
using RoR2;

namespace NetLib
{
    public static partial class Extensions
    {
		public static void Write( this NetworkWriter writer, BuffIndex buffIndex )
		{
			NetworkExtensions.WriteBuffIndex( writer, buffIndex );
		}
		public static BuffIndex ReadBuffIndex( this NetworkReader reader )
		{
			return NetworkExtensions.ReadBuffIndex( reader );
		}
	}
}
