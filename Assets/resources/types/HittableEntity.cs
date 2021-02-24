using System;
using UnityEngine;

public enum FacingDirections {
    right = 1,
    left = -1,
};


[RequireComponent(typeof(Collider2D))]
public class HittableEntity : MonoBehaviour {
    [SerializeField] public CombatEventsChannel combatEvents;
    public event Action onGotHit;
    public event Action onGotBlocked;
    private Hitpoint hitpoint;
    public Collider2D Collider { get; private set; }
    public AttackType lastHit { get; private set; }
    [SerializeField] private Vector2 hitpointPosition;
    [SerializeField] private float hitpointRadius;
    public int FacingDirection { get; protected set; }

    public LayerMask hittables;

    // private Vector2 gizmoPosition;
    // private float gizmoSize;

    public virtual void Start() {
        hitpoint = GetComponentInChildren<Hitpoint>();
        Collider = GetComponent<CapsuleCollider2D>();

        if (!hitpoint) Debug.LogError("Hitpoint required in children");
        hitpointPosition = new Vector2(Math.Abs(hitpoint.transform.localPosition.x), hitpoint.transform.localPosition.y);
        hitpointRadius = hitpoint.GetComponent<CircleCollider2D>().radius;
        GameObject.Destroy(hitpoint);

        hittables = getHittableMask();
    }

    //this should handle multiple objects hit with the same attack
    public virtual bool TestTargetHit() {
        Vector2 pos = (Vector2)transform.position + hitpointPosition * FacingDirection;
        Collider2D x = Physics2D.OverlapCircle(pos, hitpointRadius, getHittableMask());
        return x != null;
    }


    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere((Vector2)transform.position + hitpointPosition * FacingDirection, hitpointRadius);
    }


    public virtual void ConfirmTargetHit(AttackType atk) {
        Vector2 pos = (Vector2)transform.position + hitpointPosition * FacingDirection;
        Collider2D hittin = Physics2D.OverlapCircle(pos, hitpointRadius, getHittableMask());
        HittableEntity tgt = hittin.gameObject.GetComponent<HittableEntity>();
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

    protected virtual LayerMask getHittableMask() {
        Debug.Log("Need to override in your class");
        return LayerMask.GetMask("Enemies");
    }

    public virtual AttackType GenerateAttack() {
        return new AttackType(this, 0, 0);
    }

}
