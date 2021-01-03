using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInputHandler))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(MeshColorer3D))]

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
    public PlayerAttackState AttackState { get; private set; }
    public PlayerBlockState BlockState { get; private set; }
    public PlayerHitState HitState { get; private set; }
    public PlayerRollState RollState { get; private set; }

    [SerializeField] private PlayerData playerData;
    [SerializeField] private SO_GameController gameController;
    #endregion

    public Animator Anim { get; private set; }
    public Rigidbody2D Body { get; private set; }
    public Collider2D Collider { get; private set; }
    public AnimationController ATSM { get; private set; }
    public CharacterController2D CC { get; private set; }
    public Hitpoint hitpoint { get; private set; }
    public MeshColorer3D meshColorer { get; private set; }


    public Vector2 CurrentVelocity;
    public int FacingDirection { get; private set; }
    private bool freezeMovement;

    [SerializeField] bool debugMode;
    [SerializeField] bool ColorMe;

    private GameObject weaponpoint;
    private BoxCollider2D BoxCollider;
    private Vector2 BoxDefaultSize;
    [SerializeField] private Transform leftFoot;
    [SerializeField] private Transform rightFoot;
    [SerializeField] private Transform head;
    public float climbWhyPos = 1.4f;

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
        AttackState = new PlayerAttackState(this, StateMachine, playerData, "slash");
        BlockState = new PlayerBlockState(this, StateMachine, playerData, "block");
        HitState = new PlayerHitState(this, StateMachine, playerData, "hit");
        RollState = new PlayerRollState(this, StateMachine, playerData, "roll");
    }

    private void Start() {
        InputHandler = GetComponent<PlayerInputHandler>();
        CC = GetComponent<CharacterController2D>();
        Collider = GetComponent<Collider2D>();
        meshColorer = GetComponent<MeshColorer3D>();
        BoxCollider = (BoxCollider2D)Collider;

        Anim = GetComponentInChildren<Animator>();
        Body = GetComponentInChildren<Rigidbody2D>();
        ATSM = GetComponentInChildren<AnimationController>();
        hitpoint = GetComponentInChildren<Hitpoint>();

        weaponpoint = GameObject.FindGameObjectWithTag("weaponpoint");

        if (!Anim) Debug.LogError("Animator required in children");
        if (!hitpoint) Debug.LogError("Hitpoint required in children");
        if (!ATSM) Debug.LogError("EnemyAnimationController required in children");
        ATSM.OnAnimationStart += AnimationStartTrigger;
        ATSM.OnAnimationFinish += AnimationFinishTrigger;

        StateMachine.Initialize(IdleState);
        FacingDirection = 1;
        BoxDefaultSize = BoxCollider.size;
    }

    public Vector2 getRenderedPosition() {
        Vector3 newpos = head.TransformPoint(Vector3.zero);
        return new Vector2(
            newpos.x,
            newpos.y - BoxCollider.bounds.extents.y
        );
    }

    private void Update() {
        float lowerBound = Mathf.Min(leftFoot.position.y, rightFoot.position.y);
        float upperBound = Mathf.Max(head.position.y, lowerBound);
        float newY = Math.Max(upperBound - lowerBound, 0.1f) / this.transform.localScale.y;
        BoxCollider.size = new Vector2(BoxDefaultSize.x, newY);


        StateMachine.CurrentState.LogicUpdate();
        if (freezeMovement) return;
        float gravity = CheckIsGrounded() ? 0 : playerData.gravity;
        float yPlusGravity = CurrentVelocity.y + gravity * Time.deltaTime;
        CurrentVelocity.Set(CurrentVelocity.x, yPlusGravity);
        CC.move(CurrentVelocity * Time.deltaTime);

    }

    private void FixedUpdate() {
        StateMachine.CurrentState.PhysicsUpdate();

        if (debugMode) StateMachine.DebugModeOn();
        else StateMachine.DebugModeOff();

        //if (ColorMe) {
        //    if (meshColorer)
        //        meshColorer.SetTintColor(gameController.Stylesheet.playerHitOverlay);
        //    ColorMe = false;
        //}

    }

    #region Movement

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

    public void UnfreezeMovement() {
        freezeMovement = false;
    }

    public void SetPosition(Vector2 pos) {
        transform.position = pos;
    }

    private void Flip() {
        FacingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }

    #endregion

    #region Checks

    private Vector2 getRayDistance() {
        return Vector2.right * FacingDirection * CC.skinWidth * 2;
    }

    public bool CheckIsWalled() {
        Vector2 side = new Vector2(
            Collider.bounds.center.x + (Collider.bounds.extents.x * FacingDirection) - (CC.skinWidth * FacingDirection),
            Collider.bounds.center.y);
        bool hittin = Physics2D.Raycast(side, getRayDistance(), CC.platformMask);

        if (debugMode) Debug.DrawRay(side, getRayDistance(), hittin ? Color.red : Color.green);
        return hittin;
    }

    public bool CheckIsGrounded() {
        Vector2 side = new Vector2(
            Collider.bounds.center.x + (Collider.bounds.extents.x * FacingDirection) - (CC.skinWidth * FacingDirection),
            Collider.bounds.center.y - Collider.bounds.extents.y);
        bool hittin = Physics2D.Raycast(side, Vector2.down, CC.skinWidth * 2, playerData.groundLayer);
        if (debugMode) Debug.DrawRay(side, Vector2.down * CC.skinWidth * 2, hittin ? Color.cyan : Color.green);
        return hittin;
    }

    public void CheckIfShouldFlip(float xInput) {
        if (xInput != 0 && xInput != FacingDirection) Flip();
    }

    public bool CheckIsTouchingLedge() {
        Vector2 side = new Vector2(
            Collider.bounds.center.x + (Collider.bounds.extents.x * FacingDirection) - (CC.skinWidth * FacingDirection),
            Collider.bounds.center.y + (Collider.bounds.extents.y / 3 * 2));
        bool hittin = Physics2D.Raycast(side, getRayDistance(), CC.platformMask);

        if (debugMode) Debug.DrawRay(side, getRayDistance(), hittin ? Color.red : Color.green);
        return hittin;
    }

    public bool CheckHeadIsWalled() {
        Vector2 side = new Vector2(
            Collider.bounds.center.x + (Collider.bounds.extents.x * FacingDirection) - (CC.skinWidth * FacingDirection),
            Collider.bounds.center.y + Collider.bounds.extents.y);
        bool hittin = Physics2D.Raycast(side, getRayDistance(), CC.platformMask);

        if (debugMode) Debug.DrawRay(side, getRayDistance(), hittin ? Color.red : Color.green);
        return hittin;
    }

    public Vector2 getCornerPosition() {
        // OR THIS https://youtu.be/0OE-jSDRIok?list=PLy78FINcVmjA0zDBhLuLNL1Jo6xNMMq-W&t=983
        return new Vector2(
           Collider.bounds.center.x + (Collider.bounds.extents.x * FacingDirection) - (CC.skinWidth * FacingDirection),
           Collider.bounds.center.y + Collider.bounds.extents.y / 2);

    }

    #endregion

    #region Events

    public void AnimationStartTrigger() => StateMachine.CurrentState.AnimationTrigger();
    public void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
    public event Action onGotHit;
    public event Action onGotBlocked;

    #endregion

    #region Combat

    public void GotHit(Enemy enemy, int dmg) {
        if (BlockState.duringHitboxTime) {
            enemy.GotBlocked(this, dmg);
            gameController.NotifyPlayerBlock(this);
            Vector2 pos = new Vector2(weaponpoint.transform.position.x, weaponpoint.transform.position.y);
            Instantiate(gameController.BlockSparks, pos, Quaternion.identity); ;
        }
        else if (RollState.duringHitboxTime) {
            gameController.NotifyPlayerDodged(this);
        }
        else {
            gameController.NotifyPlayerHit();
            onGotHit?.Invoke();
            meshColorer.SetTintColor(gameController.Stylesheet.playerHitOverlay);
        }
    }

    #endregion
}
