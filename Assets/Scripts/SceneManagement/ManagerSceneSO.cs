using UnityEngine;

[CreateAssetMenu(fileName = "New Manager", menuName = "Scene Data/Manager Scene")]
public class ManagerSceneSO: GameSceneSO {
    public new SceneType type {
        get { return SceneType.Manager; }
    }
}