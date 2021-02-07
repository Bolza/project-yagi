using System;
using UnityEngine;
using System.Collections.Generic;


public static class GlobalUtils {
    public static GameSceneSO GetLocationScene(GameSceneSO[] sceneToLoad) {
        return System.Array.Find(sceneToLoad, (GameSceneSO scene) => {
            return scene.type == SceneType.Location;
        });
    }
    public static GameSceneSO GetLocationScene(List<GameSceneSO> sceneToLoad) {
        return sceneToLoad.Find((GameSceneSO scene) => {
            return scene.type == SceneType.Location;
        });
    }

    public static GameObject FindGameObjectChildWithTag(this GameObject parent, string tag) {
        Transform t = parent.transform;
        foreach (Transform tr in t) {
            if (tr.tag == tag) {
                return tr.gameObject;
            }
        }
        return null;
    }
}
