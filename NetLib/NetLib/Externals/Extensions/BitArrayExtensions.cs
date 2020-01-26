using System;
using UnityEngine.Networking;
using RoR2;
using System.Runtime.CompilerServices;

namespace NetLib
{
    public static partial class Extensions
    {
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static void WriteBitArray( this NetworkWriter writer, Boolean[] values )
        {
			NetworkExtensions.WriteBitArray( writer, values );
        }
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static void WriteBitArray( this NetworkWriter writer, Boolean[] values, Int32 bufferLength )
		{
			NetworkExtensions.WriteBitArray( writer, values, bufferLength );
		}

		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static void ReadBitArray( this NetworkReader reader, ref Boolean[] values )
		{
			NetworkExtensions.ReadBitArray( reader, values );
		}
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static void ReadBitArray( this NetworkReader reader, ref Boolean[] values, Int32 bufferLength )
		{
			NetworkExtensions.ReadBitArray( reader, values, bufferLength );
		}
	}
}
