using UnityEngine;
using UnityEngine.InputSystem;

public class Ability {
    public Ability(string name) {
        this.name = name;
        this.holdTime = 0.2f;
    }
    public string name { get; set; }
    public float startTime { get; set; }
    public float holdTime { get; set; }
    public bool hasInput { get; set; }
    public void Update() {
        if (Time.time >= startTime + holdTime) hasInput = false;
    }
    public void Use() => hasInput = false;
    public void Start() {
        hasInput = true;
        startTime = Time.time;
    }
}


public class PlayerInputHandler : MonoBehaviour {
    private bool frozen;
    public Vector2 RawMovementInput { get; private set; }
    public float NormInputX { get; private set; }
    public float NormInputY { get; private set; }

    public Ability jump = new Ability("jump");
    public Ability attack = new Ability("attack");
    public Ability block = new Ability("block");
    public Ability roll = new Ability("roll");
    public Ability activate = new Ability("activate");

    public void Update() {
        jump.Update();
        attack.Update();
        block.Update();
        roll.Update();
        activate.Update();
    }

    public void OnMoveInput(InputAction.CallbackContext ctx) {
        if (frozen) {
            RawMovementInput = new Vector2(0, 0);
        } else {
            RawMovementInput = ctx.ReadValue<Vector2>();
        }
        NormInputX = (RawMovementInput.x * Vector2.right).normalized.x;
        NormInputY = (RawMovementInput.y * Vector2.up).normalized.y;
    }

    public void UseJumpInput() => jump.Use();
    public void OnJumpInput(InputAction.CallbackContext ctx) {
        if (ctx.started) jump.Start();
    }

    public void UseAttackInput() => attack.Use();
    public void OnAttackInput(InputAction.CallbackContext ctx) {
        if (ctx.started) attack.Start();
    }

    public void UseBlockInput() => block.Use();
    public void OnBlockInput(InputAction.CallbackContext ctx) {
        if (ctx.started) block.Start();
    }

    public void UseRollInput() => roll.Use();
    public void OnRollInput(InputAction.CallbackContext ctx) {
        if (ctx.started) roll.Start();
    }

    public void UseActivateInput() => activate.Use();
    public void OnActivateInput(InputAction.CallbackContext ctx) {
        if (ctx.performed) activate.Start();
    }

    public void MuteInput() => frozen = true;
    public void UnmuteInput() => frozen = false;
}

