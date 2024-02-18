using UnityEngine;

public class Shockwave_Spawner : StateMachineBehaviour
{
    public GameObject shockwave;
    public float spawnDistance;
    public bool bothSides;
    Transform player;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        GameObject temp;
        temp = Instantiate(shockwave);
        temp.transform.position = new(animator.transform.position.x, -4.3435f);
        if (bothSides)
        {
            temp.transform.position += Vector3.left * spawnDistance;
            temp.GetComponent<Shockwave_Behavior>().isMovingLeft = true;
            temp = Instantiate(shockwave);
            temp.transform.position = new(animator.transform.position.x, -4.3435f);
            temp.transform.position += Vector3.right * spawnDistance;
            temp.GetComponent<Shockwave_Behavior>().isMovingLeft = false;
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            if (animator.transform.position.x < player.position.x)
            {
                temp.transform.position += Vector3.right * spawnDistance;
                temp.GetComponent<Shockwave_Behavior>().isMovingLeft = false;
            }
            else
            {
                temp.transform.position += Vector3.left * spawnDistance;
                temp.GetComponent<Shockwave_Behavior>().isMovingLeft = true;
            }

        }
    }
}
