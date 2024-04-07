using UnityEngine;

public class Monkey_Melee : StateMachineBehaviour
{
    public GameObject hitbox;
    Transform playerTransform;
    GameObject hitboxInstance;
    Animator allAnimator;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        allAnimator = animator;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        float w;
        if (animator.transform.position.x > playerTransform.position.x)
        {
            w = -2;
        }
        else
        {
            w = 2;
        }
        hitboxInstance = Instantiate(hitbox, animator.transform.position + Vector3.right * w, Quaternion.identity);
        hitboxInstance.GetComponent<Hitbox>().StrikeDelegate += HitPlayer;
    }
    void HitPlayer()
    {
        
        allAnimator.SetTrigger("Hit Paroot");
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(hitboxInstance);
    }
}
