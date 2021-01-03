using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructParticle: MonoBehaviour {
    private ParticleSystem ps;

    void Start() {
        ps = GetComponentInChildren<ParticleSystem>();
    }


    // Update is called once per frame
    void Update() {
        if (!ps.IsAlive()) {
            Destroy(gameObject);
        }
    }
}
