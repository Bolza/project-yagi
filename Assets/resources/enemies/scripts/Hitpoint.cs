using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitpoint: MonoBehaviour {
    public Collider2D currentHit { get; private set; }
    [SerializeField] private ContactFilter2D layerMask;
    public Collider2D[] hit;
    private Collider2D Collider;


    private void Start() {
        Collider = GetComponent<Collider2D>();
        hit = new Collider2D[1];
    }

    private void Update() {
        hit[0] = null;
        Collider.OverlapCollider(layerMask, hit);
        currentHit = hit[0];
    }
}

