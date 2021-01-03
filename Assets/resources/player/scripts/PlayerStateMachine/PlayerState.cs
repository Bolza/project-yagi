using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState {
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;
    private string animBoolName;
    protected float inputX;
    protected float startTime;
    protected bool isGrounded;
    protected bool isWalled;
    protected bool duringAnimation;
    protected bool isLedged;
    protected bool jumpInput;
    protected bool attackInput;
    protected bool blockInput;
    protected bool rollInput;
    protected bool headIsFree;
    protected bool gotHit;

    protected bool isExitingState { get; private set; }

    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
    }


    public virtual void Enter() {
        isExitingState = false;
        duringAnimation = true;
        player.Anim.SetBool(animBoolName, true);
        startTime = Time.time;
        player.onGotBlocked += OnGotBlocked;
        player.onGotHit += OnGotHit;
        gotHit = false;
        DoChecks();
    }

    public virtual void Exit() {
        player.onGotBlocked -= OnGotBlocked;
        player.onGotHit -= OnGotHit;
        isExitingState = true;
        duringAnimation = false;
        player.Anim.SetBool(animBoolName, false);
    }

    public virtual void LogicUpdate() {
        DoChecks();
    }

    public virtual void PhysicsUpdate() {
    }

    public virtual void DoChecks() {
        inputX = player.InputHandler.NormInputX;
        jumpInput = player.InputHandler.jump.hasInput;
        attackInput = player.InputHandler.attack.hasInput;
        blockInput = player.InputHandler.block.hasInput;
        rollInput = player.InputHandler.roll.hasInput;
        isGrounded = player.CheckIsGrounded();
        isWalled = player.CheckIsWalled();
        headIsFree = !player.CheckHeadIsWalled();
        bool ledgeRay = player.CheckIsTouchingLedge();
        isLedged = headIsFree && ledgeRay;
        if (isLedged) {
            player.LedgeClimbState.setDetectedPosition(player.transform.position);
        }
    }

    public virtual void AnimationTrigger() {
        duringAnimation = true;
    }

    public virtual void AnimationFinishTrigger() {
        duringAnimation = false;
    }


    public virtual void OnGotBlocked() { }

    public virtual void OnGotHit() {
        gotHit = true;
    }
}
