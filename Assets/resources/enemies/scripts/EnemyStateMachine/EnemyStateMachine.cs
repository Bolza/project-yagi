using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine {
    private bool isFrozen;
    private bool debugMode;
    public EnemyState currentState { get; private set; }
    public void Initialize(EnemyState startingState) {
        currentState = startingState;
        currentState.Enter();
    }

    public void ChangeState(EnemyState newState) {
        if (isFrozen) return;
        if (debugMode) Debug.Log(currentState + " => " + newState);
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void FreezeState() => isFrozen = true;
    public void UnfreezeState() => isFrozen = false;

    public void DebugModeOn() => debugMode = true;
    public void DebugModeOff() => debugMode = false;
}
