using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_IdleState: IdleState {
    private SkeletonAI enemy;

    public Skeleton_IdleState(Enemy entity, EnemyStateMachine stateMachine, string animBoolName, EnemyData enemyData) : base(entity, stateMachine, animBoolName, enemyData) {
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
        if (targetDetected) {
            DontFlipAfterIdle();
            stateMachine.ChangeState(enemy.TargetDetectedState);
        }
        else if (IsIdleExpired()) {
            FlipAfterIdle();
            stateMachine.ChangeState(enemy.MoveState);
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }
}
