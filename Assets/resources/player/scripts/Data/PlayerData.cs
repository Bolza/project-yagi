using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData: ScriptableObject {
    [Header("Move State")]
    public float runSpeed = 5f;

    [Header("Jump State")]
    public float jumpForce = 10f;
    public float airMovementSpeed = 4f;
    public int jumpsAmount = 1;
    public float coyoteTime = 0.05f;
    public LayerMask groundLayer;

    [Header("Wall State")]
    public float wallSlideVelocity = 3f;
    public float wallGrabDuration = 0.3f;
    public bool canJumpFromWall = true;

}
