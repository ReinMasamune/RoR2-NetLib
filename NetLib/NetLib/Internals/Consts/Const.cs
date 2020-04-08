using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine.Networking;

namespace NetLib.Internals
{
    internal static partial class Const
    {
        internal const String GUID = "com.Rein.NetLib";
        internal const String Name = "NetLib";

        internal const String Invalidtypekey = "Invalid";

        internal const Int16 MainMessageIndex = 31415;

        internal const BindingFlags AllFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance;

        internal const QosType DefaultQos = QosType.Reliable;
    }
}
