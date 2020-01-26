using RoR2;
using System;
using System.Runtime.CompilerServices;
using UnityEngine.Networking;

namespace NetLib
{
    public static partial class Extensions
    {
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static void Write( this NetworkWriter writer, EffectData effectData )
		{
			NetworkExtensions.Write( writer, effectData );
		}
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static EffectData ReadEffectData( this NetworkReader reader )
		{
			return NetworkExtensions.ReadEffectData( reader );
		}
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static void ReadEffectData( this NetworkReader reader, ref EffectData effectData )
		{
			NetworkExtensions.ReadEffectData( reader, effectData );
		}
	}
}
