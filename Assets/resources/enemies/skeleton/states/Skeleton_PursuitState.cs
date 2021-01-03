using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_PursuitState: PursuitState {
    private SkeletonAI enemy;

    public Skeleton_PursuitState(Enemy entity, EnemyStateMachine stateMachine, string animBoolName, EnemyData enemyData) : base(entity, stateMachine, animBoolName, enemyData) {
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
    }

    public override void Exit() {
        base.Exit();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
        if (!targetDetected) {
            stateMachine.ChangeState(enemy.IdleState);
        }
        else {
            if (distanceFromTarget > enemyData.attackRange) {
                entity.setVelocityX(entity.FacingDirection * enemyData.walkSpeed);
            }
            else {
                entity.setVelocityX(0);

                if (enemy.AttackState.CanPerform()) {
                    stateMachine.ChangeState(enemy.AttackState);
                }
                else {
                    stateMachine.ChangeState(enemy.TargetDetectedState);
                }
            }
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }
}
