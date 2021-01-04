using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_MoveState: MoveState {
    private SkeletonAI enemy;

    public Skeleton_MoveState(Enemy entity, EnemyStateMachine stateMachine, string animBoolName, EnemyData enemyData) : base(entity, stateMachine, animBoolName, enemyData) {
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
        if (gotHit) {
            stateMachine.ChangeState(enemy.HitState);
        }
        else if (targetDetected) {
            stateMachine.ChangeState(enemy.TargetDetectedState);
        }
        else if (wallDetected || !groundDetected) {
            stateMachine.ChangeState(enemy.IdleState);
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }
}
