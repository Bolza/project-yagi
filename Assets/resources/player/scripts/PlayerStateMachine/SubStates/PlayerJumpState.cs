using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState: PlayerAbilityState {
    private int jumpsLeft;

    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) {
        jumpsLeft = playerData.jumpsAmount;
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
        DecreaseJumps();
        float jumpy = Mathf.Sqrt(2f * playerData.jumpForce * -playerData.gravity);
        player.SetVelocityY(jumpy);
    }

    public override void Exit() {
        base.Exit();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
        if (isAbilityDone) {
            //if (isGrounded && player.CurrentVelocity.y < 0.01f) stateMachine.ChangeState(player.IdleState);
            //else
            stateMachine.ChangeState(player.InAirState);
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }

    public override bool CanPerform() {
        return base.CanPerform() && jumpsLeft > 0;
    }

    public void ResetJumpsLeft() => jumpsLeft = playerData.jumpsAmount;

    public void DecreaseJumps() => jumpsLeft--;

}
