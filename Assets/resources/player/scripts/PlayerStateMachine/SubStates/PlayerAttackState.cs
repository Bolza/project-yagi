using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState: PlayerAbilityState {

    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) {
    }

    public override void Enter() {
        base.Enter();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
        if (duringAnimation) {
            if (hasRemainingAnimationMovementX()) player.SetVelocityX(baseData.attackMotionSpeedX * player.FacingDirection);
            if (duringHitboxTime) {
                if (!hasRemainingAnimationMovementX()) setAnimationMovement(baseData.attackMotionSpaceX, 0);
                if (player.hitpoint.currentHit) {
                    player.HitCurrentTarget(player.GenerateAttack());
                    EndHitbox();
                }
            }
        }
        else {
            stateMachine.ChangeState(player.IdleState);
        }
    }

}
