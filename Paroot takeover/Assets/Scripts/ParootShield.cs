using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParootShield : MonoBehaviour
{
    GameObject paroot;
    private void Awake()
    {
        paroot = GameObject.FindGameObjectWithTag("Player");
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            Destroy(collision.gameObject);
        }
        else if (!collision.gameObject.CompareTag("Enemy"))
        {
            return;
        }

        paroot.GetComponent<ParootMovement>().CallCoroutine("IFrames", 1.5f);
        paroot.GetComponent<ParootAttacks>().CallCooldown(0b10000, 20);
        Destroy(gameObject);
    }
}
