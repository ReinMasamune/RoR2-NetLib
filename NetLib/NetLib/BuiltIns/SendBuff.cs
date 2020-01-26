using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Networking;

namespace NetLib.BuiltIns
{
    // TODO: Docs and comments
    public static class SendBuff
    {
        public static void AddBuff( CharacterBody body, BuffIndex buff, Int32 stacks = 1 )
        {
            if( body == null ) return;
            if( NetworkServer.active )
            {
                for( Int32 i = 0; i < stacks; ++i )
                {
                    body.AddBuff( buff );
                }
            } else
            {
                new BuffMessage( body, buff, stacks ).Send( NetMessage.Destination.Server );
            }
        }

        public static void AddTimedBuff( CharacterBody body, BuffIndex buff, Single duration, Int32 stacks = 1 )
        {
            if( body == null ) return;
            if( NetworkServer.active )
            {
                for( Int32 i = 0; i < stacks; ++i )
                {
                    body.AddTimedBuff( buff, duration );
                }
            } else
            {
                new BuffMessage( body, buff, stacks, duration, true ).Send( NetMessage.Destination.Server );
            }
        }

        public static void RemoveBuff( CharacterBody body, BuffIndex buff, Int32 stacks = 1 )
        {
            if( body == null ) return;
            if( NetworkServer.active )
            {
                for( Int32 i = 0; i < stacks; ++i )
                {
                    body.RemoveBuff( buff );
                }
            } else
            {
                new BuffMessage( body, buff, stacks, 0f, false, true ).Send( NetMessage.Destination.Server );
            }
        }

        public static void RemoveAllBuffs( CharacterBody body, BuffIndex buff )
        {
            if( body == null ) return;
            if( NetworkServer.active )
            {
                for( Int32 i = body.GetBuffCount( buff ); i > 0; --i )
                {
                    body.RemoveBuff( buff );
                }
            } else
            {
                new BuffMessage( body, buff, 0, 0f, false, false, true ).Send( NetMessage.Destination.Server );
            }
        }


        public static void Register()
        {
            Action<BuffMessage> buffMessageRecievedAction = (message) =>
            {
                if( !NetworkServer.active ) return;
                if( message.removeAll )
                {
                    for( Int32 i = message.body.GetBuffCount( message.buff ); i > 0; --i )
                    {
                        message.body.RemoveBuff( message.buff );
                    }
                } else if( message.remove )
                {
                    for( Int32 i = message.stacks; i > 0; --i )
                    {
                        message.body.RemoveBuff( message.buff );
                    }
                } else if( message.applyDuration )
                {
                    for( Int32 i = 0; i < message.stacks; ++i )
                    {
                        message.body.AddTimedBuff( message.buff, message.duration );
                    }
                } else
                {
                    for( Int32 i = 0; i < message.stacks; ++i )
                    {
                        message.body.AddBuff( message.buff );
                    }
                }
            };

            NetMethod<BuffMessage> buffMethod = new NetMethod<BuffMessage>( buffMessageRecievedAction );

            buffMethod.Register();
        }


        internal class BuffMessage : NetMessage
        {
            public BuffIndex buff { get; private set; }
            public Int32 stacks { get; private set; }
            public Single duration { get; private set; }
            public Boolean applyDuration { get; private set; }
            public Boolean remove { get; private set; }
            public Boolean removeAll { get; private set; }
            public CharacterBody body { get; private set; }
            private BuffMode mode;
            [Flags]
            private enum BuffMode : byte
            {
                None = 0,
                ApplyDuration = 1,
                Remove = 2,
                RemoveAll = 3,
            }
            public override void Serialize( NetworkWriter writer )
            {
                writer.WriteBuffIndex( this.buff );
                writer.Write( this.stacks );
                writer.Write( this.duration );
                writer.Write( this.body.gameObject );
                writer.Write( (Byte)this.mode );
            }
            public override void Deserialize( NetworkReader reader )
            {
                this.buff = reader.ReadBuffIndex();
                this.stacks = reader.ReadInt32();
                this.duration = reader.ReadSingle();
                this.body = reader.ReadGameObject().GetComponent<CharacterBody>();
                this.mode = (BuffMode)reader.ReadByte();
                this.applyDuration = this.mode.HasFlag( BuffMode.ApplyDuration );
                this.remove = this.mode.HasFlag( BuffMode.Remove );
                this.removeAll = this.mode.HasFlag( BuffMode.RemoveAll );
            }
            public BuffMessage( CharacterBody body, BuffIndex buff, Int32 stacks = 0, Single duration = 0f, Boolean applyDuration = false, Boolean remove = false, Boolean removeAll = false )
            {
                this.buff = buff;
                this.body = body;
                this.stacks = stacks;
                this.duration = duration;
                this.applyDuration = applyDuration;
                this.remove = remove;
                this.removeAll = removeAll;

                this.mode = BuffMode.None;
                if( this.applyDuration ) this.mode |= BuffMode.ApplyDuration;
                if( this.remove ) this.mode |= BuffMode.Remove;
                if( this.removeAll ) this.mode |= BuffMode.RemoveAll;
            }
            public BuffMessage() { }
        }
    }
}
