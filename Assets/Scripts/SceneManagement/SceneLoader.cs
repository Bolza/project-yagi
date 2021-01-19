using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class manages the scene loading and unloading.
/// </summary>
public class SceneLoader: MonoBehaviour {
    [Header("Persistent Scenes")]
    [SerializeField] private ManagerSceneSO managerScene = default;
    [SerializeField] private ManagerSceneSO gameplayScene = default;

    [Header("Load Events")]
    [SerializeField] private SceneManagementEventsChannel eventsChannel = default;

    private List<AsyncOperation> scenesToLoadAsyncOperations = new List<AsyncOperation>();
    private List<GameSceneSO> scenesToUnload = new List<GameSceneSO>();
    private List<GameSceneSO> loadedScenes = new List<GameSceneSO>();
    private GameSceneSO activeScene; // The scene we want to set as active (for lighting/skybox)
    //private List<GameSceneSO> persistentScenes = new List<GameSceneSO>(); //Scenes to keep loaded when a load event is raised
    //private List<Scene> scenesToUnload = new List<Scene>();

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

    /// <summary>
    /// This function loads the location scenes passed as array parameter 
    /// </summary>
    /// <param name="locationsToLoad"></param>
    /// <param name="showLoadingScreen"></param>
    private void LoadLocation(GameSceneSO[] sceneToLoad, bool showLoadingScreen) {
        Debug.Log("sceneToLoad " + sceneToLoad);

        List<GameSceneSO> tmpScenesToLoad = new List<GameSceneSO>();
        GameSceneSO locationScene = GlobalUtils.GetLocationScene(sceneToLoad);
        bool hasLocation = locationScene != null;

        UnloadScenes();

        if (!loadedScenes.Exists(scene => scene.scenePath == gameplayScene.scenePath)) {
            tmpScenesToLoad.Add(gameplayScene);
        }
        if (!loadedScenes.Exists(scene => scene.scenePath == locationScene.scenePath)) {
            tmpScenesToLoad.Add(locationScene);
            AddSceneToUnload(locationScene);
        }

        LoadScenes(tmpScenesToLoad, showLoadingScreen);
    }

    private void LoadScenes(List<GameSceneSO> scenesToLoad, bool showLoadingScreen) {
        //Take the first scene in the array as the scene we want to set active

        activeScene = GlobalUtils.GetLocationScene(scenesToLoad);


        if (showLoadingScreen) {
            eventsChannel.ToggleLoadingScreen(true);
        }

        if (scenesToLoadAsyncOperations.Count == 0) {
            for (int i = 0; i < scenesToLoad.Count; i++) {
                loadedScenes.Add(scenesToLoad[i]);
                string currentScenePath = scenesToLoad[i].scenePath;
                scenesToLoadAsyncOperations.Add(SceneManager.LoadSceneAsync(currentScenePath, LoadSceneMode.Additive));
            }
        }
        StartCoroutine(WaitForLoading(showLoadingScreen));
    }

    private IEnumerator WaitForLoading(bool showLoadingScreen) {
        bool _loadingDone = false;
        // Wait until all scenes are loaded
        while (!_loadingDone) {
            for (int i = 0; i < scenesToLoadAsyncOperations.Count; ++i) {
                if (!scenesToLoadAsyncOperations[i].isDone) {
                    break;
                }
                else {
                    _loadingDone = true;
                    scenesToLoadAsyncOperations.Clear();
                }
            }
            yield return null;
        }
        //Set the active scene
        SetActiveScene();
        if (showLoadingScreen) {
            //Raise event to disable loading screen 
            eventsChannel.ToggleLoadingScreen(false);
        }

    }

    /// <summary>
    /// This function is called when all the scenes have been loaded
    /// </summary>
    private void SetActiveScene() {
        Scene scene = SceneManager.GetSceneByPath(activeScene.scenePath);
        SceneManager.SetActiveScene(scene);
        // Will reconstruct LightProbe tetrahedrons to include the probes from the newly-loaded scene
        LightProbes.TetrahedralizeAsync();
        //Raise the event to inform that the scene is loaded and set active
        eventsChannel.OnSceneReady(activeScene);
    }


    private void AddSceneToUnload(GameSceneSO scene) {
        scenesToUnload.Add(scene);
    }

    private void UnloadScenes() {
        if (scenesToUnload.Count <= 0) return;
        Debug.Log(scenesToUnload);
        for (int i = 0; i < SceneManager.sceneCount; ++i) {
            Scene scene = SceneManager.GetSceneAt(i);
            string scenePath = scene.path;
            for (int j = 0; j < scenesToUnload.Count; ++j) {
                if (scenePath == scenesToUnload[j].scenePath) {
                    SceneManager.UnloadSceneAsync(scene);
                    loadedScenes.Remove(scenesToUnload[j]);
                }
            }
        }
        scenesToUnload.Clear();
    }


    /// <summary>
    /// This function checks if a scene is already loaded
    /// </summary>
    /// <param name="scenePath"></param>
    /// <returns>bool</returns>
    private bool IsSceneLoaded(string scenePath) {
        for (int i = 0; i < SceneManager.sceneCount; i++) {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.path == scenePath) {
                return true;
            }
        }
        return false;
    }

    private void ExitGame() {
        Application.Quit();
        Debug.Log("Exit!");
    }

}
