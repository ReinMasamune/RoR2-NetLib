NetLib is a networking library for mods. Currently beta and looking for feedback.

## For Users:
### Install:
- Download
- Open the Zip file
- Locate NetLib.dll inside the zip
- Right click on it and select copy
- Navigate to RoR2 directory
- Open the BepInEx folder
- Open the Plugins folder inside BepInEx
- Right click, click new, then folder
- Name the folder NetLib
- Open the folder
- Right click, and select paste
- You're done

### Uninstall
- Navigate to RoR2 directory
- Open BepInEx folder
- Open the plugins folder
- Locate either NetLib.dll, or the NetLib folder and delete them
- You're done

### Bug reporting
If you see errors or bugs of any kind please create an issue on github, or you can DM me on discord (@Rein#7551)

## For Developers:

There are three major functionalities offered by NetLib:

### Framework
NetLib offers a framework for sending messages over network. The framework is heavily styled after UNet (the style of networking present in base game) with a few key differences:
- Uses a single message index for all messages (Helps ensure mod interoperability)
- Forces proper structure of messages through inheritance
- Simplified process for sending the messages
- Catches most errors before they reach game code

### BuiltIns 
In addition to the framework, NetLib also offers built-in implementations of commonly used networking functionality. These BuiltIns are wrapped in a single static method.
The BuiltIns are also heavily commented and documented, and serve as a good way to get started with writing a custom message type. There are two (and a half) BuiltIns currently:
- ExampleTest, which is really just a debugging message that enters a message into chat over network.
- SendBuff, which allows a client to send a message to server to add or remove buffs from a body.
- SendDamage, which allows a more flexible way to send damage to server than BulletMessages, in that you can choose what parts of the damage process occur.

### Extensions
NetLib also contains numerous NetworkWriter and NetworkReader extension methods. Many of these are direct mappings to the In-Game functions (even the private ones).
These are intended to make it easier to work with readers and writers, and will be expanded upon in the future to include things such as caching and other performance boosts. 

## Other Stuff:
- Pull requests are welcome.
- Issues are also welcome.
- General feedback is welcome.
