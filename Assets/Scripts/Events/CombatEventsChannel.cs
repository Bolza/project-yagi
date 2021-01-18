using UnityEngine.Events;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Combat Events Channel")]
public class CombatEventsChannel: ScriptableObject {
    public UnityAction<HittableEntity, AttackType> OnEntityHit;
    public void EntityHit(HittableEntity target, AttackType atk) {
        OnEntityHit?.Invoke(target, atk);
    }

    public UnityAction<HittableEntity, AttackType> OnEntityDodge;
    public void EntityDodge(HittableEntity target, AttackType atk) {
        OnEntityDodge?.Invoke(target, atk);
    }

    public UnityAction<HittableEntity, AttackType> OnEntityBlock;
    public void EntityBlock(HittableEntity target, AttackType atk) {
        OnEntityBlock?.Invoke(target, atk);
    }

    public UnityAction<HittableEntity, AttackType> OnEntityTookDamage;
    public void EntityTookDamage(HittableEntity target, AttackType atk) {
        OnEntityTookDamage?.Invoke(target, atk);
    }
}