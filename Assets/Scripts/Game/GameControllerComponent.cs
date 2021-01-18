using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerComponent: MonoBehaviour {
    private GameObject playerObj;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private Enemy EnemyToSpawn;


    // Update is called once per frame
    void Awake() {
        playerObj = GameObject.FindWithTag("Player");
        spawnPoint = GameObject.FindWithTag("Respawn");
        Application.targetFrameRate = 30;
    }

    public void SpawnEnemy() {
        Instantiate(EnemyToSpawn, spawnPoint.transform.position, Quaternion.identity);
    }
}
