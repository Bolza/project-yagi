using UnityEngine;

public class PlayerOnLadderState : PlayerState {
    public PlayerOnLadderState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) {
    }

    public override void DoChecks() {
        base.DoChecks();
    }

    public override void Enter() {
        base.Enter();
        player.JumpState.ResetJumpsLeft();
        player.SetVelocityX(0);
        player.SetVelocityY(0);
        stateControlledPhysics = true;
        Collider2D ladder = player.CheckHasLadder();
        FacingDirections ladderFacing = ladder.transform.rotation.y == 0 ? FacingDirections.left : FacingDirections.right;
        if ((int)ladderFacing == player.FacingDirection) player.Flip();
        float xOffset = 0.2f * (int)ladderFacing;
        player.transform.position = new Vector2(ladder.transform.position.x + xOffset, player.transform.position.y);
    }

    public override void Exit() {
        base.Exit();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
        player.SetVelocityY(inputY * baseData.walkSpeed);
        if (inputY < 0 && isGrounded) stateMachine.ChangeState(player.IdleState);
        else if (inputY > 0 && !player.CheckHasLadder()) {
            player.transform.position = new Vector2(player.transform.position.x, player.transform.position.y + 0.2f);
            stateMachine.ChangeState(player.IdleState);
        } else if (jumpInput && player.JumpState.CanPerform()) {
            player.InputHandler.UseJumpInput();
            stateMachine.ChangeState(player.JumpState);
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }
}
