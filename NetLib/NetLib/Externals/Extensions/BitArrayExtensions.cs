using System;
using UnityEngine.Networking;
using RoR2;

namespace NetLib
{
    public static partial class Extensions
    {
        public static void WriteBitArray( this NetworkWriter writer, ref Boolean[] values )
        {
			NetworkExtensions.WriteBitArray( writer, values );
        }
		public static void WriteBitArray( this NetworkWriter writer, ref Boolean[] values, Int32 bufferLength )
		{
			NetworkExtensions.WriteBitArray( writer, values, bufferLength );
		}

		public static void ReadBitArray( this NetworkReader reader, ref Boolean[] values )
		{
			NetworkExtensions.ReadBitArray( reader, values );
		}
		public static void ReadBitArray( this NetworkReader reader, ref Boolean[] values, Int32 bufferLength )
		{
			NetworkExtensions.ReadBitArray( reader, values, bufferLength );
		}
	}
}
