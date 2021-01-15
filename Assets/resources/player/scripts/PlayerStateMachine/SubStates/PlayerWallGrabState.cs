using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallGrabState: PlayerTouchingWallState {
    bool permaGrab = true;
    public PlayerWallGrabState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) {
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
        //start sliding after wallGrab Duration
        if (!permaGrab && Time.time >= startTime + baseData.wallGrabDuration && !isExitingState) {
            //stateMachine.ChangeState(player.InAirState);
            stateMachine.ChangeState(player.WallSlideState);
        }
        else if (jumpInput) {
            stateMachine.ChangeState(player.WallJumpState);
        }
        else {
            player.FreezeMovement();
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }


}
