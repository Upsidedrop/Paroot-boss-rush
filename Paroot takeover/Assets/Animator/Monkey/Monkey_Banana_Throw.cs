using System.Collections;
using System.Threading;
using UnityEngine;

public class Monkey_Banana_Throw : StateMachineBehaviour
{
    float timer = 0;
    public GameObject Banana;
    [SerializeField]
    LayerMask layerMask;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Physics2D.SetLayerCollisionMask(8, layerMask);
    }

    void Throw(Animator animator)
    {
            GameObject bananaInstance;
            bananaInstance = Instantiate(Banana);
            bananaInstance.transform.position = animator.transform.position;
            bananaInstance.GetComponent<Rigidbody2D>().velocity = new(Random.Range(-15.0f, 15.0f), 0);

    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Physics2D.SetLayerCollisionMask(8, 0b111111111);
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        if (timer > 1)
        {
            timer = 0;
            Throw(animator);
        }
    }
}
