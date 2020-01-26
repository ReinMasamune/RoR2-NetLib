using RoR2;
using System;
using System.Runtime.CompilerServices;
using UnityEngine.Networking;

namespace NetLib
{
    public static partial class Extensions
    {
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static void Write( this NetworkWriter writer, DamageType damageType )
		{
			NetworkExtensions.Write( writer, damageType );
		}
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static DamageType ReadDamageType( this NetworkReader reader )
		{
			return NetworkExtensions.ReadDamageType( reader );
		}
	}
}
