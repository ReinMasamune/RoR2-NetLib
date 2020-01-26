using System;
using UnityEngine.Networking;
using RoR2;
using System.Runtime.CompilerServices;

namespace NetLib
{
    public static partial class Extensions
    {
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static void Write( this NetworkWriter writer, BuffIndex buffIndex )
		{
			NetworkExtensions.WriteBuffIndex( writer, buffIndex );
		}
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static BuffIndex ReadBuffIndex( this NetworkReader reader )
		{
			return NetworkExtensions.ReadBuffIndex( reader );
		}
	}
}
