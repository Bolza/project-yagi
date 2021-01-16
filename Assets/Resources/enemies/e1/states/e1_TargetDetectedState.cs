using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class e1_TargetDetectedState: TargetDetectedState {
    private e1AI enemy;

    public e1_TargetDetectedState(Enemy entity, EnemyStateMachine stateMachine, string animBoolName, EnemyData enemyData) : base(entity, stateMachine, animBoolName, enemyData) {
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
        enemy.SetVelocityX(0);
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
        else if (targetDetected && Time.time >= startTime + baseData.targetDetectionTime) {
            //Debug.Log("Backward: " + targetDetectedBackward + ", Forward: " + targetDetectedForward + ", distance: " + distanceFromTarget);
            if (targetDetectedBackward) entity.Flip();
            if (distanceFromTarget > baseData.attackRange) {
                stateMachine.ChangeState(enemy.PursuitState);
            }
            else if (enemy.AttackState.CanPerform()) {
                stateMachine.ChangeState(enemy.AttackState);
            }

        }
    }

}
