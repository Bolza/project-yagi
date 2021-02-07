using UnityEngine;

[CreateAssetMenu(fileName = "New Menu", menuName = "Scene Data/Menu Scene")]
public class MenuSceneSO : GameSceneSO {
    public MenuSceneSO() {
        type = SceneType.Menu;
    }
}