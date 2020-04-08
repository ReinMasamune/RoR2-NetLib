using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Networking;
using UnityEngine;

namespace NetLib.BuiltIns
{
    public static class SendDoT
    {
        public static void ApplyDoT( GameObject victimObject, GameObject attackerObject, DotController.DotIndex dotIndex, Single duration = 8f, Single damageMultiplier = 1f  )
        {
            if( dotIndex == DotController.DotIndex.None ) return;

            if( victimObject == null || !victimObject ) return;

            if( attackerObject == null || !attackerObject ) return;

            if( NetworkServer.active )
            {
                DotController.InflictDot( victimObject, attackerObject, dotIndex, duration, damageMultiplier );
            } else
            {
                new DoTMessage( victimObject, attackerObject, dotIndex, duration, damageMultiplier ).Send( NetMessage.Destination.Server );
            }
        }

        internal static void Register()
        {
            Action<DoTMessage> dotMessageRecievedAction = (message) =>
            {
                if( NetworkServer.active )
                {
                    ApplyDoT( message.victimObject, message.attackerObject, message.dotIndex, message.duration, message.damageMultiplier );
                }
            };

            NetMethod<DoTMessage> damageMethod = new NetMethod<DoTMessage>( dotMessageRecievedAction );

            damageMethod.Register();
        }

        internal class DoTMessage : NetMessage
        {
            public GameObject victimObject { get; private set; }
            public GameObject attackerObject { get; private set; }
            public DotController.DotIndex dotIndex { get; private set; }
            public Single duration { get; private set; }
            public Single damageMultiplier { get; private set; }

            public override void Serialize( NetworkWriter writer )
            {
                writer.Write( this.victimObject );
                writer.Write( this.attackerObject );
                writer.Write( (Int32)this.dotIndex );
                writer.Write( this.duration );
                writer.Write( this.damageMultiplier );
            }

            public override void Deserialize( NetworkReader reader )
            {
                this.victimObject = reader.ReadGameObject();
                this.attackerObject = reader.ReadGameObject();
                this.dotIndex = (DotController.DotIndex)reader.ReadInt32();
                this.duration = reader.ReadSingle();
                this.damageMultiplier = reader.ReadSingle();
            }

            public DoTMessage( GameObject victimObject, GameObject attackerObject, DotController.DotIndex dotIndex, Single duration, Single damageMultiplier )
            {
                this.victimObject = victimObject;
                this.attackerObject = attackerObject;
                this.dotIndex = dotIndex;
                this.duration = duration;
                this.damageMultiplier = damageMultiplier;
            }
            public DoTMessage() { }
        }
    }
}
