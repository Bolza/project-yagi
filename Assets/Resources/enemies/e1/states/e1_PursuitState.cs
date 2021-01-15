using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class e1_PursuitState: PursuitState {
    private e1AI enemy;

    public e1_PursuitState(Enemy entity, EnemyStateMachine stateMachine, string animBoolName, EnemyData enemyData) : base(entity, stateMachine, animBoolName, enemyData) {
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
        else if (!targetDetected) {
            stateMachine.ChangeState(enemy.IdleState);
        }
        else {
            if (distanceFromTarget > baseData.attackRange) {
                entity.SetVelocityX(entity.FacingDirection * baseData.walkSpeed);
            }
            else {
                entity.SetVelocityX(0);

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
