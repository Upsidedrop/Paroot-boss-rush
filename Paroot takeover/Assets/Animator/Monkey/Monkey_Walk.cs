using UnityEngine;

public class Monkey_Walk : StateMachineBehaviour
{
    private Transform playerTransform;
    private Rigidbody2D rb;
    private readonly float speed = 1;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        animator.SetInteger("Random Attack", Random.Range(0, 3));
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playerTransform.position.x - animator.transform.position.x < 0)
        {
            rb.velocity = new(-1 * speed, rb.velocity.y);
            return;
        }
        rb.velocity = new(1 * speed, rb.velocity.y);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb.velocity = Vector2.zero;
        animator.SetInteger("Random Attack", -1);
    }
}
