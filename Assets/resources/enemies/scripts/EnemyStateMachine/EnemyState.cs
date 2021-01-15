﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState {
    protected EnemyStateMachine stateMachine;
    protected Enemy entity;
    protected float startTime;
    protected string animBoolName;
    protected EnemyData baseData;
    protected bool wallDetected;
    protected bool groundDetected;
    protected RaycastHit2D targetDetectedForward;
    protected RaycastHit2D targetDetectedBackward;
    protected RaycastHit2D targetDetected;
    protected float distanceFromTarget;
    protected bool duringAnimation;
    protected bool gotHit;
    protected bool gotBlocked;

    public EnemyState(Enemy entity, EnemyStateMachine stateMachine, string animBoolName, EnemyData enemyData) {
        this.entity = entity;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
        this.baseData = enemyData;
    }

    public virtual void Enter() {
        startTime = Time.time;
        entity.Anim.SetBool(animBoolName, true);
        duringAnimation = true;
        entity.onGotBlocked += OnGotBlocked;
        entity.onGotHit += OnGotHit;
        gotHit = false;
        gotBlocked = false;
    }

    public virtual void Exit() {
        entity.Anim.SetBool(animBoolName, false);
        duringAnimation = false;
        entity.onGotBlocked -= OnGotBlocked;
        entity.onGotHit -= OnGotHit;
    }

    public virtual void LogicUpdate() {
        DoChecks();
    }

    public virtual void PhysicsUpdate() {
    }

    public virtual void DoChecks() {
        wallDetected = entity.wallDetected;
        groundDetected = entity.isGrounded;
        targetDetectedForward = entity.targetDetectedForward;
        targetDetectedBackward = entity.targetDetectedBackward;

        targetDetected = targetDetectedForward ? targetDetectedForward : targetDetectedBackward;
        distanceFromTarget = targetDetected.distance;
    }

    public virtual void AnimationTrigger() {
        duringAnimation = true;

    }
    public virtual void AnimationFinishTrigger() {
        duringAnimation = false;
    }

    public virtual void OnGotBlocked() {
        gotBlocked = true;
    }

    public virtual void OnGotHit() {
        gotHit = true;
    }


}
