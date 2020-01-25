using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace NetLib.Internals
{
    internal static partial class Const
    {
        internal const String guid = "com.Rein.NetLib";
        internal const String name = "NetLib";

        internal const BindingFlags allFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance;
        internal const Int16 mainMessageIndex = 31415;
    }
}
