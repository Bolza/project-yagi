using UnityEngine;

[CreateAssetMenu(fileName = "New Menu", menuName = "Scene Data/Menu Scene")]
public class MenuSceneSO: GameSceneSO {
    public new SceneType type {
        get { return SceneType.Menu; }
    }
}