using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;


[CreateAssetMenu]
public class SO_GameController: ScriptableObject {
    public SO_Stylesheet Stylesheet;
    public GameObject BlockSparks;
    public LayerMask groundLayer;

    public event Action onPlayerBlocked;
    public event Action onPlayerDodged;
    public event Action onPlayerHit;

    private void Awake() {
    }

    public void NotifyPlayerBlock(Player player) {
        onPlayerBlocked?.Invoke();
        Time.timeScale = 0.5f;
        FunctionTimer.Create(ResetTimescale, 0.4f);
    }

    public void NotifyPlayerDodged(Player player) {
        onPlayerDodged?.Invoke();
    }

    public void NotifyPlayerHit() {
        onPlayerHit?.Invoke();
    }

    public void ResetTimescale() {
        Time.timeScale = 1f;
    }
}