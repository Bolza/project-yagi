using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState: PlayerState {
    float oldYVelocity = 0;
    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) {
    }

    public override void DoChecks() {
        base.DoChecks();
    }

    public override void Enter() {
        base.Enter();
    }

    public override void Exit() {
        base.Exit();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
        // check oldVelocity to avoid LandState from programmatic Y adjustements
        if (isGrounded && player.CurrentVelocity.y < -playerData.landAnimationSpeedLimit) {
            stateMachine.ChangeState(player.LandState);
        }
        else if (isGrounded && player.CurrentVelocity.y < 0) {
            stateMachine.ChangeState(player.IdleState);
        }
        else if (isLedged && inputX == player.FacingDirection) {
            Debug.Log(isLedged);
            player.FreezeMovement();
            stateMachine.ChangeState(player.LedgeClimbState);
        }
        else if (jumpInput && isWalled) {
            //stateMachine.ChangeState(player.WallJumpState);
        }
        else if (jumpInput && player.JumpState.CanPerform()) {
            // we can make this action more smooth
            if (playerData.canJumpFromWall) {
                player.InputHandler.UseJumpInput();
                stateMachine.ChangeState(player.JumpState);
            }
        }
        else if (isWalled && inputX == player.FacingDirection && playerData.enableWallGrab) {
            stateMachine.ChangeState(player.WallGrabState);
        }
        else if (isWalled && inputX == player.FacingDirection && player.CurrentVelocity.y <= 0) {

        }
        else {
            player.CheckIfShouldFlip(inputX);
            player.SetVelocityX(playerData.airMovementSpeed * inputX);
            player.Anim.SetFloat("xSpeed", Mathf.Abs(player.CurrentVelocity.x) / playerData.airMovementSpeed * 100);
            player.Anim.SetFloat("ySpeed", player.CurrentVelocity.y / playerData.jumpForce * 100);
        }

        oldYVelocity = player.CurrentVelocity.y;
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }
}
