using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public class MultiSpriteRenderer: MonoBehaviour {
    public Material[] materials;
    private SpriteRenderer rendy;

    private void Awake() {
        rendy = GetComponent<SpriteRenderer>();
        rendy.materials = materials;
    }
}
