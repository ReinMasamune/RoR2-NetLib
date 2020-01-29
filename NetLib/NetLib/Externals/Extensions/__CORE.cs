using System;
using System.Reflection;
using UnityEngine.Networking;

namespace NetLib
{
    public static partial class Extensions
    {
        internal static void Construct()
        {
            Internals.Plugin.LogInternal( "Constructing Extensions" );

            foreach( var type in typeof( Extensions ).GetNestedTypes(Internals.Const.AllFlags) )
            {
                var mod = type.GetCustomAttribute<ConstructorModule>();
                if( mod == null ) continue;

                var method = type.GetMethod( "Construct", Internals.Const.AllFlags );
                if( method != null )
                {
                    method.Invoke( null, null );
                    Internals.Plugin.LogInternal( type.Name + " construct successfully invoked." );
                }
            }
        }

        [AttributeUsage( AttributeTargets.Class, AllowMultiple = false )]
        private class ConstructorModule : System.Attribute
        {
            internal ConstructorModule() { }
        }
    }
}
