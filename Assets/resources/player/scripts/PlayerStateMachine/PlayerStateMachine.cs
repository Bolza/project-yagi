using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine {
    private bool isFrozen;
    private bool debugMode;
    public PlayerState CurrentState { get; private set; }

    public void Initialize(PlayerState startingState) {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(PlayerState newState) {
        if (debugMode) Debug.Log(CurrentState + " => " + newState);
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }


    public void FreezeState() => isFrozen = true;
    public void UnfreezeState() => isFrozen = false;

    public void DebugModeOn() => debugMode = true;
    public void DebugModeOff() => debugMode = false;
}
