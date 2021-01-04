
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursuitState: EnemyState {
    protected Transform currentTarget;
    public PursuitState(Enemy entity, EnemyStateMachine stateMachine, string animBoolName, EnemyData enemyData) : base(entity, stateMachine, animBoolName, enemyData) {
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
        UndesignateTarget();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }

    public void DesignateTarget(Transform target) {
        //currentTarget = target;
    }

    public void UndesignateTarget() {
        //currentTarget = null;
    }
}
