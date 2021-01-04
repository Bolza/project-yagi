using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState: PlayerState {
    protected bool isAbilityDone;
    public bool duringHitboxTime { get; private set; }
    protected float endedTime = 0;
    public PlayerAbilityState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) {
    }

    public override void AnimationTrigger() {
        base.AnimationTrigger();
    }

    public override void AnimationFinishTrigger() {
        base.AnimationFinishTrigger();
    }

    public override void DoChecks() {
        base.DoChecks();
    }

    public override void Enter() {
        base.Enter();
        player.ATSM.OnAttackStartHitbox += StartHitbox;
        player.ATSM.OnAttackEndHitbox += EndHitbox;
        isAbilityDone = true;
    }

    public override void Exit() {
        base.Exit();
        player.ATSM.OnAttackStartHitbox -= StartHitbox;
        player.ATSM.OnAttackEndHitbox -= EndHitbox;
        endedTime = Time.time;
        isAbilityDone = false;
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }

    public virtual void StartHitbox() {
        duringHitboxTime = true;
    }

    public virtual void EndHitbox() {
        duringHitboxTime = false;
    }

    public virtual bool CanPerform() {
        return duringAnimation || !isAbilityDone;
    }
}
