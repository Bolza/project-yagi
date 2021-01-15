using System;
using UnityEngine;

public class MeshColorer3D: MonoBehaviour {
    [SerializeField] private Renderer mesh;
    [SerializeField] private float fadeSpeed = 0.1f;
    [SerializeField] private float duration = 1f;
    [SerializeField] private Color currentColor;
    private Material material;
    private float startingAlpha;

    private void Start() {
        foreach (Material m in mesh.materials) {
            if (m.shader.name == "Shader Graphs/Tint") material = m;
        }
        if (!material) return;
        Color c = material.color;
        c.a = 0;
        material.SetColor("_Tint", c);
    }

    void Update() {
        if (currentColor != null && currentColor.a > 0) {
            currentColor.a = Mathf.Clamp01(currentColor.a - Time.deltaTime * startingAlpha / duration);
            material.SetColor("_Tint", currentColor);
        }
    }

    public void SetTintColor(Color c) {
        currentColor = c;
        startingAlpha = c.a;
        material.SetColor("_Tint", currentColor);
    }
}
