using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using LocaL;

public class DeviceCTRL : MonoBehaviour{

    #region Variables
    private List<LocaL.Player> players;
    #endregion

    #region Initialization
    private void Awake() {
        InitLists();
        InitDevices();
    }
    private void InitLists() {
        players = new List<Player>();
    }
    #endregion

    #region Device handling
    private void InitDevices() {
        // Handle devices already connected
        foreach (Gamepad gamepad in Gamepad.all) {
            if (LocaL.Player.numPlayers < LocaL.Player.maxPlayers) {
                LocaL.Player newPlayer = new LocaL.Player(gamepad);
                Debug.Log("Found new controller!");
                //newPlayer.Print();    // Additional player log
                players.Add(newPlayer);
                Debug.Log("Current players: " + players.Count + "/" + LocaL.Player.maxPlayers);
                HandleNewPlayer(newPlayer, true);
            } else {
                Debug.Log("New controller found, but already reached max players (" + LocaL.Player.maxPlayers.ToString() + ")!");
                Debug.Log("Disconnect one of the connected controllers to be able to connect another one.");
            }
        }

        // Handle continous device changes
        InputSystem.onDeviceChange += OnDeviceChange;
    }
    private void OnDeviceChange(InputDevice device, InputDeviceChange eventType) {
        if (Application.isPlaying) {
            // Check if it's a gamepad
            Gamepad controller = device as Gamepad;
            if (controller == null) {
                return;
            }

            // Handle device change
            if (eventType == InputDeviceChange.Added) {
                if (LocaL.Player.numPlayers < LocaL.Player.maxPlayers) {
                    for (int i = 0; i < players.Count; i++) {
                        if (controller == players[i].gamepad) {
                            // Somehow, the controller was already initialized so skip it
                            Debug.Log("Controller already initialized, no need to readd it.");
                            return;
                        }
                    }
                    LocaL.Player newPlayer = new LocaL.Player(controller);
                    Debug.Log("New controller connected!");
                    //newPlayer.Print();    // Additional player log
                    players.Add(newPlayer);
                    Debug.Log("Current players: " + players.Count + "/" + LocaL.Player.maxPlayers);
                    HandleNewPlayer(newPlayer, false);
                } else {
                    Debug.Log("New controller connected, but reached max players (" + LocaL.Player.maxPlayers.ToString() + ")!");
                    Debug.Log("Disconnect one of the connected controllers to be able to connect another one.");
                }
            } else if (eventType == InputDeviceChange.Disconnected || eventType == InputDeviceChange.Destroyed || eventType == InputDeviceChange.Removed) {
                Debug.Log("Controller disconnected!");
                for (int i = 0; i < players.Count; i++) {
                    if (controller.deviceId == players[i].gamepad.deviceId) {
                        //players[i].Print();   // Additional player log
                        players.RemoveAt(i);
                        Debug.Log("Current players: " + players.Count + "/" + LocaL.Player.maxPlayers);
                        return;
                    }
                }
            }
        }
    }

    // If alreadConnected controller has been connected before the script initialized, otherwise
    // player plugged the controller after the script was initialized
    private void HandleNewPlayer(LocaL.Player player, bool alreadyConnected) {
        // Do additional stuff after controller connected here:
        // ...
        // ...
        // ...
    }
    #endregion

}
