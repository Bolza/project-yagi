using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType {
    Location,
    Menu,
    Manager,
    Null
}

[CreateAssetMenu(menuName = "Events/Scene Events Channel")]
public class SceneManagementEventsChannel : ScriptableObject {

    public UnityAction<GameSceneSO[], PathSO, bool> OnLoadingRequested;
    public void RequestLoading(GameSceneSO[] scenes, PathSO path, bool showLoading) => OnLoadingRequested?.Invoke(scenes, path, showLoading);

    public UnityAction<bool> OnToggleLoadingScreen;
    public void ToggleLoadingScreen(bool showLoading) => OnToggleLoadingScreen?.Invoke(showLoading);

    public UnityAction<Scene, PathSO> OnSceneReady;
    public void SceneReady(Scene scene, PathSO path) => OnSceneReady?.Invoke(scene, path);

    public List<string> getLoadedScenes() {
        List<string> res = new List<string>();
        for (int i = 0; i < SceneManager.sceneCount; ++i) {
            Scene sc = SceneManager.GetSceneAt(i);
            res.Add(sc.name);
        }
        return res;
    }


}