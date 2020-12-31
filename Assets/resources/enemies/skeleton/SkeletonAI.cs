using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAI: Enemy {
    public Skeleton_IdleState IdleState { get; private set; }
    public Skeleton_MoveState MoveState { get; private set; }
    public Skeleton_TargetDetectedState TargetDetectedState { get; private set; }
    public Skeleton_AttackState AttackState { get; private set; }


    public override void Start() {
        base.Start();
        IdleState = new Skeleton_IdleState(this, stateMachine, "idle", enemyData);
        MoveState = new Skeleton_MoveState(this, stateMachine, "move", enemyData);
        TargetDetectedState = new Skeleton_TargetDetectedState(this, stateMachine, "move", enemyData);
        AttackState = new Skeleton_AttackState(this, stateMachine, "attack", enemyData, transform);
        stateMachine.Initialize(IdleState);
    }

}




