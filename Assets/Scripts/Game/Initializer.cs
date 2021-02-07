using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class is responsible for starting the game by loading the persistent managers scene 
/// and raising the event to load the Main Menu
/// </summary>

public class Initializer : MonoBehaviour {
    [Header("Persistent managers Scene")]
    [SerializeField] private GameSceneSO ManagersScene = default;
    [SerializeField] private GameSceneSO InitializerScene = default;

    [Header("Loading settings")]
    [SerializeField] private LocationSceneSO[] defaultLocation = default;
    // [SerializeField] private GameSceneSO[] MenuToLoad = default;
    [SerializeField] private bool showLoadScreen = default;

    [Header("Broadcasting on")]
    [SerializeField] private SceneManagementEventsChannel sceneChannel = default;

    void Start() {
        //Load the persistent managers scene
        StartCoroutine(loadScene(ManagersScene.scenePath));
    }

    IEnumerator loadScene(string scenePath) {
        bool hasManagerAlreadyLoaded = false;
        for (int i = 0; i < SceneManager.sceneCount; ++i) {
            Scene sc = SceneManager.GetSceneAt(i);
            if (sc.name == ManagersScene.name) hasManagerAlreadyLoaded = true;
        }

        if (!hasManagerAlreadyLoaded) {
            AsyncOperation loadingSceneAsyncOp = SceneManager.LoadSceneAsync(scenePath, LoadSceneMode.Additive);
            //Wait until we are done loading the scene
            while (!loadingSceneAsyncOp.isDone) yield return null;
        }

        //Raise the event to load the main menu
        sceneChannel.RequestLoading(defaultLocation, default, showLoadScreen);
        // Remove Initializer scene
        for (int i = 0; i < SceneManager.sceneCount; ++i) {
            Scene sc = SceneManager.GetSceneAt(i);
            if (sc.name == InitializerScene.name)
                SceneManager.UnloadSceneAsync(sc);
        }
    }

}
