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

    [SerializeField] private PlayerData playerData;
    #endregion

    public Animator Anim { get; private set; }
    public Rigidbody2D Body { get; private set; }
    public Collider2D Collider { get; private set; }
    public GroundTrigger GroundCheck { get; private set; }
    [SerializeField] public Transform wallCheck;
    [SerializeField] public LayerMask groundLayer;

    public CharacterController2D CC;
    public Vector2 CurrentVelocity;
    private Vector2 workspace;
    public int FacingDirection { get; private set; }
    private bool isGrounded = false;

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
    }

    private void Start() {
        Anim = GetComponentInChildren<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        Body = GetComponentInChildren<Rigidbody2D>();
        Collider = GetComponent<Collider2D>();
        CC = GetComponent<CharacterController2D>();
        GroundCheck = GetComponentInChildren<CircleCollider2D>().GetComponent<GroundTrigger>();

        StateMachine.Initialize(IdleState);
        FacingDirection = 1;
        //GroundCheck.OnGroundEnter += OnGroundEnter;
    }

    private void Update() {
        StateMachine.CurrentState.LogicUpdate();
        float yPlusGravity = CurrentVelocity.y + playerData.gravity * Time.deltaTime;
        CurrentVelocity.Set(CurrentVelocity.x, yPlusGravity);
        CC.move(CurrentVelocity * Time.deltaTime);
    }

    private void FixedUpdate() {
        StateMachine.CurrentState.PhysicsUpdate();


    }

    public void SetVelocityX(float velocity) {
        CurrentVelocity.Set((float)Math.Floor(velocity), CurrentVelocity.y);
    }

    public void SetVelocityY(float velocity) {
        CurrentVelocity.Set(CurrentVelocity.x, velocity);
        //Debug.Log(velocity + " / " + CurrentVelocity.y + " / " + InputHandler.NormInputY);
    }

    private void Flip() {
        FacingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }

    public bool CheckIsWalled() {
        Vector2 v = new Vector2(wallCheck.position.x + (0.2f * FacingDirection), wallCheck.position.y);
        //Debug.DrawLine(wallCheck.position, v, Color.red, 0.0f);
        return Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, 0.2f, groundLayer);
    }

    public bool CheckIsGrounded() {
        return CC.isGrounded;
    }

    //private void OnGroundEnter(object sender, EventArgs e) {
    //    isGrounded = true;
    //    GroundCheck.OnGroundEnter -= OnGroundEnter;
    //    GroundCheck.OnGroundExit += OnGroundExit;
    //}

    //private void OnGroundExit(object sender, EventArgs e) {
    //    isGrounded = false;
    //    GroundCheck.OnGroundEnter += OnGroundEnter;
    //    GroundCheck.OnGroundExit -= OnGroundExit;
    //}

    public void CheckIfShouldFlip(float xInput) {
        if (xInput != 0 && xInput != FacingDirection) Flip();
    }

    public void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
    public void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
}
