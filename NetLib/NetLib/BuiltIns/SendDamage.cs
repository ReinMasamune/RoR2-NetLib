using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Networking;

namespace NetLib.BuiltIns
{
    /// <summary>
    /// Contains a helper function to send damage from client to server.
    /// More flexible than simply using the in game bullet damage messages.
    /// Useful mostly for clientsided hit detection.
    /// </summary>
    public static class SendDamage
    {
        /// <summary>
        /// Either deals damage or sends a damage message depending on context.
        /// If target is null will not call TakeDamage or OnHitEnemy.
        /// TakeDamage is what causes the actual health loss
        /// OnHitEnemy is where most procs and similar effects occur.
        /// OnHitAll is where Behemoth and Overloading Affix occur (Explosions when you shoot world)
        /// </summary>
        /// <param name="damage">The damageinfo to deal</param>
        /// <param name="target">The target to deal damage to</param>
        /// <param name="callDamage">Should HealthComponent.TakeDamage be called</param>
        /// <param name="callHitEnemy">Should GlobalEventManager.OnHitEnemy be called</param>
        /// <param name="callHitWorld">Should GlobalEventManager.OnHitAll be called</param>
        public static void DealDamage( DamageInfo damage, HurtBox target = null, Boolean callDamage = true, Boolean callHitEnemy = true, Boolean callHitWorld = true )
        {
            //If the damage is null, then don't do anything with it.
            if( damage == null ) return;

            //If we're running on the server, we can just deal the damage instead of passing it through network.
            if( NetworkServer.active )
            {
                //Some logic based on the args determine what we actually do here
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
            //If we aren't running this on server, we want to send a message to the server so it can handle it.
            {
                //We create a message that holds the data we want to send, and then we call Send() on it.
                new DamageMessage( damage, target, callDamage, callHitEnemy, callHitWorld ).Send( NetMessage.Destination.Server );
                //Note that we supply an argument NetMessage.Destination.Server to the message.
                //This argument tells NetLib that we only want the message being sent to the server. There is little point in sending to clients in this case.
                //If we did want to send to clients, we could do NetMessage.Destination.Client, and that would send the message to all clients.
                //We could also do NetMessage.Destionation.Server | NetMessage.Destination.Client to send to both server and clients.
                //There is a shorthand for that as well, NetMessage.Destination.All
                //It is worth noting that communication does not occur directly between clients by default.
                //If you needed to send messages directly to clients, you would need to establish a direct connection with the client, and would need the server to assist in setting it up.
                //That type of task is something that is well beyond the scope of this library, and I would highly suggest avoiding it.
                //Instead, if you want to send a message from client to client, you should have the server act as a relay for it.
                //Essentially, in the NetMethod you would want code that says if(NetworkServer.Active) message.Send( NetMessage.Destination.Client );
                //Note that that would result in the message being sent back to the client that originally sent it to server, so you would need logic to handle that situation.
            }
        }

        //Static method to register the message. Called in plugin Awake().
        internal static void Register()
        {
            //Create the action that will handle the message.
            Action<DamageMessage> damageMessageRecievedAction = (message) =>
            {
                //Don't want null refs here
                if( message.damage == null ) return;
                //This action is only intended to run on the server, so we add this to be sure.
                if( NetworkServer.active )
                {
                    //Reading data from message and performing some logic here
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
            //It is worth noting that the function we use to handle the message is nearly identical to the function used to send it.
            //In this case, it would actually be possible to use the exact same function for both sending and handling
            //For clarity, and to avoid potentially serious bugs we avoid this, but it is entirely possible.
            //The reason that these functions are nearly identical is because we want the DealDamage function to work regardless of where it is called.
            //That means DealDamage will simply do the damage if it was called on server, and if it wasn't called on server it will send to server.
            //Because the handling function is also only running on server, and that is where the message was sent, it is logical that they would have overlap in functionality.


            //Create the NetMethod for DamageMessages
            NetMethod<DamageMessage> damageMethod = new NetMethod<DamageMessage>( damageMessageRecievedAction );

            //Register the NetMethod.
            damageMethod.Register();
        }

        //The message that contains all of the info we want to send.
        internal class DamageMessage : NetMessage
        {
            //The info we want to send
            public DamageInfo damage { get; private set; }
            public HurtBox target { get; private set; }
            public Boolean callDamage { get; private set; }
            public Boolean callHitEnemy { get; private set; }
            public Boolean callHitWorld { get; private set; }

            //This is a private field that we use to compress the booleans in our data.
            private DamageMode mode;


            //A flags enum to help us with compression.
            //Note that it explicitly derives from byte. You want to make sure this is the smallest integer type that fits the number of Booleans we are using.
            //A byte is 8-bit. That means it has room for 8 seperate bits (1s or 0s)
            //A boolean is simply true(1) false(0), so we really could fit 8 Booleans in one byte.
            //Unity however, writes information as a series of bytes, meaning our 3 booleans would take up 24 bits total.
            //By using the extra space available in a byte, we can send our 3 booleans in 8 bits of space, which is a significant savings.
            [Flags]
            private enum DamageMode : byte
            {
                None = 0,
                CallDamage = 1,
                CallHitEnemy = 2,
                CallHitWorld = 4
            }

            //Serialize our data to the NetworkWriter
            public override void Serialize( NetworkWriter writer )
            {
                //In order to compress our 3 booleans into a single byte we use bitwise.
                //Technically, the enum isn't needed at all here, but it makes the code much more readable.
                //Start with no data.
                this.mode = DamageMode.None;
                //If callDamage, add the calldamage flag to mode.
                if( this.callDamage ) this.mode |= DamageMode.CallDamage;
                //If callhitenemy, add the callhitenemy flag to mode.
                if( this.callHitEnemy ) this.mode |= DamageMode.CallHitEnemy;
                //If callhitworld, add the callhitworld flag to mode.
                if( this.callHitWorld ) this.mode |= DamageMode.CallHitWorld;

                //Cast mode to a byte and write it.
                writer.Write( (Byte)this.mode );

                //Write the target hurtbox.
                //Note that this function is defined in netlib, so you must have using NetLib;
                writer.Write( HurtBoxReference.FromHurtBox( this.target ) );

                //This version of write is another helper function defined in NetLib
                //There are numerous Write functions present in game code that do this type of thing. NetLib maps them all in NetLib.Extensions
                //In addition to the in game ones, in the future NetLib may expand to add its own. If you write one that you think is useful, submit a PR for it.
                //If you have something that you really need a writer for, but don't know how to approach, submit an issue and I will take a crack at it.
                writer.Write( this.damage );
            }

            //Read back the data that we wrote.
            //Note that we read in the same order that we wrote.
            public override void Deserialize( NetworkReader reader )
            { 
                //Read the single byte we wrote to contain our three booleans.
                this.mode = (DamageMode)reader.ReadByte();

                //This is some more bitwise stuff, but you could also use this.mode.HasFlag( DamageMode.CallDamage );
                //The reason I am using bitwise instead is to show how HasFlag works.
                this.callDamage = (this.mode & DamageMode.CallDamage) > DamageMode.None;
                this.callHitEnemy = (this.mode & DamageMode.CallHitEnemy) > DamageMode.None;
                this.callHitWorld = (this.mode & DamageMode.CallHitWorld) > DamageMode.None;

                //Read back the target, in this case we need an actual HurtBox, not a HurtBoxReference, so we call ResolveHurtBox() to retreive it.
                this.target = reader.ReadHurtBoxReference().ResolveHurtBox();

                //Read back the damage itself. This read function is also part of Extensions.
                this.damage = reader.ReadDamageInfo();
            }

            //Define a constructor for our damagemessage.
            public DamageMessage( DamageInfo damage, HurtBox target = null, Boolean callDamage = true, Boolean callHitEnemy = true, Boolean callHitWorld = true )
            {
                this.damage = damage;
                this.target = target;
                this.callDamage = callDamage;
                this.callHitEnemy = callHitEnemy;
                this.callHitWorld = callHitWorld;
            }
            //Since we defined a constructor with parameters, we also need a public constructor that does not take parameters.
            //The compiler will warn you about this, so don't worry too much.
            public DamageMessage() { }
        }
    }
}
