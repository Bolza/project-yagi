using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum FacingDirections {
    right = 1,
    left = -1
};

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Enemy: HittableEntity {
    public Rigidbody2D Body { get; private set; }
    public Collider2D Collider { get; private set; }
    public Animator Anim { get; private set; }
    public EnemyAnimationController ATSM { get; private set; }
    public int FacingDirection { get; private set; }
    public EnemyStateMachine stateMachine { get; private set; }
    [SerializeField] private FacingDirections animationIsFacing = new FacingDirections();
    [SerializeField] private FacingDirections startDirection = new FacingDirections();

    public bool debugMode;
    public bool blockStates;
    public EnemyData enemyData;

    private Vector2 workspace;

    public override void Start() {
        base.Start();
        FacingDirection = (int)animationIsFacing;
        if (startDirection != animationIsFacing) Flip();
        Body = GetComponent<Rigidbody2D>();
        Collider = GetComponent<Collider2D>();
        Anim = GetComponentInChildren<Animator>();
        ATSM = GetComponentInChildren<EnemyAnimationController>();
        if (!Anim) Debug.LogError("Animator required in children");
        if (!ATSM) Debug.LogError("EnemyAnimationController required in children");
        ATSM.OnAnimationStart += AnimationStartTrigger;
        ATSM.OnAnimationFinish += AnimationFinishTrigger;
        stateMachine = new EnemyStateMachine();
    }

    public virtual void Update() {
        stateMachine.currentState.LogicUpdate();
        if (debugMode) stateMachine.DebugModeOn();
        else if (!debugMode) stateMachine.DebugModeOff();
        if (blockStates) stateMachine.FreezeState();
        else if (!blockStates) stateMachine.UnfreezeState();
    }

    public virtual void FixedUpdate() {
        stateMachine.currentState.PhysicsUpdate();
    }

    public virtual void setVelocityX(float x) {
        workspace.Set(x, Body.velocity.y);
        Body.velocity = workspace;
    }

    public virtual bool CheckWall() {
        Vector2 side = new Vector2(Collider.bounds.center.x + (Collider.bounds.extents.x * FacingDirection), Collider.bounds.center.y);
        bool hittin = Physics2D.Raycast(side, Vector2.right * FacingDirection, enemyData.wallDetectionRange, enemyData.groundMask);
        if (debugMode) Debug.DrawRay(side, Vector2.right * FacingDirection, hittin ? Color.red : Color.green);
        return hittin;
    }

    public virtual bool CheckIsGrounded() {
        Vector2 bottom = new Vector2(Collider.bounds.center.x, Collider.bounds.center.y - Collider.bounds.extents.y);
        bool hittin = Physics2D.Raycast(bottom, Vector2.down, .5f, enemyData.groundMask);
        if (debugMode) Debug.DrawRay(bottom, Vector2.down, hittin ? Color.red : Color.green);
        return hittin;
    }

    public virtual RaycastHit2D CheckTargetForward() {
        Vector2 forward = new Vector2(Collider.bounds.center.x + (Collider.bounds.extents.x * FacingDirection), Collider.bounds.center.y);
        RaycastHit2D hittin = Physics2D.Raycast(forward, Vector2.right * FacingDirection, enemyData.targetDetectionRange, enemyData.playerMask);
        if (debugMode) Debug.DrawRay(forward, Vector2.right * FacingDirection, hittin ? Color.red : Color.green);
        return hittin;
    }

    public virtual RaycastHit2D CheckTargetBackward() {
        Vector2 back = new Vector2(Collider.bounds.center.x + (Collider.bounds.extents.x * FacingDirection), Collider.bounds.center.y);
        RaycastHit2D hittin = Physics2D.Raycast(back, Vector2.right * -FacingDirection, enemyData.targetDetectionRange / 2, enemyData.playerMask);
        if (debugMode) Debug.DrawRay(back, Vector2.right * -FacingDirection, hittin ? Color.red : Color.green);
        return hittin;
    }

    private void OnDrawGizmos() {
        //Vector2 side = new Vector2(Collider.bounds.center.x + (Collider.bounds.extents.x * FacingDirection), Collider.bounds.center.y);
        //Gizmos.DrawLine()
    }

    public void Flip() {
        FacingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }

    public void AnimationStartTrigger() => stateMachine.currentState.AnimationTrigger();
    public void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();


    #region Combat

    //override HitCurrentTGt to pass dmg?

    #endregion

}
