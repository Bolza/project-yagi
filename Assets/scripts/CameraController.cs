using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraController: MonoBehaviour {
    private CinemachineVirtualCamera cam;
    private CinemachineBasicMultiChannelPerlin cameraNoise;

    private float shakeTime;

    [SerializeField] private SO_GameController gameController;

    private void Start() {
        cam = GetComponent<CinemachineVirtualCamera>();
        cameraNoise = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    void Awake() {
        gameController.onPlayerBlocked += onPlayerBlocked;
    }

    private void Update() {
        if (shakeTime > 0) {
            shakeTime -= Time.deltaTime;
            if (cam && cameraNoise) {
                cameraNoise.m_AmplitudeGain = shakeTime;
                cameraNoise.m_FrequencyGain = 0.1f;
            }
        }

    }

    private void onPlayerBlocked() {
        shakeTime = 1f;
    }

}
