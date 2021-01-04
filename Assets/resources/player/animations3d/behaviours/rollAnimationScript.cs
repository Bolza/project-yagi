using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rollAnimationScript: StateMachineBehaviour {
    private float time;
    private bool exited;
    private float fast = 1.5f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        time = 0;
        exited = false;
        //animator.SetFloat("speedMultiplier", 1f);
        //animator.speed = 2;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        time += Time.deltaTime;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Debug.Log("OnStateExit " + time);
        exited = true;
        //animator.SetFloat("speedMultiplier", 1f);
        animator.speed = 1f;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Debug.Log(time);
        if (exited) return;
        if (time <= 0.3f || time >= 0.7f) {
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
