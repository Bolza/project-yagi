using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockState: PlayerAbilityState {

    public PlayerBlockState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) {
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
        if (!duringAnimation) {
            stateMachine.ChangeState(player.IdleState);
        }
    }

}
