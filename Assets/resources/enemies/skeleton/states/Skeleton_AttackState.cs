using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_AttackState: AttackState {
    private SkeletonAI enemy;

    public Skeleton_AttackState(Enemy entity, EnemyStateMachine stateMachine, string animBoolName, EnemyData enemyData, Transform attackPosition) : base(entity, stateMachine, animBoolName, enemyData, attackPosition) {
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
        if (!animationHasFinished) {
            if (duringHitboxTime && enemy.hitpoint.currentHit) {
                enemy.hitpoint.currentHit.gameObject.GetComponent<Player>().GotHit(enemyData.attackDamage);
                EndHitbox();
            }
        }
        else {
            EvaluateNextState();
        }

    }

    private void EvaluateNextState() {
        if (!targetDetected) {
            stateMachine.ChangeState(enemy.IdleState);
        }
        else {
            stateMachine.ChangeState(enemy.TargetDetectedState);
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }
}
