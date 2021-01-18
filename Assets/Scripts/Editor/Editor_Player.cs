using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Player))]
public class Editor_Player: Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        Player player = (Player)target;


        if (GUILayout.Button("Spawn Enemy")) player.SetVelocityX(player.climbWhyPos);
    }
}
