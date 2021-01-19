using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class manages the scene loading and unloading.
/// </summary>
public class SceneLoader: MonoBehaviour {
    [Header("Persistent Manager Scene")]
    [SerializeField] private ManagerSceneSO persistentManagersScene = default;

    [Header("Gameplay Scene")]
    [SerializeField] private ManagerSceneSO gameplayScene = default;

    [Header("Load Events")]

    [SerializeField] private SceneManagementEventsChannel eventsChannel = default;

    private List<AsyncOperation> scenesToLoadAsyncOperations = new List<AsyncOperation>();
    private List<Scene> scenesToUnload = new List<Scene>();
    private GameSceneSO activeScene; // The scene we want to set as active (for lighting/skybox)
    private List<GameSceneSO> persistentScenes = new List<GameSceneSO>(); //Scenes to keep loaded when a load event is raised

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
        bool hasLocation = GlobalUtils.GetLocationScene(sceneToLoad) != null;
        if (hasLocation) {
            //When loading a location, we want to keep the persistent managers and gameplay scenes loaded
            persistentScenes.Add(gameplayScene);
        }
        persistentScenes.Add(persistentManagersScene);
        AddScenesToUnload(persistentScenes);
        LoadScenes(sceneToLoad, showLoadingScreen);
    }

    private void LoadScenes(GameSceneSO[] scenesToLoad, bool showLoadingScreen) {
        //Take the first scene in the array as the scene we want to set active

        activeScene = GlobalUtils.GetLocationScene(scenesToLoad);

        UnloadScenes();

        if (showLoadingScreen) {
            eventsChannel.ToggleLoadingScreen(true);
        }

        if (scenesToLoadAsyncOperations.Count == 0) {
            for (int i = 0; i < scenesToLoad.Length; i++) {
                string currentScenePath = scenesToLoad[i].scenePath;
                scenesToLoadAsyncOperations.Add(SceneManager.LoadSceneAsync(currentScenePath, LoadSceneMode.Additive));
            }
        }

        //Checks if any of the persistent scenes is not loaded yet and load it if unloaded
        //This is especially useful when we go from main menu to first location
        for (int i = 0; i < persistentScenes.Count; ++i) {
            if (IsSceneLoaded(persistentScenes[i].scenePath) == false) {
                scenesToLoadAsyncOperations.Add(SceneManager.LoadSceneAsync(persistentScenes[i].scenePath, LoadSceneMode.Additive));
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
                    persistentScenes.Clear();
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

    private void AddScenesToUnload(List<GameSceneSO> persistentScenes) {
        for (int i = 0; i < SceneManager.sceneCount; ++i) {
            Scene scene = SceneManager.GetSceneAt(i);
            string scenePath = scene.path;
            for (int j = 0; j < persistentScenes.Count; ++j) {
                if (scenePath != persistentScenes[j].scenePath) {
                    //Check if we reached the last persistent scenes check
                    if (j == persistentScenes.Count - 1) {
                        //If the scene is not one of the persistent scenes, we add it to the scenes to unload
                        scenesToUnload.Add(scene);
                    }
                }
                else {
                    //We move the next scene check as soon as we find that the scene is one of the persistent scenes
                    break;
                }
            }
        }
    }

    private void UnloadScenes() {
        if (scenesToUnload != null) {
            for (int i = 0; i < scenesToUnload.Count; ++i) {
                SceneManager.UnloadSceneAsync(scenesToUnload[i]);
            }
            scenesToUnload.Clear();
        }
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
