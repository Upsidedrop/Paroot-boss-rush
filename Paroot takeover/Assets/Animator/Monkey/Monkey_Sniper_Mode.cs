using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey_Sniper_Mode : StateMachineBehaviour
{
    GameObject laserPointer;
    Transform playerTransform;
    float timer;
    public GameObject chain;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        laserPointer = GameObject.Find("MonkeyGun");
        GameObject.Find("AimLine").GetComponent<SpriteRenderer>().enabled = true;
        laserPointer.transform.position = new(animator.transform.position.x, -4.15f);
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log(timer);
        if (timer < 3)
        {
            laserPointer.transform.localRotation = Quaternion.Euler(180, 180, Mathf.Atan2(playerTransform.position.y - laserPointer.transform.position.y,
                                                                                      playerTransform.position.x - laserPointer.transform.position.x)
                                                                                      * 180 / Mathf.PI);
        }
        timer += Time.deltaTime;
        if (timer > 4)
        {
            Instantiate(chain, laserPointer.transform.position, laserPointer.transform.rotation);
            timer = 0;
            return;
        }

    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject.Find("AimLine").GetComponent<SpriteRenderer>().enabled = false;

    }

}
