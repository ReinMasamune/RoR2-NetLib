using RoR2;
using System;
using System.Runtime.CompilerServices;
using UnityEngine.Networking;

namespace NetLib
{
    public static partial class Extensions
    {
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static void Write( this NetworkWriter writer, EquipmentIndex equipmentIndex )
        {
            NetworkExtensions.Write( writer, equipmentIndex );
        }
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static EquipmentIndex ReadEquipmentIndex( this NetworkReader reader )
        {
            return NetworkExtensions.ReadEquipmentIndex( reader );
        }
    }
}
