using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rollAnimationScript: StateMachineBehaviour {
    private float time;
    private bool exited;
    private float fast = 1.5f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        time = 0;
        exited = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        time += Time.deltaTime;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //Debug.Log("roll OnStateExit " + time);
        exited = true;
        animator.speed = 1f;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //Debug.Log(time);
        if (exited) return;
        if (time <= 0.35f || time >= 0.7f) {
            animator.speed = fast;
        }
        else {
            animator.speed = 1f;
        }
    }

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //    // Implement code that sets up animation IK (inverse kinematics)

    //}
}
