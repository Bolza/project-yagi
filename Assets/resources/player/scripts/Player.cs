using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: MonoBehaviour {
    #region States
    public PlayerInputHandler InputHandler { get; private set; }
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallGrabState WallGrabState { get; private set; }
    public PlayerWallClimbState WallClimbState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerLedgeClimbState LedgeClimbState { get; private set; }

    [SerializeField] private PlayerData playerData;
    #endregion

    public Animator Anim { get; private set; }
    public Rigidbody2D Body { get; private set; }
    public Collider2D Collider { get; private set; }

    public CharacterController2D CC { get; private set; }

    public Vector2 CurrentVelocity;
    public int FacingDirection { get; private set; }
    private bool freezeMovement;

    [SerializeField] bool debugMode;

    private BoxCollider2D BoxCollider;
    private Vector2 BoxDefaultSize;
    [SerializeField] private Transform leftFoot;
    [SerializeField] private Transform rightFoot;
    [SerializeField] private Transform head;
    [SerializeField] private GameObject model;
    private SkinnedMeshRenderer mesh;
    public float climbWhyPos = 1.4f;
    public float fastestFallVel = 0f;

    private void Awake() {
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallSlide");
        WallGrabState = new PlayerWallGrabState(this, StateMachine, playerData, "wallGrab");
        WallClimbState = new PlayerWallClimbState(this, StateMachine, playerData, "wallClimb");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, "inAirn");
        LedgeClimbState = new PlayerLedgeClimbState(this, StateMachine, playerData, "ledgeClimbState");
    }

    private void Start() {
        Anim = GetComponentInChildren<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        Body = GetComponentInChildren<Rigidbody2D>();
        CC = GetComponent<CharacterController2D>();
        Collider = GetComponent<Collider2D>();
        BoxCollider = GetComponent<BoxCollider2D>();
        StateMachine.Initialize(IdleState);
        FacingDirection = 1;
        BoxDefaultSize = BoxCollider.size;
        mesh = model.GetComponent<SkinnedMeshRenderer>();
    }


    private void Update() {
        //float newY = mesh.bounds.extents.y;
        float newY = Mathf.Abs(head.position.y - Mathf.Min(leftFoot.position.y, rightFoot.position.y)) / this.transform.localScale.y;
        BoxCollider.size = new Vector2(BoxDefaultSize.x, newY);

        StateMachine.CurrentState.LogicUpdate();
        if (freezeMovement) return;
        float gravity = CheckIsGrounded() ? 0 : playerData.gravity;
        float yPlusGravity = CurrentVelocity.y + gravity * Time.deltaTime;
        fastestFallVel = Mathf.Min(yPlusGravity, fastestFallVel);
        CurrentVelocity.Set(CurrentVelocity.x, yPlusGravity);
        CC.move(CurrentVelocity * Time.deltaTime);
    }


    private void FixedUpdate() {
        StateMachine.CurrentState.PhysicsUpdate();

    }

    public void SetVelocityX(float velocity) {
        freezeMovement = false;
        CurrentVelocity.Set((float)Math.Floor(velocity), CurrentVelocity.y);
    }

    public void SetVelocityY(float velocity) {
        freezeMovement = false;
        CurrentVelocity.Set(CurrentVelocity.x, velocity);
        //Debug.Log(velocity + " / " + CurrentVelocity.y + " / " + InputHandler.NormInputY);
    }

    public void SetVelocity(float v, Vector2 angle, int dir) {
        freezeMovement = false;
        angle.Normalize();
        CurrentVelocity.Set(angle.x * v * dir, angle.y * v);
    }

    public void FreezeMovement() {
        CurrentVelocity.Set(0, 0);
        freezeMovement = true;
    }

    public void SetPosition(Vector2 pos) {
        transform.position = pos;
    }

    private void Flip() {
        FacingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }

    public bool CheckIsWalled() {
        Vector2 side = new Vector2(
            Collider.bounds.center.x + (Collider.bounds.extents.x * FacingDirection) - (CC.skinWidth * FacingDirection),
            Collider.bounds.center.y);
        bool hittin = Physics2D.Raycast(side, Vector2.right * FacingDirection, CC.skinWidth * 2, CC.platformMask);

        if (debugMode) Debug.DrawRay(side, Vector2.right * FacingDirection, hittin ? Color.red : Color.green);
        return hittin;
    }

    public bool CheckIsGrounded() {
        return CC.isGrounded;
    }

    public void CheckIfShouldFlip(float xInput) {
        if (xInput != 0 && xInput != FacingDirection) Flip();
    }

    public bool CheckIsTouchingLedge() {
        Vector2 side = new Vector2(
            Collider.bounds.center.x + (Collider.bounds.extents.x * FacingDirection) - (CC.skinWidth * FacingDirection),
            Collider.bounds.center.y + (Collider.bounds.extents.y / 3 * 2));
        bool hittin = Physics2D.Raycast(side, Vector2.right * FacingDirection, CC.skinWidth * 2, CC.platformMask);

        if (debugMode) Debug.DrawRay(side, Vector2.right * FacingDirection, hittin ? Color.red : Color.green);
        return hittin;
    }

    public bool CheckHeadIsWalled() {
        Vector2 side = new Vector2(
            Collider.bounds.center.x + (Collider.bounds.extents.x * FacingDirection) - (CC.skinWidth * FacingDirection),
            Collider.bounds.center.y + Collider.bounds.extents.y);
        bool hittin = Physics2D.Raycast(side, Vector2.right * FacingDirection, CC.skinWidth * 2, CC.platformMask);

        if (debugMode) Debug.DrawRay(side, Vector2.right * FacingDirection, hittin ? Color.red : Color.green);
        return hittin;
    }

    public Vector2 getCornerPosition() {
        // OR THIS https://youtu.be/0OE-jSDRIok?list=PLy78FINcVmjA0zDBhLuLNL1Jo6xNMMq-W&t=983
        return new Vector2(
           Collider.bounds.center.x + (Collider.bounds.extents.x * FacingDirection) - (CC.skinWidth * FacingDirection),
           Collider.bounds.center.y + Collider.bounds.extents.y / 2);

    }

    public void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
    public void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
}
