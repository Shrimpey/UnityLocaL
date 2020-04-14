# Unity-LocaL
Small template for working with mutiple local gamepads in Unity (using new Unity Input System).
This template provides simple gamepad manager (for up to 8 devices), assigns each connected device to a new Player and assigns some unique fields (player id, name and color).

## Usage
1. Add new input system to your Unity project via *Window > PackageManager (Advanced > Show preview packages) > Input System > Install*
2. Add *DeviceCTRL.cs*, *InputCTRL.cs*, *Player.cs* and *InputMaster.inputactions* to your project
3. Click on *InputMaster.inputactions* and generate C# class
4. Create empty GameObject and attach DeviceCTRL script to it (this object will be the manager of newly connected gamepads)

Your gamepads should now be properly registered when connected and disconnected. Now you can attach *InputCTRL.cs* to the instantiated player GameObject for your game, set its **gamepadIndex** and trigger its **InitInputCTRL()** to initialize input for that player.
How do I know the proper gamepadIndex? Use *DeviceCTRL.cs*'s `List<LocaL.Player> players` and player's `gamepad.deviceId` field.
You can now use *InputCTRL.cs*'s **input** field to read player's input.

For additional actions to do after new gamepad/Player is added, modify **HandleNewPlayer(LocaL.Player player, bool alreadyConnected)** at the end of *DeviceCTRL.cs*.
To add more input actions, follow instructions in comments above **struct PlayerInput** in *Player.cs*.

## Additional info
This is a freshly made template after learning a bit about the new input system, it still hasn't been thoroughly tested (especially with more devices), so feel free to PR bug fixes or changes.
