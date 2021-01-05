using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity: HittableEntity {
    public float skinWidth = 1f;
    public bool debugMode;
    public int FacingDirection { get; private set; }
    public Collider2D Collider { get; private set; }

    public bool isGrounded { get; protected set; }
    public bool isWalled { get; protected set; }
    public bool isLedged { get; protected set; }
    public bool headIsFree { get; protected set; }


    public override void Start() {
        base.Start();
        Collider = GetComponent<Collider2D>();
        FacingDirection = 1;
    }

    protected virtual void Update() {
        isGrounded = CheckIsGrounded();
        isWalled = CheckIsWalled();
        bool ledgeRay = CheckIsTouchingLedge();
        headIsFree = !isWalled;
        isLedged = headIsFree && ledgeRay;
    }

    private Vector2 getRayDistance() {
        return Vector2.right * FacingDirection * skinWidth * 2;
    }

    public bool CheckIsWalled() {
        Vector2 side = new Vector2(
            Collider.bounds.center.x + (Collider.bounds.extents.x * FacingDirection) - (skinWidth * FacingDirection),
            Collider.bounds.center.y);
        bool hittin = Physics2D.Raycast(side, getRayDistance(), gameController.groundLayer);

        if (debugMode) Debug.DrawRay(side, getRayDistance(), hittin ? Color.red : Color.green);
        return hittin;
    }

    public bool CheckIsGrounded() {
        Vector2 side = new Vector2(Collider.bounds.center.x, Collider.bounds.center.y - Collider.bounds.extents.y);
        bool hittin = Physics2D.Raycast(side, Vector2.down, skinWidth * 2, gameController.groundLayer);
        if (debugMode) Debug.DrawRay(side, Vector2.down * skinWidth * 2, hittin ? Color.cyan : Color.green);
        return hittin;
    }

    public void CheckIfShouldFlip(float xInput) {
        if (xInput != 0 && xInput != FacingDirection) Flip();
    }

    public bool CheckIsTouchingLedge() {
        Vector2 side = new Vector2(
            Collider.bounds.center.x + (Collider.bounds.extents.x * FacingDirection) - (skinWidth * FacingDirection),
            Collider.bounds.center.y + (Collider.bounds.extents.y / 3 * 2));
        bool hittin = Physics2D.Raycast(side, getRayDistance(), gameController.groundLayer);

        if (debugMode) Debug.DrawRay(side, getRayDistance(), hittin ? Color.red : Color.green);
        return hittin;
    }

    public bool CheckHeadIsWalled() {
        Vector2 side = new Vector2(
            Collider.bounds.center.x + (Collider.bounds.extents.x * FacingDirection) - (skinWidth * FacingDirection),
            Collider.bounds.center.y + Collider.bounds.extents.y);
        bool hittin = Physics2D.Raycast(side, getRayDistance(), gameController.groundLayer);

        if (debugMode) Debug.DrawRay(side, getRayDistance(), hittin ? Color.red : Color.green);
        return hittin;
    }

    protected void Flip() {
        FacingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }
}
