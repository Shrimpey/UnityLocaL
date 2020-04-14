using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using LocaL;


public class InputCTRL : MonoBehaviour
{
    #region variables

    [System.NonSerialized]
    public LocaL.PlayerInput input;
    [System.NonSerialized]
    public InputMaster inputMaster;
    
    public int gamepadIndex = 0;
    #endregion

    #region generic methods

    private void Awake() {
        inputMaster = new InputMaster();
    }

    // Initializes player controls, trigger this after setting proper gamepadIndex based
    // on DeviceCTRL's list of players (gamepadID) in custom spawn script.
    public void InitInputCTRL() {
        inputMaster.devices = new[] { Gamepad.all[gamepadIndex] };

        input = new LocaL.PlayerInput();

        // Movement
        inputMaster.Player.Movement.performed += ctx => { input.movement = ctx.ReadValue<Vector2>(); };
        inputMaster.Player.Movement.canceled += ctx => { input.movement = ctx.ReadValue<Vector2>(); };
        // Jumping
        inputMaster.Player.Jump.started += ctx => { input.jump = ctx.ReadValue<float>(); };
        inputMaster.Player.Jump.performed += ctx => { input.jump = ctx.ReadValue<float>(); };
        inputMaster.Player.Jump.canceled += ctx => { input.jump = ctx.ReadValue<float>(); };
        // Shooting
        inputMaster.Player.Range.started += ctx => { input.range = ctx.ReadValue<float>(); };
        inputMaster.Player.Range.performed += ctx => { input.range = ctx.ReadValue<float>(); };
        inputMaster.Player.Range.canceled += ctx => { input.range = ctx.ReadValue<float>(); };
        // Blocking
        inputMaster.Player.Block.started += ctx => { input.block = ctx.ReadValue<float>(); };
        inputMaster.Player.Block.performed += ctx => { input.block = ctx.ReadValue<float>(); };
        inputMaster.Player.Block.canceled += ctx => { input.block = ctx.ReadValue<float>(); };
        // Melee
        inputMaster.Player.Melee.started += ctx => { input.melee = ctx.ReadValue<float>(); };
        inputMaster.Player.Melee.performed += ctx => { input.melee = ctx.ReadValue<float>(); };
        inputMaster.Player.Melee.canceled += ctx => { input.melee = ctx.ReadValue<float>(); };
    }

    private void OnEnable() {
        inputMaster.Enable();
    }

    private void OnDisable() {
        inputMaster.Disable();
    }

    #endregion
}
