using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class is a base class which contains what is common to all game scenes (Locations or Menus)
/// </summary>
[ExecuteInEditMode]
public abstract partial class GameSceneSO: ScriptableObject {
    [Header("Information")]
    //#if UNITY_EDITOR // See GameSceneSOEditor.cs
    public UnityEditor.SceneAsset sceneAsset;
    //#endif
    public string scenePath {
        get {
            return "Assets/Scenes/" + sceneAsset.name + ".unity";
        }
    }


    [TextArea] public string shortDescription;

    [Header("Sounds")]
    public AudioClip music;
    public SceneType type;
}
