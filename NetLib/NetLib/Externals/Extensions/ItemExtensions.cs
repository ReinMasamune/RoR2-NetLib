using System;
using UnityEngine.Networking;
using RoR2;
using System.Runtime.CompilerServices;

namespace NetLib
{
    public static partial class Extensions
    {
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static void Write( this NetworkWriter writer, ItemIndex itemIndex )
        {
            RoR2.ItemMaskNetworkExtensions.Write( writer, itemIndex );
        }
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static ItemIndex ReadItemIndex( this NetworkReader reader )
        {
            return RoR2.ItemMaskNetworkExtensions.ReadItemIndex( reader );
        }
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static void WriteItemStacks( this NetworkWriter writer, Int32[] itemStacks )
        {
            RoR2.ItemMaskNetworkExtensions.WriteItemStacks( writer, itemStacks );
        }
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static void ReadItemStacks( this NetworkReader reader, ref Int32[] itemStacks )
        {
            RoR2.ItemMaskNetworkExtensions.ReadItemStacks( reader, itemStacks );
        }
    }
}
