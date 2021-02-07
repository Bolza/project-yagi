using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class manages the scene loading and unloading.
/// </summary>
public class SceneLoader : MonoBehaviour {
    [Header("Persistent Scenes")]
    [SerializeField] private ManagerSceneSO persistentScenes = default;

    [Header("Load Events")]
    [SerializeField] private SceneManagementEventsChannel eventsChannel = default;

    private string sceneToActivate = "";
    private Dictionary<string, SceneRecord> record = new Dictionary<string, SceneRecord>();
    private List<AsyncOperation> scenesToLoadAsyncOperations = new List<AsyncOperation>();
    private PathSO lastPathTaken;

    private void OnEnable() {
        if (eventsChannel != null) {
            eventsChannel.OnLoadingRequested += LoadLocation;
        }
    }

    private void OnDisable() {
        if (eventsChannel != null) {
            eventsChannel.OnLoadingRequested -= LoadLocation;
        }
    }

    private void Awake() {
        for (int i = 0; i < SceneManager.sceneCount; ++i) {
            Scene sc = SceneManager.GetSceneAt(i);
            record.Add(sc.name, new SceneRecord(sc.name, sc.path));
            record[sc.name].Loaded();
        }
    }

    /// <summary>
    /// This function loads the location scenes passed as array parameter 
    /// </summary>
    private void LoadLocation(GameSceneSO[] scenesToLoad, PathSO path, bool showLoadingScreen) {
        GameSceneSO locationScene = GlobalUtils.GetLocationScene(scenesToLoad);
        lastPathTaken = path;

        if (!record.ContainsKey(persistentScenes.name)) {
            record.Add(persistentScenes.name, new SceneRecord(persistentScenes.name, persistentScenes.scenePath));
        }
        if (!record.ContainsKey(locationScene.name)) {
            record.Add(locationScene.name, new SceneRecord(locationScene.name, locationScene.scenePath));
        }

        record[SceneManager.GetActiveScene().name].MarkForUnload();
        record[persistentScenes.name].MarkForLoad();
        record[locationScene.name].isLocation = true;
        record[locationScene.name].MarkForLoad();
        sceneToActivate = locationScene.name;
        UnloadScenes();
        LoadScenes(showLoadingScreen);
    }

    private void LoadScenes(bool showLoadingScreen) {
        if (showLoadingScreen) eventsChannel.ToggleLoadingScreen(true);
        if (scenesToLoadAsyncOperations.Count == 0) {
            foreach (KeyValuePair<string, SceneRecord> scene in record) {
                if (scene.Value.willLoad) {
                    string currentScenePath = scene.Value.path;
                    scenesToLoadAsyncOperations.Add(SceneManager.LoadSceneAsync(currentScenePath, LoadSceneMode.Additive));
                    scene.Value.Loaded();
                    if (scene.Value.isLocation) {
                        sceneToActivate = scene.Key;
                    }
                }
            }
        }
        StartCoroutine(WaitForLoading(showLoadingScreen));
    }

    private IEnumerator WaitForLoading(bool showLoadingScreen) {
        bool _loadingDone = false;
        while (!_loadingDone) {
            for (int i = 0; i < scenesToLoadAsyncOperations.Count; ++i) {
                if (!scenesToLoadAsyncOperations[i].isDone) {
                    break;
                } else {
                    _loadingDone = true;
                    scenesToLoadAsyncOperations.Clear();
                }
            }
            yield return null;
        }

        SetActiveScene(record[sceneToActivate].path);
        if (showLoadingScreen) eventsChannel.ToggleLoadingScreen(false);
    }

    private void SetActiveScene(string activeScenePath) {
        Scene scene = SceneManager.GetSceneByPath(activeScenePath);
        SceneManager.SetActiveScene(scene);
        // Will reconstruct LightProbe tetrahedrons to include the probes from the newly-loaded scene
        LightProbes.TetrahedralizeAsync();
        //Raise the event to inform that the scene is loaded and set active
        eventsChannel.OnSceneReady(scene, lastPathTaken);
    }

    private void UnloadScenes() {
        for (int i = 0; i < SceneManager.sceneCount; ++i) {
            Scene scene = SceneManager.GetSceneAt(i);
            if (record[scene.name].willUnload) {
                SceneManager.UnloadSceneAsync(scene);
                record[scene.name].Unloaded();
            }
        }
    }

    private void ExitGame() {
        Application.Quit();
        Debug.Log("Exit!");
    }
}


class SceneRecord {
    public string name { get; private set; }
    public string path;
    public bool willLoad = false;
    public bool willUnload = false;
    public bool isLoaded = false;
    public bool isLocation = false;

    public SceneRecord(string name = "", string path = "") {
        this.name = name;
        this.path = path;
    }

    public void Unload() {
        for (int i = 0; i < SceneManager.sceneCount; ++i) {
            Scene sc = SceneManager.GetSceneAt(i);
            if (sc.path == path) {
                SceneManager.UnloadSceneAsync(sc);
                willUnload = false;
                isLoaded = false;
            }
        }
    }

    public void MarkForLoad() {
        if (!isLoaded) {
            willLoad = true;
            willUnload = false;
        }
    }

    public void Loaded() {
        willLoad = false;
        isLoaded = true;
    }

    public void MarkForUnload() {
        willLoad = false;
        willUnload = true;
    }

    public void Unloaded() {
        willUnload = false;
        isLoaded = false;
    }
}