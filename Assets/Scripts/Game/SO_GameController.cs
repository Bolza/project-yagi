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

    [SerializeField] private PlayerEventsChannel playerEvents;

    private void Awake() {
        playerEvents.OnPlayerBlocked += onPlayerBlock;
    }

    public void onPlayerBlock(HittableEntity owner, AttackType atk) {
        Time.timeScale = 0.5f;
        FunctionTimer.Create(ResetTimescale, 0.4f);
    }

    public void ResetTimescale() {
        Time.timeScale = 1f;
    }
}