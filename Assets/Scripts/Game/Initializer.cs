using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class is responsible for starting the game by loading the persistent managers scene 
/// and raising the event to load the Main Menu
/// </summary>

public class Initializer: MonoBehaviour {
    [Header("Persistent managers Scene")]
    [SerializeField] private GameSceneSO ManagersScene = default;

    [Header("Loading settings")]
    [SerializeField] private GameSceneSO[] MenuToLoad = default;
    [SerializeField] private bool showLoadScreen = default;

    [Header("Broadcasting on")]
    [SerializeField] private SceneManagementEventsChannel sceneChannel = default;

    void Start() {
        //Load the persistent managers scene
        StartCoroutine(loadScene(ManagersScene.scenePath));
    }

    IEnumerator loadScene(string scenePath) {
        AsyncOperation loadingSceneAsyncOp = SceneManager.LoadSceneAsync(scenePath, LoadSceneMode.Additive);

        //Wait until we are done loading the scene
        while (!loadingSceneAsyncOp.isDone) {
            yield return null;
        }
        //Raise the event to load the main menu
        sceneChannel.RequestLoading(MenuToLoad, showLoadScreen);
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }
}
