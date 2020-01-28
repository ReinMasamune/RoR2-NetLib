using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Networking;

namespace NetLib.BuiltIns
{
    /// <summary>
    /// A very basic example message setup, Sends a chat message to all connected players (assuming they also have NetLib)
    /// Source is commented and serves as an entry level example for writing a custom message.
    /// </summary>
    public static class ExampleTest
    {
        //The method that is called to send the message.
        public static void SendTestMessage( String text )
        {
            //Create an instance of the message we want to send.
            TestMessage message = new TestMessage( text );
            //Call Send() on it to send the message.
            message.Send();
        }

        //A method used to register the message type. This should usually be done in the Awake of your plugin.
        internal static void Register()
        {
            //Declare an Action that is performed on the message type when it is recieved.
            Action<TestMessage> testNetworkAction = ( testMessage ) =>
            {
                Chat.AddMessage( testMessage.text );
            };

            //Create a new instance of a NetMethod for the the message type. Pass the Action that should be performed as an argument.
            NetMethod<TestMessage> testMethod = new NetMethod<TestMessage>( testNetworkAction );

            //Call Register() to add the NetMethod to the list so messages can be recieved.
            testMethod.Register();
        }

        //Define a class that inherits NetMessage and holds the we need to send.
        internal class TestMessage : NetMessage
        {
            //The data we need to send. In this case, this represents the message that will be printed to chat.
            public String text { get; private set; }


            //Define a constructor to make it easier to create an instance of the message.
            public TestMessage( String text )
            {
                this.text = text;
            }

            //If you define a constructor, you must also define a public constructor with no parameters like this.
            public TestMessage() { }

            //This method is called on the sender. Inside, we want to write all information needed to make a new copy of this message to the NetworkWriter
            public override void Serialize( NetworkWriter writer )
            {
                //In this case, we are writing a String.
                writer.Write( this.text );
                //Note that not all types can be written this simply. For more complicated types you may need to get creative. See more advanced examples.
            }

            //This method is called on the reciever. Inside, we want to read out all the information we wrote in Serialize and assign the values of the class.
            public override void Deserialize( NetworkReader reader )
            {
                //Read out a String and save it to the class. (Remember that this is called on the reciever, who doesn't have a copy of the data)
                this.text = reader.ReadString();
                //Again, more complicated messages may require you to get creative here. See the more advanced examples.
            }

        }

    }
}
