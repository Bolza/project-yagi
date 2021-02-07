using UnityEngine;

[CreateAssetMenu(fileName = "New Location", menuName = "Scene Data/Location Scene")]
public class LocationSceneSO : GameSceneSO {
    public LocationSceneSO() {
        this.type = SceneType.Location;
    }
}