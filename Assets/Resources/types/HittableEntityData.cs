using UnityEngine;

[CreateAssetMenu(fileName = "newHittableEntityData", menuName = "Base Types")]
public class HittableEntityData : ScriptableObject {
    [Header("Stats")]
    public int totalHealth = 100;
    public float attackDamage = 10;
    public float attackKnockback = 10;
    public float knockbackAttenuation = 0;

    public LayerMask ownMask;
    public LayerMask enemyMask;
    public LayerMask hittablesMask;
    public LayerMask groundMask;
    public LayerMask platformMask;
    public LayerMask climbableMask;

}