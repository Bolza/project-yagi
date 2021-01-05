﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRollState: PlayerAbilityState {

    public PlayerRollState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) {
    }

    public override void Enter() {
        base.Enter();
        colliderShouldFitAnimation = true;
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
        if (!isGrounded) {
            Debug.LogError("Not grounded during roll");
        }
        if (duringAnimation) {
            if (duringHitboxTime) {
                player.SetVelocityX(playerData.rollSpeed * player.FacingDirection);
            }
        }
        else {
            stateMachine.ChangeState(player.IdleState);
        }
    }
}
