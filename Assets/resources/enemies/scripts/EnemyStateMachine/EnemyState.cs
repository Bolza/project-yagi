using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState {
    protected EnemyStateMachine stateMachine;
    protected Enemy entity;
    protected float startTime;
    protected string animBoolName;
    protected EnemyData enemyData;
    protected bool wallDetected;
    protected bool groundDetected;
    protected RaycastHit2D targetDetectedForward;
    protected RaycastHit2D targetDetectedBackward;
    protected RaycastHit2D targetDetected;
    protected float distanceFromTarget;
    protected bool duringAnimation;

    public EnemyState(Enemy entity, EnemyStateMachine stateMachine, string animBoolName, EnemyData enemyData) {
        this.entity = entity;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
        this.enemyData = enemyData;
    }

    public virtual void Enter() {
        startTime = Time.time;
        entity.Anim.SetBool(animBoolName, true);
        duringAnimation = true;
        entity.onGotBlocked += OnGotBlocked;
        entity.onGotHit += OnGotHit;
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
        wallDetected = entity.CheckWall();
        groundDetected = entity.CheckIsGrounded();
        targetDetectedForward = entity.CheckTargetForward();
        targetDetectedBackward = entity.CheckTargetBackward();
        targetDetected = targetDetectedForward ? targetDetectedForward : targetDetectedBackward;

        if (targetDetectedForward) distanceFromTarget = targetDetectedForward.distance;
        else if (targetDetectedBackward) distanceFromTarget = targetDetectedBackward.distance;
    }

    public virtual void AnimationTrigger() {
        duringAnimation = true;

    }
    public virtual void AnimationFinishTrigger() {
        duringAnimation = false;
    }

    public virtual void OnGotBlocked() { }

    public virtual void OnGotHit() { }


}
