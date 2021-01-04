using System;
using UnityEngine;

public class MeshColorer3D: MonoBehaviour {
    [SerializeField] private SkinnedMeshRenderer mesh;
    [SerializeField] private float fadeSpeed = 0.1f;
    [SerializeField] private Color currentColor;

    void Update() {
        if (currentColor.a > 0) {
            currentColor.a = Mathf.Clamp01(currentColor.a - fadeSpeed * Time.deltaTime);
            mesh.material.SetColor("_Tint", currentColor);
        }
    }

    public void SetTintColor(Color c) {
        currentColor = c;
        mesh.material.SetColor("_Tint", currentColor);
    }

    public void SetFadeSpeed(float speed) {
        fadeSpeed = speed;
    }

    public void SetMesh(SkinnedMeshRenderer m) {
        mesh = m;
    }
}
