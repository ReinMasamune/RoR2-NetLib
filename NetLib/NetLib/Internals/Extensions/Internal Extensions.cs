using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Networking;

namespace NetLib.Internals.Extensions
{
    internal static partial class InternalExtensions
    {
        #region NetContext
        internal static void Write( this NetworkWriter writer, NetContext context )
        {
            context.Serialize( writer );
        }
        internal static NetContext ReadNetContext( this NetworkReader reader )
        {
            return new NetContext( reader );
        }
        #endregion
    }
}
