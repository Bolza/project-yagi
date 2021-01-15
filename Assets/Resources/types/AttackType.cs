using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackType {
    public float damage { get; private set; }
    public float knockback { get; private set; }
    public string damageType { get; private set; }
    public string uid { get; private set; }
    public Vector3 origin { get; private set; }
    public HittableEntity owner { get; private set; }

    public AttackType(HittableEntity owner, float damage, float knockback) {
        this.damage = damage;
        this.knockback = knockback;
        this.owner = owner;
        this.origin = owner.transform.position;
    }

    public Vector3 getDirectionTo(Vector3 to) {
        return (to - origin).normalized;
    }
}
