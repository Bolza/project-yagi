using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState: PlayerState {

    private float lastGroundedTime;

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) {
    }

    public override void DoChecks() {
        base.DoChecks();
    }

    public override void Enter() {
        base.Enter();
        player.JumpState.ResetJumpsLeft();
    }

    public override void Exit() {
        base.Exit();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
        bool isCoyoteTimeOn = Time.time <= lastGroundedTime + playerData.coyoteTime;

        if (jumpInput && player.JumpState.CanJump()) {
            player.InputHandler.UseJumpInput();
            stateMachine.ChangeState(player.JumpState);
        }
        if (!isGrounded && !isCoyoteTimeOn) {
            player.JumpState.DecreaseJumps();
            player.StateMachine.ChangeState(player.InAirState);
        }
        if (isGrounded) {
            lastGroundedTime = Time.time;
        }

    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }
}
