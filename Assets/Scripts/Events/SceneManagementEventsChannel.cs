using UnityEngine.Events;
using UnityEngine;

public enum SceneType {
    Location,
    Menu,
    Manager,
    Null
}

[CreateAssetMenu(menuName = "Events/Scene Events Channel")]
public class SceneManagementEventsChannel: ScriptableObject {

    public UnityAction<GameSceneSO[], bool> OnLoadingRequested;
    public void RequestLoading(GameSceneSO[] scenes, bool showLoading) => OnLoadingRequested?.Invoke(scenes, showLoading);

    public UnityAction<bool> OnToggleLoadingScreen;
    public void ToggleLoadingScreen(bool showLoading) => OnToggleLoadingScreen?.Invoke(showLoading);

    public UnityAction<GameSceneSO> OnSceneReady;
    public void SceneReady(GameSceneSO scene) => OnSceneReady?.Invoke(scene);
}