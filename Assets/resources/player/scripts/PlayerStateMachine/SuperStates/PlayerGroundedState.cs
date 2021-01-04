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
        if (gotHit) {
            stateMachine.ChangeState(player.HitState);
        }
        else if (blockInput && player.BlockState.CanPerform()) {
            player.StateMachine.ChangeState(player.BlockState);
        }
        else if (rollInput && player.RollState.CanPerform()) {
            player.StateMachine.ChangeState(player.RollState);
        }
        else if (attackInput && player.AttackState.CanPerform()) {
            player.StateMachine.ChangeState(player.AttackState);
        }
        else if (jumpInput && player.JumpState.CanPerform()) {
            player.InputHandler.UseJumpInput();
            stateMachine.ChangeState(player.JumpState);
        }
        else if (!isGrounded && !isCoyoteTimeOn) {
            // Falling w/o jumping
            player.JumpState.DecreaseJumps();
            player.StateMachine.ChangeState(player.InAirState);
        }
        if (isGrounded) {
            lastGroundedTime = Time.time;
        }

    }

}
