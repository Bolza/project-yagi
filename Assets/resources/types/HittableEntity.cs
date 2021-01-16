using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class HittableEntity: MonoBehaviour {
    [SerializeField] protected SO_GameController gameController;
    public event Action onGotHit;
    public event Action onGotBlocked;
    public Hitpoint hitpoint { get; private set; }
    public Collider2D Collider { get; private set; }
    public AttackType lastHit { get; private set; }

    public virtual void Start() {
        hitpoint = GetComponentInChildren<Hitpoint>();
        Collider = GetComponent<CapsuleCollider2D>();

        if (!hitpoint) Debug.LogError("Hitpoint required in children");
    }

    public virtual void HitCurrentTarget(AttackType atk) {
        HittableEntity tgt = hitpoint.currentHit.gameObject.GetComponent<HittableEntity>();
        tgt.GotHit(atk);
    }

    public virtual void GotHit(AttackType atk) {
        lastHit = atk;
        onGotHit?.Invoke();
    }

    public virtual void GotBlocked(AttackType atk) {
        onGotBlocked?.Invoke();
    }

    public virtual float CalculateKnockback(AttackType atk) {
        float dir = atk.getDirectionTo(this.transform.position).x;
        return dir * atk.knockback;
    }

    public virtual AttackType GenerateAttack() {
        return new AttackType(this, 0, 0);
    }

}
