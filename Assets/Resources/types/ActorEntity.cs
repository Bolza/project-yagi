using System;
using UnityEngine;

public enum FacingDirections {
    right = 1,
    left = -1,
};


public class ActorEntity : HittableEntity {
    public float skinWidth = 0.1f;
    public bool debugMode;
    public int FacingDirection { get; private set; }


    public bool isGrounded { get; protected set; }
    public bool isWalled { get; protected set; }
    public bool isLedged { get; protected set; }
    public bool isOnSlope { get; protected set; }
    public bool headIsFree { get; protected set; }
    [SerializeField] private FacingDirections animationIsFacing = new FacingDirections();
    [SerializeField] private FacingDirections startDirection = new FacingDirections();

    private Vector2 gizmoCenter;
    private float slopeDownAngle;
    private float slopeDownAngleOld;
    private Vector2 slopeNormalPerp;



    public override void Start() {
        base.Start();

        FacingDirection = (int)animationIsFacing;
        if (startDirection != animationIsFacing) Flip();
    }

    protected virtual void Update() {
        isGrounded = CheckIsGrounded();
        isWalled = CheckIsWalled();
        bool ledgeRay = CheckIsTouchingLedge();
        headIsFree = !isWalled;
        isLedged = headIsFree && ledgeRay;
    }

    public bool CheckIsWalled() {
        Vector2 side = new Vector2(
            Collider.bounds.center.x + (Collider.bounds.extents.x * FacingDirection) - (skinWidth * FacingDirection),
            Collider.bounds.center.y);
        // gizmoCenter = side;
        RaycastHit2D hittin = Physics2D.Raycast(side, Vector2.right * FacingDirection, skinWidth * 2, getGroundMask());
        // if (hittin) Debug.Log("normal: " + hittin.normal + " | angle: " + Vector2.Angle(hittin.normal, Vector2.up));
        if (debugMode) Debug.DrawRay(side, Vector2.right * FacingDirection * skinWidth * 2, hittin ? Color.red : Color.green);
        return hittin;
    }

    public Collider2D CheckHasLadder() {
        Vector2 side = new Vector2(
            Collider.bounds.center.x + (Collider.bounds.extents.x * FacingDirection) - (skinWidth * FacingDirection),
            Collider.bounds.center.y);
        gizmoCenter = side;
        // RaycastHit2D hittin = Physics2D.Raycast(side, Vector2.right * FacingDirection, skinWidth * 2, getClimbableMask());
        Collider2D hittin2 = Physics2D.OverlapCircle(side, skinWidth * 4, getClimbableMask());
        // if (hittin) Debug.Log("normal: " + hittin.normal + " | angle: " + Math.Round(Vector2.Angle(hittin.normal, Vector2.up)));
        return hittin2;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(gizmoCenter, skinWidth * 4);
    }


    public virtual bool CheckIsGrounded() {
        Vector2 side = new Vector2(Collider.bounds.center.x, Collider.bounds.center.y - Collider.bounds.extents.y);
        bool hittin = Physics2D.OverlapCircle(side, skinWidth, getGroundMask());
        return hittin;
    }

    public bool CheckSlope() {
        Vector2 bottomPoint = new Vector2(Collider.bounds.center.x, Collider.bounds.center.y - Collider.bounds.extents.y);
        RaycastHit2D down = Physics2D.Raycast(bottomPoint, Vector2.down, skinWidth, getGroundMask());
        if (down) {
            slopeNormalPerp = Vector2.Perpendicular(down.normal).normalized;
            slopeDownAngle = Vector2.Angle(down.normal, Vector2.up);
            if (slopeDownAngle != slopeDownAngleOld) isOnSlope = true;
            slopeDownAngleOld = slopeDownAngle;
        }
        return down;
    }

    public bool CheckIfShouldFlip(float xInput) {
        bool should = xInput != 0 && Math.Sign(xInput) != Math.Sign(FacingDirection);
        if (should) Flip();
        return should;
    }

    public bool CheckIsTouchingLedge() {
        Vector2 side = new Vector2(
            Collider.bounds.center.x + (Collider.bounds.extents.x * FacingDirection) - (skinWidth * FacingDirection),
            Collider.bounds.center.y + (Collider.bounds.extents.y / 3 * 2));
        bool hittin = Physics2D.Raycast(side, Vector2.right * FacingDirection, skinWidth * 2, getGroundMask(), getGroundMask());

        if (debugMode) Debug.DrawRay(side, Vector2.right * FacingDirection * skinWidth * 2, hittin ? Color.red : Color.green);
        return hittin;
    }

    public bool CheckHeadIsWalled() {
        Vector2 side = new Vector2(
            Collider.bounds.center.x + (Collider.bounds.extents.x * FacingDirection) - (skinWidth * FacingDirection),
            Collider.bounds.center.y + Collider.bounds.extents.y);
        RaycastHit2D hittin = Physics2D.Raycast(side, Vector2.right * FacingDirection, skinWidth * 2, getGroundMask());

        if (debugMode) Debug.DrawRay(side, Vector2.right * FacingDirection * skinWidth * 2, hittin ? Color.red : Color.green);
        return hittin;
    }

    public void Flip() {
        FacingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }

    public virtual LayerMask getGroundMask() {
        Debug.Log("Need to override in your class");
        return LayerMask.GetMask("Ground");
    }

    public virtual LayerMask getPlatformMask() {
        Debug.Log("Need to override in your class");
        return LayerMask.GetMask("Platform");
    }

    public virtual LayerMask getClimbableMask() {
        Debug.Log("Need to override in your class");
        return LayerMask.GetMask("Climbable");
    }
}