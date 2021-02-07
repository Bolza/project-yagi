using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SpawnManager : MonoBehaviour {
    private int _defaultSpawnIndex = 0;
    private Transform _playerTransformAnchor = default;

    [Header("Asset References")]
    [SerializeField] private Player _playerPrefab = default;
    [SerializeField] private PlayerEventsChannel playerEvents = default;
    [SerializeField] private SceneManagementEventsChannel sceneEvents = default; //Raised when the scene is loaded and set active

    [Header("Scene References")]
    private Transform[] _spawnLocations;

    private void OnEnable() {
        if (sceneEvents != null) {
            sceneEvents.OnSceneReady += SpawnPlayer;
        }
    }

    private void OnDisable() {
        if (sceneEvents != null) {
            sceneEvents.OnSceneReady -= SpawnPlayer;
        }
    }

    private void SpawnPlayer(Scene scene, PathSO path) {
        GameObject[] spawnLocationsGO = GameObject.FindGameObjectsWithTag("Respawn");
        _spawnLocations = new Transform[spawnLocationsGO.Length];
        for (int i = 0; i < spawnLocationsGO.Length; ++i) {
            _spawnLocations[i] = spawnLocationsGO[i].transform;
        }
        Spawn(FindSpawnIndex(path ?? null));
    }

    void Reset() {
        AutoFill();
    }

    /// <summary>
    /// This function tries to autofill some of the parameters of the component, so it's easy to drop in a new scene
    /// </summary>
    [ContextMenu("Attempt Auto Fill")]
    private void AutoFill() {
        if (_spawnLocations == null || _spawnLocations.Length == 0)
            _spawnLocations = transform.GetComponentsInChildren<Transform>(true)
                                .Where(t => t != this.transform)
                                .ToArray();
    }

    private void Spawn(int spawnIndex) {
        Transform spawnLocation = GetSpawnLocation(spawnIndex, _spawnLocations);
        Player playerInstance = InstantiatePlayer(_playerPrefab, spawnLocation);
        playerEvents.PlayerIstantiated(playerInstance, playerInstance.transform); // The CameraSystem will pick this up to frame the player
                                                                                  //_playerTransformAnchor.Transform = playerInstance.transform;

    }

    private Transform GetSpawnLocation(int index, Transform[] spawnLocations) {
        if (spawnLocations == null || spawnLocations.Length == 0)
            throw new Exception("No spawn locations set.");

        index = Mathf.Clamp(index, 0, spawnLocations.Length - 1);
        return spawnLocations[index];
    }

    private int FindSpawnIndex(PathSO pathTaken) {
        if (pathTaken == null)
            return _defaultSpawnIndex;

        int index = Array.FindIndex(_spawnLocations, element =>
            element?.GetComponent<LocationExitTrigger>()?.path == pathTaken
        );

        return (index < 0) ? _defaultSpawnIndex : index;
    }

    private Player InstantiatePlayer(Player playerPrefab, Transform spawnLocation) {
        if (playerPrefab == null)
            throw new Exception("Player Prefab can't be null.");

        Player playerInstance = Instantiate(playerPrefab, spawnLocation.position, spawnLocation.rotation);

        return playerInstance;
    }
}
