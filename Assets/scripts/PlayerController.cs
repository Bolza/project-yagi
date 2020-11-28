using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController: MonoBehaviour {
    private Animator animator;
    private Rigidbody2D body;
    private BoxCollider2D collider;
    private Vector2 moveVector;
    public LayerMask groundLayer;
    public float jumpForce;
    public float walkSpeed;
    public float runSpeed;
    [HideInInspector] public bool isGrounded;

    float nextJump = 0;

    public event EventHandler JumpEvent;
    public event EventHandler SwingEvent;
    // Start is called before the first frame updat
    void Start() {
        animator = GetComponentsInChildren<Animator>()[0];
        body = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update() {
        isGrounded = getIsGrounded();
        moveVector = getMovement();
    }

    private void FixedUpdate() {
        body.velocity = moveVector;
    }

    Vector2 getMovement() {
        float moveY = body.velocity.y;
        Debug.Log(Input.GetAxisRaw("Horizontal"));
        // bad practice to read state from animator also controller should be animator-independent
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
        float actionMultiplierX = 1f; // c'mon
        if (state.IsName("swing")) actionMultiplierX = 0f;
        float moveX = Input.GetAxisRaw("Horizontal") * walkSpeed * actionMultiplierX;

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetAxisRaw("Horizontal") != 0 && isGrounded) {
            moveX = Input.GetAxisRaw("Horizontal") * runSpeed * actionMultiplierX;
        }

        if (Input.GetKey(KeyCode.Space) && isGrounded) {
            StartCoroutine(performJump());
        }

        if ((Input.GetMouseButton(0) || Input.GetKey(KeyCode.C)) && isGrounded) {
            SwingEvent?.Invoke(this, EventArgs.Empty);
        }

        return new Vector2(moveX, moveY);
    }

    IEnumerator performJump() {
        JumpEvent?.Invoke(this, EventArgs.Empty);
        yield return new WaitForSeconds(.1f);
        moveVector.y = 1f * jumpForce;
    }

    private bool getIsGrounded() {
        float downBy = .05f;
        RaycastHit2D cast = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, downBy, groundLayer);
        ExtDebug.DebugHitBox(cast, collider, downBy);
        return cast.collider != null;
    }
}

