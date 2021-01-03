using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLedgeClimbState: PlayerState {

    private Vector2 detectedPos;
    private Vector2 cornerPos;
    private bool isClimbing;
    public PlayerLedgeClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) {
    }

    public override void AnimationFinishTrigger() {
        base.AnimationFinishTrigger();
        player.Anim.SetBool("climbLedge", false);
        isClimbing = false;
    }

    public override void AnimationTrigger() {
        base.AnimationTrigger();
        Debug.Log("Animation Trigger");
    }

    public override void DoChecks() {
        base.DoChecks();
    }

    public override void Enter() {
        base.Enter();
        player.FreezeMovement();
    }

    public override void Exit() {
        base.Exit();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        if (!duringAnimation) {
            if (player.FacingDirection > 0) player.SetPosition(detectedPos + new Vector2(0.3f, player.climbWhyPos));
            if (player.FacingDirection < 0) player.SetPosition(detectedPos + new Vector2(-0.3f, player.climbWhyPos));
            stateMachine.ChangeState(player.MoveState);
        }
        else if (inputX == player.FacingDirection && !isClimbing) {
            isClimbing = true;
            player.Anim.SetBool("climbLedge", true);
        }

        //else if (inputY == -1 && isLedged && !isClimbing) {
        //    stateMachine.ChangeState(player.InAirState);
        //}
    }
    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }

    public void setDetectedPosition(Vector2 pos) => detectedPos = pos;
}
