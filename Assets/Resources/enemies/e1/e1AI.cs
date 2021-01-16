using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class e1AI: Enemy {
    public e1_IdleState IdleState { get; private set; }
    public e1_MoveState MoveState { get; private set; }
    public e1_TargetDetectedState TargetDetectedState { get; private set; }
    public e1_AttackState AttackState { get; private set; }
    public e1_PursuitState PursuitState { get; private set; }
    public e1_HitState HitState { get; private set; }
    public e1_StunState StunState { get; private set; }


    public override void Start() {
        base.Start();
        IdleState = new e1_IdleState(this, stateMachine, "idle", baseData);
        MoveState = new e1_MoveState(this, stateMachine, "move", baseData);
        TargetDetectedState = new e1_TargetDetectedState(this, stateMachine, "idle", baseData);
        AttackState = new e1_AttackState(this, stateMachine, "attack", baseData, transform);
        PursuitState = new e1_PursuitState(this, stateMachine, "move", baseData);
        HitState = new e1_HitState(this, stateMachine, "hit", baseData);
        StunState = new e1_StunState(this, stateMachine, "stun", baseData);
        stateMachine.Initialize(IdleState);
    }



}




