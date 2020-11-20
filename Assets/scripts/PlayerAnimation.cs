using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation: MonoBehaviour {
    private Animator animator;
    private PlayerController pc;
    private Rigidbody2D body;
    private SpriteRenderer renderer;
    private bool isGrounded;
    // Start is called before the first frame update
    void Start() {
        animator = GetComponentsInChildren<Animator>()[0];
        body = GetComponent<Rigidbody2D>();
        pc = GetComponent<PlayerController>();
        renderer = GetComponentsInChildren<SpriteRenderer>()[0];
        pc.OnJumpEvent += onJump;
    }


    void onJump(object sender, EventArgs e) {

        animator.SetTrigger("triggerJump");
    }

    // Update is called once per frame
    void Update() {
        isGrounded = pc.isGrounded;
        float x = body.velocity.x;
        float y = body.velocity.y;

        if (x < -0.1f) renderer.flipX = true;
        if (x > 0.1f) renderer.flipX = false;
        animator.SetFloat("XSpeed", Mathf.Abs(x));

        if (!isGrounded) {
            animator.SetFloat("YSpeed", y);
            //if (body.velocity.y < 0) animator.SetBool("jumpingDown", true);
        }
        else {
            animator.SetFloat("YSpeed", 0);
        }
    }


}
