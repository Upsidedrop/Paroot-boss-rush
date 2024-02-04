using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableAttack : MonoBehaviour
{
    public int enemyLayer;
    public float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == enemyLayer)
        {
            collision.GetComponent<Health>().health-=damage;
        }
        if (collision.gameObject.layer == enemyLayer || collision.gameObject.layer == 6)
        {
            Destroy(gameObject);
        }
    }
}
