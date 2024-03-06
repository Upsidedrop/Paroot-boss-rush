using UnityEngine;

public class Monkey_Hang_Intro : StateMachineBehaviour
{

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Rigidbody2D>().isKinematic = true;
        animator.transform.position = Vector2.up * 3;
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Rigidbody2D>().isKinematic = false;
    }

}
