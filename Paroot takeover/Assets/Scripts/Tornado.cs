using System;
using System.Collections;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    private Rigidbody2D rb;
    private float w;
    private float timer = 0f;
    private readonly float speed = 2;

    private void Awake()
    {
        w = UnityEngine.Random.value;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(UpdateMovement());
    }

    private IEnumerator UpdateMovement()
    {
        w += 0.1f;
        rb.linearVelocity = new Vector2(Mathf.PerlinNoise1D(w) * speed - speed / 2, rb.linearVelocity.y);
        yield return new WaitForSeconds(0.1f);
        if (w > 3)
        {
            Destroy(gameObject);
        }
        StartCoroutine(UpdateMovement());
    }

    private void OnTriggerStay2D(Collider2D collisionInfo)
    {
        if (collisionInfo.gameObject.layer == 3/*Enemy*/)
        {
            timer += Time.deltaTime;
            if (timer >= 0.5f)
            {
                collisionInfo.gameObject.GetComponent<Health>().health-=2;
                timer = 0f;
            }
        }


    }

    private void OnCollisionExit(Collision collisionInfo)
    {
        timer = 0f;
    }
}
