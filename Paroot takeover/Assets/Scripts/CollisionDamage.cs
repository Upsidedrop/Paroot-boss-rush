using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>().health -= 5;
            collision.gameObject.GetComponent<ParootMovement>().CallCoroutine("IFrames", 1.5f);
        }
    }
}
