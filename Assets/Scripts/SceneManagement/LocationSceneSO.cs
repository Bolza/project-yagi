using UnityEngine;

[CreateAssetMenu(fileName = "New Location", menuName = "Scene Data/Location Scene")]
public class LocationSceneSO: GameSceneSO {
    public new SceneType type {
        get { return SceneType.Location; }
    }

}