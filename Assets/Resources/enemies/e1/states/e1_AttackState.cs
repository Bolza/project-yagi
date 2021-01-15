using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class e1_AttackState: AttackState {
    private e1AI enemy;
    public e1_AttackState(Enemy entity, EnemyStateMachine stateMachine, string animBoolName, EnemyData enemyData, Transform attackPosition) : base(entity, stateMachine, animBoolName, enemyData, attackPosition) {
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
        else if (duringAnimation) {
            if (gotBlocked) {
                stateMachine.ChangeState(enemy.HitState);
            }
            else if (duringHitboxTime && enemy.hitpoint.currentHit) {
                enemy.HitCurrentTarget(enemy.GenerateAttack());
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
