﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMossGiant: Enemy {


    protected Animator animator;
    private SpriteRenderer renderer;


    public override void Attack() {
        base.Attack();
    }

    void Awake() {
        animator = GetComponentInChildren<Animator>();
        renderer = GetComponentInChildren<SpriteRenderer>();

        targetWaypoint = Vector3.Distance(transform.position, pointA.position) < Vector3.Distance(transform.position, pointB.position) ? pointA : pointB;
    }

    public override void Update() {
        float step = Time.deltaTime * speed;
        float xNow = transform.position.x;
        if (xNow == pointA.position.x) {
            targetWaypoint = pointB;
        }
        else if (xNow == pointB.position.x) {
            targetWaypoint = pointA;
        }
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, step);
        animator.SetFloat(varXSpeed, Mathf.Abs(xNow - transform.position.x));
        if (targetWaypoint == pointA) renderer.flipX = true;
        if (targetWaypoint == pointB) renderer.flipX = false;
    }


}
