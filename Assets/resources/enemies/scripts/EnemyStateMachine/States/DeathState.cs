using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState: EnemyState {
    public DeathState(Enemy entity, EnemyStateMachine stateMachine, string animBoolName, EnemyData enemyData) : base(entity, stateMachine, animBoolName, enemyData) {
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
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }
}
