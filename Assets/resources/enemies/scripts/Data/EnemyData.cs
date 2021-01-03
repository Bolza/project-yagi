using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyData", menuName = "Data/Enemy Data/Base Data")]
public class EnemyData: ScriptableObject {

    [Header("Generics")]
    public LayerMask groundMask;
    public LayerMask playerMask;
    public float wallDetectionRange = 1f;

    [Header("Movement")]
    public float walkSpeed = 3f;
    public float runSpeed = 5f;
    public float minIdleTime = 1 * 1000;
    public float maxIdleTime = 5 * 1000;

    [Header("Combat")]
    public int health = 100;
    public float targetDetectionRange = 3f;
    public float attackSpeed = 1f;
    public float attackRange = 1f;
    public int attackDamage = 10;
    public float knockbackDampen = 0;
}
