using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState {
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;
    private string animBoolName;
    protected float startTime;
    protected bool isGrounded;
    protected bool isWalled;
    protected bool isAnimationFinished;
    protected float inputX;
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
        //isExitingState = true;
        player.Anim.SetBool(animBoolName, false);
    }

    public virtual void LogicUpdate() { }

    public virtual void PhysicsUpdate() {
        DoChecks();
    }

    public virtual void DoChecks() {
        isGrounded = player.CheckIsGrounded();
        isWalled = player.CheckIsWalled();
        inputX = player.InputHandler.NormInputX;
    }

    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishTrigger() {
        isAnimationFinished = true;
    }
}
