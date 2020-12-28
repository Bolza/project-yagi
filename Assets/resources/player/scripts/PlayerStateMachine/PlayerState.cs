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
    protected bool isAnimationFinished;
    protected bool isLedged;
    protected bool jumpInput;
    protected bool headIsFree;
    protected bool isExitingState { get; private set; }

    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter() {
        isExitingState = false;
        DoChecks();
        player.Anim.SetBool(animBoolName, true);
        startTime = Time.time;
        isAnimationFinished = false;
    }

    public virtual void Exit() {
        isExitingState = true;
        player.Anim.SetBool(animBoolName, false);
    }

    public virtual void LogicUpdate() { }

    public virtual void PhysicsUpdate() {
        DoChecks();
    }

    public virtual void DoChecks() {
        inputX = player.InputHandler.NormInputX;
        jumpInput = player.InputHandler.JumpInput;
        isGrounded = player.CheckIsGrounded();
        isWalled = player.CheckIsWalled();
        headIsFree = !player.CheckHeadIsWalled();
        bool ledgeRay = player.CheckIsTouchingLedge();
        isLedged = headIsFree && ledgeRay;
        if (isLedged) {
            player.LedgeClimbState.setDetectedPosition(player.transform.position);
        }

    }

    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishTrigger() {
        isAnimationFinished = true;
    }
}
