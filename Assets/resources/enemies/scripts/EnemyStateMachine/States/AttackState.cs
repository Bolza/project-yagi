using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState: EnemyState {
    protected bool animationHasFinished { get; private set; }
    protected bool duringHitboxTime { get; private set; }
    protected Transform attackPosition;


    public AttackState(Enemy entity, EnemyStateMachine stateMachine, string animBoolName, EnemyData enemyData, Transform attackPosition) : base(entity, stateMachine, animBoolName, enemyData) {
        this.attackPosition = attackPosition;
    }
    public override void AnimationFinishTrigger() {
        base.AnimationFinishTrigger();
        animationHasFinished = true;
    }

    public override void AnimationTrigger() {
        base.AnimationTrigger();
        animationHasFinished = false;
    }

    public override void DoChecks() {
        base.DoChecks();
    }

    public override void Enter() {
        base.Enter();
        entity.ATSM.OnAttackStartHitbox += StartHitbox;
        entity.ATSM.OnAttackEndHitbox += EndHitbox;
        animationHasFinished = false;
    }

    public override void Exit() {
        base.Exit();
        entity.ATSM.OnAttackStartHitbox -= StartHitbox;
        entity.ATSM.OnAttackEndHitbox -= EndHitbox;
        animationHasFinished = true;
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }

    public virtual void StartHitbox() {
        duringHitboxTime = true;
    }
    public virtual void EndHitbox() {
        duringHitboxTime = false;
    }


}
