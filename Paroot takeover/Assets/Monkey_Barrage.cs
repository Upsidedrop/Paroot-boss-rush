using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey_Barrage : StateMachineBehaviour
{
    readonly float bananaFrequency = 0.5f;
    float timer;
    public GameObject bananaBullet;
    Rigidbody2D rb;
    readonly float dashForce = 15;
    readonly float spread = 20.0f;
    readonly float bulletSpeed = 1.5f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        rb.velocity = new(-1f * dashForce, 0);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        if (timer > bananaFrequency)
        {
            timer = 0;
            GameObject temp = Instantiate(bananaBullet, animator.transform.position, Quaternion.Euler(0,0,Random.Range(-spread, spread)));
            temp.GetComponent<Rigidbody2D>().velocity = temp.transform.right * bulletSpeed;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
