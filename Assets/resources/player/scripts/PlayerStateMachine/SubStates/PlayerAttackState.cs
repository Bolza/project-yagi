﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState: PlayerAbilityState {

    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) {
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
        if (duringAnimation) {
            if (duringHitboxTime && player.hitpoint.currentHit) {
                player.hitpoint.currentHit.gameObject.GetComponent<Enemy>().GotHit(player, playerData.attackDamage);
                EndHitbox();
            }
        }
        else {
            stateMachine.ChangeState(player.IdleState);
        }
    }

}
