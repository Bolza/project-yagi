using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_HitState: HitState {
    private SkeletonAI enemy;

    public Skeleton_HitState(Enemy entity, EnemyStateMachine stateMachine, string animBoolName, EnemyData enemyData) : base(entity, stateMachine, animBoolName, enemyData) {
        this.enemy = (SkeletonAI)entity;
    }


    public override void LogicUpdate() {
        base.LogicUpdate();
        if (!duringAnimation) {
            stateMachine.ChangeState(enemy.StunState);
            //stateMachine.ChangeState(enemy.HitState);
        }
        //else if (!duringAnimation) {
        //    enemy.setVelocityX(0);
        //    stateMachine.ChangeState(enemy.IdleState);
        //}
    }

    public override void Enter() {
        base.Enter();
        enemy.setVelocityX(1f * -enemy.FacingDirection);
    }
}
