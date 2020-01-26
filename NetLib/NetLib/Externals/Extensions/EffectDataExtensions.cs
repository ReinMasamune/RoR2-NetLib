using RoR2;
using System;
using UnityEngine.Networking;

namespace NetLib
{
    public static partial class Extensions
    {
		public static void Write( this NetworkWriter writer, EffectData effectData )
		{
			NetworkExtensions.Write( writer, effectData );
		}
		public static EffectData ReadEffectData( this NetworkReader reader )
		{
			return NetworkExtensions.ReadEffectData( reader );
		}
		public static void ReadEffectData( this NetworkReader reader, ref EffectData effectData )
		{
			NetworkExtensions.ReadEffectData( reader, effectData );
		}
	}
}
