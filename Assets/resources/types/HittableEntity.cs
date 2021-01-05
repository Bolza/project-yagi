using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HittableEntity: MonoBehaviour {
    [SerializeField] protected SO_GameController gameController;

    public event Action onGotHit;
    public event Action onGotBlocked;
    public Hitpoint hitpoint { get; private set; }

    public virtual void Start() {
        hitpoint = GetComponentInChildren<Hitpoint>();
        if (!hitpoint) Debug.LogError("Hitpoint required in children");

    }

    public virtual void HitCurrentTarget(int dmg) {
        HittableEntity tgt = hitpoint.currentHit.gameObject.GetComponent<HittableEntity>();
        tgt.GotHit(this, dmg);
    }

    public virtual void GotHit(HittableEntity entity, int dmg) {
        onGotHit?.Invoke();
    }

    public virtual void GotBlocked(HittableEntity entity, int dmg) {
        onGotBlocked?.Invoke();
    }

}
