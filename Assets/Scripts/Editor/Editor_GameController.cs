using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(GameControllerComponent))]
public class Editor_GameController: Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        GameControllerComponent theScript = (GameControllerComponent)target;
        if (GUILayout.Button("Spawn Enemy")) {
            theScript.SpawnEnemy();
        }
        EditorGUILayout.HelpBox("We need to consider carefully this component", MessageType.Info);
    }
}
