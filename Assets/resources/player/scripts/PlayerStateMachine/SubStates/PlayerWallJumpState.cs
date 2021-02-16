using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState {
    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) {
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
        player.JumpState.ResetJumpsLeft();
        player.SetVelocity(baseData.wallJumpForce, baseData.wallJumpAngle, -player.FacingDirection);
        player.CheckIfShouldFlip(-player.FacingDirection);
        player.JumpState.DecreaseJumps();
    }

    public override void Exit() {
        base.Exit();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
        player.Anim.SetFloat("ySpeed", player.CurrentVelocity.y);
        player.Anim.SetFloat("xSpeed", Mathf.Abs(player.CurrentVelocity.x));
        if (Time.time >= startTime + baseData.wallJumpTime) {
            isAbilityDone = true;
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }

    public void DetermineWallJumpDirection(bool isWalled) {
        // https://youtu.be/xetW10MUI0o?list=PLy78FINcVmjA0zDBhLuLNL1Jo6xNMMq-W&t=655
    }
}
