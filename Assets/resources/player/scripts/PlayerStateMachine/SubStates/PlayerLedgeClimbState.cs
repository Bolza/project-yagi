using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLedgeClimbState: PlayerState {

    private Vector2 detectedPos;
    private Vector2 cornerPos;
    public PlayerLedgeClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) {
    }

    public override void AnimationFinishTrigger() {
        base.AnimationFinishTrigger();
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
        player.transform.position = detectedPos;
    }

    public override void Exit() {
        base.Exit();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
        if (!isLedged || (int)inputX != player.FacingDirection) {
            stateMachine.ChangeState(player.InAirState);
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }

    public void setDetectedPosition(Vector2 pos) => detectedPos = pos;
}
