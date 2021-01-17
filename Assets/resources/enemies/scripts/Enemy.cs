using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Enemy: LivingEntity {
    public Rigidbody2D Body { get; private set; }
    public Animator Anim { get; private set; }
    public EnemyAnimationController ATSM { get; private set; }
    public EnemyStateMachine stateMachine { get; private set; }

    public bool blockStates;
    public EnemyData baseData;

    public bool wallDetected { get; protected set; }
    public bool targetDetectedForward;
    public bool targetDetectedBackward;
    public Collider2D targetDetected;
    public float distanceFromTarget { get; protected set; }


    private Vector2 workspace;

    public override void Start() {
        base.Start();
        Body = GetComponent<Rigidbody2D>();
        Anim = GetComponentInChildren<Animator>();
        ATSM = GetComponentInChildren<EnemyAnimationController>();
        if (!Anim) Debug.LogError("Animator required in children");
        if (!ATSM) Debug.LogError("EnemyAnimationController required in children");
        ATSM.OnAnimationStart += AnimationStartTrigger;
        ATSM.OnAnimationFinish += AnimationFinishTrigger;
        stateMachine = new EnemyStateMachine();
    }

    protected override void Update() {
        base.Update();
        stateMachine.currentState.LogicUpdate();
        if (debugMode) stateMachine.DebugModeOn();
        else if (!debugMode) stateMachine.DebugModeOff();
        if (blockStates) stateMachine.FreezeState();
        else if (!blockStates) stateMachine.UnfreezeState();

        wallDetected = CheckWall();
        targetDetected = CheckTarget();

        if (targetDetected) {
            float relativePos = targetDetected.transform.position.x - transform.position.x;
            distanceFromTarget = Vector2.Distance(targetDetected.transform.position, transform.position);
            targetDetectedForward = Math.Sign(relativePos) == FacingDirection;
            targetDetectedBackward = Math.Sign(relativePos) != FacingDirection;
        }
        else {
            targetDetectedForward = false;
            targetDetectedBackward = false;
        }
    }

    public virtual void FixedUpdate() {
        stateMachine.currentState.PhysicsUpdate();
    }

    public virtual void SetVelocityX(float x) {
        workspace.Set(x, Body.velocity.y);
        Body.velocity = workspace;
    }

    public virtual bool CheckWall() {
        Vector2 side = new Vector2(Collider.bounds.center.x + (Collider.bounds.extents.x * FacingDirection), Collider.bounds.center.y);
        bool hittin = Physics2D.Raycast(side, Vector2.right * FacingDirection, baseData.wallDetectionRange, getGroundMask());
        if (debugMode) Debug.DrawRay(side, Vector2.right * FacingDirection, hittin ? Color.red : Color.green);
        return hittin;
    }

    public virtual Collider2D CheckTarget() {
        Collider2D hittin = Physics2D.OverlapCircle(transform.position, baseData.targetDetectionRange, baseData.enemyMask);
        return hittin;
    }


    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, baseData.targetDetectionRange);
    }

    public void AnimationStartTrigger() => stateMachine.currentState.AnimationTrigger();
    public void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();


    #region Combat

    public override void GotHit(AttackType atk) {
        base.GotHit(atk);
        SetVelocityX(CalculateKnockback(atk));
    }

    public override void GotBlocked(AttackType atk) {
        SetVelocityX(CalculateKnockback(atk));
    }

    public override AttackType GenerateAttack() {
        return new AttackType(this, baseData.attackDamage, baseData.attackKnockback);
    }

    //override HitCurrentTGt to pass dmg?

    public override LayerMask getGroundMask() {
        return baseData.groundMask;
    }

    #endregion

}
