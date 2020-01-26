using RoR2;
using System;
using UnityEngine.Networking;

namespace NetLib
{
    public static partial class Extensions
    {
        public static void Write( this NetworkWriter writer, EquipmentIndex equipmentIndex )
        {
            NetworkExtensions.Write( writer, equipmentIndex );
        }
        public static EquipmentIndex ReadEquipmentIndex( this NetworkReader reader )
        {
            return NetworkExtensions.ReadEquipmentIndex( reader );
        }
    }
}
