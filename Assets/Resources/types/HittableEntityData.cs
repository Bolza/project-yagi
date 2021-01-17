using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newHittableEntityData", menuName = "Base Types")]
public class HittableEntityData: ScriptableObject {
    [Header("Stats")]
    public int totalHealth = 100;
    public float attackDamage = 10;
    public float attackKnockback = 10;
    public float knockbackAttenuation = 0;

    public LayerMask ownMask;
    public LayerMask friendlyMask;
    public LayerMask enemyMask;
    public LayerMask groundMask;
}