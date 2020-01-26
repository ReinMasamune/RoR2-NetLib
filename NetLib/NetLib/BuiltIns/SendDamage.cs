using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Networking;

namespace NetLib.BuiltIns
{
    // TODO: Comments and docs
    public static class SendDamage
    {
        public static void DealDamage( DamageInfo damage, HurtBox target = null, Boolean callDamage = true, Boolean callHitEnemy = true, Boolean callHitWorld = true )
        {
            if( damage == null ) return;
            if( NetworkServer.active )
            {
                if( callDamage )
                {
                    if( target != null && target.healthComponent != null )
                    {
                        target.healthComponent.TakeDamage( damage );
                    }
                }
                if( callHitEnemy )
                {
                    if( target != null && target.healthComponent != null )
                    {
                        GlobalEventManager.instance.OnHitEnemy( damage, target.healthComponent.gameObject );
                    }
                }
                if( callHitWorld )
                {
                    GlobalEventManager.instance.OnHitAll( damage, (target && target.healthComponent ? target.healthComponent.gameObject : null) );
                }
            } else
            {
                new DamageMessage( damage, target, callDamage, callHitEnemy, callHitWorld ).Send( NetMessage.Destination.Server );
            }
        }

        public static void Register()
        {
            Action<DamageMessage> damageMessageRecievedAction = (message) =>
            {
                if( message.damage == null ) return;
                if( NetworkServer.active )
                {
                    if( message.callDamage && message.target != null && message.target.healthComponent != null)
                    {
                        message.target.healthComponent.TakeDamage( message.damage );
                    }
                    if( message.callHitEnemy && message.target != null && message.target.healthComponent != null )
                    {
                        GlobalEventManager.instance.OnHitEnemy( message.damage, message.target.healthComponent.gameObject );
                    }
                    if( message.callHitWorld )
                    {
                        GlobalEventManager.instance.OnHitAll( message.damage, (message.target && message.target.healthComponent ? message.target.healthComponent.gameObject : null) );
                    }
                }
            };

            NetMethod<DamageMessage> damageMethod = new NetMethod<DamageMessage>( damageMessageRecievedAction );

            damageMethod.Register();
        }

        internal class DamageMessage : NetMessage
        {
            public DamageInfo damage { get; private set; }
            public HurtBox target { get; private set; }
            public Boolean callDamage { get; private set; }
            public Boolean callHitEnemy { get; private set; }
            public Boolean callHitWorld { get; private set; }

            private DamageMode mode;

            [Flags]
            private enum DamageMode : byte
            {
                None = 0,
                CallDamage = 1,
                CallHitEnemy = 2,
                CallHitWorld = 4
            }

            public override void Serialize( NetworkWriter writer )
            {
                this.mode = DamageMode.None;
                if( this.callDamage ) this.mode |= DamageMode.CallDamage;
                if( this.callHitEnemy ) this.mode |= DamageMode.CallHitEnemy;
                if( this.callHitWorld ) this.mode |= DamageMode.CallHitWorld;
                writer.Write( (Byte)this.mode );
                HurtBoxReference.FromHurtBox( this.target ).Write( writer );
                writer.Write( this.damage );
            }

            public override void Deserialize( NetworkReader reader )
            {
                this.mode = (DamageMode)reader.ReadByte();
                this.callDamage = (this.mode & DamageMode.CallDamage) > DamageMode.None;
                this.callHitEnemy = (this.mode & DamageMode.CallHitEnemy) > DamageMode.None;
                this.callHitWorld = (this.mode & DamageMode.CallHitWorld) > DamageMode.None;
                this.target = reader.ReadHurtBoxReference().ResolveHurtBox();
                this.damage = reader.ReadDamageInfo();
            }

            public DamageMessage( DamageInfo damage, HurtBox target = null, Boolean callDamage = true, Boolean callHitEnemy = true, Boolean callHitWorld = true )
            {
                this.damage = damage;
                this.target = target;
                this.callDamage = callDamage;
                this.callHitEnemy = callHitEnemy;
                this.callHitWorld = callHitWorld;
            }
            public DamageMessage() { }
        }
    }
}
