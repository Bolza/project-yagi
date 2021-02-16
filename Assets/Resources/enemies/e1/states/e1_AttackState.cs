using UnityEngine;

public class e1_AttackState : AttackState {
    private e1AI thisEntity;
    public e1_AttackState(Enemy entity, EnemyStateMachine stateMachine, string animBoolName, EnemyData enemyData, Transform attackPosition) : base(entity, stateMachine, animBoolName, enemyData, attackPosition) {
        this.thisEntity = (e1AI)entity;
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
        thisEntity.SetVelocityX(0);
    }

    public override void Exit() {
        base.Exit();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
        if (gotHit) {
            stateMachine.ChangeState(thisEntity.HitState);
        } else if (duringAnimation) {
            if (gotBlocked) {
                //stateMachine.ChangeState(enemy.HitState);
                stateMachine.ChangeState(thisEntity.IdleState);
            } else if (duringHitboxTime && thisEntity.TestTargetHit()) {
                thisEntity.ConfirmTargetHit(thisEntity.GenerateAttack());
                EndHitbox();
            }
        } else {
            EvaluateNextState();
        }
    }

    private void EvaluateNextState() {
        if (!targetDetected) {
            stateMachine.ChangeState(thisEntity.IdleState);
        } else {
            stateMachine.ChangeState(thisEntity.TargetDetectedState);
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }


}
