using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Networking;

namespace NetLib.BuiltIns
{
    /// <summary>
    /// Contains a few helper methods to allow clients to ask server to apply or remove a buff.
    /// Useful for clientsided skills.
    /// Source code serves as an example for writing a custom message and is heavily commented.
    /// </summary>
    public static class SendBuff
    {
        //Here we provide multiple methods that use the same message
        //I am only going to add comments to one of them, because their structure is largely the same.

        /// <summary>
        /// Adds stacks of a buff to the chosen body.
        /// </summary>
        /// <param name="body">The body that will recieve the buff</param>
        /// <param name="buff">The buff that will be applied</param>
        /// <param name="stacks">How many stacks to add</param>
        public static void AddBuff( CharacterBody body, BuffIndex buff, Int32 stacks = 1 )
        {
            //Want to avoid null refs where possible
            if( body == null ) return;
            //If we are already on server, just apply the buff. No need to send a message
            if( NetworkServer.active )
            {
                for( Int32 i = 0; i < stacks; ++i )
                {
                    body.AddBuff( buff );
                }
            } else
            //If we aren't on server, prepare and send a message to send to the server.
            {
                //This is how the message is sent. Destination.Server means that this message should be sent to server only.
                new BuffMessage( body, buff, stacks ).Send( NetMessage.Destination.Server );
            }
        }

        /// <summary>
        /// Adds stacks of a timed buff to the chosen body.
        /// </summary>
        /// <param name="body">The body that will recieve the buff</param>
        /// <param name="buff">The buff that will be applied</param>
        /// <param name="duration">The time until the buff expires</param>
        /// <param name="stacks">The number of stacks to add</param>
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

        /// <summary>
        /// Removes stacks of a buff from the chosen body.
        /// </summary>
        /// <param name="body">The body to remove from</param>
        /// <param name="buff">The buff to remove</param>
        /// <param name="stacks">The number of stacks to remove</param>
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

        /// <summary>
        /// Removes all stacks of a buff from the chosen body.
        /// </summary>
        /// <param name="body">The body to remove from</param>
        /// <param name="buff">The buff to remove</param>
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


        //A function used to register the message. called in Plugin Awake()
        internal static void Register()
        {
            //Define the function that will be used to handle a recieved message.
            Action<BuffMessage> buffMessageRecievedAction = (message) =>
            {
                //Return if this is not the server (Clients can't add buffs on their own)
                if( !NetworkServer.active ) return;

                //Some logic that determines what we will do based on contents of message
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

            //Create the NetMethod from the function.
            NetMethod<BuffMessage> buffMethod = new NetMethod<BuffMessage>( buffMessageRecievedAction );

            //Register the NetMethod.
            buffMethod.Register();
        }


        //The message we use to send buff related information.
        internal class BuffMessage : NetMessage
        {
            //The data that we send to control how the buff is applied or removed.
            public BuffIndex buff { get; private set; }
            public Int32 stacks { get; private set; }
            public Single duration { get; private set; }
            public Boolean applyDuration { get; private set; }
            public Boolean remove { get; private set; }
            public Boolean removeAll { get; private set; }
            public CharacterBody body { get; private set; }

            //A private var that we use to compress our settings.
            //See the comments for SendDamage for more info.
            //In this case, we are assignning mode automatically in the constructor, rather than doing it in Serialize.
            //That is just a preference choice, it doesn't particularly matter.
            private BuffMode mode;

            //The enum that helps with compression (See SendDamage comments)
            [Flags]
            private enum BuffMode : byte
            {
                None = 0,
                ApplyDuration = 1,
                Remove = 2,
                RemoveAll = 3,
            }

            //Serialize the data to NetworkWriter
            public override void Serialize( NetworkWriter writer )
            {
                //Write the index of the buff.
                //Note that this particular overload of Write is found as an extension in netlib, so you need using NetLib; to use it.
                //All of the extra extensions in netlib are currently direct mappings of game methods, with occasional args shuffled for consistency and clarity.
                writer.WriteBuffIndex( this.buff );
                //Write the number of stacks
                writer.Write( this.stacks );
                //Write the duration
                writer.Write( this.duration );
                //Write a ref to the gameobject for the characterbody
                writer.Write( this.body.gameObject );
                //Write our flags enum as a Byte
                writer.Write( (Byte)this.mode );
            }

            //Deserialize the data from the reader
            public override void Deserialize( NetworkReader reader )
            {
                //Read the buff index, as mentioned above this particular read is an extension method in NetLib
                this.buff = reader.ReadBuffIndex();
                //Read the stacks
                this.stacks = reader.ReadInt32();
                //Read the duration
                this.duration = reader.ReadSingle();
                //Read back the gameobject and then get the body it is part of.
                //Note that if messages of this type are sent frequently, you may want to consider caching the characterbody in a lookup dictionary.
                //In the future, I may add this functionality to the writer and reader extensions.
                this.body = reader.ReadGameObject().GetComponent<CharacterBody>();
                //Read our compressed flags back and cast to the proper type.
                this.mode = (BuffMode)reader.ReadByte();
                //Use HasFlag to convert back to the individual booleans
                //Bitwise can also be used in place of HasFlag (and may be slightly faster, although testing is required)
                //An example for bitwise in this context can be found in SendDamage
                this.applyDuration = this.mode.HasFlag( BuffMode.ApplyDuration );
                this.remove = this.mode.HasFlag( BuffMode.Remove );
                this.removeAll = this.mode.HasFlag( BuffMode.RemoveAll );
            }

            //Define a constructor to create the message
            public BuffMessage( CharacterBody body, BuffIndex buff, Int32 stacks = 0, Single duration = 0f, Boolean applyDuration = false, Boolean remove = false, Boolean removeAll = false )
            {
                this.buff = buff;
                this.body = body;
                this.stacks = stacks;
                this.duration = duration;
                this.applyDuration = applyDuration;
                this.remove = remove;
                this.removeAll = removeAll;

                //Here is where we do the logic to conpress our three booleans into one enum.
                //The  |= operator essentially adds that flag to the enum.
                this.mode = BuffMode.None;
                if( this.applyDuration ) this.mode |= BuffMode.ApplyDuration;
                if( this.remove ) this.mode |= BuffMode.Remove;
                if( this.removeAll ) this.mode |= BuffMode.RemoveAll;
            }
            //Define a public constructor with no parameters. One of these is required for any message type.
            //Note that you do not need to explicitly define it unless you defined another constructor already as we did above.
            public BuffMessage() { }
        }
    }
}
