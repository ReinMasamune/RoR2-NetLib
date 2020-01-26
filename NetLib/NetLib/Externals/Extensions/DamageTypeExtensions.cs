using RoR2;
using System;
using UnityEngine.Networking;

namespace NetLib
{
    public static partial class Extensions
    {
		public static void Write( this NetworkWriter writer, DamageType damageType )
		{
			NetworkExtensions.Write( writer, damageType );
		}
		public static DamageType ReadDamageType( this NetworkReader reader )
		{
			return NetworkExtensions.ReadDamageType( reader );
		}
	}
}
