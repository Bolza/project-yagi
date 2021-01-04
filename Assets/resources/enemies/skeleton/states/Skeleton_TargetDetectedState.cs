using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_TargetDetectedState: TargetDetectedState {
    private SkeletonAI enemy;

    public Skeleton_TargetDetectedState(Enemy entity, EnemyStateMachine stateMachine, string animBoolName, EnemyData enemyData) : base(entity, stateMachine, animBoolName, enemyData) {
        this.enemy = (SkeletonAI)entity;
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
        enemy.setVelocityX(0);
    }

    public override void Exit() {
        base.Exit();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
        if (gotHit) {
            stateMachine.ChangeState(enemy.HitState);
        }
        else if (!targetDetected) {
            stateMachine.ChangeState(enemy.IdleState);
        }
        else {
            if (targetDetectedBackward) entity.Flip();
            if (distanceFromTarget > enemyData.attackRange) {
                stateMachine.ChangeState(enemy.PursuitState);
            }
            else if (enemy.AttackState.CanPerform()) {
                stateMachine.ChangeState(enemy.AttackState);
            }

        }
    }

}
