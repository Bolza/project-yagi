using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData: LivingEntityData {

    [Header("World State")]
    public float gravity = -25f;

    [Header("Move State")]
    public float rollSpeed = 4f;
    public float rollDistance = 3f;

    [Header("Jump State")]
    public float jumpForce = 10f;
    public float jumpMaxY = 3f;
    public float airMovementSpeed = 4f;
    public float jumpMaxX = 3f;
    public int jumpsAmount = 1;
    public float coyoteTime = 0.05f;
    public float wallJumpForce = 10f;
    public float wallJumpTime = 0.4f;
    public Vector2 wallJumpAngle = new Vector2(1, 2);
    public float landAnimationSpeedLimit = 4f;

    [Header("Wall State")]
    public bool enableWallGrab = false;
    public float wallSlideVelocity = 3f;
    public float wallGrabDuration = 0.3f;
    public bool canJumpFromWall = true;

    [Header("Attack State")]
    public float attackMotionX = 2f;
    public float blockKnockbackAttenuation = 2f;

}
