using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchingWallState: PlayerState {


    public PlayerTouchingWallState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) {
    }

    public override void AnimationFinishTrigger() {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger() {
        base.AnimationTrigger();
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

        if (isGrounded) {
            stateMachine.ChangeState(player.IdleState);
        }
        else if (!isWalled || (int)inputX != player.FacingDirection) {
            stateMachine.ChangeState(player.InAirState);
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }
}
