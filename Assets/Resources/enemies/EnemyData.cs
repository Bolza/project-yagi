using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyData", menuName = "Data/Enemy Data/Base Data")]
public class EnemyData: LivingEntityData {

    [Header("Move State")]
    public float wallDetectionRange = 1f;
    public float minIdleTime = 1;
    public float maxIdleTime = 5;

    [Header("Attack State")]
    public float targetDetectionRange = 3f;
    public float targetDetectionTime = 3f;
    public float attackSpeed = 1f;
    public float attackRange = 1f;
}
