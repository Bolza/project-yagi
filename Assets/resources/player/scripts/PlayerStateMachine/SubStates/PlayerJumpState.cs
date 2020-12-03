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
        player.SetVelocityY(playerData.jumpForce);
        isAbilityDone = true;
    }

    public override void Exit() {
        base.Exit();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }

    public bool CanJump() => jumpsLeft > 0;

    public void ResetJumpsLeft() => jumpsLeft = playerData.jumpsAmount;

    public void DecreaseJumps() => jumpsLeft--;

}
