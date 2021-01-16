using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitState: PlayerState {

    public PlayerHitState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) {
    }


    public override void Enter() {
        base.Enter();
        player.InputHandler.MuteInput();
        float kbDirection = player.CalculateKnockback(player.lastHit);
        player.SetVelocityX(kbDirection);
    }

    public override void Exit() {
        base.Exit();
        player.InputHandler.UnmuteInput();

    }

    public override void LogicUpdate() {
        base.LogicUpdate();
        if (gotHit) {
            stateMachine.ChangeState(player.HitState);
        }
        else if (!duringAnimation) {
            stateMachine.ChangeState(player.IdleState);
        }
        else {
        }


    }
}
