using UnityEngine;

public class Hitpoint : MonoBehaviour {
    [SerializeField] private ContactFilter2D layerMask;
    private Collider2D Collider;
    public Collider2D currentHit { get; private set; }
    public Collider2D[] hit { get; private set; }


    private void Start() {
        Collider = GetComponent<Collider2D>();
        hit = new Collider2D[5];
    }

    private void Update() {
        hit[0] = null;
        Collider.OverlapCollider(layerMask, hit);
        currentHit = hit[0];
    }
}

