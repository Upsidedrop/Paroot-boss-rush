using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey_Dash_Attack : StateMachineBehaviour
{
    Transform playerTransform;
    Transform monkeyTransform;
    Rigidbody2D rb;
    readonly float dashForce = 15;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        monkeyTransform = animator.transform;
        if (playerTransform.position.x > monkeyTransform.position.x)
        {
            rb.velocity = new(1f * dashForce, 0);

        }
        else
        {
            rb.velocity = new(-1f * dashForce, 0);
        }
    }
}


