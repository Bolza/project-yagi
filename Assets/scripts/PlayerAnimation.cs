using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation: MonoBehaviour {
    private Animator animator;
    private Animator animatorArc;
    private PlayerController pc;
    private Rigidbody2D body;
    private SpriteRenderer renderer;
    private SpriteRenderer arcrenderer;
    private bool isGrounded;
    // Start is called before the first frame update
    void Start() {
        animator = GetComponentsInChildren<Animator>()[0];
        animatorArc = GetComponentsInChildren<Animator>()[1];
        body = GetComponent<Rigidbody2D>();
        pc = GetComponent<PlayerController>();
        renderer = GetComponentsInChildren<SpriteRenderer>()[0];
        arcrenderer = GetComponentsInChildren<SpriteRenderer>()[1];
        pc.JumpEvent += onJump;
        pc.SwingEvent += onSwing;
    }


    void onJump(object sender, EventArgs e) {
        StartCoroutine(trigger(animator, "triggerJump"));
    }

    void onSwing(object sender, EventArgs e) {
        StartCoroutine(trigger(animator, "triggerSwing"));
        StartCoroutine(trigger(animatorArc, "triggerArcEffect"));
    }


    // Update is called once per frame
    void Update() {
        isGrounded = pc.isGrounded;
        float x = body.velocity.x;
        float y = body.velocity.y;

        if (x < -0.1f) {
            renderer.flipX = true;
            arcrenderer.flipX = false;
        }
        if (x > 0.1f) {
            renderer.flipX = false;
            arcrenderer.flipX = true;
        }
        animator.SetFloat("XSpeed", Mathf.Abs(x));

        if (!isGrounded) {
            animator.SetFloat("YSpeed", y);
            //if (body.velocity.y < 0) animator.SetBool("jumpingDown", true);
        }
        else {
            animator.SetFloat("YSpeed", 0);
        }
    }

    IEnumerator trigger(Animator anim, string id, float delay = .01f) {
        anim.SetTrigger(id);
        yield return new WaitForSeconds(delay);
        anim.ResetTrigger(id);
    }

}
