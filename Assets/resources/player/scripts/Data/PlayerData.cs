using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData: LivingEntityData {

    [Header("Move State")]
    public float rollkMotionSpaceX = 3f;
    public float rollMotionSpeedX = 6f;

    [Header("Jump State")]
    public float jumpForce = 10f;
    public float airMovementSpeed = 4f;
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
    public float attackMotionSpaceX = 1f;
    public float attackMotionSpeedX = 2f;
    public float blockKnockbackAttenuation = 2f;

}
