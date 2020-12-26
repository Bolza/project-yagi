using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Dynamic;


public class PlayerInputHandler: MonoBehaviour {
    public Vector2 RawMovementInput { get; private set; }
    public float NormInputX { get; private set; }
    public float NormInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool AttackInput { get; private set; }
    [SerializeField] private float inputHoldTime = 0.2f;
    [SerializeField] private float attackHoldTime = 0.2f;

    private float jumpStartTime;
    private float attackStartTime;

    public void Update() {
        CheckJumpInputExpired();
        CheckAttackInputExpired();
    }


    public void OnMoveInput(InputAction.CallbackContext ctx) {
        RawMovementInput = ctx.ReadValue<Vector2>();
        NormInputX = (RawMovementInput.x * Vector2.right).normalized.x;
        NormInputY = (RawMovementInput.y * Vector2.up).normalized.y;
    }

    public void OnJumpInput(InputAction.CallbackContext ctx) {
        if (ctx.started) {
            jumpStartTime = Time.time;
            JumpInput = true;
        }
    }

    public void OnFireInput(InputAction.CallbackContext ctx) {
        if (ctx.started) {
            attackStartTime = Time.time;
            AttackInput = true;
        }
    }

    public void UseJumpInput() {
        JumpInput = false;
    }

    private void CheckJumpInputExpired() {
        if (Time.time >= jumpStartTime + inputHoldTime) JumpInput = false;
    }

    private void CheckAttackInputExpired() {
        if (Time.time >= attackStartTime + attackHoldTime) AttackInput = false;
    }

}

