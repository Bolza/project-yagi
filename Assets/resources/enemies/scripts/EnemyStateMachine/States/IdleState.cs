﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState: EnemyState {
    private float idleTime;
    private bool flipAfterIdle;
    private bool idleIsExpired;
    private int counter = 0;

    public IdleState(Enemy entity, EnemyStateMachine stateMachine, string animBoolName, EnemyData enemyData) : base(entity, stateMachine, animBoolName, enemyData) {
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
        counter++;
        idleIsExpired = false;
        entity.setVelocityX(0f);
        idleTime = Random.Range(enemyData.minIdleTime, enemyData.maxIdleTime);
    }

    public override void Exit() {
        base.Exit();
        if (flipAfterIdle && counter > 1) entity.Flip();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
        if (Time.time >= startTime + idleTime) {
            idleIsExpired = true;
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }

    public void FlipAfterIdle() => flipAfterIdle = true;
    public void DontFlipAfterIdle() => flipAfterIdle = false;
    public bool IsIdleExpired() => idleIsExpired;
}