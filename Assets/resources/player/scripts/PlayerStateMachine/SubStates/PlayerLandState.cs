﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState: PlayerGroundedState {
    public PlayerLandState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) {
    }

    public override void DoChecks() {
        base.DoChecks();
    }

    public override void Enter() {
        base.Enter();
        colliderShouldFitAnimation = true;
    }

    public override void Exit() {
        base.Exit();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
        if (isExitingState) return;
        if (inputX != 0) {
            stateMachine.ChangeState(player.MoveState);
        }
        else if (!duringAnimation) {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }
}
