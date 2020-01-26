using System;
using UnityEngine.Networking;
using RoR2;

namespace NetLib
{
    public static partial class Extensions
    {
        public static void Write( this NetworkWriter writer, ItemIndex itemIndex )
        {
            RoR2.ItemMaskNetworkExtensions.Write( writer, itemIndex );
        }

        public static ItemIndex ReadItemIndex( this NetworkReader reader )
        {
            return RoR2.ItemMaskNetworkExtensions.ReadItemIndex( reader );
        }

        public static void WriteItemStacks( this NetworkWriter writer, in Int32[] itemStacks )
        {
            RoR2.ItemMaskNetworkExtensions.WriteItemStacks( writer, itemStacks );
        }

        public static void ReadItemStacks( this NetworkReader reader, ref Int32[] itemStacks )
        {
            RoR2.ItemMaskNetworkExtensions.ReadItemStacks( reader, itemStacks );
        }
    }
}
