using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState: PlayerState {
    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) {
    }

    public override void DoChecks() {
        base.DoChecks();
    }

    public override void Enter() {
        base.Enter();
        maxYMovement = baseData.jumpMaxY;
        maxXMovement = baseData.jumpMaxX;
    }

    public override void Exit() {
        base.Exit();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
        // check oldVelocity to avoid LandState from programmatic Y adjustements
        if (isGrounded && player.CurrentVelocity.y < -baseData.landAnimationSpeedLimit) {
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
            if (baseData.canJumpFromWall) {
                player.InputHandler.UseJumpInput();
                stateMachine.ChangeState(player.JumpState);
            }
        }
        else if (isWalled && inputX == player.FacingDirection && baseData.enableWallGrab) {
            stateMachine.ChangeState(player.WallGrabState);
        }
        else if (isWalled && inputX == player.FacingDirection && player.CurrentVelocity.y <= 0) {

        }
        else {
            player.CheckIfShouldFlip(inputX);

            if (canMoveX()) player.SetVelocityX(baseData.airMovementSpeed * inputX);

            if (!canMoveY() && player.CurrentVelocity.y > 0) {
                player.SetVelocityY(0);
            }

            player.Anim.SetFloat("xSpeed", Mathf.Abs(player.CurrentVelocity.x));
            player.Anim.SetFloat("ySpeed", player.CurrentVelocity.y);
        }

    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }
}
