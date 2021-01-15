using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState: EnemyState {

    public bool duringHitboxTime { get; private set; }
    protected float lastAttackTime = 0;

    protected Transform attackPosition;

    public AttackState(Enemy entity, EnemyStateMachine stateMachine, string animBoolName, EnemyData enemyData, Transform attackPosition) : base(entity, stateMachine, animBoolName, enemyData) {
        this.attackPosition = attackPosition;
    }

    public override void AnimationTrigger() {
        base.AnimationTrigger();
    }

    public override void AnimationFinishTrigger() {
        base.AnimationFinishTrigger();
    }

    public override void DoChecks() {
        base.DoChecks();
    }

    public override void Enter() {
        base.Enter();
        entity.ATSM.OnAttackStartHitbox += StartHitbox;
        entity.ATSM.OnAttackEndHitbox += EndHitbox;
    }

    public override void Exit() {
        base.Exit();
        entity.ATSM.OnAttackStartHitbox -= StartHitbox;
        entity.ATSM.OnAttackEndHitbox -= EndHitbox;
        lastAttackTime = Time.time;
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

    public virtual bool CanPerform() {
        bool recovered = Time.time >= lastAttackTime + baseData.attackSpeed;
        return recovered;
    }

}
