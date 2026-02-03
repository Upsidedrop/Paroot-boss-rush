using UnityEngine;

public class Monkey_Start_Rain : StateMachineBehaviour
{
    public GameObject bananaPrefab;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject temp = Instantiate(bananaPrefab, new Vector2(Random.Range(-8, 8), 4), Quaternion.Euler(new(0,0,90)));
            temp.GetComponent<Rigidbody2D>().linearVelocity = Vector2.down * Random.Range(1.0f, 2.0f);
        }
    }

}
