using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class e1_MoveState: MoveState {
    private e1AI enemy;
    private float distanceCovered;
    public e1_MoveState(Enemy entity, EnemyStateMachine stateMachine, string animBoolName, EnemyData enemyData) : base(entity, stateMachine, animBoolName, enemyData) {
        this.enemy = (e1AI)entity;
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
        else if (wallDetected || !groundDetected || distanceCovered >= baseData.patrolRange) {
            stateMachine.ChangeState(enemy.IdleState);
        }
    }

    public override void PhysicsUpdate() {
        distanceCovered = Vector2.Distance(enemy.IdleState.lastPosition, enemy.transform.position);
        base.PhysicsUpdate();
    }
}
